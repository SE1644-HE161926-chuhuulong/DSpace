import React, { useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import { Link } from 'react-router-dom';

const AddUser = () => {
    const [userData, setUserData] = useState({
        firstName: '',
        lastName: '',
        address: '',
        phoneNumber: '',
        email: '',
        rolename: 'ADMIN', // Default selection
    });

    const token = localStorage.getItem('Token');
    const header = `Bearer ${token}`;
    const [successMessage, setSuccessMessage] = useState('');
    
    const handleChange = (e) => {
        const { name, value } = e.target;
        setUserData({
            ...userData,
            [name]: value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        // Send data to your API
        axios.post('https://localhost:7200/api/People/createPeople', userData,{
            headers: { Authorization: header }
        })
            .then(response => {
                console.log('User created successfully!', response.data);
                setSuccessMessage('User added successfully!');
                // Reset form after successful submission
                setUserData({
                    firstName: '',
                    lastName: '',
                    address: '',
                    phoneNumber: '',
                    email: '',
                    rolename: 'ADMIN', // Reset to default
                });
            })
            .catch(error => {
                console.error('There was an error creating the user!', error);
            });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Add User</strong></span>
                </div>
                <h1 className="mb-4">Create New User</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>}
                <div className="row">
                    <div className="col-md-12">
                        <form onSubmit={handleSubmit}>
                            <div className="form-group row w-75">
                                <div className="col">
                                    <label>First Name</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="firstName"
                                        value={userData.firstName}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                                <div className="col ">
                                    <label>Last Name</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="lastName"
                                        value={userData.lastName}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                            </div>
                            <div className="form-group row w-75">
                                <div className="col ">
                                    <label>Address</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="address"
                                        value={userData.address}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                                <div className="col ">
                                    <label>Phone Number</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="phoneNumber"
                                        value={userData.phoneNumber}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                            </div>
                            <div className="form-group row w-75">
                                <div className="col ">
                                    <label>Email</label>
                                    <input
                                        type="email"
                                        className="form-control "
                                        name="email"
                                        value={userData.email}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                                <div className="col">
                                    <label>Role</label>
                                    <select
                                        className="form-control"
                                        name="rolename"
                                        value={userData.rolename}
                                        onChange={handleChange}
                                        required
                                    >
                                        <option value="ADMIN">ADMIN</option>
                                        <option value="STAFF">STAFF</option>
                                    </select>
                                </div>
                            </div>
                            <button type="submit" className="btn btn-primary mt-3">Add</button>
                        </form>
                    </div>
                </div>
                <div className="mt-3">
                    <Link to="/Dspace/User/ListOfUsers">Back to List</Link>
                </div>
            </div>
        </div>
    );
};

export default AddUser;