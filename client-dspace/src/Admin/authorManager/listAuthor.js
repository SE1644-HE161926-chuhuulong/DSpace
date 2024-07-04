import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const ListAuthor = () => {
    const [authors, setAuthors] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [searchType, setSearchType] = useState(''); // State to track the current search type
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get("https://localhost:7200/api/Author/getListOfAuthors");
                setAuthors(response.data);
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

    const handleSearchTypeChange = (e) => {
        setSearchType(e.target.value);
        setSearchQuery(''); // Clear search query when changing search type
    };

    const filteredAuthors = authors.filter(author => {
        if (!searchQuery.trim()) return true; // Return all authors if search query is empty

        const normalizedSearchQuery = searchQuery.replace(/\s+/g, '').toLowerCase();
        switch (searchType) {
            case 'fullName':
                return (
                    `${author.firstName} ${author.lastName}`.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)
                );
            case 'email':
                return author.email.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery);
            case 'jobTitle':
                return author.jobTitle.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery);
            default:
                // Search all fields if searchType is not specified
                return (
                    `${author.firstName} ${author.lastName}`.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery) ||
                    author.email.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery) ||
                    author.jobTitle.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)
                );
        }
    });

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <h1 className="mb-4">Authors List</h1>
                <div className="row mb-4 align-items-center">
                    <div className="col-md-4">
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
                        <label htmlFor="searchTypeSelect" className="form-label fw-bold">Search By</label>
                        <select
                            id="searchTypeSelect"
                            className="form-select"
                            value={searchType}
                            onChange={handleSearchTypeChange}
                        >
                            <option value="">Select...</option>
                            <option value="fullName">Full Name</option>
                            <option value="email">Email</option>
                            <option value="jobTitle">Job Title</option>
                        </select>
                    </div>
                    <div className="col-md-4 d-flex align-items-center justify-content-end">
                        <Link to="/Dspace/Author/AddAuthor" className="btn btn-primary">Create new author</Link>
                    </div>
                </div>
                {error && <div className="alert alert-danger">Error fetching data: {error}</div>}
                <table className="table table-striped table-bordered">
                    <thead className="thead-dark">
                        <tr>
                            <th>No</th>
                            <th>Full Name</th>
                            <th>Email</th>
                            <th>Job Title</th>
                            <th>Birth Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredAuthors.length > 0 ? (
                            filteredAuthors.map((author, index) => (
                                <tr key={author.authorId}>
                                    <td>{index + 1}</td>
                                    <td>
                                        <Link to={`/Dspace/Author/AuthorDetails/${author.authorId}`}>
                                            {author.firstName} {author.lastName}
                                        </Link>
                                    </td>
                                    <td>{author.email}</td>
                                    <td>{author.jobTitle}</td>
                                    <td>{`${author.birthDate}`.split('T')[0]}</td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="5" className="text-center">No authors available</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default ListAuthor;
