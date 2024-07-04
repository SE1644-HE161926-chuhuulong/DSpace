import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const CollectionList = () => {
    const [collections, setCollections] = useState([]);
    const [error, setError] = useState(null);
    const [searchQuery, setSearchQuery] = useState('');
    const [searchType, setSearchType] = useState('collectionName'); // State to track the current search type
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 12;

    useEffect(() => {
        // Fetch data from the API
        axios.get('https://localhost:7200/api/Collection/getListOfCollections')
            .then(response => {
                setCollections(response.data);
            })
            .catch(error => {
                setError(error.message);
            });
    }, []);

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
        setCurrentPage(1); // Reset to first page on new search
    };

    const handleSearchTypeChange = (e) => {
        setSearchType(e.target.value);
        setSearchQuery(''); // Clear search query when changing search type
    };

    const filteredCollections = collections.filter(collection => {
        if (!searchQuery.trim()) return true; 
        const normalizedSearchQuery = searchQuery.replace(/\s+/g, '').toLowerCase(); // Normalize search query

        switch (searchType) {
            case 'collectionName':
                return collection.collectionName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery);
            case 'parentCommunity':
                return collection.communityDTOForSelect.communityName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery);
            case 'description':
                return collection.shortDescription.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery);
            case 'all':
                return Object.values(collection).some(value =>
                    typeof value === 'string' && value.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)
                );
            default:
                return false;
        }
    });

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentCollections = filteredCollections.slice(indexOfFirstItem, indexOfLastItem);

    const totalPages = Math.ceil(filteredCollections.length / itemsPerPage);

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    const handleNextPage = () => {
        if (currentPage < totalPages) {
            setCurrentPage(prevPage => prevPage + 1);
        }
    };

    const handlePreviousPage = () => {
        if (currentPage > 1) {
            setCurrentPage(prevPage => prevPage - 1);
        }
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Collection</strong></span>
                </div>
                <h1 className="mb-4">Collections List</h1>
                <div className="row mb-4">
                    <div className="col-md-5">
                        <label htmlFor="searchInput" className="form-label fw-bold">Search</label>
                        <input
                            id="searchInput"
                            type="text"
                            className="form-control"
                            placeholder="Search collections..."
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
                            <option value="all">Select...</option> {/* Option for searching by all fields */}
                            <option value="collectionName">Collection Name</option>
                            <option value="parentCommunity">Parent Community</option>
                            <option value="description">Description</option> {/* New option for searching by description */}
                        </select>
                    </div>
                    <div className="col-md-3 d-flex align-items-end justify-content-end">
                        <Link to="/Dspace/Collection/createCollection" className="btn btn-primary">Create new collection</Link>
                    </div>
                </div>
                {error && <div className="alert alert-danger">{error}</div>}
                <table className="table table-striped table-bordered">
                    <thead className="thead-dark">
                        <tr>
                            <th>Id</th>
                            <th>Logo</th>
                            <th>Parent Community</th>
                            <th>Name</th>
                            <th>Short Description</th>
                            <th>CreateByOrUpdateBy</th>
                            <th>Active</th>
                        </tr>
                    </thead>
                    <tbody>
                        {currentCollections.map((collection, index) => (
                            <tr key={collection.collectionId}>
                                <td>{index + 1 + (currentPage - 1) * itemsPerPage}</td>
                                <td><img src={collection.logoUrl} alt="logo" style={{ width: '50px', height: '50px' }} /></td>
                                <td></td>
                                <td>
                                    <Link to={`/Dspace/Collection/getCollection/${collection.collectionId}`}>
                                        {collection.collectionName}
                                    </Link>
                                </td>
                                <td>{collection.shortDescription}</td>
                                <td>{collection.createBy || collection.updateBy}</td>
                                <td>
                                    <input type="checkbox" checked={collection.isActive} readOnly />
                                </td>
                            </tr>
                        ))}
                        {currentCollections.length === 0 && (
                            <tr>
                                <td colSpan="7" className="text-center">No collections found</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <div className="d-flex justify-content-center">
                    <nav>
                        <ul className="pagination">
                            <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
                                <button className="page-link" onClick={handlePreviousPage}>Previous</button>
                            </li>
                            {[...Array(totalPages)].map((_, i) => (
                                <li key={i} className={`page-item ${currentPage === i + 1 ? 'active' : ''}`}>
                                    <button className="page-link" onClick={() => handlePageChange(i + 1)}>
                                        {i + 1}
                                    </button>
                                </li>
                            ))}
                            <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
                                <button className="page-link" onClick={handleNextPage}>Next</button>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </div>
    );
};

export default CollectionList;
