import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../components/Header/Header-admin';

const GroupDetails = () => {
    const { groupId } = useParams();
    const navigate = useNavigate();
    const [group, setGroup] = useState(null);
    const [error, setError] = useState(null);
    const [successMessage, setSuccessMessage] = useState('');
    const [failureMessage, setFailureMessage] = useState('');

    useEffect(() => {
        const fetchGroupDetails = async () => {
            try {
                const response = await axios.get(`https://localhost:7200/api/Group/getGroup/${groupId}`);
                setGroup(response.data);
            } catch (error) {
                setError(error.message);
                console.error('Error fetching group details:', error);
            }
        };

        fetchGroupDetails();
    }, [groupId]);

   

    const handleDeleteMember = async (userId, groupId) => {
        if (window.confirm('Are you sure you want to delete this member?')) {
            try {
                const response = await axios.put('https://localhost:7200/api/GroupPeople/DeletePeopleInGroup', {
                    "groupId": groupId + '',
                    "peopleId": userId + '',
                });
                if (response.status === 200) {
                    setSuccessMessage('Member deleted successfully');
                    setFailureMessage('');
                    setGroup({
                        ...group,
                        listPeopleInGroup: group.listPeopleInGroup.filter(member => member.peopleId !== userId)
                    });
                } else {
                    setFailureMessage('Failed to delete member');
                    setSuccessMessage('');
                }
            } catch (error) {
                console.error('Error deleting member:', error);
                setFailureMessage('Failed to delete member');
                setSuccessMessage('');
            }
        }
    };

    if (!group) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark"><strong>| Group Detail</strong></span>
                </div>
                <h1 className="mb-4">Group Details</h1>
                {error && <div className="alert alert-danger">Error: {error}</div>}
                {successMessage && <div className="alert alert-success">{successMessage}</div>}
                {failureMessage && <div className="alert alert-danger">{failureMessage}</div>}
                <div className="card">
                    <div className="card-body">
                        <h6 className="card-text mb-2 text-end">Title: {group.title}</h6>
                        <p className="card-text text-end">Description: {group.description}</p>
                        <p className="card-text text-end">Active: <input type="checkbox" checked={group.isActive} readOnly /></p>
                    </div>
                </div>
                <div className="mt-4">
                    <h4>Group Members</h4>
                    <table className="table table-striped table-bordered">
                        <thead className="thead-dark">
                            <tr>
                                <th>Full Name</th>
                                <th>Address</th>
                                <th>Phone Number</th>
                                <th>Email</th>
                                {/* <th>Actions</th> */}
                            </tr>
                        </thead>
                        <tbody>
                            {group.listPeopleInGroup.map(member => (
                                <tr key={member.peopleId}>
                                    <td>{`${member.firstName || ''} ${member.lastName || ''}`.trim() || 'N/A'}</td>
                                    <td>{member.address || 'N/A'}</td>
                                    <td>{member.phoneNumber || 'N/A'}</td>
                                    <td>{member.email || 'N/A'}</td>
                                    {/* <td>
                                        <button
                                            className="btn btn-danger btn-sm"
                                            onClick={() => handleDeleteMember(member.peopleId, groupId)}
                                        >
                                            Delete
                                        </button>
                                    </td> */}
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                <div className="mt-4">
                    <button className="btn btn-success me-2" onClick={() => navigate(``)}>Add people to group</button>
                    <button className="btn btn-primary me-2" onClick={() => navigate(``)}>Edit</button>
                    <button className="btn btn-danger me-2" onClick={() => navigate(``)}>Delete</button>
                    <button className="btn btn-secondary" onClick={() => navigate('/Dspace/Group/ListOfGroupStaff')}>Back to List</button>
                </div>
            </div>
        </div>
    );
};

export default GroupDetails;
