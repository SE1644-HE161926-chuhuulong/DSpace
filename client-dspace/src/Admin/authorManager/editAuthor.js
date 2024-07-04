import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';

const EditAuthor = () => {
  const { authorId } = useParams();
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    jobTitle: '',
    birthDate: '',
    dateAccessioned: '',
    dateAvailable: '',
    uri: '',
    type: '',
  });
  const [successMessage, setSuccessMessage] = useState('');

  useEffect(() => {
    axios.get(`https://localhost:7200/api/Author/getAuthor/${authorId}`)
      .then(response => {
        setFormData(response.data);
      })
      .catch(error => {
        console.error('There was an error fetching the author!', error);
      });
  }, [authorId]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    axios.put(`https://localhost:7200/api/Author/updateAuthor/${authorId}`, formData)
      .then(response => {
        console.log('Author updated successfully!', response.data);
        setSuccessMessage('Author updated successfully!');
      })
      .catch(error => {
        console.error('There was an error updating the author!', error);
      });
  };

  return (
    <div>
      <Header />
      <div className="container mt-5">
        <h2>Update Author</h2>
        {successMessage && <div className="alert alert-success">{successMessage}</div>}
        <form onSubmit={handleSubmit}>
          <div className="row">
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="firstName">First Name</label>
                <input
                  type="text"
                  className="form-control"
                  name="firstName"
                  value={formData.firstName}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="lastName">Last Name</label>
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
          </div>
          <div className="row">
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="email">Email</label>
                <input
                  type="email"
                  className="form-control"
                  name="email"
                  value={formData.email}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="jobTitle">Job Title</label>
                <input
                  type="text"
                  className="form-control"
                  name="jobTitle"
                  value={formData.jobTitle}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="birthDate">Birth Date</label>
                <input
                  type="date"
                  className="form-control"
                  name="birthDate"
                  value={formData.birthDate}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="dateAccessioned">Date Accessioned</label>
                <input
                  type="date"
                  className="form-control"
                  name="dateAccessioned"
                  value={formData.dateAccessioned}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="dateAvailable">Date Available</label>
                <input
                  type="date"
                  className="form-control"
                  name="dateAvailable"
                  value={formData.dateAvailable}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="uri">URI</label>
                <input
                  type="text"
                  className="form-control"
                  name="uri"
                  value={formData.uri}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-3">
              <div className="form-group">
                <label htmlFor="type">Type</label>
                <input
                  type="text"
                  className="form-control"
                  name="type"
                  value={formData.type}
                  onChange={handleChange}
                  required
                />
              </div>
            </div>
          </div>
          <div className="d-flex justify-content-start">
            <button type="submit" className="btn btn-primary">Save</button>
          </div>
        </form>
        <div className="mt-3">
          <Link to="/Dspace/Author/ListOfAuthor">Back to List</Link>
        </div>
      </div>
    </div>
  );
};

export default EditAuthor;
