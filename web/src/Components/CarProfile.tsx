
import React from 'react';

import {Link, useNavigate, useParams} from "react-router-dom";
import '../Styles/CarProfile.css';
import {CarWithImageData} from "../Models/Responses/CarWithImageData";

const CarProfile: React.FC<CarWithImageData> = (CarResponse ) => {
    console.log(CarResponse)

    const { id } = useParams<{ id: string }>();

    console.log(id);
    const link = `/car-profile/${CarResponse.id}`;
    const navigate = useNavigate();

    const handleNavigate = () => {
        navigate(link);
    };
    return (
        <div className="product card container-fluid">
            <div className="row" >
                <div className="product-image col" >
                    <img className="card-img" src={`data:image/jpeg;base64,${CarResponse.file}`} onClick={handleNavigate} alt=""></img>
                </div>
                <div className="product-details card-body col-auto">
                    <h5 className="product-name">
                        {CarResponse.mark + " " + CarResponse.model}
                    </h5>
                    <h6>
                        {"Year: " + CarResponse.year + "   Status:" + CarResponse.carState.status}
                    </h6>
                    <span className="product-bottom">
                        {CarResponse.price}
                    </span>
                </div>
            </div>

        </div>
    );
};

export default CarProfile;
