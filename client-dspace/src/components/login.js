// import React, { useState } from 'react';
// import axios from 'axios';
// import { useNavigate } from 'react-router-dom';
// import 'bootstrap/dist/css/bootstrap.min.css';
// import Header from '../components/Header/Header';
// import NavDropdown from 'react-bootstrap/NavDropdown';


// const Login = () => {
//     const [email, setEmail] = useState('');
//     const [password, setPassword] = useState('');
//     const [selectedCampus, setSelectedCampus] = useState('Select Campus');
//     const [token, setToken] = useState(null);
//     const [error, setError] = useState(null);
//     const navigate = useNavigate();

//     const handleGoogleLogin = async () => {
//         try {
//           const res = await axios.get('https://localhost:7200/api/Authentication/callback-signin', {
//             headers: {
//               'Content-Type': 'application/json'
//             },
//           });

//           setToken(res.data);
//         } catch (err) {
//           setError(err.message);
//         }
//       };
//     const handleEmailChange = (event) => {
//         setEmail(event.target.value);
//     };

//     const handlePasswordChange = (event) => {
//         setPassword(event.target.value);
//     };

//     const handleLogin = async (event) => {
//         event.preventDefault();
//         try {

//             const response = await axios.post('http://localhost:8000/api/login', {
//                 "email": email,
//                 "password": password
//             });
//             if (response.data.user != null) {
//                 const { email, name } = response.data.user;
//                 navigate('/sss', { state: { name: name } });

//             } else {
//                 alert('Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin đăng nhập.');
//             }
//         } catch (error) {
//             console.error('Lỗi đăng nhập:', error);
//             alert('Đã xảy ra lỗi trong quá trình đăng nhập. Vui lòng thử lại sau.');
//         }
//     };


//     return (
//         <div>
//             <Header></Header>
//             <div className="d-flex justify-content-center align-items-center" style={{}}>
//                 <form onSubmit={handleLogin} style={{ width: '30%', marginTop: '2%' }}>
//                     <h3 className="mb-3" style={{ textAlign: 'center', fontSize: '3rem' }}>Login</h3>

//                     <div className="mb-3" style={{ marginTop: '5%' }}>
//                         <input
//                             type="email"
//                             className="form-control"
//                             placeholder="Enter email"
//                             value={email}
//                             onChange={handleEmailChange}
//                         />
//                     </div>

//                     <div className="mb-3">
//                         <input
//                             type="password"
//                             className="form-control"
//                             placeholder="Enter password"
//                             value={password}
//                             onChange={handlePasswordChange}
//                         />
//                     </div>

//                     <div className="mb-3 d-flex justify-content-between align-items-center">
//                         <div className="dropdown">
//                             <NavDropdown title={selectedCampus} id="basic-nav-dropdown">
//                                 <NavDropdown.Item onClick={() => setSelectedCampus('FU - Hòa Lạc')}>FU - Hòa Lạc</NavDropdown.Item>
//                                 <NavDropdown.Item onClick={() => setSelectedCampus('FU - Hồ Chí Minh')}>FU - Hồ Chí Minh</NavDropdown.Item>
//                                 <NavDropdown.Item onClick={() => setSelectedCampus('FU - Đà Nẵng')}>FU - Đà Nẵng</NavDropdown.Item>
//                                 <NavDropdown.Item onClick={() => setSelectedCampus('FU - Cần Thơ')}>FU - Cần Thơ</NavDropdown.Item>
//                                 <NavDropdown.Item onClick={() => setSelectedCampus('FU - Quy Nhơn')}>FU - Quy Nhơn</NavDropdown.Item>
//                             </NavDropdown>
//                         </div>
//                         <a href="#" className="forgot-password" style={{ textDecoration: 'none', color: 'grey' }}>Forgot password?</a>
//                         </div>

//                     <div className="d-grid gap-4" style={{ marginTop: '7%' }}>
//                         <button type="submit" className="btn btn-primary">
//                             Sign in
//                         </button>
//                     </div>
//                 </form>
//             </div>
//             <div className="d-flex justify-content-center align-items-center" style={{ marginTop: '20px' }}>

//                 <a href='https://localhost:7200/api/Authentication/login-google'
//                         className="w-100 btn btn-danger"
//                         name="provider"
//                         value="Google"
//                         title="Sign in with Google"
//                 >Login with Google</a>

//                 <form action='https://localhost:7200/api/Authentication/logout-google' method='post' style={{ width: '14%', marginLeft: '1%' }}>
//                     <button
//                         type="submit"
//                         className="w-100 btn btn-danger"
//                         name="provider"
//                         value="Google"
//                         title="logout"
//                     >
//                         Sign out Google 🚀
//                     </button>
//                 </form>
//             </div>
//             <div className="d-flex justify-content-center align-items-center" style={{ marginTop: '20px' }}>

//                 <button type="button" className="btn btn-primary" style={{ width: '30%' }}>
//                     Sign in with FeID
//                 </button>
//             </div>

