import React, { useEffect, useState } from 'react';
import { Link, useNavigate, useParams, useSearchParams } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../components/Header/Header';
import axios from 'axios';
// import './homepageStu.css'; // Import file CSS


const CollectionList = () => {
    const [items, setItems] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [searchQuery, setSearchQuery] = useState('');
    const navigate = useNavigate();
    
    const [searchParams] = useSearchParams();
    const collectionId  = searchParams.get('colid');
    useEffect(() => {
        // Lấy dữ liệu từ API
        axios.get(`https://localhost:7200/api/Item/itemsInCollection/${collectionId}`)
            .then(({ data }) => {
                setItems(data);
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
    const handleRowClick = (itemId) => {
        // Chuyển hướng sang trang chi tiết bộ sưu tập
        navigate(`/DSpace/ItemDetails/${itemId}`);
    };



    // console.log(items[0].dateOfIssue);
    // const filteredCollections = items;

    if (loading) {
        return (
            <div>
                <Header />
                <div className="container mt-5">
                    <h1 className="mb-4">Loading...</h1>
                </div>
            </div>
        );
    }
    const filteredCollections = items.filter(item =>
        item.otherTitle.toLowerCase().includes(searchQuery.toLowerCase())
    );
    return (
        <div>
            <Header />
            <div className="container mt-5">
                <h1 className="mb-4">List Items</h1>
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
                            <th>Other Title</th>
                            <th>Author</th>
                            <th>DateOfIssue</th>
                            <th>Publisher</th>
                            <th>Type</th>
                            <th>SubjectKeywords</th>
                        </tr>
                    </thead>
                    <tbody>
                    {filteredCollections.map((item, index) => (
                            <tr key={index} onClick={() => handleRowClick(item.itemId)}
                                className="clickable-row">
                                <td>{index + 1}</td>
                                <td>{item.metadata.title}</td>
                                <td>{item.authorItems[0].author.firstName} {item.authorItems[0].author.lastName}</td>
                                <td>{item.dateOfIssue}</td>
                                <td>{ item.metadata.publisher}</td>
                                <td>{item.type}</td>
                                <td>{item.itemKeywords[0].keyword.keywordName }</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default CollectionList;
