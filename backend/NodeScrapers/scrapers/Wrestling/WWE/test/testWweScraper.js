import { scrapeWweCalendar } from "../scraper/wweScraper.js";

async function run() {
    try {
        const events = await scrapeWweCalendar({
            date_start: new Date("2025-01-01"),
            date_end: new Date("2025-01-07")
        });

        console.log("\nTotal de eventos:", events.length);
        console.table(events);
    } 
    
    catch (err) {
        console.error("\nErro no scraper:", err);
    }
}

run();