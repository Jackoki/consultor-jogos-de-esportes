import { useState } from "react";
import { FilterForm } from "../../components/FilterForm/FilterForm";
import { EventCard } from "../../components/EventCard/EventCard";
import type { SportEvent } from "../../types/SportEvent";

export function Home() {
    const [events, setEvents] = useState<SportEvent[]>([]);

    return (
        <>
            <h1>Consultor de Eventos Esportivos</h1>
            <FilterForm setEvents={setEvents} />
            
            <div>
                {events.map(event => (
                    <EventCard key={`${event.eventName}-${event.beginDate}`} event={event}/>
                ))}
            </div>
        </>
    );
}