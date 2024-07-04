import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const AddUserToGroup = () => {
    const [group, setGroup] = useState(null);
    const [users, setUsers] = useState([]);
    const [filteredUsers, setFilteredUsers] = useState([]);
    const [selectedUsers, setSelectedUsers] = useState([]);
    const { groupId } = useParams();
    const [error, setError] = useState(null);
    const [successMessage, setSuccessMessage] = useState('');
    const [failureMessage, setFailureMessage] = useState('');
    const [searchQuery, setSearchQuery] = useState('');
    const [searchType, setSearchType] = useState('');

    useEffect(() => {
        const fetchGroupDetails = async () => {
            try {
                const response = await axios.get(`https://localhost:7200/api/Group/getGroup/${groupId}`);
                setGroup(response.data);
            } catch (error) {
                setError(error.message);
                console.error("Error fetching group details:", error);
            }
        };

        const fetchUsers = async () => {
            try {
                const response = await axios.get("https://localhost:7200/api/People/getListOfPeople");
                setUsers(response.data);
            } catch (error) {
                setError(error.message);
                console.error("Error fetching data:", error);
            }
        };

        fetchGroupDetails();
        fetchUsers();
    }, [groupId]);

    useEffect(() => {
        if (group && users.length > 0) {
            const groupMemberIds = group.listPeopleInGroup.map(member => member.peopleId);
            const nonGroupMembers = users.filter(user => !groupMemberIds.includes(user.peopleId));
            setFilteredUsers(nonGroupMembers);
        }
    }, [group, users]);

    const handleUserSelection = (peopleId) => {
        const isSelected = selectedUsers.includes(peopleId);
        if (isSelected) {
            setSelectedUsers(selectedUsers.filter(id => id !== peopleId));
        } else {
            setSelectedUsers([...selectedUsers, peopleId]);
        }
    };

    const handleAddToGroup = async () => {
        if (selectedUsers.length === 0) {
            setFailureMessage('Please select at least one user to add to the group.');
            setSuccessMessage('');
            return;
        }

        try {
            const response = await axios.post(`https://localhost:7200/api/GroupPeople/AddListPeopleInGroup`, {
                groupId: groupId,
                listPeopleId: selectedUsers
            });
            if (response.status === 200) {
                setSuccessMessage('Users added to group successfully!');
                setFailureMessage('');
                // Remove added users from the list
                setFilteredUsers(filteredUsers.filter(user => !selectedUsers.includes(user.peopleId)));
                setSelectedUsers([]);
            } else {
                setFailureMessage('Failed to add users to group');
                setSuccessMessage('');
            }
        } catch (error) {
            console.error('Error adding users to group:', error.response?.data || error.message);
            setFailureMessage('Failed to add users to group');
            setSuccessMessage('');
        }
    };
console.log(selectedUsers)
    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
    };

    const handleSearchTypeChange = (e) => {
        setSearchType(e.target.value);
        setSearchQuery(''); // Clear search query when changing search type
    };

    const filteredAndSearchedUsers = filteredUsers.filter(user => {
        if (!searchQuery.trim()) return true; // Return all users if search query is empty

        const normalizedSearchQuery = searchQuery.replace(/\s+/g, '').toLowerCase();
        switch (searchType) {
            case 'fullName':
                return (
                    (user.firstName && user.firstName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.lastName && user.lastName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery))
                );
            case 'address':
                return (user.address && user.address.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery));
            case 'phoneNumber':
                return (user.phoneNumber && user.phoneNumber.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery));
            case 'email':
                return (user.email && user.email.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery));
            default:
                // Search all fields if searchType is not specified
                return (
                    (user.firstName && user.firstName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.lastName && user.lastName.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.address && user.address.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.phoneNumber && user.phoneNumber.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery)) ||
                    (user.email && user.email.replace(/\s+/g, '').toLowerCase().includes(normalizedSearchQuery))
                );
        }
    });

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <h1 className="mb-4">Add Users to Group</h1>
                {error && <div className="alert alert-danger">Error: {error}</div>}
                {successMessage && <div className="alert alert-success">{successMessage}</div>}
                {failureMessage && <div className="alert alert-danger">{failureMessage}</div>}
                <div className="d-flex justify-content-between align-items-bottom mb-4">
                    <div className="w-50">
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
                    <div className="w-25">
                        <label htmlFor="searchTypeSelect" className="form-label fw-bold">Search By</label>
                        <select
                            id="searchTypeSelect"
                            className="form-select"
                            value={searchType}
                            onChange={handleSearchTypeChange}
                        >
                            <option value="">Select...</option>
                            <option value="fullName">Full Name</option>
                            <option value="address">Address</option>
                            <option value="phoneNumber">Phone Number</option>
                            <option value="email">Email</option>
                        </select>
                    </div>
                </div>
                <table className="table table-striped table-bordered">
                    <thead className="thead-dark">
                        <tr>
                            <th>Full Name</th>
                            <th>Address</th>
                            <th>Phone Number</th>
                            <th>Email</th>
                            <th>Select</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredAndSearchedUsers.map(user => (
                            <tr key={user.peopleId}>
                                <td>{user.firstName} {user.lastName}</td>
                                <td>{user.address}</td>
                                <td>{user.phoneNumber}</td>
                                <td>{user.email}</td>
                                <td>
                                    <input
                                        type="checkbox"
                                        checked={selectedUsers.includes(user.peopleId)}
                                        onChange={() => handleUserSelection(user.peopleId)}
                                    />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <div className="mt-4">
                    <button className="btn btn-primary me-2" onClick={handleAddToGroup}>Add to Group</button>
                    <Link to={`/Dspace/Group/getGroup/${groupId}`} className="btn btn-secondary">Back to Group Details</Link>
                </div>
            </div>
        </div>
    );
};

export default AddUserToGroup;
