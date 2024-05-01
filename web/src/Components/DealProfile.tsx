import React, {useEffect, useState} from 'react';
import {Link, useNavigate, useParams} from "react-router-dom";
import '../Styles/CarProfile.css';
import {DealData} from "../Models/Responses/DealData";
import {CarWithImageData} from "../Models/Responses/CarWithImageData";
import {GetByID} from "../Models/Requests/GetByID";
import {GetCar, GetImages, GetPrimaryImage} from "../API/api";
import {CarFullData} from "../Models/Responses/CarFullData";
import {ImageData} from "../Models/Responses/ImageData";
import {WheelDrive} from "../Models/Enums/WheelDrive";
import {DealState} from "../Models/Enums/DealState";

const DealProfile: React.FC<DealData> = (DealData ) => {
    const [loading, setLoading] = useState(true);
    const [car, setCarData] = useState<CarFullData>();
    const [image, setImage] = useState<ImageData>();
    const link = `/deal/${DealData.id}`;
    const navigate = useNavigate();
    useEffect(() => {
        const fetchCar = async () => {
            try {
                let getById: GetByID = {
                    Id: DealData.carId
                }
                const result = await GetCar(getById);
                if (result !== null) {
                    setCarData(result);
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
                    Id: DealData.carId
                }
                const result = await GetPrimaryImage(getById);
                if (result !== null) {
                    console.log(result)
                    setImage(result);
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

    const handleNavigate = () => {
        navigate(link);
    };
    return (
        <div className="product card container-fluid">
            <div className="row" >
                <div className="product-image col" >
                    <img className="card-img" src={`data:image/jpeg;base64,${image?.file}`} onClick={handleNavigate} alt=""></img>
                </div>
                <div className="product-details card-body col-auto">
                    <div className="car-info">
                        <h3>Car Information</h3>
                        <p>Car Mark: {car?.mark}</p>
                        <p>Car Model: {car?.model}</p>
                        <p>Year: {car?.year}</p>
                        <p>Color: {car?.color}</p>
                    </div>
                    <h6>
                        {"Deal Status:" + DealState[DealData.state]}
                    </h6>
                    <div className="order-info">
                        <p>Total Price: {DealData.totalPrice}</p>
                    </div>
                    <div className="order-info">
                        <p>Finished: {DealData.finished?.toLocaleString()}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default DealProfile;
