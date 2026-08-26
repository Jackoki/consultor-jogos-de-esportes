import { chromium } from "playwright";
import * as cheerio from "cheerio";

const CAGEMATCH_URL = "https://www.cagematch.net/";

export async function scrapeWweCalendar({ date_start, date_end }) {
    if (!date_start || !date_end) {
        throw new Error("Datas da filtragem são obrigatórias.");
    }

    const startDate = new Date(date_start);
    const endDate = new Date(date_end);

    if (isNaN(startDate.getTime()) || isNaN(endDate.getTime())) {
        throw new Error("Datas inválidas.");
    }

    if (startDate > endDate) {
        throw new Error("A data inicial não pode ser maior que a data final.");
    }

    const html = await fetchEvents(startDate, endDate);
    const events = parseEvents(html);

    const filteredEvents = events.filter(event => {
        if (!event.date_start) {
            return false;
        }

        if (!event.date_end) {
            return false;
        }

        return (event.date_start <= endDate &&event.date_end >= startDate);
    });

    return filteredEvents;
}

async function fetchEvents(dateStart, dateEnd) {
    const params = new URLSearchParams({
        id: "1",
        view: "search",
        sEventName: "",
        sPromotion: "1",
        sDateFromDay: String(dateStart.getDate()).padStart(2, "0"),
        sDateFromMonth: String(dateStart.getMonth() + 1).padStart(2, "0"),
        sDateFromYear: String(dateStart.getFullYear()),
        sDateTillDay: String(dateEnd.getDate()).padStart(2, "0"),
        sDateTillMonth: String(dateEnd.getMonth() + 1).padStart(2, "0"),
        sDateTillYear: String(dateEnd.getFullYear()),
        sRegion: "",
        sEventType: "",
        sLocation: "",
        sArena: "",
        sAny: ""
    });

    const url =`${CAGEMATCH_URL}?${params.toString()}`;

    const browser = await chromium.launch({
        headless: false
    });

    try {
        const page = await browser.newPage();

        await page.goto(url, {
            waitUntil: "domcontentloaded",
            timeout: 60000
        });

        await page.waitForTimeout(5000);
        const html = await page.content();
        const $ = cheerio.load(html);

        const eventLinks = $("a")
            .filter((_, element) => {
                const href =
                    $(element).attr("href");

                return (
                    href &&
                    href.includes("id=1&nr=")
                );
            });

        const bodyText = $("body").text();
        const dateMatches = bodyText.match(/\b\d{2}\.\d{2}\.\d{4}\b/g);
        return html;
    } 
    
    finally {
        await browser.close();
    }
}

function parseEvents(html) {
    const $ = cheerio.load(html);
    const events = [];
    const eventLinks = $("a")
        .filter((_, element) => {
            const href = $(element).attr("href");

            return (
                href &&
                /^(\?id=1&nr=\d+)$/.test(href)
            );
        });

    eventLinks.each((_, element) => {
        const eventAnchor = $(element);
        const row = eventAnchor.closest("tr");

        if (!row.length) {
            return;
        }

        const columns = row.find("td");
        if (columns.length < 4) {
            return;
        }

        const dateText = $(columns[1]).text().replace(/[\u200B-\u200D\uFEFF]/g, "").replace(/\s+/g, " ").trim();
        const name = eventAnchor.text().replace(/[\u200B-\u200D\uFEFF]/g, "").replace(/\s+/g, " ").trim();
        const locationText = $(columns[3]).text().replace(/[\u200B-\u200D\uFEFF]/g, "").replace(/\s+/g, " ").trim();
        const date = parseDate(dateText);
        const location = parseLocation(locationText);

        const imageElement = row.find("img.ImagePromotionLogoMini");
        let image = imageElement.attr("src");

        if (image && !image.startsWith("http")) {
            image = new URL(image, CAGEMATCH_URL).href;
        }

        let link = eventAnchor.attr("href");

        if (link && !link.startsWith("http")) {
            link = new URL(link, CAGEMATCH_URL).href;
        }

        events.push({
            name,
            link,
            image: image,
            date_start: date,
            date_end: date,
            city: location.city,
            state: location.state,
            country_name: location.country
        });
    });

    return events;
}

function parseEventInfo(dateText, locationText) {
    const date = parseDate(dateText);
    const location = parseLocation(locationText);

    return {
        date_start: date,
        date_end: date,
        city: location.city,
        country: location.country
    };
}

function parseDate(text) {
    if (!text) {
        return null;
    }

    const cleanText = text.replace(/[\u200B-\u200D\uFEFF]/g, "").replace(/\s+/g, " ").trim();
    const match = cleanText.match(/^(\d{1,2})\.(\d{1,2})\.(\d{4})$/);

    if (!match) {
        return null;
    }

    const day = Number(match[1]);
    const month = Number(match[2]) - 1;
    const year = Number(match[3]);

    const date = new Date(year, month, day);

    if (date.getFullYear() !== year || date.getMonth() !== month || date.getDate() !== day) {
        return null;
    }

    return date;
}

function parseLocation(text) {
    if (!text) {
        return {            
            city: null,
            state: null,
            country: null
        };
    }

    const parts = text.split(",").map(part => part.trim()).filter(Boolean);

    if (parts.length === 1) {
        return {
            city: parts[0],
            state: null,
            country: null
        };
    }

    if (parts.length === 2) {
        return {
            city: parts[0],
            state: null,
            country: parts[1]
        };
    }

    return {
        city: parts[0],
        state: parts.slice(1, -1).join(", "),
        country: parts[parts.length - 1]
    };
}