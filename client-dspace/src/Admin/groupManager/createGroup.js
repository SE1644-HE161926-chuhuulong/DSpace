import React, { useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import { Link } from 'react-router-dom'; // Assuming you're using react-router-dom

const CreateGroup = () => {
    const [groupData, setGroupData] = useState({
        title: '',
        description: '',
        isActive: true,
    });
    const [successMessage, setSuccessMessage] = useState('');

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setGroupData({
            ...groupData,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        // Send data to your API
        axios.post('https://localhost:7200/api/Group/createGroup', groupData) // Updated API endpoint
            .then(response => {
                console.log('Group created successfully!', response.data);
                setSuccessMessage('Group added successfully!');
                // Reset form after successful submission
                setGroupData({
                    title: '',
                    description: '',
                    isActive: true,
                });
            })
            .catch(error => {
                console.error('There was an error creating the group!', error);
            });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Add Group</strong></span>
                </div>
                <h1 className="mb-4">Create New Group</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>}
                <form onSubmit={handleSubmit}>
                    <div className="col-md-6 mb-3">
                        <div className="form-group">
                            <label>Title</label>
                            <input
                                type="text"
                                className="form-control"
                                name="title"
                                value={groupData.title}
                                onChange={handleChange}
                                required
                            />
                        </div>
                    </div>
                    <div className="col-md-6 mb-3">
                        <div className="form-group">
                            <label>Description</label>
                            <textarea
                                className="form-control"
                                name="description"
                                value={groupData.description}
                                onChange={handleChange}
                                required
                            />
                        </div>
                    </div>
                    <div className="form-group form-check mb-3">
                        <input
                            type="checkbox"
                            className="form-check-input"
                            name="active"
                            checked={groupData.isActive}
                            onChange={handleChange}
                        />
                        <label className="form-check-label">Is Active</label>
                    </div>
                    <button type="submit" className="btn btn-primary">Add</button>
                </form>
                <div className="mt-3">
                    <Link to="/Dspace/Group/ListOfGroup">Back to List</Link>
                </div>
            </div>
        </div>
    );
};

export default CreateGroup;
