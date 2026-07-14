import express from "express";
import { scrapeNpbCalendar } from "../scrapers/Baseball/NPB/scraper/npbScraper.js";

const router = express.Router();

router.post("/events", async (req, res) => {
    try {
        const events = await scrapeNpbCalendar(req.body);
        res.json(events);
    } 
    
    catch (err) {
        console.error(err);

        res.status(500).json({
            message: "Erro ao executar o scraper do NPB (Liga Japonesa de Beisebol).",
            error: err.message
        });
    }
});

export default router;