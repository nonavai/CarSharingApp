import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import {UserNecessaryData} from "../Models/Requests/UserNecessaryData";
import {Registration} from "../API/api";
import '../Styles/Login.css'


const RegistrationPage: React.FC = () => {
    const [RegistrData, setRegistrationData] =
        useState<UserNecessaryData>({
            Email: '',
            Password: '',
            FirstName: '',
            LastName: '',
            PhoneNumber: '',
            RecordNumber: '',
            UserName: ''
        });
    const [error, setError] = useState('');

    const handleRegistration = async (e: any) => {
        try {
            e.preventDefault();
            const response = await Registration(RegistrData); // Pass the Login model

            // Handle successful login, e.g., store a token in local storage.
        } catch (error) {
            setError('Invalid credentials. Please try again.');
        }
    };

    return (
        <form className="form" onSubmit={e=> handleRegistration(e)}>
            <p className="form-title">Create an account</p>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="Email"
                    type="email"
                    value={RegistrData.Email}
                    onChange={(e) =>
                        setRegistrationData({ ...RegistrData, Email: e.target.value })
                    }
                />
            </div>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="Password"
                    type="password"
                    value={RegistrData.Password}
                    onChange={(e) =>
                        setRegistrationData({ ...RegistrData, Password: e.target.value })
                    }
                />
            </div>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="FirstName"
                    type="firstname"
                    value={RegistrData.FirstName}
                    onChange={(e) =>
                        setRegistrationData({ ...RegistrData, FirstName: e.target.value })
                    }
                />
            </div>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="LastName"
                    type="lastname"
                    value={RegistrData.LastName}
                    onChange={(e) =>
                        setRegistrationData({ ...RegistrData, LastName: e.target.value })
                    }
                />
            </div>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="PhoneNumber"
                    type="phoneNumber"
                    value={RegistrData.PhoneNumber}
                    onChange={(e) =>
                        setRegistrationData({ ...RegistrData, PhoneNumber: e.target.value })
                    }
                />
            </div>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="RecordNumber"
                    type="recordNumber"
                    value={RegistrData.RecordNumber}
                    onChange={(e) =>
                        setRegistrationData({ ...RegistrData, RecordNumber: e.target.value })
                    }
                />
            </div>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="UserName"
                    type="userName"
                    value={RegistrData.UserName}
                    onChange={(e) =>
                        setRegistrationData({ ...RegistrData, UserName : e.target.value })
                    }
                />
            </div>
            <button type="submit" className="submit">
                Sing In
            </button>

            <p className="signup-link">
                Already have account?
                <Link to="/login">Log In</Link>
            </p>
        </form>
    );
};

export default RegistrationPage;







