import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const CommunityDetails = () => {
    const { communityId } = useParams();
    const [community, setCommunity] = useState(null);
    const [collections, setCollections] = useState([]);
    const [message, setMessage] = useState(null); 
    const [messageType, setMessageType] = useState(''); 
    const navigate = useNavigate();

    useEffect(() => {
        axios.get(`https://localhost:7200/api/Community/getCommunity/${communityId}`)
            .then(response => {
                setCommunity(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the community!', error);
            });

        axios.get(`https://localhost:7200/api/Collection/getListOfCollectionByCommunityId/${communityId}`)
            .then(response => {
                setCollections(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the collections!', error);
            });
    }, [communityId]);

    const handleDelete = () => {
        if (window.confirm('Are you sure you want to delete this community? This action cannot be undone.')) {
            axios.delete(`https://localhost:7200/api/Community/DeleteCommunity/${communityId}`)
                .then(response => {
                    setMessage('Community deleted successfully!');
                    setMessageType('success'); 
                    setTimeout(() => navigate('/Dspace/Communities/ListOfCommunities'), 2000); 
                })
                .catch(error => {
                    console.error('There was an error deleting the community!', error);
                    setMessage('Community has collections or sub-communities. Please delete all collections or sub-communities in the community before deleting it.');
                    setMessageType('danger'); 
                });
        }
    };

    return (
        <div>
            <Header />
            
            <div className="container mt-5">
                <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Community Detail</strong></span>
                </div>
            
                {message && <div className={`alert alert-${messageType}`}>{message}</div>} 
                <h1 className="mb-4">Community Details</h1>
                <div className="card">
                    <div className="card-body">
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Name:</div>
                            <div className="col-sm-6 ">{community?.communityName}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Short Description:</div>
                            <div className="col-sm-6 ">{community?.shortDescription}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Created By:</div>
                            <div className="col-sm-6 ">{community?.createBy}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Updated By:</div>
                            <div className="col-sm-6">{community?.updateBy}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Create Time:</div>
                            <div className="col-sm-6 ">{new Date(community?.createTime).toLocaleString()}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Update Time:</div>
                            <div className="col-sm-6 ">{new Date(community?.updateTime).toLocaleString()}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Logo:</div>
                            <div className="col-sm-6 ">
                                <img src={community?.logoUrl} alt="logo" style={{ width: '100px', height: '100px' }} />
                            </div>
                        </div>
                        <div className="row pb-2 my-3">
                            <div className="col-sm-2 text-end">Active:</div>
                            <div className="col-sm-6"><input type="checkbox" checked={community?.isActive} readOnly /></div>
                        </div>
                    </div>
                </div>
                <div className="mt-4">
                    <button className="btn btn-primary me-2" onClick={() => navigate(`/Dspace/Communities/editCommunity/${community?.communityId}`)}>Edit</button>
                    <button className="btn btn-danger me-2" onClick={handleDelete}>Delete</button>
                    <button className="btn btn-secondary" onClick={() => navigate('/Dspace/Communities/ListOfCommunities')}>Back to List</button>
                </div>

                <div className="mt-5">
                    <h2>Collections</h2>
                    <table className="table table-bordered">
                        <thead>
                            <tr>
                                <th>No</th>
                                <th>Logo</th>
                                <th>Name</th>
                                <th>Short Description</th>
                                <th>Create By</th>
                                <th>Active</th>
                            </tr>
                        </thead>
                        <tbody>
                            {collections.map((collection, index) => (
                                <tr key={collection.collectionId}>
                                    <td>{index + 1}</td> 
                                    <td>
                                        <img src={collection.logo} alt="logo" style={{ width: '50px', height: '50px' }} />
                                    </td>
                                    <td>
                                        <button className="btn btn-link" onClick={() => navigate(`/Dspace/Collection/getCollection/${collection.collectionId}`)}>
                                            {collection.collectionName}
                                        </button>
                                    </td>
                                    <td>{collection.shortDescription}</td>
                                    <td>{collection.createBy}</td>
                                    <td><input type="checkbox" checked={collection.isActive} readOnly /></td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default CommunityDetails;
