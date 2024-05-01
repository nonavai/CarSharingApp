import {DealState} from "../Enums/DealState";

export interface DealData{
    id: string,
    carId: string,
    userId: string,
    requested: Date
    finished?: Date | null,
    state: DealState,
    totalPrice: number,
    rating?: number | null
}