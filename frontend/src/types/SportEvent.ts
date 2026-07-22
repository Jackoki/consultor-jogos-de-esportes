export interface SportEvent {
    sportName: string;
    eventName: string;
    beginDate: string;
    endDate: string;
    location: string;
    hasTime: boolean;
    centralImage?: string;
    leftImage?: string;
    rightImage?: string;
}