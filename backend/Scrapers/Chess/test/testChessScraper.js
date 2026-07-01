import { scrapeFideCalendar } from "../scraper/chessScraper.js";

async function run() {
  try {
    const events = await scrapeFideCalendar();

    console.log("Total de eventos:", events.length);
    console.log(events.slice(0, 10));
  } 
  
  catch (err) {
    console.error("Erro no scraper:", err);
  }
}

run();