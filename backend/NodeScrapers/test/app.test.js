import request from "supertest";
import app from "../app.js";

describe("API Scrapers", () => {

    it("deve responder a rota chess", async () => {
        const response = await request(app)
            .post("/chess/events")
            .send({
                date_start: "2026-01-01",
                date_end: "2026-12-31"
            });

        expect(response.statusCode).toBe(200);
    });

});