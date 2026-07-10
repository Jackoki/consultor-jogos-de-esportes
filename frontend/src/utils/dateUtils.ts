export function formatDate(date: string, hasTime: boolean) {
    const d = new Date(date);

    if (!hasTime) {
        return d.toLocaleDateString("pt-BR");
    }

    return d.toLocaleString("pt-BR", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    });
}