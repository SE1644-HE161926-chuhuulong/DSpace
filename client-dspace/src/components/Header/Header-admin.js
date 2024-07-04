import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import './Header.css';
import Menu from '../Menu/Menu';
import MenuStaff from '../Menu/MenuStaff';

function HeaderAdmin() {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  return (
    <Navbar expand="lg" className="header-nav">
      <div className="menu-icon">
      {localStorage.getItem("Role") === "ADMIN" ? <Menu /> : <MenuStaff />}
      </div>
      <Container>
        <Navbar.Brand href="/homepageUser">FPT Education on DSpace V2.0</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            {/* Add Nav items here if needed */}
          </Nav>
          <Nav>
            {localStorage.getItem("Token") != null ? (
              <div>
                <Nav.Link href="#link">Welcome {localStorage.getItem("Name")}</Nav.Link>
                <Nav.Link onClick={
                  async logout => {
                    localStorage.removeItem("Token");
                    localStorage.removeItem("Role");
                    localStorage.removeItem("Name");
                    navigate('/');
                  }
                }>Logout</Nav.Link>
              </div>
            ) : (
              <p><Nav.Link href="/">Login</Nav.Link></p>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default HeaderAdmin;
