import React, { useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import { Link } from 'react-router-dom';

const Addauthor = () => {
    const [authorData, setAuthorData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        jobTitle: '',
        birthDate: '',
        dateAccessioned: '',
        dateAvailable: '',
        uri: '',
        type: '',
    });
    const [successMessage, setSuccessMessage] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setAuthorData({
            ...authorData,
            [name]: value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        // Send data to your API
        axios.post('https://localhost:7200/api/Author/createAuthor', authorData) // Updated API endpoint
            .then(response => {
                console.log('Author created successfully!', response.data);
                setSuccessMessage('Author added successfully!');
                // Reset form after successful submission
                setAuthorData({
                    firstName: '',
                    lastName: '',
                    email: '',
                    jobTitle: '',
                    birthDate: '',
                    dateAccessioned: '',
                    dateAvailable: '',
                    uri: '',
                    type: '',
                });
            })
            .catch(error => {
                console.error('There was an error creating the author!', error);
            });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <h1>Create New Author</h1>
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
                                        value={authorData.firstName}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                                <div className="col">
                                    <label>Last Name</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="lastName"
                                        value={authorData.lastName}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                            </div>
                            <div className="form-group row w-75">
                                <div className="col">
                                    <label>Email</label>
                                    <input
                                        type="email"
                                        className="form-control"
                                        name="email"
                                        value={authorData.email}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                                <div className="col">
                                    <label>Job Title</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="jobTitle"
                                        value={authorData.jobTitle}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                            </div>
                            <div className="form-group row w-75">
                                <div className="col">
                                    <label>Birth Date</label>
                                    <input
                                        type="date"
                                        className="form-control"
                                        name="birthDate"
                                        value={authorData.birthDate}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                                <div className="col">
                                    <label>Date Accessioned</label>
                                    <input
                                        type="date"
                                        className="form-control"
                                        name="dateAccessioned"
                                        value={authorData.dateAccessioned}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                            </div>
                            <div className="form-group row w-75">
                                <div className="col">
                                    <label>Date Available</label>
                                    <input
                                        type="date"
                                        className="form-control"
                                        name="dateAvailable"
                                        value={authorData.dateAvailable}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                                <div className="col">
                                    <label>URI</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="uri"
                                        value={authorData.uri}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                            </div>
                            <div className="form-group row w-75">
                                <div className="col">
                                    <label>Type</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="type"
                                        value={authorData.type}
                                        onChange={handleChange}
                                        required
                                    />
                                </div>
                            </div>
                            <button type="submit" className="btn btn-primary mt-3">Add</button>
                        </form>
                    </div>
                </div>
                <div className="mt-3">
                    <Link to="/Dspace/Author/ListOfAuthor">Back to List</Link>
                </div>
            </div>
        </div>
    );
};

export default Addauthor;
