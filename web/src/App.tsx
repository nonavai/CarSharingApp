import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import LoginPage from "./Pages/LoginPage";
import RegistrationPage from "./Pages/RegistrationPage";
import SearchPage from "./Pages/SearchPage";
import CarPage from "./Pages/CarPage";
import DealPage from "./Pages/DealPage";
import UserPage from "./Pages/UserPage";

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/registration" element={<RegistrationPage />} />
          <Route path="/" element={<SearchPage />}/>
          <Route path="/car-profile/:id" element={<CarPage />} />
          <Route path="/deal/:id" element={<DealPage />} />
          <Route path="/profile/:id" element={<UserPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
