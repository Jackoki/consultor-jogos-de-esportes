import axios from "axios";
import * as cheerio from "cheerio";
import dayjs from "dayjs";

const FIDE_URL =
  "https://calendar.fide.com/majorcalendar.php?name_filter=&from_date=2025-01-01&to_date=2026-01-01&country=all&show=showYear&page=2025&cat_filter=wfe%2Cwte&cat_cont=0&event_type=all&time_control=all";

export async function scrapeFideCalendar() {
  const { data: html } = await axios.get(FIDE_URL, {
    headers: {
      "User-Agent": "Mozilla/5.0"
    }
  });

  const $ = cheerio.load(html);

  const events = [];

  // 🔍 pega todas as linhas da tabela principal
  $("table tr").each((_, row) => {
    const cols = $(row).find("td");

    if (cols.length < 3) return;

    const name = $(cols[0]).text().trim();
    const dateRaw = $(cols[1]).text().trim();
    const location = $(cols[2]).text().trim();

    if (!name || !dateRaw) return;

    const { startDate, endDate } = parseDateRange(dateRaw);

    events.push({
      name,
      dateRaw,
      location,
      startDate,
      endDate,
      source: "fide"
    });
  });

  return events;
}