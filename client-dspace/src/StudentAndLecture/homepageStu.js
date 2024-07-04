import React, { useEffect, useState, useRef } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './homepageStu.css';
import Header from '../components/Header/Header';

const HomepageStu = () => {
    const bellRef = useRef(null);
    const [isBellAnimating, setIsBellAnimating] = useState(false);

    const ringBell = () => {
        setIsBellAnimating(true);
        setTimeout(() => setIsBellAnimating(false), 1000); // Giả sử thời lượng của hiệu ứng của bạn là 1 giây
    };

    const [isOpen, setIsOpen] = useState(false);
    const [activeDropdown, setActiveDropdown] = useState(null);

    const toggleDropdown = (dropdown) => {
        if (activeDropdown === dropdown) {
            setIsOpen(!isOpen);
        } else {
            setActiveDropdown(dropdown);
            setIsOpen(true);
        }
    };

    const handleLinkClick = () => {
        setIsOpen(false);
    };

    const handleCommunityClick = (communityId) => {
        getCommunityId(communityId);
        setIsOpen(false);
    };

    const [communityId, setCommunityId] = useState(null);
    const navigate = useNavigate();

    const getCommunityId = async (communityName) => {
        try {
            const response = await fetch(`https://localhost:7200/api/CommunityUser/getCommunityByName/${communityName}`);
            const data = await response.json();

            links.communityName.label = `${communityName}`;
            links.communityName.url = `/Dspace/Communities/ListOfStuCom?cid=${data[0].communityId}`;
            setCommunityId(data[0].id);
            navigate(`/Dspace/Communities/ListOfStuCom?cid=${data[0].communityId}`);
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div>
            <Header />
            <img
                src="image2.jpg"
                alt="Background"
                style={{
                    position: 'absolute',
                    top: '80px', // Điều chỉnh khoảng cách từ top để hình ảnh xuất hiện dưới Header
                    left: 0,
                    width: '100%',
                    height: 'calc(100% - 80px)', // Điều chỉnh chiều cao để hình ảnh kết thúc trước footer
                    objectFit: 'cover',
                    zIndex: -1,
                    opacity: 0.7,
                }}
            />
            <div className="container my-5" style={{ position: 'relative' }}>


                <h1 className="text-center" style={{ fontWeight: 'bold', marginTop: '4%', fontSize: '3rem' }}>
                    Welcome Lectures And Students
                </h1>

                <div className="row justify-content-center" style={{ marginTop: '4%' }}>
                    <div className="col-md-6">
                        <div className="list-group" style={{ opacity: 0.85 }}>
                            <div
                                onClick={() => toggleDropdown('communities')}
                                className="list-group-item list-group-item-action d-flex justify-content-center align-items-center"
                                style={{ fontSize: '1.8rem', fontWeight: 'bold', width: '100%' }}
                            >
                                Communities
                                <span className={`caret ${isOpen && activeDropdown === 'communities' ? 'caret-up' : 'caret-down'}`} />
                            </div>
                            {isOpen && activeDropdown === 'communities' && (
                                <div>
                                    <div
                                        onClick={() => handleCommunityClick('Dissertations')}
                                        className="list-group-item list-group-item-action d-flex justify-content-center align-items-center"
                                        style={{ fontSize: '1.2rem', fontWeight: 'bold', width: '100%' }}
                                    >
                                        Dissertations
                                    </div>
                                    <div
                                        onClick={() => handleCommunityClick('E-textbooks')}
                                        className="list-group-item list-group-item-action d-flex justify-content-center align-items-center"
                                        style={{ fontSize: '1.2rem', fontWeight: 'bold', width: '100%' }}
                                    >
                                        E-textbooks
                                    </div>
                                </div>
                            )}
                            <div
                                onClick={() => toggleDropdown('convenient')}
                                className="list-group-item list-group-item-action d-flex justify-content-center align-items-center"
                                style={{ fontSize: '1.8rem', fontWeight: 'bold', width: '100%', marginTop: '10px' }}
                            >
                                Convenient
                                <span className={`caret ${isOpen && activeDropdown === 'convenient' ? 'caret-up' : 'caret-down'}`} />
                            </div>
                            {isOpen && activeDropdown === 'convenient' && (
                                <div>
                                    <Link
                                        to="/"
                                        onClick={handleLinkClick}
                                        className="list-group-item list-group-item-action d-flex justify-content-center align-items-center"
                                        style={{ fontSize: '1.2rem', fontWeight: 'bold' }}
                                    >
                                        Dissertations Statistic
                                    </Link>
                                    <Link
                                        to="/"
                                        onClick={handleLinkClick}
                                        className="list-group-item list-group-item-action d-flex justify-content-center align-items-center"
                                        style={{ fontSize: '1.2rem', fontWeight: 'bold' }}
                                    >
                                        Tools and Tips
                                    </Link>
                                </div>
                            )}
                        </div>
                    </div>
                </div>
                <div style={{ textAlign: 'center', marginTop: '20px' }}>
                    <span
                        style={{
                            backgroundColor: 'rgba(220, 53, 69)',
                            width: '17%',
                            borderRadius: '10px',
                        }}
                        onClick={ringBell}
                        ref={bellRef}
                        className={`bell ${isBellAnimating ? 'bell-animation' : ''}`}
                    >
                        🔔 Subscribes
                    </span>
                </div>
            </div>

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

export default HomepageStu;
export const links = {
    home: { label: 'Home', url: '/homepageStu' },
    communityName: { label: ``, url: `` }
    // Thêm các liên kết khác vào đây
};
