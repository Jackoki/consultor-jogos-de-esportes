import { useState } from "react";
import { api } from "../../services/api";
import { DateFilterType } from "../../types/FilterDates";

export function FilterForm({ setEvents }: any) {

    const [dateFilterType, setDateFilterType] = useState<1 | 2 | 3>(DateFilterType.Today);

    const [date, setDate] = useState("");
    const [error, setError] = useState<string | null>(null);

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError(null);

        const payload = {
            dateFilterType,
            date: date || null
        };

        try {
            const response = await api.post("/f1/meetings", payload);
            setEvents(response.data);
        }

        catch (error: any) {
            const message = error?.response?.data?.message || "Erro ao buscar eventos";
            setError(message);
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <select value={dateFilterType} onChange={e => setDateFilterType(Number(e.target.value) as 1 | 2 | 3)}>
                <option value={DateFilterType.Today}>
                    Hoje
                </option>

                <option value={DateFilterType.SpecificDate}>
                    Data Específica
                </option>

                <option value={DateFilterType.Week}>
                    Semana
                </option>
            </select>

            {dateFilterType !== DateFilterType.Today && (
                <input type="date" value={date} onChange={e => setDate(e.target.value)}/>
            )}

            <button type="submit">
                Buscar
            </button>

            {error && (
                <div style={{ color: "red", marginTop: "10px" }}>
                    {error}
                </div>
            )}
        </form>
    );
}