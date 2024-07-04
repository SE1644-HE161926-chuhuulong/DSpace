import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const EditGroup = () => {
    const { groupId } = useParams();
    const [groupData, setGroupData] = useState({
        title: '',
        description: '',
        isActive: false,
    });
    const [successMessage, setSuccessMessage] = useState('');

    useEffect(() => {
        // Fetch the group data from your API using the id from the URL
        axios.get(`https://localhost:7200/api/Group/getGroup/${groupId}`)
            .then(response => {
                setGroupData(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the group!', error);
            });
    }, [groupId]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setGroupData({
            ...groupData,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        axios.put(`https://localhost:7200/api/Group/updateGroup/${groupId}`, groupData)
            .then(response => {
                console.log('Group updated successfully!', response.data);
                setSuccessMessage('Group updated successfully!');
            })
            .catch(error => {
                console.error('There was an error updating the group!', error);
            });
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
            <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Edit Group</strong></span>
                </div>
                <h1 className="mb-4">Edit Group</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>}
                <form onSubmit={handleSubmit}>
                    <div className="col-md-6 mb-3">
                        <div className="form-group">
                            <label>Title</label>
                            <input
                                type="text"
                                className="form-control"
                                name="title"
                                value={groupData.title}
                                onChange={handleChange}
                            />
                        </div>
                    </div>
                    <div className="col-md-6 mb-3">
                        <div className="form-group">
                            <label>Description</label>
                            <textarea
                                className="form-control"
                                name="description"
                                value={groupData.description}
                                onChange={handleChange}
                            />
                        </div>
                    </div>
                    <div className="form-group form-check mb-3">
                        <input
                            type="checkbox"
                            className="form-check-input"
                            name="isActive"
                            checked={groupData.isActive}
                            onChange={handleChange}
                        />
                        <label className="form-check-label">Is Active</label>
                    </div>
                    <button type="submit" className="btn btn-primary">Save</button>
                </form>
                <div className="mt-3">
                    <Link to="/Dspace/Group/ListOfGroup">Back to List</Link>
                </div>
            </div>
        </div>
    );
};

export default EditGroup;
