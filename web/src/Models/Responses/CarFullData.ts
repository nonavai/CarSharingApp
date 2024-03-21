import {FuelType} from "../Enums/FuelType";
import {VehicleType} from "../Enums/VehicleType";
import {WheelDrive} from "../Enums/WheelDrive";
import {CarStateData} from "../Dtos/CarStateData";

export interface CarFullData{
    id: string,
    userId: string,
    year: number,
    registrationNumber: string,
    mark: string,
    model: string,
    price: number,
    fuelType: FuelType,
    vehicleType: VehicleType,
    color: string,
    wheelDrive: WheelDrive,
    engineCapacity: number,
    description?: string| null,
    carState: CarStateData
}