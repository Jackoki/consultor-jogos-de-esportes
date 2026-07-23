import axios from "axios";
import * as cheerio from "cheerio";
import { getTeamName } from "../../../../utils/npbTeamsHelper.js";

const BASE_URL = "https://npb.jp";

export async function scrapeNpbCalendar({ date_start, date_end }) {

    if (!date_start || !date_end) {
        throw new Error("Datas da filtragem são obrigatórias.");
    }

    date_start = new Date(date_start);
    date_end = new Date(date_end);

    const months = getMonthsBetween(date_start, date_end);
    const events = [];

    for (const month of months) {
        const html = await fetchCalendarMonth(month.year, month.month);
        events.push(...parseCalendar(html, month.year, month.month));
    }

    return events.filter(event =>
        event.date >= date_start &&
        event.date <= date_end
    );

}

function getMonthsBetween(startDate, endDate) {
    const months = [];

    let year = startDate.getFullYear();
    let month = startDate.getMonth() + 1;

    while (year < endDate.getFullYear() || (year === endDate.getFullYear() && month <= endDate.getMonth() + 1)) {
        months.push({
            year,
            month
        });

        month++;

        if (month > 12) {
            month = 1;
            year++;
        }
    }
    return months;
}

async function fetchCalendarMonth(year, month) {

    const url = `${BASE_URL}/bis/eng/${year}/calendar/index_${String(month).padStart(2, "0")}.html`;

    const { data } = await axios.get(url, {
        headers: {
            "User-Agent": "Mozilla/5.0"
        }
    });

    return data;
}

function parseCalendar(html, year, month) {
    const $ = cheerio.load(html);
    const events = [];

    $("td.stschedule").each((_, td) => {
        events.push(...parseDay($, td, year, month));
    });

    return events;
}

function parseDay($, td, year, month) {
    const games = [];

    const dayText = $(td).find(".teschedate").first().text().trim();

    const day = parseInt(dayText);

    if (isNaN(day))
        return games;

    $(td).find(".stvsteam > div").each((_, div) => {
        const game = parseGame($, div, year,month,day);

        if (game)
            games.push(game);
    });

    return games;
}

function parseGame($, gameNode, year, month, day) {
    const text = $(gameNode).text().replace(/\s+/g, " ").trim();

    if (!text)
        return null;

    if (text.includes("All-Star"))
        return null;

    let match = text.match(/^([A-Z]+)\s*-\s*([A-Z]+)\s+(\d{1,2}:\d{2})$/);

    if (match) {
        const [, home, away, time] = match;
        let link = "";
        const anchor = $(gameNode).find("a");

        if (anchor.length) {
            link = BASE_URL + anchor.attr("href");
        }

        return {
            name: `${getTeamName(home)} x ${getTeamName(away)}`,
            date: new Date(year, month - 1, day),
            homeTeam: {
                code: home,
                name: getTeamName(home)
            },

            awayTeam: {
                code: away,
                name: getTeamName(away)
            }
        };
    }

    match = text.match(/^([A-Z]+)\s+(\*|\d+)\s*-\s*(\*|\d+)\s+([A-Z]+)$/);

    if (match) {
        const [, away, awayScore, homeScore, home] = match;
        let link = "";
        const anchor = $(gameNode).find("a");

        if (anchor.length) {
            link = BASE_URL + anchor.attr("href");
        }

        return {
            name: `${getTeamName(home)} x ${getTeamName(away)}`,
            date: new Date(year, month - 1, day),
            homeTeam: {
                code: home,
                name: getTeamName(home)
            },

            awayTeam: {
                code: away,
                name: getTeamName(away)
            }
        };
    }
    
    return null;
}