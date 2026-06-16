import { useState } from "react";
import { api } from "../../services/api";

export function FilterForm({ setEvents }: any) {
    const [dateFilterType, setDateFilterType] = useState("Today");
    const [date, setDate] = useState("");

    async function handleSubmit(e: React.FormEvent){
        e.preventDefault();

        const response = await api.post(
            "/f1/meetings",{
                dateFilterType,
                date
            }
        );

        setEvents(response.data);
    }

    return (
        <form onSubmit={handleSubmit}>
            <select value={dateFilterType} onChange={e => setDateFilterType(e.target.value)}>
                <option value="Today">Hoje</option>
                <option value="SpecificDate">Data Específica</option>
                <option value="Week">Semana</option>
            </select>

            {dateFilterType !== "Today" && (<input type="date" value={date} onChange={e => setDate(e.target.value)}/>)}

            <button type="submit">
                Buscar
            </button>
        </form>
    )
}