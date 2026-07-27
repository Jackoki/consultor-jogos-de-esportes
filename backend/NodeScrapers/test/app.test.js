import { jest } from "@jest/globals";

jest.unstable_mockModule("../scrapers/Chess/scraper/chessScraper.js", () => ({
    scrapeFideCalendar: jest.fn().mockResolvedValue([
        {
            name: "World Championship",
            country: "Brasil"
        }
    ])
}));

import request from "supertest";

const { default: app } = await import("../app.js");

describe("API Scrapers", () => {

    it("deve responder a rota chess", async () => {

        const response = await request(app)
            .post("/chess/events")
            .send({
                date_start: "2026-01-01",
                date_end: "2026-01-31"
            });

        expect(response.status).toBe(200);

        expect(response.body).toHaveLength(1);

        expect(response.body[0].name).toBe("World Championship");
    });

});