import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link } from 'react-router-dom';
import Header from '../../components/Header/Header';

const CreateCommunity = () => {
    const [selectedValue, setSelectedValue] = useState('');
    const [communityData, setCommunityData] = useState({
        logoUrl: '',
        communityName: '',
        shortDescription: '',
        isActive: true,
        parentCommunityId: null,
    });
    const token = localStorage.getItem('Token');
    const header = `Bearer ${token}`;

    const [parentCommunities, setParentCommunities] = useState([]); // State for parent communities
    const [successMessage, setSuccessMessage] = useState(''); // State for success message

    useEffect(() => {
        // Fetch the list of parent communities from the API
        axios.get('https://localhost:7200/api/Community/getListOfCommunities')
            .then(response => {
                setParentCommunities(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the parent communities!', error);
            });
    }, []);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        if (name === 'parentCommunityId') {
            setSelectedValue(newValue);
            setCommunityData({
                ...communityData,
                parentCommunityId: newValue === '' ? null : newValue,
            });
        } else {
            setCommunityData({
                ...communityData,
                [name]: newValue,
            });
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        // Send data to your API
        axios.post('https://localhost:7200/api/Community/createCommunity', communityData, {
            headers: { Authorization: header }
        })
        .then(response => {
            console.log('Community created successfully!', response.data);
            setSuccessMessage('Community created successfully!');
            // Redirect to the communities list after 2 seconds
            setTimeout(() => {
                window.location.href = '/Dspace/Communities/ListOfCommunities';
            }, 2000);
        })
        .catch(error => {
            console.error('There was an error creating the community!', error);
        });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Add Community</strong></span>
                </div>
                <h1 className="mb-4 text-center">Create New Community</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>} {/* Success message */}
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
                                            required
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
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Short Description</label>
                                        <textarea
                                            className="form-control"
                                            name="shortDescription"
                                            value={communityData.shortDescription}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Parent Community</label>
                                        <select
                                            className="form-control"
                                            name="parentCommunityId"
                                            value={selectedValue}
                                            onChange={handleChange}
                                        >
                                            <option value=''>Select Parent Community</option>
                                            {parentCommunities.map(parent => (
                                                <option key={parent.communityId} value={parent.communityId}>
                                                    {parent.communityName}
                                                </option>
                                            ))}
                                        </select>
                                    </div>
                                    <button type="submit" className="btn btn-primary mt-4">Add</button>
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

export default CreateCommunity;
