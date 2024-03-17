
import React, {useEffect, useState} from 'react';
import Navigation from "../Components/Navigation";
import {AddDeal, GetCar, GetImages, ListenStatus, LogIn, SignalRConnectToCarStatus} from "../API/api";
import {parsePath, useParams, useNavigate} from "react-router-dom";

import "../Styles/CarPage.css"
import {CarFullData} from "../Models/Responses/CarFullData";
import {GetByID} from "../Models/Requests/GetByID";
import {parse} from "querystring";
import {ImageData} from "../Models/Responses/ImageData";
import Carousel from "react-bootstrap/Carousel"
import {Status} from "../Models/Enums/Status";
import {WheelDrive} from "../Models/Enums/WheelDrive";
import {VehicleType} from "../Models/Enums/VehicleType";
import {FuelType} from "../Models/Enums/FuelType";
import {CreateDeal} from "../Models/Requests/CreateDeal";
import {DealData} from "../Models/Responses/DealData";

const CarPage = ()=> {

    const [CarData, setCarData] = useState<CarFullData>();
    const [CarImages, setCarImages] = useState<ImageData[]>();
    const [CarStatus, setCarStatus] = useState<Status>();
    let DealData: DealData | null = null
    const [CreateDeal, setCreateDealData] = useState<CreateDeal>({ CarId: '', UserId: '', TotalPrice: 0 });
    const [loading, setLoading] = useState(true);


    const loginLink = `/login`;
    const navigate = useNavigate();
    const { id } = useParams<{ id: string }>();
    const idString: string = id!.toString()






    useEffect(() => {
        let connection = SignalRConnectToCarStatus(idString)
        ListenStatus(connection,  (status: Status)=>{
            setCarStatus(status)
        })
    }, []);

    useEffect(() => {
        const fetchCar = async () => {
            try {
                let getById: GetByID = {
                    Id: idString
                }
                const result = await GetCar(getById);
                if (result !== null) {
                    setCarData(result);
                    setCarStatus(result.carState.status)
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            } finally {
                setLoading(false);
            }
        };
        const fetchImages = async () => {
            try {
                let getById: GetByID = {
                    Id: idString
                }
                const result = await GetImages(getById);
                if (result !== null) {
                    setCarImages(result);
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            } finally {

                setLoading(false);
            }
        };

        fetchCar();
        fetchImages();
    }, []);
    if (loading) {
        return <p>Loading...</p>;
    }


    const mainPicture = CarImages?.find( o=> o.isPrimary);
    const otherPictures = CarImages?.filter(o => !o.isPrimary)


    const addDeal = async () => {
        try {
            let userId = localStorage.getItem("userId");
            let createDealData: CreateDeal = {
                CarId: CarData?.id!,
                UserId: userId!,
                TotalPrice: CarData?.price!
            };
            setCreateDealData(createDealData);

            const result = await AddDeal(createDealData);
            if (result !== null) {
                DealData = result
            }
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    const handleOrder =  async () => {
        let token = localStorage.getItem('accessToken')
        if (token == null) {
            handleLoginNavigate()
        } else {
            try {
                await addDeal();
                const deallink = `/deal/${DealData?.id}`
                console.log(deallink)
                //navigate(deallink);
            } catch (error) {
                console.error('Error adding deal:', error);
            }
        }
    };




    const getButtonClass = () => {
        switch (CarStatus) {
            case 0:
                return 'button-green';
            case 1:
                return 'button-red';
            case 2:
                return 'button-brown';
            case 3:
                return 'button-gray';
            default:
                return '';
        }
    };


    const handleLoginNavigate = () => {
        navigate(loginLink);
    };


    return (
        <body>
            <header>
                <Navigation/>
            </header>
            <div className="Page container-fluid">
                <div className="row">
                    <div className="col-6">
                        <Carousel slide={false} data-bs-theme="dark">
                            <Carousel.Item>
                                <img className="d-block w-100" src={`data:image/jpeg;base64,${mainPicture?.file}`} style={{ height: '30rem', width: 'auto', objectFit: 'cover' }} alt="Main slide"/>
                            </Carousel.Item>
                            {otherPictures?.map((picture, index) => (
                                <Carousel.Item key={index} className="carousel-item ">
                                    <img className="d-block w-100" src={`data:image/jpeg;base64,${picture.file}`} alt={`Slide ${index + 1}`} style={{ height: '30rem', width: 'auto', objectFit: 'cover' }}/>
                                </Carousel.Item>
                            ))}
                        </Carousel>
                    </div>
                    <div className="car-data col-6">
                        <div className="price">
                            {CarData?.price + '$ Per Day' }
                        </div>

                        <div className="order-car">
                            <button
                                type="button"
                                className={`btn ${getButtonClass()}`}
                                onClick={handleOrder}
                                disabled={CarStatus !== 0}
                            >
                                Order
                            </button>
                        </div>
                        <div className="whole-data">
                            <div className="item">
                                <span className="field">
                                    Mark:
                                </span>
                                <span>{CarData?.mark}</span>
                            </div>
                            <div className="item">
                                <span className="field">
                                    Model:
                                </span>
                                <span>{CarData?.model}</span>
                            </div>
                            <div className="item">
                                <span className="field">
                                    Color:
                                </span>
                                <span>{CarData?.color}</span>
                            </div>
                            <div className="item">
                                <span className="field">
                                    Year:
                                </span>
                                <span>{CarData?.year}</span>
                            </div>
                            <div className="item">
                                <span className="field">
                                    Engine Capacity:
                                </span>
                                <span>{CarData?.engineCapacity + 'l'}</span>
                            </div>
                            <div className="item">
                                <span className="field">
                                    Fuel Type:
                                </span>
                                <span>{FuelType[CarData!.fuelType]}</span>
                            </div>
                            <div className="item">
                                <span className="field">
                                    Wheel Drive:
                                </span>
                                <span>{WheelDrive[CarData!.wheelDrive]}</span>
                            </div>
                            <div className="item">
                                <span className="field">
                                    Vehicle Type:
                                </span>
                                <span>{VehicleType[CarData!.vehicleType]}</span>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </body>
    );
};

export default CarPage;
