import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const MetadataDetails = () => {
    const { metadataId } = useParams(); // Get the ID from the URL
    const [metadata, setMetadata] = useState(null);
    const [message, setMessage] = useState(null); // New state variable for messages
    const [messageType, setMessageType] = useState(''); // New state variable for message type
    const navigate = useNavigate();

    useEffect(() => {
        // Fetch the metadata from your API using the id from the URL
        axios.get(`https://localhost:7200/api/Metadata/metadata/${metadataId}`) // Replace with your actual API endpoint
            .then(response => {
                setMetadata(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the metadata!', error);
            });
    }, [metadataId]);

    // Conditionally render the JSX based on whether metadata is null or not
    if (!metadata) {
        return (
            <div>
                <Header />
                <div className="container mt-5">
                    <h1 className="mb-4">Metadata Details</h1>
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
                    <span className="text-dark" ><strong>| Metadata Detail</strong></span>
                </div>
                <h1 className="mb-4">Metadata Details</h1>
                {message && <div className={`alert alert-${messageType}`}>{message}</div>}
                <div className="card">
                    <div className="card-body">
                        
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Coverage:</div>
                            <div className="col-sm-6">{metadata.coverage}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Contributor:</div>
                            <div className="col-sm-6">{metadata.contributor}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Date:</div>
                            <div className="col-sm-6">{metadata.date}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Description:</div>
                            <div className="col-sm-6">{metadata.description}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Creator:</div>
                            <div className="col-sm-6">{metadata.creator}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Format:</div>
                            <div className="col-sm-6">{metadata.format}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Type:</div>
                            <div className="col-sm-6">{metadata.type}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Publisher:</div>
                            <div className="col-sm-6">{metadata.publisher}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Identifier:</div>
                            <div className="col-sm-6">{metadata.identifier}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Relation:</div>
                            <div className="col-sm-6">{metadata.relation}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Rights:</div>
                            <div className="col-sm-6">{metadata.rights}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Language:</div>
                            <div className="col-sm-6">{metadata.language}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Source:</div>
                            <div className="col-sm-6">{metadata.source}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Subject:</div>
                            <div className="col-sm-6">{metadata.subject}</div>
                        </div>
                        <div className="row pb-2 my-3">
                            <div className="col-sm-2 text-end">Title:</div>
                            <div className="col-sm-6">{metadata.title}</div>
                        </div>
                    </div>
                </div>
                <div className="mt-4">
                <button 
                        onClick={() => navigate(`/Dspace/Metadata/editMetadata/${metadata.metadataId}`)} 
                        className="btn btn-primary me-2"
                    >
                        Edit
                    </button>
                    <button 
                        onClick={() => navigate('/Dspace/Metadata/ListOfMetadata')} 
                        className="btn btn-secondary"
                    >
                        Back to List
                    </button>
                </div>
            </div>
        </div>
    );
};

export default MetadataDetails;
