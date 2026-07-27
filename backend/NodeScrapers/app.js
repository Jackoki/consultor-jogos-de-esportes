import express from "express";
import chessRouter from "./routes/chess.js";
import npbRouter from "./routes/npb.js";

const app = express();

app.use(express.json());

app.use("/chess", chessRouter);
app.use("/npb", npbRouter);

export default app;