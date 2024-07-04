import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link, useParams, useNavigate } from 'react-router-dom';
import Header from '../../components/Header/Header';


const EditItem = () => {
    // const { collectionId } = useParams();

    const { itemId } = useParams(); // Get the ID from the URL
    const [item, setItem] = useState(null);
    const [authors, setAuthors] = useState([{ author: '' }]);
    const navigate = useNavigate();
    const [files, setFiles] = useState([]);

    const handleChange2 = (index, event) => {
        const newAuthors = [...authors];
        newAuthors[index][event.target.name] = event.target.value;
        setAuthors(newAuthors);
        const { name, value, type, checked } = event.target;
        setitemData({
            ...itemData,
            [name]: newAuthors,
        });

    };
    useEffect(() => {
        if (item?.file) {
            setFiles(item.file);
        }
    }, [item]);

    const handleActiveChange = (fileId) => {
        // const updatedFiles = files.map(file => {
        //     if (file.fileId === fileId) {
        //         return { ...file, isActive: !file.isActive };
        //     }
        //     return file;
        // });
        // setFiles(updatedFiles);
    };

    const handleAddAuthor = (event) => {
        setAuthors([...authors, { author: '' }]);
        // const { name, value, type, checked } = event.target;
        //     setitemData({
        //         ...itemData,
        //         [name]: setAuthors([...authors, { author: '' }]),
        //     });
    };

    const handleRemoveAuthor = (index, event) => {
        const newAuthors = authors.filter((_, i) => i !== index);
        setAuthors(newAuthors);
        console.log(event);
        setitemData({
            ...itemData,
            ['authors']: newAuthors,
        });

    };


    const [otherTitles, setOtherTitles] = useState([{ otherTitle: '' }]); // Mảng lưu các Other Title
    const handleOtherTitleChange = (index, event) => {
        const newOtherTitles = [...otherTitles];
        newOtherTitles[index][event.target.name] = event.target.value;
        setOtherTitles(newOtherTitles);

        const { name, value, type, checked } = event.target;
        setitemData({
            ...itemData,
            [name]: newOtherTitles,
        });
    };

    const handleAddOtherTitles = (event) => {
        setOtherTitles([...otherTitles, { otherTitle: '' }]);

    };

    const handleRemoveOtherTitles = (index) => {
        const newOtherTitles = otherTitles.filter((_, i) => i !== index);
        setOtherTitles(newOtherTitles);
        setitemData({
            ...itemData,
            ['otherTitles']: newOtherTitles,
        });
    };
    // console.log(otherTitles);

    const [keywords, setKeywords] = useState('');

    const handleChangeKeywords = (event) => {
        setKeywords(event.target.value.split(','));
        const { name, value, type, checked } = event.target;
        console.log(name);
        setitemData({
            ...itemData,
            [name]: event.target.value.split(','),
        });
    };
    // console.log(keywords);
    // console.log(otherTitles);
    const [selectedValueType, setSelectedValueType] = useState('');
    const [selectedValueLanguage, setSelectedValueLanguage] = useState('');
    const [itemData, setitemData] = useState({
        author: '',
        title: '',
        otherTitle: '',
        dateOfIssue: '',
        publisher: '',
        citation: '',
        seriesNo: '',
        identifiers: '',
        type: '',
        language: '',
        subjectKeywords: '',
        abstract: '',
        sponsors: '',
        description: ''
    });


    useEffect(() => {
        // Fetch the item data from your API using the id from the URL
        axios.get(`https://localhost:7200/api/Item/getItem/${itemId}`) // Replace with your actual API endpoint
            .then(response => {
                setItem(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the item!', error);
            });
    }, [itemId]);

    useEffect(() => {
        if (item) {

            setitemData({
                author: item.authorItems[0].author.firstName,
                title: item.metadata.title,
                otherTitle: item?.otherTitle,
                dateOfIssue: item && new Date(item.dateOfIssue).toISOString().split('T')[0],
                publisher: item?.metadata?.publisher,
                citation: item?.citation,
                seriesNo: item?.seriesNo,
                identifiers: item?.metadata?.identifier,
                type: item?.metadata.type,
                language: item?.language?.languageName,
                subjectKeywords: item?.itemKeywords[0]?.keyword?.keywordName,
                abstract: item?.abstract,
                sponsors: item?.sponsors,
                description: item?.metadata?.description
            });
        }
    }, [item]);

    const [multipleFileUploads, setMultipleFileUploads] = useState([]);

    // setMultipleFileUploads(item?.file[0]);

    const handleShowFile = (event) => {
        setMultipleFileUploads(item?.file[0]);
    };

    const handleFileUpload = (event) => {
        const files = Array.from(event.target.files);
        const newFiles = [...multipleFileUploads, ...files];
        setMultipleFileUploads(newFiles);
        const { name, value, type, checked } = event.target;
        newFiles.forEach(file => {
            setitemData({
                ...itemData,
                [name]: file,
            });
        });

    };
    console.log(multipleFileUploads);
    const handleFileRemove = (indexToRemove) => {
        const newFiles = multipleFileUploads.filter((_, index) => index !== indexToRemove);
        setMultipleFileUploads(newFiles);
        newFiles.forEach(file => {
            setitemData({
                ...itemData,
                ['multipleFileUploads']: file,
            });
        });
    };
    const getFileExtension = (filename) => {
        return filename.split('.').pop();
    };

    const getBitstreamFormat = (extension) => {
        switch (extension.toLowerCase()) {
            case 'docx':
                return 'Microsoft Word XML';
            // Thêm các định dạng tệp tin khác nếu cần
            default:
                return 'Unknown';
        }
    };
    const formatFileSize = (size) => {
        if (size === 0) return '0 Bytes';
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
        const i = Math.floor(Math.log(size) / Math.log(k));
        return parseFloat((size / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
    };

    const [parentCommunities, setParentCommunities] = useState([]); // State for parent communities
    const [successMessage, setSuccessMessage] = useState(''); // State for success message




    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        setSelectedValueType(value);
        // setSelectedValueLanguage(value);
        if (name === 'type' && newValue === '') {
            // Nếu người dùng không chọn parent community, thiết lập parentCommunityId thành null
            setitemData({
                ...itemData,
                [name]: null,
            });

        } else {
            setitemData({
                ...itemData,
                [name]: newValue,
            });
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(itemData);
        // Send data to your API
        axios.put(`https://localhost:7200/api/Item/updateItem/${itemId}`, itemData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        }) // Replace with your actual API endpoint
            .then(response => {
                console.log('Item created successfully!', response.data);
                // Set success message
                setSuccessMessage('Item created successfully!');

                // history.push('/Dspace/Communities/ListOfCommunities'); // Redirect to the communities list
            })
            .catch(error => {
                console.error('There was an error creating the Item!', error);
            });
    };
    const handleClickFile = (id) => {
        // const fileUrl = item.file[0].fileUrl; // Lấy URL của file PDF
        navigate(`/DSpace/PdfViewer/${id}`);
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <h1 className="mb-4 text-center">Update Item</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>} {/* Success message */}
                <div className="row justify-content-center">
                    <div className="col-md-11">
                        <form onSubmit={handleSubmit}>
                            <div className="row">
                                <div className="col-md-6">
                                    {/* {authors.map((author, index) => ( */}
                                    <div>
                                        <label style={{ marginRight: '10px' }}>Author </label>

                                        <div
                                            // key={index} 
                                            className="form-group mb-3 d-flex align-items-center">
                                            <input
                                                type="text"
                                                className="form-control"
                                                name="author"
                                                value={itemData.author}
                                                onChange={handleChange}
                                                style={{ marginRight: '10px' }}
                                                required
                                            />
                                            {/* {authors.length !== 1 && ( // Kiểm tra nếu index không phải là 0 thì hiển thị nút Delete
                                                    <button
                                                        type="button"
                                                        className="btn btn-danger d-flex align-items-center"
                                                        onClick={() => handleRemoveAuthor(index)}
                                                    >
                                                        Delete
                                                    </button>
                                                )} */}
                                        </div>
                                    </div>
                                    {/* ))} */}
                                    {/* <button style={{ marginBottom: '30px' }}
                                        type="button"
                                        className="btn btn-primary d-flex align-items-center"
                                        onClick={handleAddAuthor}
                                    >
                                        Add More
                                    </button> */}
                                    <div className="form-group mb-3">
                                        <label>Title</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="title"
                                            value={itemData.title}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>
                                    {/* {otherTitles.map((title, index) => ( */}
                                    <div>
                                        <label style={{ marginRight: '10px' }}>Other Title </label>

                                        <div
                                            // key={index} 
                                            className="form-group mb-3 d-flex align-items-center">

                                            <input
                                                type="text"
                                                className="form-control"
                                                name="otherTitle"
                                                value={itemData.otherTitle}
                                                onChange={handleChange}
                                                style={{ marginRight: '10px' }}
                                                required
                                            />
                                            {/* {otherTitles.length !== 1 && ( // Kiểm tra nếu index không phải là 0 thì hiển thị nút Delete
                                                    <button
                                                        type="button"
                                                        className="btn btn-danger d-flex align-items-center"
                                                        onClick={() => handleRemoveOtherTitles(index)}
                                                    >
                                                        Delete
                                                    </button>
                                                )} */}
                                        </div>
                                    </div>
                                    {/* ))} */}
                                    {/* <button style={{ marginBottom: '30px' }}
                                        type="button"
                                        className="btn btn-primary d-flex align-items-center"
                                        onClick={handleAddOtherTitles}
                                    >
                                        Add More
                                    </button> */}

                                    <div className="form-group mb-3">
                                        <label>Date of Issue *</label>
                                        <input
                                            type="date"
                                            className="form-control"
                                            name="dateOfIssue"
                                            value={itemData.dateOfIssue}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Publisher</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="publisher"
                                            value={itemData.publisher}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Citation</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="citation"
                                            value={itemData.citation}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Series No</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="seriesNo"
                                            value={itemData.seriesNo}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>


                                    <button type="submit" className="btn btn-primary mt-4">Update Item</button>
                                    <div className="d-flex justify-content-between mt-4">
                                        <button type="button" style={{marginRight: '10px'}} onClick={() => navigate(`/DSpace/Collection/listItem/-1`)} class="btn btn-secondary">Back to List</button>
                                    </div>
                                </div>

                                <div className="col-md-6">
                                    <div className="form-group mb-3">
                                        <label>Identifiers</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="identifiers"
                                            value={itemData.identifiers}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Choose Type</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="type"
                                            value={itemData.type}
                                            onChange={handleChange}
                                            required
                                        />
                                        {/* <select
                                            className="form-control"
                                            name="type"
                                            value={selectedValueType}
                                            onChange={handleChange}

                                        >
                                            
                                            <option value=''>Select Type</option>
                                            {parentCommunities.map(parent => (
                                                <option key={parent.communityId} value={parent.communityId}>
                                                    {parent.communityName}
                                                </option>
                                            ))
                                            }
                                        </select> */}
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Keywords</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="subjectKeywords"
                                            value={itemData.subjectKeywords}
                                            onChange={handleChange}
                                            placeholder="Enter keywords separated by comma"
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Abstract</label>
                                        <textarea
                                            className="form-control"
                                            name="abstract"
                                            value={itemData.abstract}
                                            onChange={handleChange}
                                            rows={4}
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Sponsors</label>
                                        <textarea
                                            className="form-control"
                                            name="sponsors"
                                            value={itemData.sponsors}
                                            onChange={handleChange}
                                            rows={4}
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Description</label>
                                        <textarea
                                            className="form-control"
                                            name="description"
                                            value={itemData.description}
                                            onChange={handleChange}
                                            rows={4}
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Language</label>
                                        {/* <select
                                            className="form-control"
                                            name="language"
                                            value={selectedValueLanguage}
                                            onChange={handleChange3}

                                        >
                                            <option value=''>Select Language</option>
                                            {parentCommunities.map(parent => (
                                                <option key={parent.communityId} value={parent.communityId}>
                                                    {parent.communityName}
                                                </option>
                                            ))
                                            }
                                        </select> */}
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="language"
                                            value={itemData.language}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>
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
                                                                style={{ cursor: 'pointer', color: 'blue', marginRight: '10px', flexGrow: 1 }}
                                                            >
                                                                {file.fileName}
                                                            </div>
                                                            {/* <ListItemIcon className={classes.listItemIcon} onClick={() => handleDownloadFile(file.fileKeyId)} style={{ cursor: 'pointer' }}> */}
                                                            {/* <VerticalAlignBottom className="download-icon" onClick={() => handleDownloadFile(file.fileKeyId)} style={{ cursor: 'pointer' }} /> */}
                                                            {/* </ListItemIcon> */}
                                                            <input
                                                                type="checkbox"
                                                                checked={file.isActive}
                                                                onChange={() => handleActiveChange(file.fileId)}
                                                            // style={{ marginLeft: '10px' }}
                                                            />
                                                        </div>
                                                    ))}
                                                </div>
                                            ) : (
                                                <p>No file available</p>
                                            )}
                                        </div>
                                        {/* ))} */}
                                    </div>
                                    <div className="form-group mb-3">
                                        {/* <label style={{ marginRight: '10px' }}>Upload File</label> */}
                                        <label className="custom-file-upload">
                                            <input name="multipleFileUploads" type="file" multiple onChange={handleFileUpload} />
                                            {multipleFileUploads.length > 0 && (
                                                <div>
                                                    {multipleFileUploads.map((file, index) => (
                                                        <div key={index} style={{ display: 'flex', alignItems: 'center', marginTop: '10px' }}>
                                                            <p style={{ margin: 0 }}>{file.name} ({formatFileSize(file.size)})</p>
                                                            <p style={{ margin: 0, marginLeft: '10px' }}>Bitstream format: {getBitstreamFormat(getFileExtension(file.name))}</p>
                                                            <button
                                                                style={{ marginLeft: '10px' }}
                                                                onClick={() => handleFileRemove(index)}
                                                            >
                                                                Delete
                                                            </button>
                                                        </div>
                                                    ))}
                                                </div>
                                            )}
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div style={{ marginTop: '200px' }}></div>
                        </form>
                    </div>
                </div>
            </div>

        </div>
    );
};

export default EditItem;
