import { render, screen } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
import { Header } from "../../components/Header/Header";

describe("Header", () => {
    it("deve renderizar o link Home", () => {
        render(
            <BrowserRouter>
                <Header />
            </BrowserRouter>
        );

        expect(screen.getByText("Home")).toBeInTheDocument();
    });
});