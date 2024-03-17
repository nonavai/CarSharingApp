import {LoginData} from "../Models/Requests/LoginData";
import {UserNecessaryData} from "../Models/Requests/UserNecessaryData";
import {UserAuthorizedData} from "../Models/Responses/UserAuthorizedData";
import {CarWithImageData} from "../Models/Responses/CarWithImageData";
import {SearchData} from "../Models/Requests/SearchData";
import {GetByID} from "../Models/Requests/GetByID";
import {CarFullData} from "../Models/Responses/CarFullData";
import {ImageData} from "../Models/Responses/ImageData";
import * as signalR from '@microsoft/signalr';
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {Status} from "../Models/Enums/Status";
import {CreateDeal} from "../Models/Requests/CreateDeal";
import {DealState} from "../Models/Enums/DealState";
import {DealData} from "../Models/Responses/DealData";
import {UserData} from "../Models/Responses/UserData";


const API_BASE_URL = 'https://localhost:7231';

export function SignalRConnectToCarStatus(carId: string){
    let connection: HubConnection | null = null
    connection = new HubConnectionBuilder()
        .withUrl(`https://localhost:7198/car-state`)
        .withAutomaticReconnect()
        .build();

    connection.start()
        .then(() => {
            console.log('SignalR connected');
            connection?.send('ConnectToCar', carId, "status")
                .then(() => console.log(`Connected to car ${carId} of type status`))
                .catch((error: any) => console.error('Error connecting to car: ', error));
        })
        .catch((error: any) => console.error('SignalR connection error: ', error));

    return connection;
}
export function SignalRConnectToCarLocation(carId: string){
    let connection: HubConnection | null = null
    connection = new HubConnectionBuilder()
        .withUrl('/car-state')
        .withAutomaticReconnect()
        .build();

    connection.start()
        .then(() => console.log('SignalR connected'))
        .catch((error: any) => console.error('SignalR connection error: ', error));
    connection.send('ConnectToCar', carId, "location")
        .then(() => console.log(`Connected to car ${carId} of type location`))
        .catch((error: any) => console.error('Error connecting to car: ', error));
    return connection
}

export function SignalRDisconnect(connection: HubConnection, carId: string, type: string){
    connection.send('DisconnectFromCar', carId, type)
        .then(() => console.log(`Disconnected from car ${carId} of type ${type}`))
        .catch((error: any) => console.error('Error disconnecting from car: ', error));
    connection.stop()
        .then(() => console.log('SignalR disconnected'))
        .catch((error: any) => console.error('SignalR disconnection error: ', error));
}

export function ListenStatus(connection : HubConnection,  callback: (status: Status) => void){
    connection.on('ReceiveCarStatusUpdate', callback => {
        console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
    });
}

export function ListenLocation(connection : HubConnection,  callback: (latitude: number, longitude: number) => void){
    connection.on('ReceiveCarStatusUpdate', callback);
}

async function apiRequest<T>(
    method: 'GET' | 'POST' | 'PUT' | 'DELETE',
    url: string,
    data?: any
): Promise<T | null> {
    try {

        const token = localStorage.getItem('accessToken')
        const fetchUrl = API_BASE_URL + "/" + url
        const response = await fetch(fetchUrl, {
            method: method,
            mode: "cors",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(data)
        },)
        console.log(response)
        if (response.status !== 200){
            return null;
        }
        return response.json()
    }
    catch (error){
        throw error;
    }
}




export function GetCar(GetById: GetByID) {
    return apiRequest<CarFullData | null>('GET', `api/car?Id=${GetById.Id}`);
}
export function LogIn(logInData: LoginData) {
    return apiRequest<UserAuthorizedData | null>('POST', `api/user/login`, logInData);
}
export function Registration(RegistrationData: UserNecessaryData) {
    return apiRequest<UserNecessaryData | null>('POST', `api/user`, RegistrationData);
}
export function GetUser(id: string) {
    return apiRequest<UserData | null>('GET', `api/user/${id}`);
}
export function GetImages(GetById: GetByID) {
    return apiRequest<ImageData[] | null>('GET', `api/image?CarId=${GetById.Id}`);
}
export function GetDeal(id: string){
    return apiRequest<DealData | null>('GET', `api/deal?Id=${id}`)
}
export function AddDeal(CreateDeal: CreateDeal){
    return apiRequest<DealData | null>('POST', 'api/deal', CreateDeal)
}
export function CancelDeal(GetById: GetByID){
    return apiRequest<DealData | null>('PUT', 'api/deal/cancel', GetById)
}
export function CompleteDeal(GetById: GetByID){
    return apiRequest<DealData | null>('PUT', 'api/deal/complete', GetById)
}
export function ConfirmDeal(GetById: GetByID){
    return apiRequest<DealData | null>('PUT', 'api/deal/confirm', GetById)
}


export function SearchCars(SearchData: SearchData) {
    let url:string = "api/car/search?";
    if (SearchData.Status !== null && SearchData.Status !== undefined){
        url += `Status=${SearchData.Status.valueOf()}&`;
    }
    if (SearchData.RadiusKm !== null){
        url += `RadiusKm=${SearchData.RadiusKm}&`;
    }
    if (SearchData.Latitude !== null){
        url += `Latitude=${SearchData.Latitude}&`;
    }
    if (SearchData.Longitude !== null){
        url += `Longitude=${SearchData.Longitude}&`;
    }
    if (SearchData.MinYear !== null){
        url += `MinYear=${SearchData.MinYear}&`;
    }
    if (SearchData.MaxYear !== null){
        url += `MaxYear=${SearchData.MaxYear}&`;
    }
    if (SearchData.MinPrice !== null){
        url += `MinPrice=${SearchData.MinPrice}&`;
    }
    if (SearchData.MaxPrice !== null){
        url += `MaxPrice=${SearchData.MaxPrice}&`;
    }
    if (SearchData.VehicleType !== null && SearchData.VehicleType !== undefined){
        url += `VehicleType=${SearchData.VehicleType.valueOf()}&`;
    }
    if (SearchData.FuelType !== null && SearchData.FuelType !== undefined){
        url += `FuelType=${SearchData.FuelType.valueOf()}&`;
    }
    if (SearchData.Mark !== null){
        url += `Mark=${SearchData.Mark}&`;
    }
    if (SearchData.Model !== null){
        url += `Model=${SearchData.Model}&`;
    }
    if (SearchData.WheelDrive !== null&& SearchData.WheelDrive !== undefined){
        url += `WheelDrive=${SearchData.WheelDrive.valueOf()}&`;
    }
    if (SearchData.MinEngineCapacity !== null){
        url += `MinEngineCapacity=${SearchData.MinEngineCapacity}&`;
    }
    if (SearchData.MaxEngineCapacity !== null){
        url += `MaxEngineCapacity=${SearchData.MaxEngineCapacity}&`;
    }
    if (SearchData.CurrentPage !== null){
        url += `CurrentPage=${SearchData.CurrentPage}&`;
    }
    if (SearchData.PageSize !== null){
        url += `PageSize=${SearchData.PageSize}&`;
    }

    url = url.slice(0, -1);
    console.log(url)
    return apiRequest<CarWithImageData[]>('GET', url);
}
