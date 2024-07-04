import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link, useNavigate, useLocation } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import { VerticalAlignBottom } from '@material-ui/icons';
import { saveAs } from 'file-saver';

const ItemDetails = () => {
    const { itemId } = useParams(); // Get the ID from the URL
    const [item, setItem] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const colId = searchParams.get('colId');

    useEffect(() => {
        // Fetch the community data from your API using the id from the URL
        axios.get(`https://localhost:7200/api/Item/GetItemSimpleById/${itemId}`) // Replace with your actual API endpoint
            .then(response => {
                const fetchedItem = response.data;
                // Check if the file array exists and has at least one element
                // if (fetchedItem.file && fetchedItem.file.length > 0) {
                //     // Get the latest file
                //     const latestFile = fetchedItem.file[fetchedItem.file.length - 1];
                //     // Update the item file array to only include the latest file
                //     fetchedItem.file = [latestFile];
                //     setLoading(false);
                // }
                setItem(fetchedItem);
                setLoading(false);
            })
            .catch(error => {
                console.error('There was an error fetching the community!', error);
                setLoading(false);
            });

    }, [itemId]);
    const handleClickFile = (id) => {
        // const fileUrl = item.file[0].fileUrl; // Lấy URL của file PDF
        navigate(`/DSpace/PdfViewer/${id}`);
    };
    const handleDeleteItem = () => {
        const confirmDelete = window.confirm("Are you sure you want to delete this item?");
        if (confirmDelete) {
            // Gọi API để xóa item
            fetch(`https://localhost:7200/api/Item/deleteItem/${itemId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                    // Các headers khác nếu cần
                }
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Failed to delete item.');
                    }
                    window.alert('Item deleted successfully!');
                    navigate('/DSpace/Collection/listItem/0');
                    // Xử lý khi xóa thành công (vd: redirect, refresh danh sách, ...)
                })
                .catch(error => {
                    console.error('Error deleting item:', error);
                    // Xử lý khi có lỗi xảy ra
                });
        }
    };
    const handleDownloadFile = (fileKeyId, fileName) => {
        const downloadUrl = `https://localhost:7200/api/FileUpload/downloadFile/${fileKeyId}`;

        fetch(downloadUrl)
            .then(response => response.blob())
            .then(blob => {
                // Sử dụng file-saver để lưu tệp
                saveAs(blob, fileName);
            })
            .catch(error => {
                console.error('Error downloading file:', error);
            });
    };
    const handleAuthorClick = (author) => {
        // Xử lý sự kiện khi tác giả được click
        console.log('Tác giả được chọn:', author);
    };
    const formatDate = (dateString) => {
        const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
        const date = new Date(dateString);
        return date.toLocaleDateString('en-GB', options).replace(/\//g, '-');
    };

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
    // console.log(item?.metadata?.identifier);
    return (
        <div>
            <Header />
            {/* <div className="container mt-5">
                <h1 className="mb-4">Item Details</h1>
                <div className="card">
                    <div className="card-body">
                        <h6 className="card-text">Title: {item?.metadata?.title}</h6>
                        <h6 className="card-text">Author: {item?.authorItems[0]?.author?.firstName} {item?.authorItems[0]?.author?.lastName}</h6>
                        <h6 className="card-text">Other Titles: {item?.otherTitle}</h6>
                        <h6 className="card-text">Date Of Issue: {item?.dateOfIssue}</h6>
                        <h6 className="card-text">Publisher: {item?.metadata?.publisher}</h6>
                        <h6 className="card-text">Citation: {}</h6>
                        <h6 className="card-text">Series No: {}</h6>
                        <h6 className="card-text">Identifiers: {}</h6>
                        <h6 className="card-text">Type: {}</h6>
                        <h6 className="card-text">Keywords: {}</h6>
                        <h6 className="card-text">Abstract: {}</h6>
                        <h6 className="card-text">Sponsors: {}</h6>
                        <h6 className="card-text">Description: {}</h6>
                        <h6 className="card-text">Language: {}</h6>
                        <h6 className="card-text">File: {}</h6>
                        <h6 className="card-text">Collection: {}</h6>
                    </div>
                </div>
                <div className="mt-4">
                    <Link to={`/`} className="mr-2">Edit</Link> |
                    <Link to={`/`} className="ml-2">Delete</Link> |
                    <Link to={`/api/Collection/listItem/${item?.collection?.collectionId}`} className="ml-2">Back to List</Link>
                </div>
            </div> */}



            <div className="container mt-5">
                <h1 className="mb-5 text-center">{item.title}</h1>
                <div className="row justify-content-center">
                    <div className="col-md-11">
                        <div className="row">
                            <div className="col-md-5">
                                <div>
                                    <label style={{ marginRight: '10px', fontSize: '1.4rem', marginBottom: '3px' }}>Author</label>
                                    <div className="form-group mb-3 d-flex flex-column">
                                        {item.authors.map((authorItem, index) => (
                                            <div key={index} className="author-container">
                                                <span
                                                    onClick={() => handleAuthorClick(authorItem.author)}
                                                    className="author-item"
                                                >
                                                    {authorItem}
                                                </span>
                                            </div>
                                        ))}
                                    </div>
                                </div>
                                <div>
                                    <label style={{ marginRight: '10px', fontSize: '1.4rem', marginBottom: '3px' }}>Date Of Issue</label>
                                    <div className="form-group mb-3 d-flex flex-column">
                                        {formatDate(item.dateOfIssue)}
                                    </div>
                                </div>
                                <div>
                                    <label style={{ marginRight: '10px', fontSize: '1.4rem', marginBottom: '3px' }}>Publisher</label>
                                    <div className="form-group mb-3 d-flex flex-column">
                                        {item.publisher}
                                    </div>
                                </div>
                                <div>
                                    <label style={{ marginRight: '10px', fontSize: '1.4rem', marginBottom: '3px' }}>Keywords</label>
                                    <div className="form-group mb-3 d-flex flex-column">
                                        {item.subjectKeywords.map((keywordItem, index) => (
                                            <div key={index} className="author-container">

                                                {keywordItem}
                                            </div>
                                        ))}
                                    </div>
                                </div>
                                {/* <div>
                                    <label style={{ marginRight: '10px', fontSize: '1.4rem', marginBottom: '3px' }}>URI</label>
                                    <div className="form-group mb-3 d-flex flex-column">
                                    {item?.metadata?.publisher}
                                    </div>
                                </div> */}
                                <div>
                                    <label style={{ marginRight: '10px', fontSize: '1.4rem', marginBottom: '3px' }}>Collection</label>
                                    <div className="form-group mb-3 d-flex flex-column">
                                        {item.collectionName}
                                    </div>
                                </div>
                                {/* <div className="form-group mb-3">
                                    <label>Title</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="title"
                                        value={item?.metadata?.title}
                                        readOnly
                                    />
                                </div> */}
                                {/* <div>
                                    <label style={{ marginRight: '10px' }}>Other Title </label>

                                    <div
                                        // key={index} 
                                        className="form-group mb-3 d-flex align-items-center">

                                        <input
                                            type="text"
                                            className="form-control"
                                            name="otherTitle"
                                            value={item?.otherTitle}
                                            style={{ marginRight: '10px' }}
                                            readOnly
                                        />

                                    </div>
                                </div> */}


                                {/* <div className="form-group mb-3">
                                    <label>Date of Issue *</label>
                                    <input
                                        type="date"
                                        className="form-control"
                                        name="dateOfIssue"
                                        value={item && new Date(item.dateOfIssue).toISOString().split('T')[0]}
                                        readOnly
                                    />
                                </div> */}

                                {/* <div className="form-group mb-3">
                                    <label>Publisher</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="publisher"
                                        value={item?.metadata?.publisher}
                                        readOnly
                                    />
                                </div> */}
                                {/* <div className="form-group mb-3">
                                    <label>Citation</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="citation"
                                        value={item?.citation}
                                        readOnly
                                    />
                                </div> */}
                                {/* <div className="form-group mb-3">
                                    <label>Series No</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="seriesNo"
                                        value={item?.seriesNo}
                                        readOnly
                                    />
                                </div> */}

                                <div>
                                    <button type="button" style={{ marginRight: '10px', color: 'white' }} onClick={() => navigate(`/Dspace/Metadata/getMetadata/${item.metadataId}`)} class="btn btn-info">Full Item Page</button>
                                    <div className="mt-2">
                                        {localStorage.getItem("Role") !== "STUDENT" && (
                                            <>
                                                <button type="button" style={{ marginRight: '10px' }} onClick={() => navigate(`/DSpace/EditItem/${itemId}`)} class="btn btn-primary">Edit</button>
                                                <button type="button" style={{ marginRight: '10px' }} onClick={handleDeleteItem} class="btn btn-danger">Delete</button>
                                            </>

                                        )}
                                        {colId ? (
                                            <button
                                                type="button"
                                                style={{ marginRight: '10px' }}
                                                onClick={() => navigate(`/DSpace/Collection/listItemsInCollection/${colId}`)}
                                                className="btn btn-secondary"
                                            >
                                                Back to List
                                            </button>
                                        ) : (
                                            <button
                                                type="button"
                                                style={{ marginRight: '10px' }}
                                                onClick={() => navigate(`/DSpace/Collection/listItem/-1`)}
                                                className="btn btn-secondary"
                                            >
                                                Back to List
                                            </button>
                                        )}
                                    </div>
                                </div>
                            </div>

                            <div className="col-md-7">
                                {/* <div className="form-group mb-3">
                                    <label>Identifiers</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="identifier"
                                        value={item?.metadata?.identifier}
                                        readOnly
                                    />
                                </div>
                                <div className="form-group mb-3">
                                    <label>Choose Type</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="type"
                                        value={item?.metadata.type}
                                        readOnly
                                    />

                                </div>
                                <div className="form-group mb-3">
                                    <label>Keywords</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="subjectKeywords"
                                        value={item?.itemKeywords[0]?.keyword?.keywordName}
                                        readOnly
                                    />
                                </div>
                                <div className="form-group mb-3">
                                    <label>Abstract</label>
                                    <textarea
                                        className="form-control"
                                        name="abstract"
                                        value={item?.abstract}
                                        rows={4}
                                        readOnly
                                    />
                                </div>
                                <div className="form-group mb-3">
                                    <label>Sponsors</label>
                                    <textarea
                                        className="form-control"
                                        name="sponsors"
                                        value={item?.sponsors}
                                        rows={4}
                                        readOnly
                                    />
                                </div> */}
                                <div>
                                    <label style={{ marginRight: '10px', fontSize: '1.4rem', marginBottom: '3px' }}>Description</label>
                                    <div className="form-group mb-3 d-flex flex-column">
                                        {item.description}
                                    </div>
                                </div>
                                {/* <div className="form-group mb-3">
                                    <label>Description</label>
                                    <textarea
                                        className="form-control"
                                        name="description"
                                        value={item?.metadata?.description}
                                        rows={4}
                                        readOnly
                                    />
                                </div> */}
                                {/* <div className="form-group mb-3">
                                    <label>Language</label>

                                    <input
                                        type="text"
                                        className="form-control"
                                        name="language"
                                        value={item?.language?.languageName}
                                        readOnly
                                    />
                                </div> */}
                                <div className="form-group mb-3">
                                    {/* <label style={{ marginRight: '10px' }}>Upload File</label> */}
                                    <div className="custom-file-uploadx">
                                        {/* <input name="multipleFileUploads" type="file" multiple readOnly /> */}
                                        {/* {multipleFileUploads.length > 0 && ( */}
                                        <div>
                                            {/* {multipleFileUploads.map((file, index) => ( */}
                                            <div>Files:</div>
                                            <div>
                                                {item?.file && item.file.length > 0 ? (
                                                    <div>
                                                        {item.file.filter(file => file.isActive).map((file, index) => (
                                                            <div className="file-item" key={index} style={{ display: 'flex', alignItems: 'center', marginBottom: '10px' }}>
                                                                <div
                                                                    className="fileName"
                                                                    onClick={() => handleClickFile(file.fileKeyId + "@" + itemId)}
                                                                    style={{ cursor: 'pointer', color: '#207698', marginRight: '10px', flexGrow: 1 }}
                                                                >
                                                                    {file.fileName}
                                                                </div>
                                                                {/* <ListItemIcon className={classes.listItemIcon} onClick={() => handleDownloadFile(file.fileKeyId)} style={{ cursor: 'pointer' }}> */}
                                                                {localStorage.getItem("Role") === "ADMIN" || localStorage.getItem("Role") === "STAFF" ? (
                                                                    <>
                                                                        <VerticalAlignBottom className="download-icon" onClick={() => handleDownloadFile(file.fileKeyId, file.fileName)} style={{ cursor: 'pointer' }} />
                                                                    </>
                                                                ) : null}
                                                                {/* </ListItemIcon> */}
                                                            </div>
                                                        ))}
                                                    </div>
                                                ) : (
                                                    <p>No file available</p>
                                                )}

                                            </div>
                                            {/* ))} */}
                                        </div>
                                        {/* )} */}
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style={{ marginTop: '200px' }}></div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ItemDetails;
