import express from "express";
import chessRouter from "./routes/chess.js";

const app = express();

app.use(express.json());

app.use("/chess", chessRouter);

app.listen(3000, () => {
    console.log("Servidor de scrapers iniciado na porta 3000");
});