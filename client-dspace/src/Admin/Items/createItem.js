import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Link, useParams, useNavigate } from 'react-router-dom';
import Header from '../../components/Header/Header';
import Select from 'react-select';
import { WithContext as ReactTags } from 'react-tag-input';
import { TagsInput } from "react-tag-input-component";
// import './CollectionList.css';
// import * as React from 'react';
import { TextareaAutosize } from '@mui/base/TextareaAutosize';
import { styled } from '@mui/system';
import {
    Autocomplete,
    TextField,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogContentText,
    DialogActions,
    Button,
} from '@mui/material';
import { createFilterOptions } from '@mui/material/Autocomplete';

const CreateCommunity = () => {
    const [identifierFields, setIdentifierFields] = useState([{ type: "", value: "" }]);
    const { collectionId } = useParams();
    const navigate = useNavigate();
    const token = localStorage.getItem('Token');

    const header = `Bearer ${token}`;
    const [type, setType] = useState([]);


    const [selectedValueType, setSelectedValueType] = useState('');


    const [authors, setAuthors] = useState([]);
    const [selectedAuthors, setSelectedAuthors] = useState([]);
    useEffect(() => {
        axios.get('https://localhost:7200/api/Author/getListOfAuthors')
            .then(response => {
                setAuthors(response.data);
            })
            .catch(error => {
                console.error('There was an error fetching the authors!', error);
            });
    }, []);


    const filter = createFilterOptions();

    const AuthorSelect = ({ authors, selectedAuthors, handleAuthorSelectChange, setAuthors }) => {
        const [value, setValue] = useState([]);
        const [open, toggleOpen] = useState(false);
        const [dialogValue, setDialogValue] = useState({
            fullName: '',
            jobTitle: '',
            uri: '',
            type: '',
        });
        const [errors, setErrors] = useState({});

        const options = authors.map(author => ({
            value: author.authorId,
            label: `${author.fullName}`,
        }));

        const selectedOptions = options.filter(option =>
            selectedAuthors.includes(option.value)
        );

        const handleClose = () => {
            setDialogValue({
                fullName: '',
                jobTitle: '',
                uri: '',
                type: '',
            });
            setErrors({});
            toggleOpen(false);
        };

        const handleSubmitAuthor = (event) => {
            event.preventDefault();

            // Validate fields
            const newErrors = {};
            if (!dialogValue.fullName) newErrors.firstName = 'Full name is required';
            if (!dialogValue.jobTitle) newErrors.jobTitle = 'Job title is required';
            if (!dialogValue.uri) newErrors.uri = 'URI is required';
            if (!dialogValue.type) newErrors.type = 'Type is required';

            if (Object.keys(newErrors).length > 0) {
                setErrors(newErrors);
                return;
            }

            const newAuthor = {
                fullName: dialogValue.fullName,
                jobTitle: dialogValue.jobTitle,
                dateAccessioned: new Date().toISOString(),
                dateAvailable: new Date().toISOString(),
                uri: dialogValue.uri,
                type: dialogValue.type,
            };

            axios.post('https://localhost:7200/api/Author/createAuthor', newAuthor)
                .then(response => {
                    const createdAuthor = response.data;

                    // Update authors state with new author
                    setAuthors(prevAuthors => [...prevAuthors, createdAuthor]);

                    // Update selectedAuthors state to include new author's id
                    setSelectedAuthors(prevSelectedAuthors => [
                        ...prevSelectedAuthors,
                        createdAuthor.authorId,
                    ]);

                    // Update itemData.author to include new author's name
                    setitemData(prevData => ({
                        ...prevData,
                        author: [
                            ...prevData.author,
                            `${createdAuthor.fullName}`
                        ],
                    }));

                    handleClose();
                })
                .catch(error => {
                    console.error('There was an error creating the author!', error);
                });
        };

        return (
            <React.Fragment>
                <Autocomplete
                    multiple
                    value={selectedOptions}
                    onChange={(event, newValue) => {
                        if (newValue && newValue.length && newValue[newValue.length - 1].inputValue) {
                            toggleOpen(true);
                            setDialogValue({
                                fullName: newValue[newValue.length - 1].inputValue,
                                jobTitle: '',
                                uri: window.location.origin + "/Dspace/Author/AuthorDetails/",
                                type: '',
                            });
                        } else {
                            handleAuthorSelectChange(newValue.map(option => option.value));
                        }
                    }}
                    filterOptions={(options, params) => {
                        const filtered = filter(options, params);

                        if (params.inputValue !== '') {
                            filtered.push({
                                inputValue: params.inputValue,
                                label: `Add "${params.inputValue}"`,
                            });
                        }

                        return filtered;
                    }}
                    options={options}
                    getOptionLabel={(option) => {
                        if (typeof option === 'string') {
                            return option;
                        }
                        if (option.inputValue) {
                            return option.inputValue;
                        }
                        return option.label;
                    }}
                    renderOption={(props, option) => (
                        <li {...props}>
                            {option.label}
                        </li>
                    )}
                    sx={{ width: '100%' }}
                    renderInput={(params) => (
                        <TextField {...params} label="Search and select authors" placeholder="Search and select authors..." />
                    )}
                />
                <Dialog open={open} onClose={handleClose}>
                    <form>
                        <DialogTitle>Add a new author</DialogTitle>
                        <DialogContent>
                            <DialogContentText>
                                {/* Did you miss any author in our list? Please, add them! */}
                            </DialogContentText>
                            <TextField
                                autoFocus
                                margin="dense"
                                id="fullName"
                                value={dialogValue.fullName}
                                onChange={(event) => setDialogValue({ ...dialogValue, fullName: event.target.value })}
                                label="Full Name"
                                type="text"
                                fullWidth
                                error={!!errors.fullName}
                                helperText={errors.fullName}
                            />
                            {/* <TextField
                                margin="dense"
                                id="lastName"
                                value={dialogValue.lastName}
                                onChange={(event) => setDialogValue({ ...dialogValue, lastName: event.target.value })}
                                label="Last Name"
                                type="text"
                                fullWidth
                                error={!!errors.lastName}
                                helperText={errors.lastName}
                            /> */}
                            {/* <TextField
                                margin="dense"
                                id="email"
                                value={dialogValue.email}
                                onChange={(event) => setDialogValue({ ...dialogValue, email: event.target.value })}
                                label="Email"
                                type="email"
                                fullWidth
                                error={!!errors.email}
                                helperText={errors.email}
                            /> */}
                            {/* <TextField
                                margin="dense"
                                id="birthDate"
                                value={dialogValue.birthDate}
                                onChange={(event) => setDialogValue({ ...dialogValue, birthDate: event.target.value })}
                                label="Birth Date"
                                type="date"
                                fullWidth
                                InputLabelProps={{ shrink: true }}
                                error={!!errors.birthDate}
                                helperText={errors.birthDate}
                            /> */}
                            <TextField
                                margin="dense"
                                id="jobTitle"
                                value={dialogValue.jobTitle}
                                onChange={(event) => setDialogValue({ ...dialogValue, jobTitle: event.target.value })}
                                label="Job Title"
                                type="text"
                                fullWidth
                                error={!!errors.jobTitle}
                                helperText={errors.jobTitle}
                            />
                            <TextField
                                margin="dense"
                                id="uri"
                                value={dialogValue.uri}
                                onChange={(event) => setDialogValue({ ...dialogValue, uri: event.target.value })}
                                label="URI"
                                type="text"
                                fullWidth
                                error={!!errors.uri}
                                helperText={errors.uri}
                                hidden
                            />
                            <TextField
                                margin="dense"
                                id="type"
                                value={dialogValue.type}
                                onChange={(event) => setDialogValue({ ...dialogValue, type: event.target.value })}
                                label="Type"
                                type="text"
                                fullWidth
                                error={!!errors.type}
                                helperText={errors.type}
                            />
                        </DialogContent>
                        <DialogActions>
                            <Button onClick={handleClose}>Cancel</Button>
                            <Button type="submit" onClick={(e) => {
                                e.stopPropagation();
                                handleSubmitAuthor(e);
                            }}>Add</Button>
                        </DialogActions>
                    </form>
                </Dialog>
            </React.Fragment>
        );
    };
    const handleAuthorSelectChange = (selectedAuthorIds) => {
        setSelectedAuthors(selectedAuthorIds);
        const selectedAuthorNames = selectedAuthorIds.map(authorId => {
            const author = authors.find(author => author.authorId === authorId);
            return author ? `${author.fullName}` : '';
        });
        setitemData(prevData => ({
            ...prevData,
            author: selectedAuthorNames,
        }));
    };
    const types = [
        'Animation', 'Article', 'Book', 'Book chapter', 'Dataset', 'Learning Object',
        'Image', 'Image, 3-D', 'Map', 'Musical Score', 'Plan or blueprint', 'Preprint', 'Presentation', 'Recording, acoustical', 'Recording, musical', 'Recording, oral'
        , 'Software', 'Technical Report', 'Thesis', 'Video', 'Working Paper', 'Other'
    ];
    const [selectedTypes, setSelectedTypes] = useState([]);
    const TypesSelect = ({ types, selectedTypes, setSelectedTypes }) => {
        const options = types.map(type => ({
            value: type,
            label: type
        }));

        const selectedOptions = options.filter(option =>
            selectedTypes.includes(option.value)
        );

        return (
            <Select
                isMulti
                options={options}
                value={selectedOptions}
                onChange={selectedOptions => handleTypeSelectChange(selectedOptions ? selectedOptions.map(option => option.value) : [])}
                placeholder="Search and select types"
                isClearable
                styles={{
                    container: (base) => ({ ...base, width: '100%' }),
                    menu: (provided) => ({
                        ...provided,
                        maxHeight: '200px',
                        overflowY: 'auto',
                    }),
                    menuList: (provided) => ({
                        ...provided,
                        maxHeight: '200px',
                        overflowY: 'auto',
                    }),
                }}
            />
        );
    };
    const handleTypeSelectChange = (selectedTypes) => {
        setSelectedTypes(selectedTypes);
        setitemData(prevData => ({
            ...prevData,
            type: selectedTypes
        }));
    };





    const languages = [
        'English (United States)', 'English', 'Spanish', 'German', 'French', 'Italian',
        'Japanese', 'Chinese', 'Portuguese', 'Turkish', '(Other)'
    ];
    const [selectedLanguage, setSelectedLanguage] = useState([]);
    const LanguageSelect = ({ languages, selectedLanguage, setSelectedLanguage }) => {
        const options = languages.map(language => ({
            value: language,
            label: language
        }));

        return (
            <Select
                options={options}
                value={options.find(option => option.value === selectedLanguage)} // selectedLanguage là một mảng với một phần tử duy nhất
                onChange={selectedLanguage => handleLanguagesSelectChange(selectedLanguage ? selectedLanguage.value : null)} // Chỉ trả về giá trị của option được chọn
                placeholder="Search and select language..."
                isClearable
                isMulti={false}
                styles={{
                    container: (base) => ({ ...base, width: '100%' }),
                    menu: (provided) => ({
                        ...provided,
                        maxHeight: '200px',
                        overflowY: 'auto',
                    }),
                    menuList: (provided) => ({
                        ...provided,
                        maxHeight: '200px',
                        overflowY: 'auto',
                    }),
                }}
            />
        );
    };
    const handleLanguagesSelectChange = (selectedLanguage) => {
        setSelectedLanguage(selectedLanguage);
        setitemData(prevData => ({
            ...prevData,
            language: selectedLanguage
        }));
    };
    console.log(selectedLanguage);
    const handleKeywordsChange = (keywords) => {
        setitemData(prevData => ({
            ...prevData,
            subjectKeywords: keywords
        }));
    };
    const handleOtherTitleChange = (otherTitle) => {
        setitemData(prevData => ({
            ...prevData,
            otherTitle: otherTitle
        }));
    };
    const handleSeriesNoChange = (seriesNo) => {
        setitemData(prevData => ({
            ...prevData,
            seriesNo: seriesNo
        }));
    };


    const [itemData, setitemData] = useState({
        author: [],
        title: '',
        otherTitle: [],
        dateOfIssue: '',
        publisher: '',
        citation: '',
        seriesNo: [],
        identifiersISBN: [],
        identifiersISSN: [],
        identifiersSICI: [],
        identifiersISMN: [],
        identifiersOther: [],
        identifiersLCCN: [],
        identifiersURI: [],
        type: [],
        language: '',
        subjectKeywords: [],
        abstract: '',
        sponsors: '',
        description: '',
        collectionId: collectionId
    });
    const [successMessage, setSuccessMessage] = useState(''); // State for success message

    console.log(itemData.author);
    console.log(selectedAuthors);
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        setSelectedValueType(value);
        // setSelectedValueLanguage(value);
        if (name === 'type') {
            setitemData({
                ...itemData,
                [name]: e.target.value,
            });
        } else if (name === 'identifiersISBN') {
            setitemData(prevData => ({
                ...prevData,
                identifiersISBN: [newValue]
            }));
        } else if (name === 'identifiersISSN') {
            itemData.identifiersISSN.push(newValue)
        } else if (name === 'identifiersSICI') {
            itemData.identifiersSICI.push(newValue)
        } else if (name === 'identifiersISMN') {
            itemData.identifiersISMN.push(newValue)
        } else if (name === 'identifiersOther') {
            itemData.identifiersOther.push(newValue)
        } else if (name === 'identifiersLCCN') {
            itemData.identifiersLCCN.push(newValue)
        } else if (name === 'identifiersURI') {
            itemData.identifiersURI.push(newValue)
        } else {
            setitemData({
                ...itemData,
                [name]: newValue,
            });
        }
    };
    console.log(itemData.title);
    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(itemData);
        identifierFields.forEach(field => {
            const key = getIdentifierKey(field.type);
            if (key) {
                itemData[key].push(field.value);
            }
        });
        // Send data to your API
        axios.post(`https://localhost:7200/api/Item/CreateSimpleItem`, itemData, {
            headers: {
                Authorization: header
            }
        }) // Replace with your actual API endpoint
            .then(response => {
                console.log('Item created successfully!', response.data);
                // Set success message
                setSuccessMessage('Item created successfully!');
                //navigate(`/DSpace/ItemDetails/${itemId}`);
                // history.push('/Dspace/Communities/ListOfCommunities'); // Redirect to the communities list
            })
            .catch(error => {
                console.error('There was an error creating the Item!', error);
            });
    };
    const handleDelete = (i) => {
        setitemData(prevData => ({
            ...prevData,
            otherTitle: prevData.otherTitle.filter((_, index) => index !== i)
        }));
    };

    const handleAddition = (tag) => {
        setitemData(prevData => ({
            ...prevData,
            otherTitle: [...prevData.otherTitle, tag.text]
        }));
    };


    // Thêm một cặp select và input mới
    const addIdentifierField = () => {
        setIdentifierFields([...identifierFields, { type: "", value: "" }]);
    };

    // Xóa một cặp select và input
    const removeIdentifierField = (index) => {
        const newIdentifierFields = [...identifierFields];
        newIdentifierFields.splice(index, 1);
        setIdentifierFields(newIdentifierFields);
    };
    const handleChangeIdentify = (index, event) => {
        const { name, value } = event.target;
        const newIdentifierFields = [...identifierFields];
        newIdentifierFields[index] = { ...newIdentifierFields[index], [name]: value };
        setIdentifierFields(newIdentifierFields);


        console.log(itemData);
    };
    const getIdentifierKey = (type) => {
        switch (type) {
            case "ISBN":
                return "identifiersISBN";
            case "ISSN":
                return "identifiersISSN";
            case "SICI":
                return "identifiersSICI";
            case "ISMN":
                return "identifiersISMN";
            case "Other":
                return "identifiersOther";
            case "LCCN":
                return "identifiersLCCN";
            case "URI":
                return "identifiersURI";
            default:
                return null;
        }
    };

    return (
        <div>
            <Header />
            <div className="container mt-5">
                <h1 className="mb-4 text-center">Create New Item</h1>
                {successMessage && <div className="alert alert-success">{successMessage}</div>} {/* Success message */}
                <div className="row justify-content-center">
                    <div className="col-md-11">
                        <form onSubmit={handleSubmit}>
                            <div className="row">
                                <div className="col-md-6">
                                    {/* {authors.map((author, index) => ( */}
                                    <div>
                                        <label style={{ marginRight: '10px' }}>Author </label>
                                        <div className="form-group mb-3 d-flex align-items-center">
                                            <AuthorSelect
                                                authors={authors}
                                                selectedAuthors={selectedAuthors}
                                                handleAuthorSelectChange={handleAuthorSelectChange}
                                                setAuthors={setAuthors}  // Truyền setAuthors vào AuthorSelect
                                            />
                                        </div>
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Title</label>
                                        <TextareaAutosize
                                            className="form-control"
                                            aria-label="empty textarea"
                                            placeholder="Enter title"
                                            name="title"
                                            value={itemData.title}
                                            onChange={handleChange}
                                            required
                                            style={{
                                                boxSizing: 'border-box',
                                                width: '100%',
                                                padding: '8px',
                                                borderRadius: '4px',
                                                border: '1px solid #ccc',
                                            }}
                                        />
                                        {/* <input
                                            type="text"
                                            className="form-control"
                                            name="title"
                                            value={itemData.title}
                                            onChange={handleChange}
                                            required
                                        /> */}
                                    </div>
                                    <div>
                                        <label style={{ marginRight: '10px' }}>Other Title: Enter other titles separated by Enter Button </label>

                                        {/* <div
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

                                        </div> */}
                                        <div className="form-group mb-3 align-items-center">
                                            <TagsInput
                                                name='otherTitle'
                                                value={itemData.otherTitle}
                                                onChange={handleOtherTitleChange}
                                                placeHolder="Enter other titles separated by Enter Button"
                                            />
                                        </div>

                                    </div>

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
                                        <TextareaAutosize
                                            className="form-control"
                                            aria-label="empty textarea"
                                            placeholder="Enter publisher"
                                            name="publisher"
                                            value={itemData.publisher}
                                            onChange={handleChange}
                                            required
                                            style={{
                                                boxSizing: 'border-box',
                                                width: '100%',
                                                padding: '8px',
                                                borderRadius: '4px',
                                                border: '1px solid #ccc',
                                            }}
                                        />
                                        {/* <input
                                            type="text"
                                            className="form-control"
                                            name="publisher"
                                            value={itemData.publisher}
                                            onChange={handleChange}
                                            required
                                        /> */}
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Citation</label>
                                        <TextareaAutosize
                                            className="form-control"
                                            aria-label="empty textarea"
                                            placeholder="Enter citation"
                                            name="citation"
                                            value={itemData.citation}
                                            onChange={handleChange}
                                            required
                                            style={{
                                                boxSizing: 'border-box',
                                                width: '100%',
                                                padding: '8px',
                                                borderRadius: '4px',
                                                border: '1px solid #ccc',
                                            }}
                                        />
                                        {/* <input
                                            type="text"
                                            className="form-control"
                                            name="citation"
                                            value={itemData.citation}
                                            onChange={handleChange}
                                            required
                                        /> */}
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Series No: Enter Series No separated by Enter Button</label>
                                        <TagsInput
                                            name="seriesNo"
                                            value={itemData.seriesNo}
                                            onChange={handleSeriesNoChange}
                                            placeHolder="Enter SeriesNo separated by Enter Button"
                                        />
                                    </div>

                                    <div className='mt-4'>
                                        <button type="submit" className="btn btn-primary">Create Item</button>
                                        <button type="button" style={{ marginLeft: '10px' }} onClick={() => navigate(`/DSpace/Collection/listItem/-1`)} class="btn btn-secondary">Back to List</button>
                                    </div>
                                </div>

                                <div className="col-md-6">
                                    {/* <div className="form-group mb-3">
                                        <label>Identifiers</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            name="identifiersISBN"
                                            value={itemData.identifiersISBN}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div> */}
                                    <div className="form-group mb-3">
                                        <label>Identifiers</label>
                                        {identifierFields.map((field, index) => (
                                            <div key={index} className="d-flex align-items-center mb-3">
                                                <select
                                                    name="type"
                                                    value={field.type}
                                                    onChange={(e) => handleChangeIdentify(index, e)}
                                                    className="form-select me-2"
                                                    style={{ width: "135px" }} // Đặt chiều rộng của select
                                                >
                                                    <option value="">Select Type</option>
                                                    <option value="ISBN">ISBN</option>
                                                    <option value="ISSN">ISSN</option>
                                                    <option value="SICI">SICI</option>
                                                    <option value="ISMN">ISMN</option>
                                                    <option value="Other">Other</option>
                                                    <option value="LCCN">LCCN</option>
                                                    <option value="URI">URI</option>
                                                </select>
                                                <input
                                                    type="text"
                                                    className="form-control me-2"
                                                    name="value"
                                                    value={field.value}
                                                    onChange={(e) => handleChangeIdentify(index, e)}
                                                    placeholder="Enter value"
                                                    style={{ flex: 1 }} // Đặt input để dãn ra hết khoảng còn lại
                                                    required
                                                />
                                                {identifierFields.length > 1 && (
                                                    <button
                                                        type="button"
                                                        className="btn btn-danger"
                                                        onClick={() => removeIdentifierField(index)}
                                                    >
                                                        Delete
                                                    </button>
                                                )}
                                            </div>
                                        ))}
                                        <button
                                            type="button"
                                            className="btn btn-primary"
                                            onClick={addIdentifierField}
                                        >
                                            Add More
                                        </button>
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Choose Type</label>
                                        <TypesSelect
                                            name="type"
                                            types={types}
                                            selectedTypes={selectedTypes}
                                            handleTypeSelectChange={handleTypeSelectChange}
                                            required
                                        />

                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Keywords: Enter keywords separated by Enter Button</label>
                                        {/* <input
                                            type="text"
                                            className="form-control"
                                            name="subjectKeywords"
                                            value={itemData.subjectKeywords}
                                            onChange={handleChange}
                                            placeholder="Enter keywords separated by comma"
                                            required
                                        /> */}
                                        <TagsInput
                                            name="subjectKeywords"
                                            value={itemData.subjectKeywords}
                                            onChange={handleKeywordsChange}
                                            placeHolder="Enter keywords separated by Enter Button"
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
                                            placeHolder="Enter abstract"
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
                                            placeHolder="Enter sponsors"
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
                                            placeHolder="Enter description"
                                            required
                                        />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label>Language</label>

                                        <LanguageSelect
                                            name='language'
                                            languages={languages}
                                            selectedLanguage={selectedLanguage}
                                            handleLanguagesSelectChange={handleLanguagesSelectChange}
                                        />
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

export default CreateCommunity;
