import { useState } from "react";
import { FilterForm } from "../../components/FilterForm/FilterForm";
import { Header } from "../../components/Header/Header";
import { EventSections } from "../../components/EventSections/EventSections";
import type { SportEvent } from "../../types/SportEvent";
import "./Home.css";

export function Home() {
    const [events, setEvents] = useState<SportEvent[]>([]);

    return (
        <>
            <Header />
            <h1>Consultor de Eventos Esportivos</h1>
            <FilterForm setEvents={setEvents} />
            <EventSections events={events} />
        </>
    );
}