import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import { useParams, Link } from 'react-router-dom';

const EditUser = () => {
  const { peopleId } = useParams();
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    address: '',
    phoneNumber: '',
    email: '',
    rolename: '', // Changed from 'role' to 'rolename' to match the form
  });
  const [successMessage, setSuccessMessage] = useState('');
  const token = localStorage.getItem('Token');
    const header = `Bearer ${token}`;
  useEffect(() => {
    // Fetch user data from your API using the id from the URL
    axios.get(`https://localhost:7200/api/People/getPeopleById/${peopleId}`)
      .then(response => {
        setFormData(response.data);
      })
      .catch(error => {
        console.error('There was an error fetching the user!', error);
      });
  }, [peopleId]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    axios.put(`https://localhost:7200/api/People/updatePeople/${peopleId}`, formData, {
      headers: { Authorization: header }
  })
      .then(response => {
        console.log('User updated successfully!', response.data);
        setSuccessMessage('User updated successfully!');
      })
      .catch(error => {
        console.error('There was an error updating the user!', error);
      });
  };

  return (
    <div>
      <Header />
      <div className="container mt-5">
      <div className="py-1 bg-light">
                    <Link to="/homepageAdmin" className="text-decoration-none">Home </Link>
                    <span className="text-dark" ><strong>| Edit User</strong></span>
                </div>
        <h1>Edit User</h1>
        {successMessage && <div className="alert alert-success">{successMessage}</div>}
        <div className="row">
          <div className="col-md-12">
            <form onSubmit={handleSubmit}>
              <div className="form-group row w-75">
                <div className="col">
                  <label>First Name</label>
                  <input
                    type="text"
                    className="form-control"
                    name="firstName"
                    value={formData.firstName}
                    onChange={handleChange}
                    required
                  />
                </div>
                <div className="col">
                  <label>Last Name</label>
                  <input
                    type="text"
                    className="form-control"
                    name="lastName"
                    value={formData.lastName}
                    onChange={handleChange}
                    required
                  />
                </div>
              </div>
              <div className="form-group row w-75">
                <div className="col">
                  <label>Address</label>
                  <input
                    type="text"
                    className="form-control"
                    name="address"
                    value={formData.address}
                    onChange={handleChange}
                    required
                  />
                </div>
                <div className="col">
                  <label>Phone Number</label>
                  <input
                    type="text"
                    className="form-control"
                    name="phoneNumber"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    required
                  />
                </div>
              </div>
              <div className="form-group row w-75">
                <div className="col">
                  <label>Email</label>
                  <input
                    type="email"
                    className="form-control"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                  />
                </div>
                <div className="col">
                  <label>Role</label>
                  <select
                    className="form-control"
                    name="rolename"
                    value={formData.rolename}
                    onChange={handleChange}
                    required
                  >
                    <option value="ADMIN">ADMIN</option>
                    <option value="STAFF">STAFF</option>
                  </select>
                </div>
              </div>
              <button type="submit" className="btn btn-primary mt-3">Save</button>
            </form>
          </div>
        </div>
        <div className="mt-3">
          <Link to="/Dspace/User/ListOfUsers">Back to List</Link>
        </div>
      </div>
    </div>
  );
};

export default EditUser;