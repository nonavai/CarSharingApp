import React, {useState} from 'react';
import Navigation from "../Components/Navigation";
import {SearchData} from "../Models/Requests/SearchData";
import {SearchCars} from "../API/api";
import {CarWithImageData} from "../Models/Responses/CarWithImageData";
import CarProfile from "../Components/CarProfile";
import "../Styles/Search.css"
import {Status} from "../Models/Enums/Status";
import {VehicleType} from "../Models/Enums/VehicleType";
import {FuelType} from "../Models/Enums/FuelType";
import {WheelDrive} from "../Models/Enums/WheelDrive";

import {
    CDBSidebar,
    CDBSidebarContent,
    CDBSidebarFooter,
    CDBSidebarHeader,
    CDBSidebarMenu,
    CDBSidebarMenuItem,
} from 'cdbreact';

const CarSearch = () => {

    const [searchCriteria, setSearchCriteria] = useState<SearchData>({
        Color: null,
        Mark: null,
        MaxPrice: null,
        MaxYear: null,
        MinPrice: null,
        MinYear: null,
        Model: null,
        Status: null,
        RadiusKm: null,
        Latitude: null,
        Longitude: null,
        VehicleType: null,
        FuelType: null,
        WheelDrive: null,
        MaxEngineCapacity: null,
        MinEngineCapacity: null,
        CurrentPage: 1,
        PageSize: 10
    }as SearchData);

    const [results, setResults] = useState<CarWithImageData[]>([]);
    const [isSidebarOpen, setIsSidebarOpen] = useState(true); // Состояние для открытия/закрытия sidebar

    // Функция для изменения состояния открытия/закрытия sidebar
    const toggleSidebar = () => {
        setIsSidebarOpen(!isSidebarOpen);
    };

    const handleSearch = async () => {
        const searchResults = await SearchCars(searchCriteria);
        if (searchResults !== null){
            setResults(searchResults);
        }// Update results with the search results
    };

    // @ts-ignore
    return (
        <body>
            <header>
                <Navigation />
            </header>
        <div className="fixed-top" style={{ display: 'flex', height: '100vh', overflow: 'scroll initial', marginTop: '8vh' }}>
            <CDBSidebar
                className="some-class" // Пример className
                breakpoint={1} // Пример breakpoint
                toggled={true} // Пример toggled (может быть true или false)
                minWidth="5rem" // Пример minWidth
                maxWidth="20rem" // Пример maxWidth
                textColor="#fff"
                backgroundColor="#333"
            >
                <CDBSidebarHeader prefix={<i className="fa fa-bars fa-large" onClick={toggleSidebar}></i>}>
                    <a href="/" className="text-decoration-none" style={{ color: 'inherit' }}>
                        Cars
                    </a>
                </CDBSidebarHeader>

                <CDBSidebarFooter>
                    {!isSidebarOpen && (
                        <form>
                            <div>
                                <select
                                    className="form-select"
                                    onChange={(e) => {
                                        const selectedStatus = e.target.value as keyof typeof Status | '';
                                        setSearchCriteria({ ...searchCriteria, Status: selectedStatus !== '' ? Status[selectedStatus] : null })
                                    }}
                                >
                                    <option value="" hidden>Select Status</option>
                                    <option value={Status.free}>Free</option>
                                    <option value={Status.taken}>Taken</option>
                                    <option value={Status.booking}>Booking</option>
                                    <option value={Status.notAvailable}>NotAvailable</option>
                                </select>
                            </div>
                            <div>
                                <select
                                    className="form-select"
                                    onChange={(e) => {
                                        const vehicleType = e.target.value as keyof typeof VehicleType | '';
                                        setSearchCriteria({ ...searchCriteria, VehicleType: vehicleType !== '' ? VehicleType[vehicleType] : null })
                                    }}
                                >
                                    <option value="" hidden>Select Vehicle Type</option>
                                    <option value={VehicleType.hatchBack}>HatchBack</option>
                                    <option value={VehicleType.sedan}>Sedan</option>
                                    <option value={VehicleType.universal}>Universal</option>
                                </select>
                            </div>
                            <div>
                                <select
                                    className="form-select"
                                    onChange={(e) => {
                                        const fuelType = e.target.value as keyof typeof FuelType | '';
                                        setSearchCriteria({ ...searchCriteria, FuelType: fuelType !== '' ? FuelType[fuelType] : null })
                                    }}
                                >
                                    <option value="" hidden>Select Fuel Type</option>
                                    <option value={FuelType.lpg}>LPG</option>
                                    <option value={FuelType.diesel}>Diesel</option>
                                    <option value={FuelType.hybrid}>Hybrid</option>
                                    <option value={FuelType.electric}>Electric</option>
                                    <option value={FuelType.petrol}>Petrol</option>
                                    <option value={FuelType.petrol_lpg}>Petrol_LPG</option>
                                </select>
                            </div>
                            <div>
                                <select
                                    className="form-select"
                                    onChange={(e) => {
                                        const wheelDrive = e.target.value as keyof typeof WheelDrive | '';
                                        setSearchCriteria({ ...searchCriteria, WheelDrive: wheelDrive !== '' ? WheelDrive[wheelDrive] : null })
                                    }}
                                >
                                    <option value="" hidden>Select Wheel Drive</option>
                                    <option value={WheelDrive.AWD}>AWD</option>
                                    <option value={WheelDrive.FWD}>FWD</option>
                                    <option value={WheelDrive.RWD}>RWD</option>
                                    <option value={WheelDrive.FourWD}>FourWD</option>
                                </select>
                            </div>
                            <div>

                                <input
                                    className="form-control"
                                    type="text"
                                    placeholder="Mark"
                                    value={searchCriteria.Mark || ''}
                                    onChange={(e) =>
                                        setSearchCriteria({ ...searchCriteria, Mark: e.target.value || null})
                                    }
                                />
                            </div>
                            <div>

                                <input
                                    className="form-control"
                                    type="text"
                                    placeholder="Model"
                                    value={searchCriteria.Model || ''}
                                    onChange={(e) =>
                                        setSearchCriteria({ ...searchCriteria, Model: e.target.value  || null})
                                    }
                                />
                            </div>
                            <div>

                                <input
                                    className="form-control"
                                    type="number"
                                    placeholder="Min Year"
                                    value={searchCriteria.MinYear ?? ''}
                                    onChange={(e) =>
                                        setSearchCriteria({ ...searchCriteria, MinYear: e.target.value ? +e.target.value : null })
                                    }
                                />
                            </div>
                            <div>

                                <input
                                    className="form-control"
                                    type="number"
                                    placeholder="Max Year"
                                    value={searchCriteria.MaxYear || ''}
                                    onChange={(e) =>
                                        setSearchCriteria({ ...searchCriteria, MaxYear: e.target.value ? +e.target.value : null })
                                    }
                                />
                            </div>
                            <div>

                                <input
                                    className="form-control"
                                    type="number"
                                    placeholder="Min Price"
                                    value={searchCriteria.MinPrice || ''}
                                    onChange={(e) =>
                                        setSearchCriteria({ ...searchCriteria, MinPrice: e.target.value ? +e.target.value : null })
                                    }
                                />
                            </div>
                            <div>

                                <input
                                    className="form-control"
                                    type="number"
                                    placeholder="Max Price"
                                    value={searchCriteria.MaxPrice || ''}
                                    onChange={(e) =>
                                        setSearchCriteria({ ...searchCriteria, MaxPrice: e.target.value ? +e.target.value : null })
                                    }
                                />
                            </div>
                            <div>

                                <input
                                    className="form-control"
                                    type="number"
                                    placeholder="Minimal Engine Capacity"
                                    value={searchCriteria.MinEngineCapacity || ''}
                                    onChange={(e) =>
                                        setSearchCriteria({ ...searchCriteria, MinEngineCapacity: e.target.value ? +e.target.value : null })
                                    }
                                />
                            </div>
                            <div>

                                <input
                                    className="form-control"
                                    type="number"
                                    placeholder="Maximum Engine Capacity"
                                    value={searchCriteria.MaxEngineCapacity || ''}
                                    onChange={(e) =>
                                        setSearchCriteria({ ...searchCriteria, MaxEngineCapacity: e.target.value ? +e.target.value : null })
                                    }
                                />
                            </div>

                            <button type="button" className="btn btn-primary" onClick={handleSearch}>
                                Search
                            </button>
                        </form>
                    )}
                </CDBSidebarFooter>
            </CDBSidebar>
            <div className={`search-results col-lg-8 col-md-12 `}>
                <ul style={{ listStyleType: 'none' }}>
                    {results.map((car, index) => (
                        <li key={index}>
                            <CarProfile {...car} />
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    </body>
    );
};

export default CarSearch;