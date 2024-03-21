import React, {useEffect, useState} from 'react';
import {Link, useParams} from 'react-router-dom';
import {GetCar, GetDeal, GetDealsByUser, GetUser, LogIn} from '../API/api';
//import '../Styles/Login.css'
import {DealData} from "../Models/Responses/DealData";
import {UserData} from "../Models/Responses/UserData";
import DealProfile from "../Components/DealProfile";
import user_icon from "../images/user_icon.png"

import '../Styles/UserPage.css'
import Navigation from "../Components/Navigation";


const UserPage: React.FC = () => {
    const [dealData, setDealData] = useState<DealData[]>([]);
    const [userData, setUserData] = useState<UserData | null>(null);
    const { id } = useParams<{ id: string }>();
    const idString: string = id!.toString()
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const dealResult = await GetDealsByUser(idString,currentPage, 10 );
                if (dealResult !== null) {
                    setDealData(dealResult);

                    const userResult = await GetUser(dealResult[0].userId);
                    if (userResult !== null) {
                        setUserData(userResult);
                    }
                }
            } catch (error) {
                console.error('Error fetching data:', error);
            } finally {
            }
        };

        fetchData();
    }, [currentPage]);

    const nextPage = () => {
        setCurrentPage(currentPage + 1);
    };

    const prevPage = () => {
        setCurrentPage(currentPage - 1);
    };

    return (
        <body className="container-fluid">
            <Navigation></Navigation>
            <div className="d-flex flex-column align-items-center pt-4">
                <div>
                    <img className="user-icon" src={user_icon} alt="User Icon" />
                </div>
                <div className="user-info">
                    <p>Name: {userData?.userName}</p>
                    <p>Email: {userData?.email}</p>
                </div>
                <div className="car-list">
                    <ul className="list-group">
                        {dealData.map((deal, index) => (
                            <li key={index} id={`result-${index}`} className="list-group-item">
                                <DealProfile {...deal} />
                            </li>
                        ))}
                    </ul>
                </div>
                <div>
                    <button className={currentPage === 1 ? 'disabled' : ''} onClick={prevPage} disabled={currentPage === 1}>Previous</button>
                    <span>Page: {currentPage}</span>
                    <button className={dealData.length < 10 ? 'disabled' : ''} onClick={nextPage} disabled={dealData.length < 10}>Next</button>
                </div>
            </div>
        </body>
    );
};

export default UserPage;
