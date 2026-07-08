import { scrapeFideCalendar } from "../scraper/chessScraper.js";

async function run() {
  try {
    const events = await scrapeFideCalendar({
      date_start: new Date("2025-01-01"),
      date_end: new Date("2025-12-31")
    });

    console.log("Total de eventos:", events.length);
    console.table(events);
  } 
  
  catch (err) {
    console.error("Erro no scraper:", err);
  }
}

run();