import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const EditCommunity = () => {
    const { communityId } = useParams(); // Get the community ID from the URL

    const [communityData, setCommunityData] = useState({
        logoUrl: '',
        communityName: '',
        shortDescription: '',
        isActive: true,
    });
    const token = localStorage.getItem('Token');
    const header = `Bearer ${token}`;
    const [successMessage, setSuccessMessage] = useState('');

    useEffect(() => {
        // Fetch the community data from your API using the id from the URL
        axios.get(`https://localhost:7200/api/Community/getCommunity/${communityId}`)
            .then(response => {
                const data = response.data;
                setCommunityData({
                    logoUrl: data.logoUrl || '',
                    communityName: data.communityName || '',
                    shortDescription: data.shortDescription || '',
                    createTime: data.createTime || '',
                    updateTime: data.updateTime || '',
                    createBy: data.createBy || '',
                    updateBy: data.updateBy || '',
                    isActive: data.isActive,
                });
            })
            .catch(error => {
                console.error('There was an error fetching the community!', error);
            });
    }, [communityId]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setCommunityData({
            ...communityData,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        // Send data to your API to update the community
        axios.put(`https://localhost:7200/api/Community/updateCommunity/${communityId}`, communityData, {
            headers: { Authorization: header }
        })
            .then(response => {
                console.log('Community updated successfully!', response.data);
                setSuccessMessage('Community updated successfully!');
            })
            .catch(error => {
                console.error('There was an error updating the community!', error);
            });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Edit Community</strong></span>
                </div>
                <h1 className="mb-4 text-center">Edit Community</h1>
                {successMessage && <div className="alert alert-success text-center">{successMessage}</div>}
                <div className="row justify-content-center">
                    <div className="col-md-11">
                        <form onSubmit={handleSubmit}>
                            <div className="row">
                                <div className="col-md-5">
                                    <div className="form-group mb-3">
                                        <label>Logo URL</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="logoUrl"
                                            value={communityData.logoUrl}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Name</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="communityName"
                                            value={communityData.communityName}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Short Description</label>
                                        <textarea
                                            className="form-control"
                                            name="shortDescription"
                                            value={communityData.shortDescription}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Active</label>
                                        <input
                                            type="checkbox"
                                            className="form-check-input ms-2"
                                            name="isActive"
                                            checked={communityData.isActive}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <button type="submit" className="btn btn-primary mt-4">Update</button>
                                </div>
                            </div>
                            <div className="d-flex justify-content-between mt-4">
                                <Link to="/Dspace/Communities/ListOfCommunities">Back to List</Link>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default EditCommunity;
