import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const CommunitiesList = () => {
    const [communities, setCommunities] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [searchType, setSearchType] = useState(''); // State to track the current search type
    const [error, setError] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const communitiesPerPage = 12;

    useEffect(() => {
        // Fetch data from the API
        axios.get('https://localhost:7200/api/Community/getListOfCommunities')
            .then(response => {
                setCommunities(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the communities!', error);
                setError(error.message);
            });
    }, []);

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
        setCurrentPage(1); // Reset to the first page when searching
    };

    const handleSearchTypeChange = (e) => {
        setSearchType(e.target.value);
        setSearchQuery(''); // Clear search query when changing search type
    };

    const filteredCommunities = communities.filter(community => {
        const normalizedSearchQuery = searchQuery.trim().toLowerCase(); // Normalize and trim search query

        if (!normalizedSearchQuery) return true; // Return all communities if search query is empty

        const searchKeywords = normalizedSearchQuery.split(/\s+/); // Split search query by whitespace

        switch (searchType) {
            case 'communityName':
                return searchKeywords.every(keyword =>
                    community.communityName.toLowerCase().includes(keyword)
                );
            case 'shortDescription':
                return searchKeywords.every(keyword =>
                    community.shortDescription.toLowerCase().includes(keyword)
                );
            default:
                return (
                    searchKeywords.some(keyword =>
                        community.communityName.toLowerCase().includes(keyword)
                    ) ||
                    searchKeywords.some(keyword =>
                        community.shortDescription.toLowerCase().includes(keyword)
                    )
                );
        }
    });

    // Pagination logic
    const indexOfLastCommunity = currentPage * communitiesPerPage;
    const indexOfFirstCommunity = indexOfLastCommunity - communitiesPerPage;
    const currentCommunities = filteredCommunities.slice(indexOfFirstCommunity, indexOfLastCommunity);

    const totalPages = Math.ceil(filteredCommunities.length / communitiesPerPage);

    const paginate = (pageNumber) => setCurrentPage(pageNumber);

    const handleNext = () => {
        if (currentPage < totalPages) {
            setCurrentPage(currentPage + 1);
        }
    };

    const handlePrevious = () => {
        if (currentPage > 1) {
            setCurrentPage(currentPage - 1);
        }
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Community</strong></span>
                </div>
                <h1 className="mb-4">Communities List</h1>
                <div className="row mb-4">
                    <div className="col-md-5">
                        <label htmlFor="searchInput" className="form-label fw-bold">Search</label>
                        <input
                            id="searchInput"
                            type="text"
                            className="form-control"
                            placeholder="Search by community name"
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
                            <option value="communityName">Community Name</option>
                            <option value="shortDescription">Short Description</option>
                        </select>
                    </div>
                    <div className="col-md-3 d-flex align-items-end justify-content-end">
                        <Link to="/Dspace/Communities/createCommunities" className="btn btn-primary">Create new community</Link>
                    </div>
                </div>
                {error && <div className="alert alert-danger">{error}</div>}
                <table className="table table-striped table-bordered">
                    <thead className="thead-dark">
                        <tr>
                            <th>No</th>
                            <th>Logo</th>
                            <th>Name</th>
                            <th>Short Description</th>
                            <th>Active</th>
                        </tr>
                    </thead>
                    <tbody>
                        {currentCommunities.map((community, index) => (
                            <tr key={community.communityId}>
                                <td>{indexOfFirstCommunity + index + 1}</td>
                                <td><img src={community.logo} alt="logo" style={{ width: '50px', height: '50px' }} /></td>
                                <td>
                                    <Link to={`/Dspace/Communities/getCommunity/${community.communityId}`}>
                                        {community.communityName}
                                    </Link>
                                </td>
                                <td>{community.shortDescription}</td>
                                <td>
                                    <input type="checkbox" checked={community.isActive} readOnly />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <nav>
                    <ul className="pagination justify-content-center">
                        <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
                            <button className="page-link" onClick={handlePrevious}>
                                Previous
                            </button>
                        </li>
                        {Array.from({ length: totalPages }, (_, index) => (
                            <li key={index + 1} className={`page-item ${index + 1 === currentPage ? 'active' : ''}`}>
                                <button className="page-link" onClick={() => paginate(index + 1)}>
                                    {index + 1}
                                </button>
                            </li>
                        ))}
                        <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
                            <button className="page-link" onClick={handleNext}>
                                Next
                            </button>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
    );
};

export default CommunitiesList;
