import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { LogIn } from '../API/api';
import { LoginData } from '../Models/Requests/LoginData';
import '../Styles/Login.css'


const LoginPage: React.FC = () => {
    const [loginData, setLoginData] = useState<LoginData>({ Email: '', Password: '' });
    const [error, setError] = useState('');

    const handleLogin = async (e: any) => {
        e.preventDefault();
        try {
            const response = await LogIn(loginData);
            if(response !== null){
                localStorage.removeItem('accessToken')
                localStorage.setItem('accessToken', response.accessToken)

                localStorage.removeItem('userId')
                localStorage.setItem('userId', response.id)
                window.location.href = `/profile/${response.id}`;
            }
            // Handle successful login, e.g., store a token in local storage.
        } catch (error) {
            setError('Invalid credentials. Please try again.');
        }
    };

    return (
        <form className="form" onSubmit={e=> handleLogin(e)}>
            <p className="form-title">Sign in to your account</p>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="Enter email"
                    type="email"
                    value={loginData.Email}
                    onChange={(e) =>
                        setLoginData({ ...loginData, Email: e.target.value })
                    }
                />
            </div>
            <div className="input-container">
                <input
                    className="input"
                    placeholder="Enter password"
                    type="password"
                    value={loginData.Password}
                    onChange={(e) =>
                        setLoginData({ ...loginData, Password: e.target.value })
                    }
                />
            </div>
            <button type="submit" className="submit" onClick={handleLogin}>
                Log In
            </button>

            <p className="signup-link">
                No account?
                <Link to="/registration">Sign up</Link>
            </p>
        </form>
    );
};

export default LoginPage;
