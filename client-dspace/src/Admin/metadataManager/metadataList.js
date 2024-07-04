import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const MetadataList = () => {
    const [metadata, setMetadata] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [searchType, setSearchType] = useState(''); // State to track the current search type

    useEffect(() => {
        // Fetch data from the API
        axios.get('https://localhost:7200/api/Metadata/listMetadata')
            .then(response => {
                setMetadata(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the metadata!', error);
            });
    }, []);

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
    };

    const handleSearchTypeChange = (e) => {
        setSearchType(e.target.value);
        setSearchQuery(''); // Clear search query when changing search type
    };

    const filteredMetadata = metadata.filter(item => {
        const normalizedSearchQuery = searchQuery.trim().toLowerCase();

        if (!normalizedSearchQuery) return true; // Return all metadata if search query is empty

        switch (searchType) {
            case 'coverage':
                return item.coverage.toLowerCase().includes(normalizedSearchQuery);
            case 'contributor':
                return item.contributor.toLowerCase().includes(normalizedSearchQuery);
            case 'description':
                return item.description.toLowerCase().includes(normalizedSearchQuery);
            case 'format':
                return item.format.toLowerCase().includes(normalizedSearchQuery);
            case 'type':
                return item.type.toLowerCase().includes(normalizedSearchQuery);
            case 'publisher':
                return item.publisher.toLowerCase().includes(normalizedSearchQuery);
            case 'identifier':
                return item.identifier.toLowerCase().includes(normalizedSearchQuery);
            case 'relation':
                return item.relation.toLowerCase().includes(normalizedSearchQuery);
            case 'rights':
                return item.rights.toLowerCase().includes(normalizedSearchQuery);
            case 'language':
                return item.language.toLowerCase().includes(normalizedSearchQuery);
            case 'source':
                return item.source.toLowerCase().includes(normalizedSearchQuery);
            case 'subject':
                return item.subject.toLowerCase().includes(normalizedSearchQuery);
            case 'title':
                return item.title.toLowerCase().includes(normalizedSearchQuery);
            default:
                return (
                    item.coverage.toLowerCase().includes(normalizedSearchQuery) ||
                    item.contributor.toLowerCase().includes(normalizedSearchQuery) ||
                    item.description.toLowerCase().includes(normalizedSearchQuery) ||
                    item.format.toLowerCase().includes(normalizedSearchQuery) ||
                    item.type.toLowerCase().includes(normalizedSearchQuery) ||
                    item.publisher.toLowerCase().includes(normalizedSearchQuery) ||
                    item.identifier.toLowerCase().includes(normalizedSearchQuery) ||
                    item.relation.toLowerCase().includes(normalizedSearchQuery) ||
                    item.rights.toLowerCase().includes(normalizedSearchQuery) ||
                    item.language.toLowerCase().includes(normalizedSearchQuery) ||
                    item.source.toLowerCase().includes(normalizedSearchQuery) ||
                    item.subject.toLowerCase().includes(normalizedSearchQuery) ||
                    item.title.toLowerCase().includes(normalizedSearchQuery)
                );
        }
    });

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Metadata</strong></span>
                </div>
                <h1 className="mb-4">Metadata List</h1>
                <div className="row mb-4">
                    <div className="col-md-5">
                        <label htmlFor="searchInput" className="form-label fw-bold">Search</label>
                        <input
                            id="searchInput"
                            type="text"
                            className="form-control"
                            placeholder="Search metadata..."
                            value={searchQuery}
                            onChange={handleSearchChange}
                        />
                    </div>
                    <div className="col-md-4">
                        <label htmlFor="searchTypeSelect" className="form-label fw-bold">Search By</label>
                        <select
                            id="searchTypeSelect"
                            className="form-select"
                            value={searchType}
                            onChange={handleSearchTypeChange}
                        >
                            <option value="">Select...</option>
                            <option value="coverage">Coverage</option>
                            <option value="contributor">Contributor</option>
                            <option value="description">Description</option>
                            <option value="format">Format</option>
                            <option value="type">Type</option>
                            <option value="publisher">Publisher</option>
                            <option value="identifier">Identifier</option>
                            <option value="relation">Relation</option>
                            <option value="rights">Rights</option>
                            <option value="language">Language</option>
                            <option value="source">Source</option>
                            <option value="subject">Subject</option>
                            <option value="title">Title</option>
                        </select>
                    </div>
                    <div className="col-md-3 d-flex align-items-end justify-content-end">
                        <Link to="/Dspace/Metadata/createMetadata" className="btn btn-primary">Create new metadata</Link>
                    </div>
                </div>
                <table className="table table-striped table-bordered">
                    <thead className="thead-dark">
                        <tr>
                            <th>No</th>
                            <th>Coverage</th>
                            <th>Contributor</th>
                            <th>Description</th>
                            <th>Format</th>
                            <th>Type</th>
                            <th>Publisher</th>
                            <th>Identifier</th>
                            <th>Relation</th>
                            <th>Rights</th>
                            <th>Language</th>
                            <th>Source</th>
                            <th>Subject</th>
                            <th>Title</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredMetadata.map((item, index) => (
                            <tr key={item.metadataId}>
                                <td>{index + 1}</td>
                                <td>{item.coverage}</td>
                                <td>{item.contributor}</td>
                                <td>{item.description}</td>
                                <td>{item.format}</td>
                                <td>{item.type}</td>
                                <td>{item.publisher}</td>
                                <td>{item.identifier}</td>
                                <td>{item.relation}</td>
                                <td>{item.rights}</td>
                                <td>{item.language}</td>
                                <td>{item.source}</td>
                                <td>{item.subject}</td>
                                <td>{item.title}</td>
                                <td>
                                    <Link to={`/Dspace/Metadata/getMetadata/${item.metadataId}`} >
                                        Details
                                    </Link>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default MetadataList;
