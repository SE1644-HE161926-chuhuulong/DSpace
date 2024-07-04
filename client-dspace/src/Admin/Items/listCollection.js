import React, { useEffect, useState } from 'react';
import { Link , useNavigate} from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import axios from 'axios';
import './CollectionList.css'; // Import file CSS


const CollectionList = () => {
    const [collections, setCollections] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [searchQuery, setSearchQuery] = useState('');
    const navigate = useNavigate();
    useEffect(() => {
        // Lấy dữ liệu từ API
        axios.get('https://localhost:7200/api/Collection/getListOfCollections')
            .then(({ data }) => {
                setCollections(data);
                setLoading(false);
            })
            .catch(error => {
                setError(error.message);
                setLoading(false);
            });
    }, []);

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
    };
    const handleRowClick = (collectionId) => {
        // Chuyển hướng sang trang chi tiết bộ sưu tập
        navigate(`/Dspace/CreateItem/${collectionId}`);
    };

    const filteredCollections = collections.filter(collection =>
        collection.collectionName.toLowerCase().includes(searchQuery.toLowerCase())
    );

    if (loading) {
        return (
            <div>
                <Header />
                <div className="container mt-5 d-flex justify-content-center align-items-center" style={{ height: '60vh' }}>
                    <div class="load-4">
                        {/* <p>Loading 4</p> */}
                        <div class="ring-1"></div>
                    </div>
                </div>
            </div>
        );
    }

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <h1 className="mb-4">Choose Collection To Access Items</h1>
                <div className="d-flex justify-content-between align-items-center mb-4">
                    <input
                        type="text"
                        className="form-control w-50"
                        placeholder="Search By Name"
                        value={searchQuery}
                        onChange={handleSearchChange}
                    />
                </div>
                {error && <div className="alert alert-danger">{error}</div>}
                <table className="table table-striped table-bordered table-hover">
                    <thead className="thead-dark">
                        <tr>
                        <th>Id</th>
                            <th>Logo</th>
                            <th>Parent Community</th>
                            <th>Name</th>
                            <th>Short Description</th>
                            <th>Create By</th>
                            <th>Active</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredCollections.map((collection, index) => (
                            <tr key={index} onClick={() => handleRowClick(collection.collectionId)} 
                            className="clickable-row">
                                <td>{index + 1}</td>
                                <td><img src={collection.logoUrl} alt="logo" style={{ width: '50px', height: '50px' }} /></td>
                                <td>{collection.communityDTOForSelect?.communityName}</td>
                                <td>{collection.collectionName}</td>
                                <td>{collection.shortDescription}</td>
                                <td>{collection.createBy || collection.updateBy}</td>
                                <td>
                                    <input type="checkbox" checked={collection.isActive} readOnly />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default CollectionList;
