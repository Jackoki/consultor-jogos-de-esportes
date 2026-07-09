import { useState } from "react";
import { FilterForm } from "../../components/FilterForm/FilterForm";
import { EventCard } from "../../components/EventCard/EventCard";
import type { SportEvent } from "../../types/SportEvent";
import "./Home.css";

export function Home() {
    const [events, setEvents] = useState<SportEvent[]>([]);

    const eventsBySport = events.reduce((acc, event) => {
        if (!acc[event.sportName]) {
            acc[event.sportName] = [];
        }

        acc[event.sportName].push(event);
        return acc;
    }, {} as Record<string, SportEvent[]>);

    return (
        <>
            <h1>Consultor de Eventos Esportivos</h1>

            <FilterForm setEvents={setEvents} />

            {Object.entries(eventsBySport).map(([sport, sportEvents]) => (
                <section key={sport} className="sport-section">
                    <h2>{sport}</h2>

                    <div className="event-grid">
                        {sportEvents.map(event => (
                            <EventCard
                                key={`${event.eventName}-${event.beginDate}`}
                                event={event}
                            />
                        ))}
                    </div>
                </section>
            ))}
        </>
    );
}