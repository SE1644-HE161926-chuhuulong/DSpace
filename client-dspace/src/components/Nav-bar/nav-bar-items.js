import React, { useState, useEffect } from 'react';
import { NavLink } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import {
    List, ListItem, ListItemIcon, ListItemText, Collapse, Checkbox
} from '@material-ui/core';
import {
    ExpandLess, ExpandMore, Add as NewIcon, Edit as EditIcon, RotateLeft, Search,
    ImportExport as ImportIcon, GetApp as ExportIcon, DateRange as DateRangeIcon, Class, MenuBook, SwapVert, AssignmentInd, VpnKey,
    Search as AdminSearchIcon, Book as RegistriesIcon, RateReview,
    Assignment as CurationTaskIcon, Settings as ProcessesIcon,
    Build as WorkflowAdminIcon, LocalHospital as HealthIcon,
    NotificationImportant as SystemWideAlertIcon
} from '@material-ui/icons';
import Select from 'react-select';
import { TagsInput } from "react-tag-input-component";
import TextField from '@mui/material/TextField';

const useStyles = makeStyles({
    root: {
        display: 'flex',
        flexDirection: 'column',
        height: '100%',
        // backgroundColor: '#f5f5f5',
        // margin: '20px',
        // width: '250px', // Automatically adjust width on hover

    },
    listItem: {
        display: 'flex',
        alignItems: 'center',
        padding: '10px',
        marginBottom: '10px',
        borderRadius: '5px',
        cursor: 'pointer',
        backgroundColor: '#F8F9FA'
    },
    listItemIcon: {
        marginRight: '10px',
    },
    listItemText: {
        opacity: 1, // Show text immediately
    },

});

