import React from 'react';
import Cookies from 'js-cookie';
import jwt_decode from 'jwt-decode';
import {Link, useNavigate} from 'react-router-dom'

const ProfileButton = () => {
    const navigate = useNavigate();

    const handleProfileClick = async () => {

        const accessToken = localStorage.getItem("accessToken");
        let userId = null;
        if (accessToken) {
            try {
                let decodedToken: any;
                decodedToken = jwt_decode.jwtDecode(accessToken);
                if (decodedToken) {
                    const userId = decodedToken.jti;


                    navigate(`/profile/${userId}`)
                } else {
                    navigate('/login')
                }
            } catch (error) {
                console.error('Error decoding the access token', error);
                navigate('/login')
            }
        }
        else {
            navigate('/login')
        }
    };


    return (
        <div className="img-fluid" onClick={handleProfileClick}>
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" id="profile"><g fill="none" fill-rule="evenodd" stroke="#200E32" stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5" transform="translate(4 2.5)"><circle cx="7.579" cy="4.778" r="4.778"></circle><path d="M5.32907052e-15,16.2013731 C-0.00126760558,15.8654831 0.0738531734,15.5336997 0.219695816,15.2311214 C0.677361723,14.3157895 1.96797958,13.8306637 3.0389178,13.610984 C3.81127745,13.4461621 4.59430539,13.3360488 5.38216724,13.2814646 C6.84083861,13.1533327 8.30793524,13.1533327 9.76660662,13.2814646 C10.5544024,13.3366774 11.3373865,13.4467845 12.1098561,13.610984 C13.1807943,13.8306637 14.4714121,14.270023 14.929078,15.2311214 C15.2223724,15.8479159 15.2223724,16.5639836 14.929078,17.1807781 C14.4714121,18.1418765 13.1807943,18.5812358 12.1098561,18.7917621 C11.3383994,18.9634099 10.5550941,19.0766219 9.76660662,19.1304349 C8.57936754,19.2310812 7.38658584,19.2494317 6.19681255,19.1853548 C5.92221301,19.1853548 5.65676678,19.1853548 5.38216724,19.1304349 C4.59663136,19.077285 3.8163184,18.9640631 3.04807112,18.7917621 C1.96797958,18.5812358 0.686515041,18.1418765 0.219695816,17.1807781 C0.0745982583,16.8746908 -0.000447947969,16.5401098 5.32907052e-15,16.2013731 Z"></path></g></svg>
        </div>
    );
};

export default ProfileButton;
