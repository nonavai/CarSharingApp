import {Status} from "../Enums/Status";

export interface CarStateData{
    id: string,
    carId: string
    status: Status,
    latitude: number,
    longitude: number,
}