const ManagementMenu = ({ startDate, endDate, setStartDate, setEndDate, numPerPage, setNumPerPage, sortState, setSortState, selectedAuthors, setSelectedAuthors, selectedKeywords, setSelectedKeywords, selectedTypes, setSelectedTypes, searchQuery, setSearchQuery, filterSubmit }) => {
    const classes = useStyles();
    const [openDate, setopenDate] = useState(false);
    const [openType, setOpenType] = useState(false);
    const [openNumPerPage, setOpenNumPerPage] = useState(false);
    const [openSort, setOpenSort] = useState(false);
    const [openAuthors, setOpenAuthors] = useState(false);
    const [openPublishers, setOpenPublishers] = useState(false);
    const [openKeywords, setOpenKeywords] = useState(false);
    // const [searchQuery, setSearchQuery] = useState('');
    const [authors, setAuthors] = useState([]);
    const [keywords, setKeywords] = useState([]);
    // const [selectedAuthors, setSelectedAuthors] = useState([]);

    useEffect(() => {
        async function fetchAuthors() {
            const response = await fetch('https://localhost:7200/api/Author/getListOfAuthors');
            const data = await response.json();
            setAuthors(data);
        }
        fetchAuthors();
    }, []);
    useEffect(() => {
        async function fetchKeywords() {
            const response = await fetch('https://localhost:7200/api/Author/getListOfAuthors');
            const data = await response.json();
            setKeywords(data);
        }
        fetchKeywords();
    }, []);
    // const [selectedValue, setSelectedValue] = useState('');

    // const [startDate, setStartDate] = useState(null);
    // const [endDate, setEndDate] = useState(null);
    const handleDateClick = () => {
        setopenDate(!openDate);
    };
    const handleTypeClick = () => {
        setOpenType(!openType);
    };
    const handleNumClick = () => {
        setOpenNumPerPage(!openNumPerPage);
    };
    const handleSortClick = () => {
        setOpenSort(!openSort);
    };
    const handleAuthorsClick = () => {
        setOpenAuthors(!openAuthors);
    };
    const handlePublishersClick = () => {
        setOpenPublishers(!openPublishers);
    };
    const handleKeywordsClick = () => {
        setOpenKeywords(!openKeywords);
    };
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        setNumPerPage(e.target.value)
    };
    const handleSortChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        setSortState(e.target.value)
    };
    const handleAuthorsChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        // setSortState(e.target.value)
    };
    const handleKeywordsChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        // setSortState(e.target.value)
    };
    const handleAuthorCheckboxChange = (event, authorId) => {
        if (event.target.checked) {
            setSelectedAuthors([...selectedAuthors, authorId]);
        } else {
            setSelectedAuthors(selectedAuthors.filter((id) => id !== authorId));
        }
    };
    // console.log(selectedAuthors);
    const handlePublishersChange = (e) => {
        setSearchQuery(e.target.value);
    };
    const AuthorSelect = ({ authors, selectedAuthors, handleAuthorSelectChange }) => {
        const options = authors.map(author => ({
            value: author.fullName,
            label: author.fullName
            //  +' '+ author.lastName
        }));

        const selectedOptions = options.filter(option =>
            selectedAuthors.includes(option.value)
        );

        return (
            <Select
                isMulti
                options={options}
                value={selectedOptions}
                onChange={selectedOptions => handleAuthorSelectChange(selectedOptions ? selectedOptions.map(option => option.value) : [])}
                placeholder="Search and select authors..."
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
    const handleAuthorSelectChange = (selectedAuthorIds) => {
        setSelectedAuthors(selectedAuthorIds);
    };
    console.log(selectedAuthors);

    const resetFilters = () => {
        setStartDate(null);
        setEndDate(null);
        setSelectedAuthors([]);
        setSelectedKeywords([]);
        setSearchQuery('');
        setSelectedTypes([]);
        setOpenKeywords(false);
        setOpenPublishers(false);
        setOpenAuthors(false);
        setOpenType(false);
        setopenDate(false);

    };
    const types = [
        'Animation', 'Article', 'Book', 'Book chapter', 'Dataset', 'Learning Object',
        'Image', 'Image, 3-D', 'Map', 'Musical Score', 'Plan or blueprint', 'Preprint', 'Presentation', 'Recording, acoustical', 'Recording, musical', 'Recording, oral'
        , 'Software', 'Technical Report', 'Thesis', 'Video', 'Working Paper', 'Other'
    ];
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
    };


    const KeywordSelect = ({ keywords, selectedKeywords, handleKeywordSelectChange }) => {
        const options = keywords.map(keyword => ({
            value: keyword.authorId,
            label: keyword.firstName
        }));

        const selectedOptions = options.filter(option =>
            selectedKeywords.includes(option.value)
        );

        return (
            <Select
                isMulti
                options={options}
                value={selectedOptions}
                onChange={selectedOptions => handleKeywordSelectChange(selectedOptions ? selectedOptions.map(option => option.value) : [])}
                placeholder="Select keywords..."
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
    const handleKeywordSelectChange = (newKeywords) => {
        setSelectedKeywords(newKeywords);
    };
    console.log(searchQuery);
    return (
        <div className={classes.root}>
            <h2 style={{ color: '#757575', marginBottom: '' }}>Filters</h2>
            <List>
                <ListItem button onClick={handleDateClick} className={classes.listItem}>
                    <ListItemIcon className={classes.listItemIcon}>
                        <DateRangeIcon />
                    </ListItemIcon>
                    <ListItemText style={{ marginTop: '' }} primary="Date" className={classes.listItemText} />
                    {openDate ? <ExpandLess /> : <ExpandMore />}
                </ListItem>
                <Collapse in={openDate} timeout="auto" unmountOnExit>
                    <List component="div" disablePadding>
                        <ListItem>
                            <ListItemIcon className='col-md-5'>
                                Start Date:
                            </ListItemIcon>
                            <ListItemText>
                                <input
                                    type="date"
                                    value={startDate ? startDate.toISOString().slice(0, 10) : ''}
                                    onChange={(e) => setStartDate(e.target.value ? new Date(e.target.value) : null)}
                                />
                            </ListItemText>
                        </ListItem>
                        <ListItem>
                            <ListItemIcon className='col-md-5'>
                                End Date:
                            </ListItemIcon>
                            <ListItemText>

                                <input
                                    type="date"
                                    value={endDate ? endDate.toISOString().slice(0, 10) : ''}
                                    onChange={(e) => setEndDate(e.target.value ? new Date(e.target.value) : null)}
                                />
                            </ListItemText>
                        </ListItem>
                    </List>
                </Collapse>



                {/* ====================================================== */}

                <ListItem button onClick={handleAuthorsClick} className={classes.listItem}>
                    <ListItemIcon className={classes.listItemIcon}>
                        <AssignmentInd />
                    </ListItemIcon>
                    <ListItemText primary="Authors" className={classes.listItemText} />
                    {openAuthors ? <ExpandLess /> : <ExpandMore />}
                </ListItem>


                <Collapse in={openAuthors} timeout="auto" unmountOnExit>

                    <div style={{ padding: '0 16px' }}>
                        {/* <List component="div" disablePadding> */}
                        <AuthorSelect
                            authors={authors}
                            selectedAuthors={selectedAuthors}
                            handleAuthorSelectChange={handleAuthorSelectChange}
                        />
                        {/* </List> */}
                    </div>

                </Collapse>
                {/* ====================================================== */}

                <ListItem button onClick={handlePublishersClick} className={classes.listItem}>
                    <ListItemIcon className={classes.listItemIcon}>
                        <RateReview />
                    </ListItemIcon>
                    <ListItemText primary="Publishers" className={classes.listItemText} />
                    {openPublishers ? <ExpandLess /> : <ExpandMore />}
                </ListItem>
                <Collapse in={openPublishers} timeout="auto" unmountOnExit>
                    <List component="div" disablePadding>
                        <ListItem>
                            <ListItemText>
                                {/* <select
                                    className="form-control"
                                    name="parentCommunityId"
                                    value={''}
                                    onChange={handlePublishersChange}
                                >
                                    <option value=''></option>
                                </select> */}
                                <TextField id="outlined-basic" label="Search publisher" variant="outlined" value={searchQuery} onChange={handlePublishersChange}/>

                            </ListItemText>
                        </ListItem>
                    </List>
                </Collapse>
                {/* ====================================================== */}
                <ListItem button onClick={handleKeywordsClick} className={classes.listItem}>
                    <ListItemIcon className={classes.listItemIcon}>
                        <VpnKey />
                    </ListItemIcon>
                    <ListItemText primary="Keywords" className={classes.listItemText} />
                    {openKeywords ? <ExpandLess /> : <ExpandMore />}
                </ListItem>
                <Collapse in={openKeywords} timeout="auto" unmountOnExit>
                    <div style={{ padding: '0 16px' }}>
                        {/* <KeywordSelect
                            keywords={keywords}
                            selectedKeywords={selectedKeywords}
                            handleKeywordSelectChange={handleKeywordSelectChange}
                        /> */}
                        <TagsInput
                            value={selectedKeywords}
                            onChange={handleKeywordSelectChange}
                            placeHolder="Enter keywords"
                        />
                    </div>
                </Collapse>

                {/* ====================================================== */}
                <ListItem button onClick={handleTypeClick} className={classes.listItem}>
                    <ListItemIcon className={classes.listItemIcon}>
                        <Class />
                    </ListItemIcon>
                    <ListItemText primary="Type" className={classes.listItemText} />
                    {openType ? <ExpandLess /> : <ExpandMore />}
                </ListItem>
                <Collapse in={openType} timeout="auto" unmountOnExit>
                    <div style={{ padding: '0 16px' }}>
                        <TypesSelect
                            name="type"
                            types={types}
                            selectedTypes={selectedTypes}
                            handleTypeSelectChange={handleTypeSelectChange}
                            required
                        />
                    </div>
                </Collapse>
            </List>
            <div className="d-flex mb-4 justify-content-between">
                <div
                    className="btn d-flex"
                    style={{
                        backgroundColor: '#757575',
                        borderRadius: '4px',
                    }}
                    onClick={resetFilters}
                // onClick={() => handleButtonClick('list')}
                >
                    <RotateLeft style={{ color: '#FFFFFF' }} />
                    <p style={{ margin: '0', color: 'white', fontWeight: 'bolder' }}>Reset filters</p>
                </div>
                
                <div
                    className="btn d-flex"
                    style={{
                        backgroundColor: '#757575',
                        borderRadius: '4px',
                    }}
                    onClick={filterSubmit}
                // onClick={() => handleButtonClick('list')}
                >
                    <AdminSearchIcon style={{ color: '#FFFFFF' }} />
                    <p style={{ margin: '0', color: 'white', fontWeight: 'bolder' }}>Filters</p>
                </div>
            </div>
            <h2 style={{ color: '#757575', marginBottom: '' }}>Settings</h2>
            {/* ====================================================== */}
            <ListItem button className={classes.listItem}>
                <div>
                    <div className='d-flex'>
                        <ListItemIcon className={classes.listItemIcon}>
                            <SwapVert />
                        </ListItemIcon>
                        <ListItemText primary="Sort By" className={classes.listItemText} />
                    </div>

                    <List component="div" disablePadding>
                        <ListItem>
                            <ListItemText>
                                <select

                                    className="form-control"
                                    name="parentCommunityId"
                                    value={sortState}
                                    onChange={handleSortChange}
                                >
                                    <option value='1'>Title Ascending</option>
                                    <option value='2'>Title Descending</option>
                                    <option value='3'>Date Issue Ascending</option>
                                    <option value='4'>Date Issue Descending</option>
                                </select>
                            </ListItemText>
                        </ListItem>
                    </List>
                </div>
                {/* {openSort ? <ExpandLess /> : <ExpandMore />} */}
            </ListItem>

            {/* ====================================================== */}
            <ListItem button className={classes.listItem}>
                <div>
                    <div className='d-flex'>
                        <ListItemIcon className={classes.listItemIcon}>
                            <MenuBook />
                        </ListItemIcon>
                        <ListItemText primary="Items per page" className={classes.listItemText} />
                    </div>
                    <List component="div" disablePadding>
                        <ListItem>
                            <ListItemText>
                                <select
                                    style={{ width: '135%' }}
                                    className="form-control"
                                    name="parentCommunityId"
                                    value={numPerPage}
                                    onChange={handleChange}
                                >
                                    <option value='5'>5</option>
                                    <option value='10'>10</option>
                                    <option value='20'>20</option>
                                    <option value='30'>30</option>
                                    <option value='40'>40</option>
                                    <option value='50'>50</option>
                                </select>
                            </ListItemText>
                        </ListItem>
                    </List>
                </div>
                {/* {openNumPerPage ? <ExpandLess /> : <ExpandMore />} */}
            </ListItem>
            {/* <Collapse in={openNumPerPage} timeout="auto" unmountOnExit>
                
            </Collapse> */}
        </div>
    );
};

export default ManagementMenu;
