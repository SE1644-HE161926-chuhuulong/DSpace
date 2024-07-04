import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../components/Header/Header';
import './homepageStaff.css'
const GroupList = () => {
    const [groups, setGroups] = useState([]);
    const [error, setError] = useState(null);
    const [searchQuery, setSearchQuery] = useState('');
    const [searchCriteria, setSearchCriteria] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 12;
    const token = localStorage.getItem('Token');

    const header = `Bearer ${token}`;
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get("https://localhost:7200/api/GroupStaff/GetGroupByPeopleId", {
                    headers: {
                        Authorization: header
                    }
                });
                setGroups(response.data);
            } catch (error) {
                setError(error.message);
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, []);

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
    };

    const handleSearchCriteriaChange = (e) => {
        setSearchCriteria(e.target.value);
        setSearchQuery('');
    };

    const filteredGroups = groups.filter(group => {
        if (!searchQuery.trim()) return true; // Return all groups if search query is empty

        const normalizedSearchQuery = searchQuery.replace(/\s+/g, '').toLowerCase();
        switch (searchCriteria) {
            case 'title':
                return group.title.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery);
            case 'description':
                return group.description.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery);
            default:
                return (
                    group.title.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery) ||
                    group.description.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)
                );
        }
    });

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentGroups = filteredGroups.slice(indexOfFirstItem, indexOfLastItem);

    const totalPages = Math.ceil(filteredGroups.length / itemsPerPage);

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
                <h1 className="mb-4">Groups List</h1>
                <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Group</strong></span>
                </div>
                <div className="row mb-4">
                    <div className="col-md-5">
                        <label htmlFor="searchInput" className="form-label fw-bold">Search</label>
                        <input
                            id="searchInput"
                            type="text"
                            className="form-control"
                            placeholder="Search"
                            value={searchQuery}
                            onChange={handleSearchChange}
                        />
                    </div>
                    <div className="col-md-4">
                        <label htmlFor="searchCriteria" className="form-label fw-bold">Search By</label>
                        <select
                            id="searchCriteria"
                            className="form-select"
                            value={searchCriteria}
                            onChange={handleSearchCriteriaChange}
                        >
                            <option value="">Select...</option>
                            <option value="title">Title</option>
                            <option value="description">Description</option>
                        </select>
                    </div>
                    {/* <div className="col-md-3 d-flex align-items-end justify-content-end">
                        <Link to="/Dspace/Group/createGroup" className="btn btn-primary">Create new group</Link>
                    </div> */}

                </div>
                {error && <div className="alert alert-danger">Error fetching data: {error}</div>}
                <table className="table table-striped table-bordered">
                    <thead className="thead-dark">
                        <tr>
                            <th className="short-column">No</th>
                            <th className="short-column">Title</th>
                            <th>Description</th>
                            {/* <th>Active</th> */}
                        </tr>
                    </thead>
                    <tbody>
                        {currentGroups.length > 0 ? (
                            currentGroups.map((group, index) => (
                                <tr key={group.groupId}>
                                    <td className="short-column">{index + 1 + (currentPage - 1) * itemsPerPage}</td>
                                    <td className="short-column">
                                        <Link to={`/Dspace/Group/getGroupStaff/${group.groupId}`}>
                                            {group.title}
                                        </Link>
                                    </td>
                                    <td>{group.description}</td>
                                    {/* <td>
                                        <input type="checkbox" checked={group.isActive} readOnly />
                                    </td> */}
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="4" className="text-center">No groups available</td>
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

export default GroupList;
