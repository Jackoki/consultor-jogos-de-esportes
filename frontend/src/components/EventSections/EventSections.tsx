import type { SportEvent } from "../../types/SportEvent";
import { EventCard } from "../EventCard/EventCard";

export function EventSections({ events }: { events: SportEvent[] }) {

    const eventsBySport = events.reduce((acc, event) => {
        if (!acc[event.sportName]) {
            acc[event.sportName] = [];
        }

        acc[event.sportName].push(event);
        return acc;
    }, {} as Record<string, SportEvent[]>);


    return (
        <>
            {Object.entries(eventsBySport).map(([sport, sportEvents]) => (
                <section key={sport} className="sport-section">
                    <h2>{sport}</h2>

                    <div className="event-grid">
                        {sportEvents.map(event => (
                            <EventCard key={`${event.eventName}-${event.beginDate}`} event={event}/>
                        ))}
                    </div>
                </section>
            ))}
        </>
    );
}