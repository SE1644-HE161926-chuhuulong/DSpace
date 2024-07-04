import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const EditMetadata = () => {
    const { metadataId } = useParams();
    const [metadataData, setMetadataData] = useState({
        coverage: '',
        contributor: '',
        date: '',
        description: '',
        format: '',
        type: '',
        publisher: '',
        identifier: '',
        relation: '',
        rights: '',
        language: '',
        source: '',
        subject: '',
        title: '',
    });
    const [successMessage, setSuccessMessage] = useState('');

    useEffect(() => {
        // Fetch the metadata data from your API using the id from the URL
        axios.get(`https://localhost:7200/api/Metadata/metadata/${metadataId}`)
            .then(response => {
                setMetadataData(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the metadata!', error);
            });
    }, [metadataId]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setMetadataData({
            ...metadataData,
            [name]: value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        // Send data to your API
        axios.put(`https://localhost:7200/api/Metadata/updateMetadata/${metadataId}`, metadataData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
            .then(response => {
                console.log('Metadata updated successfully!', response.data);
                setSuccessMessage('Metadata updated successfully!');
            })
            .catch(error => {
                console.error('There was an error updating the metadata!', error);
            });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <h1 className="mb-4 text-center">Edit Metadata</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>}
                <div className="row justify-content-center">
                    <div className="col-md-11">
                        <form onSubmit={handleSubmit}>
                            <div className="row">
                                <div className="col-md-5">
                                    <div className="form-group mb-3">
                                        <label>Coverage</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="coverage"
                                            value={metadataData.coverage}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Contributor</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="contributor"
                                            value={metadataData.contributor}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Date</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="date"
                                            value={metadataData.date}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Description</label>
                                        <textarea
                                            className="form-control"
                                            name="description"
                                            value={metadataData.description}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Format</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="format"
                                            value={metadataData.format}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Type</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="type"
                                            value={metadataData.type}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Publisher</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="publisher"
                                            value={metadataData.publisher}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <button type="submit" className="btn btn-primary mt-4">Update</button>
                                </div>
                                <div className="col-md-5 offset-md-1">
                                    <div className="form-group mb-3">
                                        <label>Identifier</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="identifier"
                                            value={metadataData.identifier}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Relation</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="relation"
                                            value={metadataData.relation}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Rights</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="rights"
                                            value={metadataData.rights}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Language</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="language"
                                            value={metadataData.language}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Source</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="source"
                                            value={metadataData.source}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Subject</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="subject"
                                            value={metadataData.subject}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Title</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="title"
                                            value={metadataData.title}
                                            onChange={handleChange}
                                        />
                                    </div>
                                </div>
                            </div>
                            <div className="d-flex justify-content-between mt-4">
                                <Link to="/Dspace/Metadata/ListOfMetadata">Back to List</Link>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default EditMetadata;
