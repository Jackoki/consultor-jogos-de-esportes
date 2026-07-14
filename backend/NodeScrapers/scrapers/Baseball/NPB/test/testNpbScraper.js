import { scrapeNpbCalendar } from "../scraper/npbScraper.js";


async function test() {
    try {
        const events = await scrapeNpbCalendar({
            date_start: "2026-07-01",
            date_end: "2026-07-05"
        });

        console.log(`Total de jogos encontrados: ${events.length}`);

        events.forEach((event, index) => {
            console.log(`Jogo ${index + 1}`);
            console.log("Nome:", event.name);
            console.log("Casa:", event.home_team);
            console.log("Fora:", event.away_team);
            console.log("Data:", event.date);
        });
    } 

    catch (error) {
        console.error("Erro no teste:");
        console.error(error);
    }

}

test();