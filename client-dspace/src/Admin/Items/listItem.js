import React, { useEffect, useState } from 'react';
import { Link, useNavigate, useParams, useLocation } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import axios from 'axios';
import './CollectionList.css'; // Import file CSS
import ManagementMenu from '../../components/Nav-bar/nav-bar-items';
import Select, { components } from 'react-select';
import { styled } from '@material-ui/core';
import { FormatListBulleted, GridOn, Search } from '@material-ui/icons';
import TextField from '@mui/material/TextField';
import InputAdornment from '@mui/material/InputAdornment';
import Typography from '@mui/material/Typography';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';

const CollectionList = () => {
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const search = searchParams.get('search');
    const [page, setPage] = React.useState(1);
    // const currentPage = searchParams.get('currentPage');
    const { collectionId } = useParams();
    const [selectedValue, setSelectedValue] = useState(collectionId);
    const [items, setItems] = useState([]);
    const [objItemsPerPage, setobjItemsPerPage] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [searchQuery, setSearchQuery] = useState('');
    const [searchQuery2, setSearchQuery2] = useState('');
    const handleChangePage = (event, value) => {
        setPage(value);
    };
    useEffect(() => {
        if (search) {
            setSearchQuery(search);
        }
    }, [search]);
    const navigate = useNavigate();

    const [startDate, setStartDate] = useState(null);
    const [endDate, setEndDate] = useState(null);
    const [numPerPage, setNumPerPage] = useState(5);
    const [sortState, setSortState] = useState(null);
    const [selectedAuthors, setSelectedAuthors] = useState([]);
    const [selectedKeywords, setSelectedKeywords] = useState([]);
    const [selectedTypes, setSelectedTypes] = useState([]);
    const [activeButton, setActiveButton] = useState('list'); // Default active button is 'list'

    const handleButtonClick = (buttonType) => {
        setActiveButton(buttonType);
    };
    const [currentPage, setCurrentPage] = useState(1);
    // const numPerPage = 12;
    const [collections, setCollections] = useState([]);

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

    useEffect(() => {
        // Lấy dữ liệu từ API
        if (collectionId != 0) {
            if(collectionId == -1){setobjItemsPerPage([])}
            axios.get(`https://localhost:7200/api/Item/itemsInCollection/${collectionId}`)
                .then(({ data }) => {
                    setItems(data);
                    setLoading(false);
                })
                .catch(error => {
                    setError(error.message);
                    setLoading(false);
                });
        } else {
            axios.get(`https://localhost:7200/api/Item/getAllItems/${page}/${numPerPage}`)
                .then(({ data }) => {
                    // setItems(data);
                    setobjItemsPerPage(data);
                    setLoading(false);
                })
                .catch(error => {
                    setError(error.message);
                    setLoading(false);
                });
        }
    }, [collectionId, numPerPage, page]);
    // useEffect(() => {
    //     console.log(objItemsPerPage.objectResponse?.length || 0);
    // }, [objItemsPerPage]);
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

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
    };
    const handleRowClick = (itemId) => {
        // Chuyển hướng sang trang chi tiết bộ sưu tập
        navigate(`/DSpace/ItemDetails/${itemId}`);
    };


    // console.log(items[0].dateOfIssue);
    // const filteredCollections = items.filter(item =>
    //     item.metadata.title.toLowerCase().includes(searchQuery.toLowerCase()) &&
    //     startDate?.getTime() <= itemDate.getTime() &&
    // itemDate.getTime() <= endDate?.getTime()
    // );

    // const filteredCollections = items.filter(item => {
    //     const itemDate = new Date(item.metadata.dateOfIssue);
    //     return (
    //       item.metadata.title.toLowerCase().includes(searchQuery.toLowerCase()) 
    //     );
    //   });
    // const filteredCollections = items.filter(item => {
    //     const itemDate = new Date(item.dateOfIssue);
    //     return (
    //         item.metadata.title.toLowerCase().includes(searchQuery.toLowerCase()) &&
    //         (!startDate || itemDate >= startDate) &&
    //         (!endDate || itemDate <= endDate)
    //     );
    // });
    // const normalizeText = (text) => {
    //     // Loại bỏ dấu câu và dấu cách
    //     text = text.replace(/[\s\p{P}]/gu, '');
    //     // Loại bỏ dấu tiếng Việt
    //     text = text.normalize('NFD').replace(/[\u0300-\u036f]/g, '');
    //     return text.toLowerCase();
    // };
    const normalizeText = (text) => {
        // Chuyển đổi tất cả các ký tự thành chữ thường
        text = text.toLowerCase();

        // Loại bỏ dấu câu và dấu cách
        text = text.replace(/[\s\p{P}]/gu, '');

        // Loại bỏ dấu tiếng Việt
        text = text.replace(/[áàảãạăắằẳẵặâấầẩẫậ]/g, 'a');
        text = text.replace(/[đ]/g, 'd');
        text = text.replace(/[éèẻẽẹêếềểễệ]/g, 'e');
        text = text.replace(/[íìỉĩị]/g, 'i');
        text = text.replace(/[óòỏõọôốồổỗộơớờởỡợ]/g, 'o');
        text = text.replace(/[úùủũụưứừửữự]/g, 'u');
        text = text.replace(/[ýỳỷỹỵ]/g, 'y');

        // Loại bỏ các dấu thanh và âm cuối
        text = text.normalize('NFD').replace(/[\u0300-\u036f]/g, '');

        return text;
    };


    // const filteredCollections = items.filter(item => {
    //     const itemDate = new Date(item.dateOfIssue);
    //     const normalizedTitle = normalizeText(item.metadata.title);
    //     const normalizedQuery = normalizeText(searchQuery);
    //     const authorIds = item.authorItems.map(authorItem => authorItem.author.authorId);
    //     const containsAllSelectedAuthors = selectedAuthors.every(id => authorIds.includes(id));

    //     return (
    //         item.isActive &&
    //         normalizedTitle.includes(normalizedQuery) &&
    //         (!startDate || itemDate >= startDate) &&
    //         (!endDate || itemDate <= endDate) &&
    //         (selectedAuthors.length === 0 || containsAllSelectedAuthors)

    //     );
    // }).sort((a, b) => {
    //     if (sortState === '1') {
    //         return a.metadata.title.localeCompare(b.metadata.title);
    //     } else if (sortState === '2') {
    //         return b.metadata.title.localeCompare(a.metadata.title);
    //     } else if (sortState === '3') {
    //         return new Date(a.dateOfIssue) - new Date(b.dateOfIssue);
    //     } else if (sortState === '4') {
    //         return new Date(b.dateOfIssue) - new Date(a.dateOfIssue);
    //     }
    //     return 0;
    // });
    // const filteredCollections = items
    //     .filter(item => {
    //         const itemDate = new Date(item.dateOfIssue);
    //         const normalizedTitle = normalizeText(item.title);
    //         const normalizedQuery = normalizeText(searchQuery.trim());
    //         // const authorIds = item.authorItems.map(authorItem => authorItem.author.authorId);
    //         // const containsAllSelectedAuthors = selectedAuthors.every(id => authorIds.includes(id));
    //         // const itemKeywords = item.subjectKeywords.map(keyword => normalizeText(keyword));
    //         // const normalizedKeywords = selectedKeywords.map(keyword => normalizeText(keyword));
    //         // const containsAllKeywords = normalizedKeywords.every(keyword =>
    //         //     itemKeywords.some(itemKeyword => itemKeyword.includes(keyword))
    //         // );
    //         // const itemPublishers = normalizeText(item.publisher);
    //         // const searchPublishers = normalizeText(searchQuery2);
    //         // const containsPublishers = itemPublishers.includes(searchPublishers);
    //         // const itemKeywords = item.subjectKeywords.map(keyword => normalizeText(keyword));


    //         const asteriskIndex = searchQuery.indexOf('*');
    //         let filteredTitle = normalizedQuery;
    //         const phraseMatch = searchQuery.match(/"([^"]+)"/);
    //         let phraseWords = [];
    //         if (phraseMatch) {
    //             phraseWords = phraseMatch[1].split(' ').map(word => normalizeText(word));
    //         }

    //         const containsAllPhraseWords = phraseWords.every(word => normalizedTitle.includes(word));
    //         // Xử lý từ bị loại bỏ (sử dụng dấu trừ (-) hoặc NOT)
    //         const excludeWords = searchQuery
    //             .match(/(?:-|NOT\s+)(\w+)/gi) // Tìm các từ bắt đầu bằng dấu trừ (-) hoặc NOT
    //             ?.map(word => normalizeText(word.replace(/(?:-|NOT\s+)/, ''))) || []; // Chuẩn hóa và loại bỏ dấu trừ hoặc NOT
    //         const containsAnyExcludeWord = excludeWords.some(word => normalizedTitle.includes(word));



    //         if (asteriskIndex !== -1) {
    //             filteredTitle = normalizedQuery.substring(0, asteriskIndex);


    //             return (
    //                 // item.isActive && // Chỉ lấy các item có isActive là true
    //                 normalizedTitle.startsWith(filteredTitle) 
    //                 // && 
    //                 // Sử dụng startsWith để kiểm tra từ bắt đầu bằng filteredTitle
    //                 // (!startDate || itemDate >= startDate) &&
    //                 // (!endDate || itemDate <= endDate)
    //                 // && containsAllKeywords
    //                 // && containsPublishers

    //                 // (selectedAuthors.length === 0 || containsAllSelectedAuthors)
    //             );
    //         } else {
    //             return (
    //                 // item.isActive &&
    //                 // !containsAnyExcludeWord &&

    //                 ((phraseWords.length > 0 && containsAllPhraseWords) || (phraseWords.length === 0 && normalizedTitle.includes(normalizedQuery))) 
    //                 && normalizedTitle.includes(normalizedQuery)
    //                 //  &&
    //                 // (!startDate || itemDate >= startDate) &&
    //                 // (!endDate || itemDate <= endDate)
    //                 // && containsAllKeywords
    //                 // && containsPublishers
    //                 // (selectedAuthors.length === 0 || containsAllSelectedAuthors)
    //             );
    //         }
    //     })
    //     .sort((a, b) => {
    //         if (sortState === '1') {
    //             return a.title.localeCompare(b.title); // Sắp xếp theo title tăng dần
    //         } else if (sortState === '2') {
    //             return b.title.localeCompare(a.title); // Sắp xếp theo title giảm dần
    //         } else if (sortState === '3') {
    //             return new Date(a.dateOfIssue) - new Date(b.dateOfIssue); // Sắp xếp theo ngày tăng dần
    //         } else if (sortState === '4') {
    //             return new Date(b.dateOfIssue) - new Date(a.dateOfIssue); // Sắp xếp theo ngày giảm dần
    //         }
    //         return a.title.localeCompare(b.title); // Mặc định không sắp xếp
    //     });
    // console.log(filteredCollections);
    const { ValueContainer, Placeholder } = components;

    const CustomValueContainer = ({ children, ...props }) => {
        return (
            <ValueContainer {...props}>
                <Placeholder {...props} isFocused={props.isFocused}>
                    {props.selectProps.placeholder}
                </Placeholder>
                {React.Children.map(children, child =>
                    child && child.key !== 'placeholder' ? child : null,
                )}
            </ValueContainer>
        );
    };
    // const CollectionSelect = ({ collections, selectedValue, handleChange }) => {
    //     const options = [
    //         { value: '0', label: 'All items' },
    //         ...collections.map(collection => ({
    //             value: collection.collectionId.toString(),
    //             label: collection.collectionName
    //         }))
    //     ];

    //     return (
    //         <Select
    //             options={options}
    //             value={options.find(option => option.value === selectedValue)}
    //             onChange={selectedOption => handleChange(selectedOption ? selectedOption.value : '-1')}
    //             placeholder="Choose collections"
    //             isClearable
    //             styles={{
    //                 container: (base) => ({ ...base, width: '160px' }),
    //                 control: (base) => ({
    //                     ...base,
    //                     border: '1px solid black',
    //                     borderRadius: '4px 0 0 4px',
    //                 }),
    //                 indicatorSeparator: () => ({
    //                     display: 'none',
    //                 }),
    //                 dropdownIndicator: (base) => ({
    //                     ...base,
    //                     display: 'none',
    //                 }),
    //             }}
    //         />

    //     );
    // };
    const CollectionSelect = ({ collections, selectedValue, handleChange }) => {
        const [focused, setFocused] = useState(false);

        const options = [
            { value: '0', label: 'All items' },
            ...collections.map(collection => ({
                value: collection.collectionId.toString(),
                label: collection.collectionName
            }))
        ];

        return (
            <Select
                options={options}
                value={options.find(option => option.value === selectedValue)}
                onChange={selectedOption => handleChange(selectedOption ? selectedOption.value : '-1')}
                placeholder="Choose collections"
                isClearable
                components={{
                    ValueContainer: CustomValueContainer
                }}
                maxMenuHeight={300}
                onFocus={() => setFocused(true)}
                onBlur={() => setFocused(false)}
                isFocused={focused}
                theme={(theme) => ({
                    ...theme,
                    spacing: {
                        ...theme.spacing,
                        baseUnit: 2,
                        controlHeight: 56,
                        menuGutter: 8,
                    },
                })}
                styles={{
                    container: (provided) => ({
                        ...provided,
                        width: '160px',
                        // marginTop: 5
                    }),
                    control: (base) => ({
                        ...base,
                        border: '1px solid black',
                        borderRadius: '4px 0 0 4px',
                    }),
                    valueContainer: (provided) => ({
                        ...provided,
                        overflow: "visible",

                    }),
                    placeholder: (base, state) => ({
                        ...base,
                        position: 'absolute',
                        top: (state.hasValue || state.selectProps.inputValue || state.selectProps.isFocused) ? '-120%' : '0%',
                        transition: 'top 0.2s, font-size 0.2s',
                        fontSize: (state.hasValue || state.selectProps.inputValue || state.selectProps.isFocused) && 14,

                    }),
                    indicatorSeparator: () => ({
                        display: 'none',
                    }),
                    dropdownIndicator: (base) => ({
                        ...base,
                        display: 'none',
                    }),
                }}
            />
        );
    };

    const handleChange = (e) => {
        // const { name, value, type, checked } = e.target;
        // const newValue = type === 'checkbox' ? checked : value;
        setSelectedValue(e)
        navigate(`/DSpace/Collection/listItem/${e}`);
    };

    // Pagination logic
    const indexOfLastCommunity = currentPage * numPerPage;
    const indexOfFirstCommunity = indexOfLastCommunity - numPerPage;
    const currentItems = items.slice(indexOfFirstCommunity, indexOfLastCommunity);

    const totalPages = Math.ceil(items.length / numPerPage);

    const paginate = (pageNumber) => setCurrentPage(pageNumber);

    const handleNext = () => {
        if (currentPage < totalPages) {
            setCurrentPage(currentPage + 1);
        }
    };

    const handlePrevious = () => {
        if (currentPage > 1) {
            setCurrentPage(currentPage - 1);
        }
    };
    const formatDate = (dateString) => {
        const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
        const date = new Date(dateString);
        return date.toLocaleDateString('en-GB', options).replace(/\//g, '-');
    };
    // if (currentItems.length != 0) {
    //     setLoading(false)
    // }
    // console.log({ currentItems });
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
    const handleSearchSubmit = () => {
        const payload = {
            collectionId: collectionId,
            title: normalizeText(searchQuery.trim()),
            startDateIssue: startDate ? startDate.toISOString() : null,
            endDateIssue: endDate ? endDate.toISOString() : null,
            publisher: searchQuery2, // Lấy từ trạng thái hoặc đầu vào người dùng
            authors: selectedAuthors, // Mảng các tác giả đã chọn
            keywords: selectedKeywords, // Mảng các từ khóa đã chọn
            types: selectedTypes // Mảng các loại đã chọn, nếu có
        };
        console.log(payload);

        axios.post('https://localhost:7200/api/Item/SearchItem', payload)
            .then(({ data }) => {
                setItems(data);
                setLoading(false);
            })
            .catch(error => {
                setError(error.message);
                setLoading(false);
            });
    };
    return (
        <div>
            <Header />
            <div className="main-content">
                <div className="row">
                    <div className="col-md-3 mt-5">
                        <div className="d-flex mb-4" style={{ marginLeft: '35%' }}>
                            <div
                                className="btn"
                                style={{
                                    backgroundColor: activeButton === 'list' ? '#9e9e9e' : '#757575',
                                    borderRadius: '4px 0 0 4px',
                                }}
                                onClick={() => handleButtonClick('list')}
                            >
                                <FormatListBulleted style={{ color: '#FFFFFF' }} />
                            </div>
                            <div
                                className="btn"
                                style={{
                                    backgroundColor: activeButton === 'grid' ? '#9e9e9e' : '#757575',
                                    borderRadius: '0 4px 4px 0',
                                }}
                                onClick={() => handleButtonClick('grid')}
                            >
                                <GridOn style={{ color: '#FFFFFF' }} />
                            </div>
                        </div>
                        {/* <h2 style={{ marginLeft: '35%', color: '#757575', marginBottom: '' }}>Filters</h2> */}
                        <div style={{ marginLeft: '30%' }}>
                            <ManagementMenu
                                startDate={startDate}
                                endDate={endDate}
                                setStartDate={setStartDate}
                                setEndDate={setEndDate}
                                numPerPage={numPerPage}
                                setNumPerPage={setNumPerPage}
                                sortState={sortState}
                                setSortState={setSortState}
                                selectedAuthors={selectedAuthors}
                                setSelectedAuthors={setSelectedAuthors}
                                selectedKeywords={selectedKeywords}
                                setSelectedKeywords={setSelectedKeywords}
                                selectedTypes={selectedTypes}
                                setSelectedTypes={setSelectedTypes}
                                searchQuery={searchQuery2}
                                setSearchQuery={setSearchQuery2}
                                filterSubmit={handleSearchSubmit}
                            />
                        </div>


                    </div>
                    <div className="container mt-5 col-md-9">
                        <div className="d-flex justify-content-between mb-4">
                            <div className="d-flex ">
                                {/* <TextField id="outlined-basic" label="Choose collections" variant="outlined" /> */}

                                {/* <div className="me-2" style={{}}>
                                    
                                </div> */}
                                <div className="me-2 d-flex">
                                    <div>
                                        <CollectionSelect
                                            collections={collections}
                                            selectedValue={selectedValue}
                                            handleChange={handleChange}
                                        />
                                    </div>
                                    <div>
                                        <TextField
                                            // className='mh-50'
                                            id="outlined-basic"
                                            label="Search By Title"
                                            variant="outlined"
                                            value={searchQuery}
                                            onChange={handleSearchChange}
                                            sx={{ "& .MuiOutlinedInput-root": { width: '35ch' }, "& fieldset": { borderRadius: 0, width: '35ch', } }}

                                        /></div>
                                    {/* <input
                                type="text"
                                className="form-control w-50"
                                placeholder="Search By Title"
                                style={{ borderRadius: '0 0 0 0', flex: '0 1 auto' }}
                                value={searchQuery}
                                onChange={handleSearchChange}
                            /> */}
                                    <button onClick={handleSearchSubmit} style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }} className="btn">
                                        <Search />
                                    </button>
                                </div>
                            </div>
                            {localStorage.getItem("Role") === "ADMIN" || localStorage.getItem("Role") === "STAFF" ? (
                                <div style={{ marginRight: '4%' }}>
                                    <Link to="/Dspace/Collection/ListOfCollectionCreateItems" className="btn btn-primary" style={{ backgroundColor: '#207698' }}>
                                        Create new item
                                    </Link>
                                </div>
                            ) : null}
                        </div>

                        {error && <div className="alert alert-danger">{error}</div>}
                        <h2>Search result</h2>

                        {/* {filteredCollections.length > 0 ? (
                            <div className='col-md-9'>
                                <h6>Now showing {indexOfFirstCommunity + 1} - {currentItems.length + (currentPage - 1) * numPerPage} of  {filteredCollections.length}</h6>
                                {currentItems.map((item, index) => (
                                    <div key={index} style={{ marginLeft: '5%' }}>
                                        <p style={{ margin: '0' }}><span style={{ margin: '0', backgroundColor: '#207698', borderRadius: '3px', color: 'white', padding: '2px', fontSize: 'small' }}>Item</span></p>
                                        <h3 style={{ margin: '0' }}><span onClick={() => handleRowClick(item.itemId)} className="author-item">{item.title}</span></h3>
                                        <p style={{ margin: '0' }}>({item.publisher}, {formatDate(item.dateOfIssue)}) {item.authors.map(authorItem => `${authorItem}`).join('; ')}</p>
                                        <p style={{ marginBottom: '20px' }}>{item.abstract}</p>
                                    </div>
                                ))}
                            </div>

                        ) : <h6>Not found</h6>} */}

                        {/* {items.length > 0 ? (
                            <div className='col-md-9'>
                                <h6>Now showing {indexOfFirstCommunity + 1} - {currentItems.length + (currentPage - 1) * numPerPage} of  {items.length}</h6>
                                {currentItems.map((item, index) => (
                                    <div key={index} style={{ marginLeft: '5%', display: 'flex', alignItems: 'flex-start', marginBottom: '20px' }}>
                                        <img
                                            src={item.imageURL || 'https://www.shutterstock.com/image-vector/chat-bot-logo-design-concept-600nw-1938811039.jpg'} // Sử dụng URL ảnh từ item hoặc ảnh placeholder
                                            alt={item.title}
                                            style={{ width: '150px', height: '200px', objectFit: 'cover', marginRight: '10px' }}
                                        />
                                        <div>
                                            <p style={{ margin: '0' }}>
                                                <span style={{ margin: '0', backgroundColor: '#207698', borderRadius: '3px', color: 'white', padding: '2px', fontSize: 'small' }}>Item</span>
                                            </p>
                                            <h3 style={{ margin: '0' }}>
                                                <span onClick={() => handleRowClick(item.itemId)} className="author-item">{item.title}</span>
                                            </h3>
                                            <p style={{ margin: '0' }}>({item.publisher}, {formatDate(item.dateOfIssue)}) {item.authors.map(authorItem => `${authorItem}`).join('; ')}</p>
                                            <p>{item.abstract}</p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        ) : <h6>Not found</h6>} */}
                        {objItemsPerPage.objectResponse?.length > 0 ? (
                            <div className='col-md-9'>
                                <h6>Now showing {(page - 1) * numPerPage + 1} - {objItemsPerPage.objectResponse?.length + (page - 1) * numPerPage} of  {objItemsPerPage.totalCounts}</h6>
                                {objItemsPerPage.objectResponse.map((item, index) => (
                                    <div key={index} style={{ marginLeft: '5%', display: 'flex', alignItems: 'flex-start', marginBottom: '20px' }}>
                                        <img
                                            src={item.imageURL || 'https://www.shutterstock.com/image-vector/chat-bot-logo-design-concept-600nw-1938811039.jpg'} // Sử dụng URL ảnh từ item hoặc ảnh placeholder
                                            alt={item.title}
                                            style={{ width: '150px', height: '200px', objectFit: 'cover', marginRight: '10px' }}
                                        />
                                        <div>
                                            <p style={{ margin: '0' }}>
                                                <span style={{ margin: '0', backgroundColor: '#207698', borderRadius: '3px', color: 'white', padding: '2px', fontSize: 'small' }}>Item</span>
                                            </p>
                                            <h3 style={{ margin: '0' }}>
                                                <span onClick={() => handleRowClick(item.itemId)} className="author-item">{item.title}</span>
                                            </h3>
                                            <p style={{ margin: '0' }}>({item.publisher}, {formatDate(item.dateOfIssue)}) {item.authors.map(authorItem => `${authorItem}`).join('; ')}</p>
                                            <p>{item.abstract}</p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        ) : <h6>Not found</h6>}

                        <table className="table table-striped table-bordered table-hover">
                            <thead className="thead-dark">
                                <tr>

                                </tr>
                            </thead>
                            <tbody>
                                {currentItems.map((item, index) => (
                                    <tr key={index} onClick={() => handleRowClick(item.itemId)}
                                        className="clickable-row">
                                        {/* <td>{index + 1}</td>
                                    <td>{item.metadata.title}</td> */}
                                        {/* <td>{item.authorItems[0].author.firstName} {item.authorItems[0].author.lastName}</td> */}
                                        {/* <td>
                                        {item.authorItems.map(authorItem => `${authorItem.author.firstName}`).join(', ')}
                                    </td>
                                    <td>{formatDate(item.dateOfIssue)}</td>
                                    <td>{item.metadata.publisher}</td>
                                    <td>{item.type}</td>
                                    <td>
                                        {item.itemKeywords.map(keywordItem => keywordItem.keyword.keywordName).join(', ')}
                                    </td> */}
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                        {/* <nav>
                            <ul className="pagination justify-content-center">
                                <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
                                    <button style={{ color: '#207698' }} className="page-link" onClick={handlePrevious}>
                                        Previous
                                    </button>
                                </li>
                                {Array.from({ length: totalPages }, (_, index) => (
                                    <li key={index + 1} className={`page-item ${index + 1 === currentPage ? 'active' : ''}`}>
                                        <button className="page-link" style={{
                                            backgroundColor: index + 1 === currentPage ? '#207698' : '',
                                            color: index + 1 === currentPage ? '' : '#207698'
                                        }} onClick={() => paginate(index + 1)}>
                                            {index + 1}
                                        </button>
                                    </li>
                                ))}
                                <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
                                    <button style={{ color: '#207698' }} className="page-link" onClick={handleNext}>
                                        Next
                                    </button>
                                </li>
                            </ul>
                        </nav> */}
                        <Stack spacing={2} alignItems="center">
                            {/* <Typography>Page: {page}</Typography> */}
                            <Pagination count={objItemsPerPage.totalPages} page={page} onChange={handleChangePage} size="large" />
                        </Stack>
                    </div>
                </div>
            </div>

        </div>
    );
};
export default CollectionList;
