import "./EventCard.css";
import type { SportEvent } from "../../types/SportEvent";
import { formatDate } from "../../utils/dateUtils";

interface Props {
    event: SportEvent;
}

export function EventCard({ event }: Props) {
    return (
        <div className="event-card">
            <h3>
                {event.eventName}
                    {
                        event.leftImage && event.rightImage 
                        ? 
                            (
                            <div className="left-right-images">
                                <img src={event.leftImage} /> 
                                <span>X</span>
                                <img src={event.rightImage} />
                            </div>
                            )
                        :
                            event.centralImage ? 
                            (
                                <div className="event-image">
                                    <img src={event.centralImage}/>
                                </div>
                            ) : null
                    }
            </h3>
            <p>
                <strong>Local:</strong> {event.location}
            </p>

            <p>
                <strong>Início:</strong> {formatDate(event.beginDate, event.hasTime)}
            </p>

            <p>
                <strong>Termino:</strong> {formatDate(event.endDate, event.hasTime)}
            </p>
        </div>
    );
}