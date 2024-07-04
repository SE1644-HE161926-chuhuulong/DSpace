import React, { useState, useEffect } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import { Link } from 'react-router-dom';

const CreateCollection = () => {
    const [collectionData, setCollectionData] = useState({
        logoUrl: '',
        collectionName: '',
        shortDescription: '',
        communityId: '',
        isActive: true,
        license: '',
        entityTypeId: ''
    });

    const [communities, setCommunities] = useState([]);
    const [entityTypes, setEntityTypes] = useState([]);
    const [successMessage, setSuccessMessage] = useState('');
    const token = localStorage.getItem('Token');
    const header = `Bearer ${token}`;

    useEffect(() => {
        // Fetch the list of communities from the API
        axios.get('https://localhost:7200/api/Community/getListOfCommunities')
            .then(response => {
                setCommunities(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the communities!', error);
            });

        // Fetch the list of entity types from the API
        axios.get('https://localhost:7200/api/EntityCollection/listEntityCollection')
            .then(response => {
                setEntityTypes(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the entity types!', error);
            });
    }, []);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setCollectionData({
            ...collectionData,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(collectionData);
        axios.post('https://localhost:7200/api/Collection/createCollection', collectionData, {
            headers: { Authorization: header }
        })
            .then(response => {
                console.log('Collection created successfully!', response.data);
                setSuccessMessage('Collection added successfully!');
                // Redirect to collections list or reset form
            })
            .catch(error => {
                console.error('There was an error creating the collection!', error);
            });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Add Collection</strong></span>
                </div>
                <h1 className="mb-4">Create New Collection</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>}
                <form onSubmit={handleSubmit}>
                    <div className="row">
                        <div className="col-md-6 mb-3">
                            <div className="form-group">
                                <label>Logo URL</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    name="logoUrl"
                                    value={collectionData.logoUrl}
                                    onChange={handleChange}
                                />
                            </div>
                        </div>
                        <div className="col-md-6 mb-3">
                            <div className="form-group">
                                <label>Collection Name</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    name="collectionName"
                                    value={collectionData.collectionName}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                        </div>
                        <div className="col-md-6 mb-3">
                            <div className="form-group">
                                <label>Short Description</label>
                                <textarea
                                    className="form-control"
                                    name="shortDescription"
                                    value={collectionData.shortDescription}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                        </div>
                        <div className="col-md-6 mb-3">
                            <div className="form-group">
                                <label>Community</label>
                                <select
                                    className="form-control"
                                    name="communityId"
                                    value={collectionData.communityId}
                                    onChange={handleChange}
                                    required
                                >
                                    <option value="">Select Community</option>
                                    {communities.map((community) => (
                                        <option key={community.communityId} value={community.communityId}>
                                            {community.communityName}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-md-6 mb-3">
                            <div className="form-group">
                                <label>License</label>
                                <textarea
                                    type="text"
                                    className="form-control"
                                    name="license"
                                    value={collectionData.license}
                                    onChange={handleChange}
                                    required
                                />
                            </div>
                        </div>
                        <div className="col-md-6 mb-3">
                            <div className="form-group form-check">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    name="isActive"
                                    checked={collectionData.isActive}
                                    onChange={handleChange}
                                />
                                <label className="form-check-label">Is Active</label>
                            </div>
                        </div>
                        <div className="col-md-6 mb-3">
                            <div className="form-group">
                                <label>Entity Type</label>
                                <select
                                    className="form-control"
                                    name="entityTypeId"
                                    value={collectionData.entityTypeId}
                                    onChange={handleChange}
                                    required
                                >
                                    <option value="">Select Entity Type</option>
                                    {entityTypes.map((entity) => (
                                        <option key={entity.id} value={entity.id}>
                                            {entity.entityType}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>
                    </div>
                    <button type="submit" className="btn btn-primary">Add</button>
                </form>
                <div className="mt-3">
                    <Link to="/Dspace/Collection/ListOfCollection">Back to List</Link>
                </div>
            </div>
        </div>
    );
};

export default CreateCollection;
