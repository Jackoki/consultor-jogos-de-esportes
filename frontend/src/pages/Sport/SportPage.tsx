import { useParams } from "react-router-dom";
import { useState } from "react";
import { Header } from "../../components/Header/Header";
import { FilterForm } from "../../components/FilterForm/FilterForm";
import { EventSections } from "../../components/EventSections/EventSections";
import type { SportEvent } from "../../types/SportEvent";

export function SportPage() {
    const { sport, league } = useParams();
    const [events, setEvents] = useState<SportEvent[]>([]);

    return (
        <>
            <Header />
                <FilterForm setEvents={setEvents} sport={sport} league={league}/>
            <EventSections events={events}/>
        </>
    );
}