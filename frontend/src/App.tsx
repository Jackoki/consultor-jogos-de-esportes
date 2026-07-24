import { Routes, Route } from "react-router-dom";
import { Home } from "./pages/Home/Home";
import { SportPage } from "./pages/Sport/SportPage";

export function App() {
    return (
        <Routes>
            <Route path="/" element={<Home />}/>
            <Route path="/:sport/:league?"element={<SportPage />}/>
        </Routes>
    );
}