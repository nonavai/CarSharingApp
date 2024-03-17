import {VehicleType} from "../Enums/VehicleType";
import {FuelType} from "../Enums/FuelType";
import {WheelDrive} from "../Enums/WheelDrive";
import {Status} from "../Enums/Status";

export interface SearchData{
    Status?: Status | null,
    RadiusKm?: number | null,
    Latitude?: number | null,
    Longitude?: number | null,
    MinYear?: number | null,
    MaxYear?: number | null,
    Mark?: string| null,
    Model?: string| null,
    Color?: string| null,
    MinPrice?: number| null,
    MaxPrice?: number| null,
    VehicleType?: VehicleType| null,
    FuelType?: FuelType| null,
    WheelDrive?:WheelDrive| null,
    MinEngineCapacity?: number | null,
    MaxEngineCapacity?: number | null,
    CurrentPage?: number | null,
    PageSize?: number | null,
}