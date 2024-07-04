import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const ListUser = () => {
    const [users, setUsers] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [searchType, setSearchType] = useState(''); // State to track the current search type
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get("https://localhost:7200/api/People/getListOfPeople");
                setUsers(response.data);
            } catch (error) {
                setError(error.message);
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, []);

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
    };

    const handleSearchTypeChange = (e) => {
        setSearchType(e.target.value);
        setSearchQuery(''); // Clear search query when changing search type
    };

    const filteredUsers = users.filter(user => {
        if (!searchQuery.trim()) return true; // Return all users if search query is empty

        const normalizedSearchQuery = searchQuery.replace(/\s+/g, '').toLowerCase();
        switch (searchType) {
            case 'fullName':
                return (
                    (user.firstName && user.firstName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.lastName && user.lastName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery))
                );
            case 'address':
                return (user.address && user.address.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery));
            case 'phoneNumber':
                return (user.phoneNumber && user.phoneNumber.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery));
            case 'email':
                return (user.email && user.email.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery));
            default:
                // Search all fields if searchType is not specified
                return (
                    (user.firstName && user.firstName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.lastName && user.lastName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.address && user.address.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.phoneNumber && user.phoneNumber.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.email && user.email.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery))
                );
        }
    });

    return (
        <div>
            <Header />
            <div className="container mt-4 d-flex flex-column align-self-end">
            <div className=" d-flex align-self-start py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| People</strong></span>
            </div>
            <div className="d-flex align-self-start">
            <h1 className="mb-4">Users List</h1>
            </div>
                <div className="d-flex flex-row align-items-end justify-content-between mb-4 w-100">
                    <div className="w-50">
                        <label htmlFor="searchInput" className="form-label fw-bold">Search</label>
                        <input
                            id="searchInput"
                            type="text"
                            className="form-control"
                            placeholder="Search"
                            value={searchQuery}
                            onChange={handleSearchChange}
                        />
                    </div>
                    <div className="w-25">
                        <label htmlFor="searchTypeSelect" className="form-label fw-bold">Search By</label>
                        <select
                            id="searchTypeSelect"
                            className="form-select"
                            value={searchType}
                            onChange={handleSearchTypeChange}
                        >
                            <option value="">Select...</option>
                            <option value="fullName">Full Name</option>
                            <option value="address">Address</option>
                            <option value="phoneNumber">Phone Number</option>
                            <option value="email">Email</option>
                        </select>
                    </div>
                    <Link to="/Dspace/User/Adduser" className="btn btn-primary">Create new User</Link>
                </div>
                {error && <div className="alert alert-danger">Error fetching data: {error}</div>}
                <table className="table table-striped table-bordered">
                    <thead className="thead-dark">
                        <tr>
                            <th>No</th>
                            <th>Full Name</th>
                            <th>Address</th>
                            <th>Phone Number</th>
                            <th>Email</th>
                            <th>Role</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredUsers.length > 0 ? (
                            filteredUsers.map((user, index) => (
                                <tr key={user.peopleId}>
                                    <td>{index + 1}</td>
                                    <td>
                                        <Link to={`/Dspace/User/UserDetails/${user.peopleId}`}>
                                            {user.firstName} {user.lastName}
                                        </Link>
                                    </td>
                                    <td>{user.address}</td>
                                    <td>{user.phoneNumber}</td>
                                    <td>{user.email}</td>
                                    <td>{user.role}</td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="6" className="text-center">No users available</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default ListUser;
