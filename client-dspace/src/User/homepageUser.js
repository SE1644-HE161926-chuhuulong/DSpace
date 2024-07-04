import { Link, Navigate, useNavigate } from 'react-router-dom';
import './homepageUser.css';
import Header from '../components/Header/Header';
import React , { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const HomepageUser = () => {
    const navigate = useNavigate();

    useEffect(() => {
      const role = localStorage.getItem('Role');
      if (role) {
        if (role === "STUDENT" || role === "LECTURER") {
          navigate('/homepageStu');
        } else if (role === "STAFF") {
          navigate('/homepageStaff');
        } else if (role === "ADMIN") {
          navigate('/homepageAdmin');
        }
      }
    }, [navigate]);

    return (
        <div style={{ minHeight: '100vh', backgroundImage: 'url(image2.jpg)', backgroundSize: 'cover', backgroundPosition: 'center' }}>
            <Header />
            <div className="container my-5">
                <div className="row align-items-center">
                    <div className="col-md-6">
                        {/* <img
                            src="Logo_Trường_Đại_học_FPT.svg"
                            alt="DSpace 2.0"
                            className="img-fluid"
                        /> */}
                    </div>
                    <div className="col-md-6">
                        <h1 className="display-4 mb-4" style={{ fontWeight: 'bold', marginTop: "%" }}>Welcome to DSpace 2.0</h1>
                        <p className="lead mb-4" 
                        // style={{
                        //     backgroundColor: 'rgba(34, 67, 164, 0.6)',
                        //     padding: '15px',
                        //     borderRadius: '10px',
                        //     color: 'whitesmoke'
                        // }}
                        >
                            {/* DSpace v2.0 is an upgraded version of DSpace, created to make the process of learning, searching for materials, as well as managing study materials in school libraries more convenient and easier.<br /><br /> */}
                            DSpace v2.0 is an upgraded version of DSpace, created to make the process of learning, searching for materials, as well as managing study materials in school libraries more convenient and easier.</p>

                        <div className="d-flex justify-content-start">
                            <Link to="/" className="btn btn-primary mr-3" style={{ marginRight: "10px" }}>
                                Get Started
                            </Link>
                            {/* <Link to="/get-started" className="btn btn-outline-primary">
                                Get Started
                            </Link> */}
                        </div>
                    </div>

                </div>
            </div>
            {/* <div className="footer py-2" style={{ backgroundColor: 'rgba(255, 255, 255, 0.5)', margin: '0' }}>
                <div className="container">
                    <div className="d-flex justify-content-between align-items-center">
                        <span>&copy; 2023 - FPT Education</span>
                        <Link to="/about-us" className="text-muted">
                            About Us
                        </Link>
                    </div>
                </div>
            </div> */}

            <footer className="footer" style={{ backgroundColor: 'rgba(255, 255, 255, 0.6)', margin: '0' }}>
                <div className="container">
                    <div className="row">
                        <div className="col-12 text-center">
                            <span>&copy; 2023 - FPT Education | <Link to="/about-us">About us</Link></span>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    );
};

export default HomepageUser;