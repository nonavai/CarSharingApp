import React, {useEffect, useState} from 'react';
import {useParams} from 'react-router-dom';
import {CompleteDeal, ConfirmDeal, GetCar, GetDeal, GetUser} from '../API/api';
import '../Styles/Login.css'
import {CarFullData} from "../Models/Responses/CarFullData";
import {UserData} from "../Models/Responses/UserData";
import "../Styles/DealPage.css"
import {GetByID} from "../Models/Requests/GetByID";
import {DealData} from "../Models/Responses/DealData";
import {DealState} from "../Models/Enums/DealState";

const DealPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const idString: string = id!.toString()
    const [carData, setCarData] = useState<CarFullData | null>(null);
    const [dealData, setDealData] = useState<DealData | null>(null);
    const [userData, setUserData] = useState<UserData | null>(null);
    const [totalPrice, setTotalPrice] = useState<number>(0);
    const [carID, setCarId] = useState<string>("");
    const [userID, setUserId] = useState<string>("");
    const [loading, setLoading] = useState<boolean>(true);


    useEffect(() => {
        const fetchData = async () => {
            try {
                // Запрос на получение данных сделки
                const dealResult = await GetDeal(idString);
                if (dealResult !== null) {
                    setCarId(dealResult.carId);
                    setUserId(dealResult.userId);
                    setDealData(dealResult);

                    // Запрос на получение данных машины
                    const carResult = await GetCar({ Id: dealResult.carId });
                    if (carResult !== null) {
                        setCarData(carResult);
                    }

                    // Запрос на получение данных пользователя
                    const userResult = await GetUser(dealResult.userId);
                    if (userResult !== null) {
                        setUserData(userResult);
                    }
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    const handleDealButton = async () => {
        let GetByID: GetByID = {
            Id: idString
        }
        switch (dealData?.state) {
            case DealState.booking:
                // Вызовите метод для подтверждения сделки
                await ConfirmDeal(GetByID);
                break;
            case DealState.active:
                // Вызовите метод для возврата автомобиля
                await CompleteDeal(GetByID);
                break;
            default:
                // Действия для других статусов сделки
                break;
    }};

    return (
        <div>
            <h2>Deal Confirmation</h2>
            <div className="car-info">
                <h3>Car Information</h3>
                <p>Car Mark: {carData?.mark}</p>
                <p>Car Model: {carData?.model}</p>
                <p>Year: {carData?.year}</p>
            </div>
            <div className="user-info">
                <h3>User Information</h3>
                <p>Name: {userData?.firstName} {userData?.lastName}</p>
                <p>Username: {userData?.userName}</p>
                <p>Email: {userData?.email}</p>
            </div>
            <div className="order-info">
                <h3>Order Summary</h3>
                <p>Total Price: {dealData?.totalPrice}</p>
            </div>
            {(dealData?.state === DealState.booking || dealData?.state === DealState.active) && (
                <button onClick={handleDealButton}>
                    {dealData?.state === DealState.booking ? 'Confirm Deal' : 'Return Car'}
                </button>
            )}
        </div>
    );
};

export default DealPage;
