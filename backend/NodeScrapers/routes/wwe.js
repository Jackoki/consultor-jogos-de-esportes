import express from "express";
import { scrapeWweCalendar } from "../scrapers/Wrestling/WWE/scraper/wweScraper.js";

const router = express.Router();

router.post("/events", async (req, res) => {
    try {
        const events = await scrapeWweCalendar(req.body);
        res.json(events);
    } 
    
    catch (err) {
        console.error(err);

        res.status(500).json({
            message: "Erro ao executar o scraper do WWE.",
            error: err.message
        });
    }
});

export default router;