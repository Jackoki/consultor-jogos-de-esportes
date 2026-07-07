import axios from "axios";
import * as cheerio from "cheerio";

const FIDE_URL = "https://calendar.fide.com/calendar_server.php";
const BASE_URL = "https://calendar.fide.com";

export async function scrapeFideCalendar({ startDate, endDate }) {
    if (!startDate || !endDate) {
        throw new Error("Datas da filtragem são obrigatórias.");
    }

    const years = getYearsBetween(startDate, endDate);

    const events = [];

    for (const year of years) {
        const html = await fetchCalendarYear(year);
        events.push(...parseCalendar(html, year));
    }

    return events.filter(event =>
        event.startDate <= endDate &&
        event.endDate >= startDate
    );
}

function getYearsBetween(startDate, endDate) {
    const years = [];

    for (
        let year = startDate.getFullYear();
        year <= endDate.getFullYear();
        year++
    ) {
        years.push(year);
    }

    return years;
}

async function fetchCalendarYear(year) {
    const body = new URLSearchParams();

    body.append("country", "all");
    body.append("name_filter", "");
    body.append("event_type", "all");
    body.append("page", year);
    body.append("cat_filter[]", "wfe");
    body.append("cat_filter[]", "wte");
    body.append("cat_cont[]", "0");
    body.append("id", "");
    body.append("show", "showYear");

    const { data } = await axios.post(FIDE_URL, body, {
        headers: {
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
            "X-Requested-With": "XMLHttpRequest",
            "Referer": `https://calendar.fide.com/majorcalendar.php?page=${year}&show=showYear`,
            "User-Agent": "Mozilla/5.0"
        }
    });

    return data;
}

function parseCalendar(html, year) {
    const $ = cheerio.load(html);

    const events = [];

    $(".border-info").each((_, el) => {
        const anchor = $(el).find(".session-title a");

        const name = anchor.text().trim();

        if (!name)
            return;

        let link = anchor.attr("href");

        if (link && !link.startsWith("http")) {
            link = `${BASE_URL}/${link}`;
        }

        const time = $(el)
            .find(".session-time")
            .first()
            .text()
            .trim();

        const dates = parseDates(time, year);

        events.push({
            name,
            link,
            time,
            startDate: dates.startDate,
            endDate: dates.endDate,
            source: "fide"
        });
    });

    return events;
}

function parseDates(text, year) {
    const match = text.match(
        /(\d{1,2}) (\w{3}) - (\d{1,2}) (\w{3})/
    );

    if (!match) {
        return {
            startDate: null,
            endDate: null
        };
    }

    const months = {
        Jan: 0,
        Feb: 1,
        Mar: 2,
        Apr: 3,
        May: 4,
        Jun: 5,
        Jul: 6,
        Aug: 7,
        Sep: 8,
        Oct: 9,
        Nov: 10,
        Dec: 11
    };

    const startDay = Number(match[1]);
    const startMonth = months[match[2]];

    const endDay = Number(match[3]);
    const endMonth = months[match[4]];

    let startYear = year;
    let endYear = year;

    if (endMonth < startMonth) {
        endYear++;
    }

    return {
        startDate: new Date(startYear, startMonth, startDay),
        endDate: new Date(endYear, endMonth, endDay)
    };
}