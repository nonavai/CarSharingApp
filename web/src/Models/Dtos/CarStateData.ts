import {Status} from "../Enums/Status";

export interface CarStateData{
    Id: string,
    CarId: string
    Status: Status,
    Latitude: number,
    Longitude: number,
}