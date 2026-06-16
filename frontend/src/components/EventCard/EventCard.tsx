import type { SportEvent } from "../../types/SportEvent";

interface Props {
    event: SportEvent;
}

export function EventCard({ event }: Props) {
    return (
        <div>
            <h3>{event.eventName}</h3>

            <p>
                <strong>Esporte:</strong> {event.sportName}
            </p>

            <p>
                <strong>Local:</strong> {event.location}
            </p>

            <p>
                <strong>Início:</strong> {event.beginDate}
            </p>

            <p>
                <strong>Fim:</strong> {event.endDate}
            </p>
        </div>
    );
}