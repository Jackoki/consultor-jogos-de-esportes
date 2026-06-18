export const DateFilterType = {
    Today: 1,
    SpecificDate: 2,
    Week: 3
} as const;

export type DateFilterType = typeof DateFilterType[keyof typeof DateFilterType];

export interface FilterDates {
    dateFilterType: DateFilterType;
    date?: string | null;
}