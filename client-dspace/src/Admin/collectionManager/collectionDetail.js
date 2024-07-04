import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const CollectionDetails = () => {
    const { collectionId } = useParams();
    const [collection, setCollection] = useState(null);
    const [message, setMessage] = useState(null);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        axios.get(`https://localhost:7200/api/Collection/getCollection/${collectionId}`)
            .then(response => {
                setCollection(response.data);
            })
            .catch(error => {
                setError('There was an error fetching the collection!');
                console.error('There was an error fetching the collection!', error);
            });
    }, [collectionId]);

    const handleDelete = () => {
        if (window.confirm('Are you sure you want to delete this collection? This action cannot be undone.')) {
            axios.delete(`https://localhost:7200/api/Collection/DeleteCollection/${collectionId}`)
                .then(response => {
                    setMessage({ text: 'Collection deleted successfully!', type: 'success' });
                    setTimeout(() => navigate('/Dspace/Collection/ListOfCollection'), 2000);
                })
                .catch(error => {
                    console.error('There was an error deleting the collection!', error);
                    setMessage({ text: 'Collection cannot be deleted as it contains items or sub-collections. Please remove them first.', type: 'danger' });
                });
        }
    };

    if (error) {
        return (
            <div>
                <Header />
                <div className="container mt-5">
                    <h1 className="mb-4">Collection Details</h1>
                    <div className="alert alert-danger">{error}</div>
                    <button className="btn btn-secondary" onClick={() => navigate('/Dspace/Collection/ListOfCollection')}>Back to List</button>
                </div>
            </div>
        );
    }

    if (!collection) {
        return (
            <div>
                <Header />
                <div className="container mt-5">
                    <h1 className="mb-4">Collection Details</h1>
                    <p>Loading...</p>
                </div>
            </div>
        );
    }

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Collection Detail</strong></span>
                </div>
                <h1 className="mb-4">Collection Details</h1>
                {message && (
                    <div className={`alert alert-${message.type} text-center`}>{message.text}</div>
                )}
                <div className="card">
                    <div className="card-body">
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Name:</div>
                            <div className="col-sm-6">{collection.collectionName}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Short Description:</div>
                            <div className="col-sm-6">{collection.shortDescription}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Parent Community:</div>
                            <div className="col-sm-6">{collection.communityDTOForSelect?.communityName}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">License:</div>
                            <div className="col-sm-6">{collection.license}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Entity Type Name:</div>
                            <div className="col-sm-6">{collection.entityTypeName}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Logo:</div>
                            <div className="col-sm-6">
                                <img src={collection.logoUrl} alt="logo" style={{ width: '100px', height: '100px' }} />
                            </div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Create Time:</div>
                            <div className="col-sm-6">{new Date(collection.createTime).toLocaleString()}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Update Time:</div>
                            <div className="col-sm-6">{new Date(collection.updateTime).toLocaleString()}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Created By:</div>
                            <div className="col-sm-6">{collection.createBy}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Updated By:</div>
                            <div className="col-sm-6">{collection.updateBy}</div>
                        </div>
                        <div className="row pb-2 my-3">
                            <div className="col-sm-2 text-end">Active:</div>
                            <div className="col-sm-6"><input type="checkbox" checked={collection.isActive} readOnly /></div>
                        </div>
                    </div>
                </div>
                <div className="mt-4">
                    <button className="btn btn-primary me-2" onClick={() => navigate(`/Dspace/Collection/editCollection/${collectionId}`)}>Edit</button>
                    <button className="btn btn-danger me-2" onClick={handleDelete}>Delete</button>
                    <button className="btn btn-secondary" onClick={() => navigate('/Dspace/Collection/ListOfCollection')}>Back to List</button>
                </div>
            </div>
        </div>
    );
};

export default CollectionDetails;
