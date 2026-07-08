import express from "express";
import { scrapeFideCalendar } from "../scrapers/Chess/scraper/chessScraper.js";

const router = express.Router();

router.post("/events", async (req, res) => {
    try {
        const events = await scrapeFideCalendar(req.body);
        res.json(events);
    } 
    
    catch (err) {
        console.error(err);

        res.status(500).json({
            message: "Erro ao executar o scraper de xadrez.",
            error: err.message
        });
    }
});

export default router;