import "./EventCard.css";
import type { SportEvent } from "../../types/SportEvent";
import { formatDate } from "../../utils/dateUtils";

interface Props {
    event: SportEvent;
}

export function EventCard({ event }: Props) {
    return (
        <div className="event-card">
            <h3>{event.eventName}</h3>
            <p>
                <strong>Local:</strong> {event.location}
            </p>

            <p>
                <strong>Início:</strong> {formatDate(event.beginDate, event.hasTime)}
            </p>

            <p>
                <strong>Início:</strong> {formatDate(event.endDate, event.hasTime)}
            </p>
        </div>
    );
}