import axios from "axios";
import * as cheerio from "cheerio";

const FIDE_URL = "https://calendar.fide.com/calendar_server.php";

export async function scrapeFideCalendar() {
  const body = new URLSearchParams();

  body.append("country", "all");
  body.append("name_filter", "");
  body.append("event_type", "all");
  body.append("page", "2025");
  body.append("cat_filter[]", "wfe");
  body.append("cat_filter[]", "wte");
  body.append("cat_cont[]", "0");
  body.append("id", "");
  body.append("show", "showYear");

  const { data: html } = await axios.post(FIDE_URL, body, {
    headers: {
      "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
      "X-Requested-With": "XMLHttpRequest",
      "Referer":
        "https://calendar.fide.com/majorcalendar.php?page=2025&show=showYear",
      "User-Agent": "Mozilla/5.0"
    }
  });

  const $ = cheerio.load(html);

  const events = [];

  $(".border-info").each((_, el) => {
    const anchor = $(el).find(".session-title a");

    const name = anchor.text().trim();
    let link = anchor.attr("href");

    const time = $(el).find(".session-time").first().text().trim();

    if (!name) return;

    if (link && !link.startsWith("http")) {
      link = `https://calendar.fide.com/${link}`;
    }

    events.push({
      name,
      link,
      time,
      source: "fide"
    });
  });

  return events;
}