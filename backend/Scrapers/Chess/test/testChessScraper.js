import { scrapeFideCalendar } from "../scraper/chessScraper.js";

async function run() {
  try {
    const events = await scrapeFideCalendar({
      startDate: new Date("2025-04-01"),
      endDate: new Date("2025-04-30")
    });

    console.log("Total de eventos:", events.length);
    console.table(events);
  } 
  
  catch (err) {
    console.error("Erro no scraper:", err);
  }
}

run();