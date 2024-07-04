import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link, useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const UserDetails = () => {
    const { peopleId } = useParams();
    const [user, setUser] = useState(null);
    const [error, setError] = useState(null);
    const [message, setMessage] = useState(null); // State for success or error messages
    const [messageType, setMessageType] = useState(''); // State for message type (success or error)
    const navigate = useNavigate();

    useEffect(() => {
        const fetchUserDetails = async () => {
            try {
                const response = await axios.get(`https://localhost:7200/api/People/getPeopleById/${peopleId}`);
                setUser(response.data);
            } catch (error) {
                setError(error.message);
                console.error('Error fetching user details:', error);
            }
        };

        fetchUserDetails();
    }, [peopleId]);

    const deleteUser = async () => {
        try {
            await axios.delete(`https://localhost:7200/api/People/DeletePeople/${peopleId}`);
            setMessage('User deleted successfully.');
            setMessageType('success');
            setTimeout(() => navigate('/Dspace/User/ListOfUsers'), 2000); // Redirect after 2 seconds
        } catch (error) {
            setMessage('Error deleting user.');
            setMessageType('danger');
            console.error('Error deleting user:', error);
        }
    };

    if (!user) {
        return (
            <div>
                <Header />
                <div className="container mt-5">
                    <h1 className="mb-4">User Details</h1>
                    <p>Loading...</p>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div>
                <Header />
                <div className="container mt-5">
                    <h1 className="mb-4">User Details</h1>
                    <div className="alert alert-danger">{error}</div>
                    <Link to="/Dspace/User/ListOfUsers">Back to List</Link>
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
                    <span className="text-dark"><strong>| User Detail</strong></span>
                </div>
                <h1 className="mb-4">User Details</h1>
                {message && <div className={`alert alert-${messageType}`}>{message}</div>}
                <div className="card">
                    <div className="card-body">
                    <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">First Name:</div>
                            <div className="col-sm-6">{user.firstName}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Last Name:</div>
                            <div className="col-sm-6">{user.lastName}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Address:</div>
                            <div className="col-sm-6">{user.address}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Phone Number:</div>
                            <div className="col-sm-6">{user.phoneNumber}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Email:</div>
                            <div className="col-sm-6">{user.email}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Role:</div>
                            <div className="col-sm-6">{user.role}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Created By:</div>
                            <div className="col-sm-6">{user.createdBy}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Created Date:</div>
                            <div className="col-sm-6">{new Date(user.createdDate).toLocaleString()}</div>
                        </div>
                        <div className="row border-bottom pb-2 my-3">
                            <div className="col-sm-2 text-end">Last Modified By:</div>
                            <div className="col-sm-6">{user.lastModifiedBy}</div>
                        </div>
                        <div className="row pb-2 my-3">
                            <div className="col-sm-2 text-end">Last Modified Date:</div>
                            <div className="col-sm-6">{new Date(user.lastModifiedDate).toLocaleString()}</div>
                        </div>
                    </div>
                </div>
                <div className="mt-4">
                    <button onClick={() => navigate(`/Dspace/User/editUser/${user.peopleId}`)} className="btn btn-primary me-2">Edit</button>
                    <button onClick={deleteUser} className="btn btn-danger me-2">Delete</button>
                    <button onClick={() => navigate('/Dspace/User/ListOfUsers')} className="btn btn-secondary me-2">Back to List</button>
                    
                </div>
            </div>
        </div>
    );
};

export default UserDetails;
