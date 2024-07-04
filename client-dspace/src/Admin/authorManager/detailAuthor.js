import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const AuthorDetails = () => {
    const { authorId } = useParams();
    const [author, setAuthor] = useState(null);
    const [message, setMessage] = useState(null);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchAuthorDetails = async () => {
            try {
                const response = await axios.get(`https://localhost:7200/api/Author/getAuthor/${authorId}`);
                setAuthor(response.data);
            } catch (error) {
                setError('There was an error fetching the author details!');
                console.error('Error fetching author details:', error);
            }
        };

        fetchAuthorDetails();
    }, [authorId]);

    const handleDelete = () => {
        if (window.confirm('Are you sure you want to delete this author? This action cannot be undone.')) {
            axios.delete(`https://localhost:7200/api/Author/DeleteAuthor/${authorId}`)
                .then(response => {
                    setMessage({ text: 'Author deleted successfully!', type: 'success' });
                    setTimeout(() => navigate('/Dspace/Author/ListOfAuthor'), 2000);
                })
                .catch(error => {
                    console.error('There was an error deleting the author!', error);
                    setMessage({ text: 'Author cannot be deleted as it may have associated records. Please remove them first.', type: 'danger' });
                });
        }
    };

    if (error) {
        return (
            <div>
                <Header />
                <div className="container mt-5">
                    <h1 className="mb-4">Author Details</h1>
                    <div className="alert alert-danger">{error}</div>
                    <button className="btn btn-secondary" onClick={() => navigate('/Dspace/Author/ListOfAuthor')}>Back to List</button>
                </div>
            </div>
        );
    }

    if (!author) {
        return (
            <div>
                <Header />
                <div className="container mt-5">
                    <h1 className="mb-4">Author Details</h1>
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
                    <span className="text-dark"><strong>| Author Detail</strong></span>
                </div>
                <h1 className="mb-4">Author Details</h1>
                {message && (
                    <div className={`alert alert-${message.type} text-center`}>{message.text}</div>
                )}
                <div className="card">
                    <div className="card-body">
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">First Name:</div>
                            <div className="col-sm-6">{author.firstName}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Last Name:</div>
                            <div className="col-sm-6">{author.lastName}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Email:</div>
                            <div className="col-sm-6">{author.email}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Job Title:</div>
                            <div className="col-sm-6">{author.jobTitle}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Birth Date:</div>
                            <div className="col-sm-6">{`${author.birthDate}`.split('T')[0]}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Date Accessioned:</div>
                            <div className="col-sm-6">{`${author.dateAccessioned}`.split('T')[0]}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Date Available:</div>
                            <div className="col-sm-6">{`${author.dateAvailable}`.split('T')[0]}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">URI:</div>
                            <div className="col-sm-6">{author.uri}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Type:</div>
                            <div className="col-sm-6">{author.type}</div>
                        </div>
                    </div>
                </div>
                <div className="mt-4">
                    <button className="btn btn-primary me-2" onClick={() => navigate(`/Dspace/Author/editAuthor/${author.authorId}`)}>Edit</button>
                    <button className="btn btn-danger me-2" onClick={handleDelete}>Delete</button>
                    <button className="btn btn-secondary" onClick={() => navigate('/Dspace/Author/ListOfAuthor')}>Back to List</button>
                </div>
            </div>
        </div>
    );
};

export default AuthorDetails;
