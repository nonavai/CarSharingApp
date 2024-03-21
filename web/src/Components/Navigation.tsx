
import React from 'react';
import ProfileButton from "./ProfileButton";
import 'bootstrap/dist/css/bootstrap.css';

const Navigation: React.FC = () => {
    return (

        <div className="fixed-top">
            <nav className="navbar bg-body-tertiary">
                <div className="d-flex flex-row">
                    <a className="navbar-brand" href="/">Search</a>
                    <ul className="navbar-nav d-flex flex-row gap-3 ">
                        <li className="nav-item">
                            <a className="nav-link active" aria-current="page" href="/">Home</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" aria-current="page" href="/">Link</a>
                        </li>
                    </ul>
                </div>
                <div className ="nav">
                    <ProfileButton></ProfileButton>
                </div>
            </nav>
        </div>
    );
};

export default Navigation;