//         </div>
//     );
// };

// export default Login;

import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../components/Header/Header';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { GoogleLogin } from '@react-oauth/google';
import { useGoogleLogin } from '@react-oauth/google';
import { jwtDecode } from "jwt-decode";


const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [selectedCampus, setSelectedCampus] = useState('Select Campus');
    const navigate = useNavigate();

    const [formdata, setFormdata] = useState({
        email: '',
        given_name: '',
        family_name: ''
    });

    const handleEmailChange = (event) => {
        setEmail(event.target.value);
    };

    const handlePasswordChange = (event) => {
        setPassword(event.target.value);
    };

    // const handleLogin = async (event) => {
    //     event.preventDefault();
    //     try {

    //         const response = await axios.post('http://localhost:8000/api/login', {
    //             "email": email,
    //             "password": password
    //         });
    //         // const { email, name } = response.data.user;
    //         // console.log(name);
    //         // const { email, name } = email, name;
    //         if (response.data.user != null) {
    //             // Đăng nhập thành công, chuyển hướng đến trang khác
    //             const { email, name } = response.data.user;
    //             navigate('/sss', { state: { name: name } });

    //             // navigate('/sss');
    //         } else {
    //             alert('Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin đăng nhập.');
    //         }
    //     } catch (error) {
    //         console.error('Lỗi đăng nhập:', error);
    //         alert('Đã xảy ra lỗi trong quá trình đăng nhập. Vui lòng thử lại sau.');
    //     }
    // };

    return (
        <div>
            <Header></Header>
            <div className="d-flex justify-content-center align-items-center" style={{}}>
                <form style={{ width: '30%', marginTop: '2%' }}>
                    <h3 className="mb-3" style={{ textAlign: 'center', fontSize: '3rem' }}>Login</h3>

                    <div className="mb-3" style={{ marginTop: '5%' }}>
                        <input
                            type="email"
                            className="form-control"
                            placeholder="Enter email"
                            value={email}
                            onChange={handleEmailChange}
                        />
                    </div>

                    <div className="mb-3">
                        <input
                            type="password"
                            className="form-control"
                            placeholder="Enter password"
                            value={password}
                            onChange={handlePasswordChange}
                        />
                    </div>

                    <div className="mb-3 d-flex justify-content-between align-items-center">
                        <div className="dropdown">
                            <NavDropdown title={selectedCampus} id="basic-nav-dropdown">
                                <NavDropdown.Item onClick={() => setSelectedCampus('FU - Hòa Lạc')}>FU - Hòa Lạc</NavDropdown.Item>
                                <NavDropdown.Item onClick={() => setSelectedCampus('FU - Hồ Chí Minh')}>FU - Hồ Chí Minh</NavDropdown.Item>
                                <NavDropdown.Item onClick={() => setSelectedCampus('FU - Đà Nẵng')}>FU - Đà Nẵng</NavDropdown.Item>
                                <NavDropdown.Item onClick={() => setSelectedCampus('FU - Cần Thơ')}>FU - Cần Thơ</NavDropdown.Item>
                                <NavDropdown.Item onClick={() => setSelectedCampus('FU - Quy Nhơn')}>FU - Quy Nhơn</NavDropdown.Item>
                            </NavDropdown>
                        </div>
                        <a href="#" className="forgot-password">Forgot password?</a>
                    </div>

                    <div className="d-grid gap-4" style={{ marginTop: '7%' }}>
                        <button type="submit" className="btn btn-primary">
                            Sign in
                        </button>
                        {/* <button type="button" className="btn btn-danger" onClick={() => login1()}>Sign in with Google 🚀</button> */}
                        <GoogleLogin
                            onSuccess={async (credentialResponse) => {
                                const credentialResponseDecoded = jwtDecode(credentialResponse.credential);
                                console.log(credentialResponseDecoded);
                                setFormdata({
                                    email: credentialResponseDecoded.email,
                                    given_name: credentialResponseDecoded.given_name,
                                    family_name: credentialResponseDecoded.family_name,
                                })
                                    const apiResponse = axios.post('https://localhost:7200/api/Authentication/Login', credentialResponseDecoded);
                                    console.log(apiResponse)
                                if ((await apiResponse).data.role === "STUDENT" || (await apiResponse).data.role === "LECTURER") {
                                    navigate('/homepage');
                                } else if((await apiResponse).data.role === "STAFF") {
                                    navigate('/homepageStaff');
                                } else if((await apiResponse).data.role === "ADMIN"){
                                    navigate('/homepageAdmin');
                                }
                                localStorage.setItem('Role', (await apiResponse).data.role);
                                localStorage.setItem('Name', (await apiResponse).data.name);
                                localStorage.setItem('Token', (await apiResponse).data.token);

                            }}
                            onError={() => {
                                console.log('Login Failed');
                            }}
                        />
                        <button type="button" className="btn btn-primary">
                            Sign in with FeID
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Login;