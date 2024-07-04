import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const EditCollection = () => {
    const { collectionId } = useParams(); // Get the collection ID from the URL

    const [collectionData, setCollectionData] = useState({
        logoUrl: '',
        communityId: '',
        collectionName: '',
        shortDescription: '',
       
        isActive: true,
    });
    const token = localStorage.getItem('Token');
    const header = `Bearer ${token}`;
    const [communityOptions, setCommunityOptions] = useState([]); // State variable for the list of communities

    const [successMessage, setSuccessMessage] = useState('');

    useEffect(() => {
        // Fetch the collection data from your API using the ID from the URL
        axios.get(`https://localhost:7200/api/Collection/getCollection/${collectionId}`)
            .then(response => {
                setCollectionData(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the collection!', error);
            });

        // Fetch the list of communities
        axios.get('https://localhost:7200/api/Community/getListOfCommunities')
            .then(response => {
                setCommunityOptions(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the list of communities!', error);
            });
    }, [collectionId, successMessage]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCollectionData({
            ...collectionData,
            [name]: value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        // Send data to your API to update the collection
        axios.put(`https://localhost:7200/api/Collection/updateCollection/${collectionId}`, collectionData,{
            headers: { Authorization: header }
        })
            .then(response => {
                console.log('Collection updated successfully!', response.data);
                setSuccessMessage('Collection updated successfully!');
            })
            .catch(error => {
                console.error('There was an error updating the collection!', error);
            });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Edit Collection</strong></span>
                </div>
                <h1 className="mb-4">Edit Collection</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>}
                <form onSubmit={handleSubmit}>
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
                            <label>Parent Community</label>
                            <select
                                className="form-control"
                                name="communityId"
                                value={collectionData.communityId}
                                onChange={handleChange}
                            >
                                <option value="">Select a parent community</option>
                                {communityOptions.map(community => (
                                    <option key={community.communityId} value={community.communityId}>{community.communityName}</option>
                                ))}
                            </select>
                        </div>
                    </div>
                    <div className="col-md-6 mb-3">
                        <div className="form-group">
                            <label>Name</label>
                            <input
                                type="text"
                                className="form-control"
                                name="collectionName"
                                value={collectionData.collectionName}
                                onChange={handleChange}
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
                            />
                        </div>
                    </div>
                    <button type="submit" className="btn btn-primary">Update</button>
                </form>
                <div className="mt-3">
                    <Link to="/Dspace/Collection/ListOfCollection">Back to List</Link>
                </div>
            </div>
        </div>
    );
};

export default EditCollection;
