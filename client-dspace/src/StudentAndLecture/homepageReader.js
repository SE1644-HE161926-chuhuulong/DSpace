import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Header from '../components/Header/Header';
import './homepageReader.css'; 
import { Search } from '@material-ui/icons'; 

const HomepageReader = () => {
    const [communities, setCommunities] = useState([]);
    const [collections, setCollections] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        const fetchCommunities = async () => {
            try {
                const responseCommunities = await fetch('https://localhost:7200/api/Community/getListOfCommunities');
                const dataCommunities = await responseCommunities.json();
                setCommunities(dataCommunities);
            } catch (error) {
                console.error('Error fetching communities:', error);
            }
        };

        const fetchCollections = async () => {
            try {
                const responseCollections = await fetch('https://localhost:7200/api/Collection/getListOfCollections');
                const dataCollections = await responseCollections.json();
                setCollections(dataCollections);
            } catch (error) {
                console.error('Error fetching collections:', error);
            }
        };

        fetchCommunities();
        fetchCollections();
    }, []);

    const handleSearchChange = (event) => {
        setSearchQuery(event.target.value);
    };

    const handleSearchSubmit = () => {
        navigate(`/Dspace/Collection/listItem/0?search=${searchQuery}`);
    };

    return (
        <div className="homepage-reader">
            <Header />
            <img
                src="FPT.jpg"
                alt="Background"
                className="background-image"
            />

            <div className="content-container">
                <div className="container">
                    <div className="row">
                        <div className="col-12">
                            <div className="search-container d-flex align-items-center mb-4">
                                <input
                                    type="text"
                                    className="form-control w-50"
                                    placeholder="Search by reponsitory"
                                    style={{ borderRadius: '0', flex: '0 1 auto' }}
                                    value={searchQuery}
                                    onChange={handleSearchChange}
                                />
                                <button
                                    style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }}
                                    className="btn"
                                    onClick={handleSearchSubmit}
                                >
                                    <Search />
                                </button>
                            </div>

                            <h1>Communities in DSpace</h1>
                            <h4>Select a community to browse its collections</h4>
                            <ul>
                                {communities.map((community, index) => (
                                    <li key={index}>
                                        <Link to={`/Dspace/Communities/ListOfStuCom?cid=${community.communityId}`} className="community-link">
                                            {community.communityName}
                                        </Link>
                                    </li>
                                ))}
                            </ul>
                        </div>
                    </div>

                    <hr className="separator-line" />

                    <div className="row">
                        <div className="col-12">
                            <h2>Recent Submissions</h2>
                            <ul className="collection-list">
                                {collections.map((collection, index) => (
                                    <li key={index} className="collection-item">
                                        <img src={collection.logoUrl} alt="Logo" className="collection-logo" />
                                        <div>
                                            <h5 className="collection-name">{collection.collectionName}</h5>
                                            <p className="collection-description">{collection.shortDescription}</p>
                                        </div>
                                    </li>
                                ))}
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <div className="ft">
                <div className="container">
                    <div className="row">
                        <div className="col-12 text-center">
                            <span>&copy; 2023 - FPT Education | <Link to="/about-us">About us</Link></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default HomepageReader;
