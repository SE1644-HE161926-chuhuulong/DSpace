import React, { useEffect, useState } from 'react';
import { Link, useNavigate, useParams, useLocation } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../../components/Header/Header';
import axios from 'axios';
import './CollectionList.css'; // Import file CSS
import ManagementMenu from '../../components/Nav-bar/nav-bar-items';
import Select from 'react-select';
import { styled } from '@material-ui/core';
import { FormatListBulleted, GridOn, Search } from '@material-ui/icons';
import { TagsInput } from "react-tag-input-component";


const CollectionList = () => {
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const colName = searchParams.get('colName');

    const queryParams = new URLSearchParams(location.search);
    const initialButton = queryParams.get('button') || 'Recent submissions';

    const [selectedValue, setSelectedValue] = useState('');
    const [items, setItems] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [searchQuery, setSearchQuery] = useState('');
    const navigate = useNavigate();
    const { collectionId } = useParams();
    const [startDate, setStartDate] = useState(null);
    const [endDate, setEndDate] = useState(null);
    const [numPerPage, setNumPerPage] = useState(5);
    const [sortState, setSortState] = useState(null);
    const [selectedAuthors, setSelectedAuthors] = useState([]);
    const [selectedKeywords, setSelectedKeywords] = useState([]);

    // const [activeButton, setActiveButton] = useState('list'); // Default active button is 'list'
    const [activeButton, setActiveButton] = useState(initialButton);

    useEffect(() => {
        setActiveButton(initialButton);
    }, [initialButton]);

    const handleButtonClick = (buttonName) => {
        navigate(`?colName=${colName}&button=${buttonName}`);
        window.location.reload();
    };

    const buttons = [
        'Recent submissions',
        'By issue date',
        'By authors',
        'By title',
        'By subject keywords',
        // 'By subject category'
    ];


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
    console.log(activeButton != 'By issue date');
    useEffect(() => {
        // Lấy dữ liệu từ API
        if (collectionId != 0) {
            if (activeButton == 'By issue date' || activeButton == 'By authors' || activeButton == 'By title' || activeButton == 'By subject keywords') {

            } else {
                axios.get(`https://localhost:7200/api/Item/Get5ItemRecentInACollection?collectionId=${collectionId}`)
                    .then(({ data }) => {
                        setItems(data);
                        setLoading(false);
                    })
                    .catch(error => {
                        setError(error.message);
                        setLoading(false);
                    });
            }

        } else {
            // axios.get(`https://localhost:7200/api/Item/getAllItems`)
            //     .then(({ data }) => {
            //         setItems(data);
            //         setLoading(false);
            //     })
            //     .catch(error => {
            //         setError(error.message);
            //         setLoading(false);
            //     });
        }
    }, [collectionId]);

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
    };
    const handleRowClick = (itemId) => {
        // Chuyển hướng sang trang chi tiết bộ sưu tập
        navigate(`/DSpace/ItemDetails/${itemId}?colId=${collectionId}`);
    };



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
    const [selectedYear, setSelectedYear] = useState('');
    const [selectedMonth, setSelectedMonth] = useState('');
    const [selectedDate, setSelectedDate] = useState('');
    const [years, setYears] = useState([]);

    const months = [
        'January', 'February', 'March', 'April', 'May', 'June',
        'July', 'August', 'September', 'October', 'November', 'December'
    ];
    useEffect(() => {
        const uniqueYears = Array.from(new Set(items.map(item => new Date(item.dateOfIssue).getFullYear())));
        uniqueYears.sort((a, b) => b - a);
        setYears(uniqueYears);
    }, [items]);
    const handleYearChange = (e) => setSelectedYear(e.target.value);
    const handleMonthChange = (e) => setSelectedMonth(e.target.value);
    const handleDateChange = (e) => setSelectedDate(e.target.value);

    const handleSearch = () => {
        // Your search logic here
        console.log('Searching for:', { year: selectedYear, month: selectedMonth, date: selectedDate });
    };

    // const filteredCollections = items
    //     .filter(item => {
    //         const itemDate = new Date(item.dateOfIssue);
    //         const normalizedTitle = normalizeText(item.title);
    //         const normalizedQuery = normalizeText(searchQuery.trim());
    //         // const authorIds = item.authorItems.map(authorItem => authorItem.author.authorId);
    //         // const containsAllSelectedAuthors = selectedAuthors.every(id => authorIds.includes(id));

    //         // Check if active button is 'By issue date' and filter by year and month
    //         const filterByIssueDate = (activeButton === 'By issue date') &&
    //             (!selectedYear || itemDate.getFullYear() === parseInt(selectedYear)) &&
    //             (!selectedMonth || itemDate.getMonth() === (parseInt(selectedMonth) - 1)) &&
    //             (!selectedDate || itemDate.getDate() === (parseInt(selectedDate)))

    //         // Handle asterisk (*) in search query
    //         const asteriskIndex = searchQuery.indexOf('*');
    //         let filteredTitle = normalizedQuery;

    //         // Handle phrase search
    //         const phraseMatch = searchQuery.match(/"([^"]+)"/);
    //         let phraseWords = [];
    //         if (phraseMatch) {
    //             phraseWords = phraseMatch[1].split(' ').map(word => normalizeText(word));
    //         }

    //         const containsAllPhraseWords = phraseWords.every(word => normalizedTitle.includes(word));

    //         // Handle exclude words (using - or NOT)
    //         const excludeWords = searchQuery
    //             .match(/(?:-|NOT\s+)(\w+)/gi)
    //             ?.map(word => normalizeText(word.replace(/(?:-|NOT\s+)/, ''))) || [];
    //         const containsAnyExcludeWord = excludeWords.some(word => normalizedTitle.includes(word));

    //         if (asteriskIndex !== -1) {
    //             filteredTitle = normalizedQuery.substring(0, asteriskIndex);
    //             return (
    //                 // item.isActive &&
    //                 normalizedTitle.startsWith(filteredTitle) &&
    //                 // (selectedAuthors.length === 0 || containsAllSelectedAuthors) &&
    //                 (activeButton !== 'By issue date' || filterByIssueDate)  // Apply filter by issue date if active button is 'By issue date'
    //             );
    //         } else {
    //             return (
    //                 // item.isActive &&
    //                 !containsAnyExcludeWord &&
    //                 ((phraseWords.length > 0 && containsAllPhraseWords) || (phraseWords.length === 0 && normalizedTitle.includes(normalizedQuery))) &&
    //                 // (selectedAuthors.length === 0 || containsAllSelectedAuthors) &&
    //                 (activeButton !== 'By issue date' || filterByIssueDate)  // Apply filter by issue date if active button is 'By issue date'
    //             );
    //         }
    //     })
    //     .sort((a, b) => {
    //         if (sortState === '1') {
    //             return a.metadata.title.localeCompare(b.metadata.title); // Sort by title ascending
    //         } else if (sortState === '2') {
    //             return b.metadata.title.localeCompare(a.metadata.title); // Sort by title descending
    //         } else if (sortState === '3') {
    //             return new Date(a.dateOfIssue) - new Date(b.dateOfIssue); // Sort by date ascending
    //         } else if (sortState === '4') {
    //             return new Date(b.dateOfIssue) - new Date(a.dateOfIssue); // Sort by date descending
    //         }
    //         return 0; // Default no sorting
    //     });

    // console.log(filteredCollections);

    const CollectionSelect = ({ collections, selectedValue, handleChange }) => {
        const options = collections.map(collection => ({
            value: collection.collectionId,
            label: collection.collectionName
        }));

        return (
            <Select
                options={options}
                value={options.find(option => option.value === selectedValue)}
                onChange={selectedOption => handleChange(selectedOption ? selectedOption.value : '0')}
                placeholder="All collections"
                isClearable
                styles={{
                    container: (base) => ({ ...base, width: 'auto' }),
                    control: (base) => ({
                        ...base,
                        border: '1px solid black',
                        borderRadius: '4px 0 0 4px',
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

    const [authors, setAuthors] = useState([]);
    useEffect(() => {
        async function fetchAuthors() {
            const response = await fetch('https://localhost:7200/api/Author/getListOfAuthors');
            const data = await response.json();
            setAuthors(data);
        }
        fetchAuthors();
    }, []);
    const AuthorSelect = ({ authors, selectedAuthors, handleAuthorSelectChange }) => {
        const options = authors.map(author => ({
            value: author.fullName,
            label: author.fullName
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
    const handleAuthorSelectChange = (selectedAuthorIds) => {
        setSelectedAuthors(selectedAuthorIds);
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
    console.log({ currentItems });
    // useEffect(() => {
    //     filterAndSortItems();
    // }, [searchQuery, selectedAuthors, selectedYear, selectedMonth, sortState, activeButton, items]);
    // const filterAndSortItems = () => {filteredCollections}
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
    // console.log(selectedDate)
    const handleSearchSubmit = () => {
        const payload = {
            authors: selectedAuthors,
            keywords: selectedKeywords,
            title: normalizeText(searchQuery.trim()),
            year: Number(selectedYear) !== 0 ? Number(selectedYear) : null,
            month: Number(selectedMonth) !== 0 ? Number(selectedMonth) : null,
            day: Number(selectedDate) !== 0 ? Number(selectedDate) : null,
            collectionId: collectionId
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
    const handleKeywordSelectChange = (newKeywords) => {
        setSelectedKeywords(newKeywords);
    };
    return (
        <div>
            <Header />
            <div className="main-content d-flex flex-column align-items-center">
                <div className="container mt-5 col-md-8">
                    <h1 style={{ marginBottom: '3%' }}>{colName}</h1>
                    <h5 style={{ marginBottom: '' }}>Browse</h5>
                    <div className='d-flex mb-4'>
                        {buttons.map((buttonName, index) => (
                            <div
                                key={index}
                                className="btn"
                                style={{
                                    border: '1px solid black',
                                    borderRadius: index === 0 ? '4px 0 0 4px' : index === buttons.length - 1 ? '0 4px 4px 0' : '0',
                                    padding: '13px',
                                    fontWeight: 'bolder',
                                    backgroundColor: activeButton === buttonName ? '#43515F' : 'white',
                                    color: activeButton === buttonName ? 'white' : '#207698',
                                    cursor: 'pointer'
                                }}
                                onClick={() => handleButtonClick(buttonName)}
                            >
                                {buttonName}
                            </div>
                        ))}

                    </div>


                    {error && <div className="alert alert-danger">{error}</div>}

                    {activeButton === 'Recent submissions' && <p></p>}
                    {activeButton === 'By issue date' && (
                        <>
                            <p style={{ fontWeight: 'bolder' }}>Filter results by year or month</p>
                            <div className="d-flex mb-4">
                                <select value={selectedYear} onChange={handleYearChange} className="form-select w-50">
                                    <option value="">(Choose year)</option>
                                    {years.map(year => (
                                        <option key={year} value={year}>{year}</option>
                                    ))}
                                </select>
                                <select value={selectedMonth} onChange={handleMonthChange} className="form-select mx-2 w-50">
                                    <option value="">(Choose month)</option>
                                    {months.map((month, index) => (
                                        <option key={index} value={index + 1}>{month}</option>
                                    ))}
                                </select>
                                <input
                                    style={{ borderRadius: '6px 0 0 6px' }}
                                    type="number"
                                    placeholder="Filter results by date"
                                    value={selectedDate}
                                    onChange={handleDateChange}
                                    className="form-control"
                                />
                                <button onClick={handleSearchSubmit} style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }} className="btn">
                                    <Search />
                                </button>
                            </div>
                        </>
                    )}
                    {activeButton === 'By authors' && (
                        <div className="d-flex mb-4 w-50" style={{ padding: '' }}>
                            <AuthorSelect
                                authors={authors}
                                selectedAuthors={selectedAuthors}
                                handleAuthorSelectChange={handleAuthorSelectChange}
                            />
                            <button onClick={handleSearchSubmit} style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }} className="btn">
                                <Search />
                            </button>
                        </div>
                    )}
                    {activeButton === 'By title' && (
                        <div className="d-flex mb-4">
                            <input
                                type="text"
                                className="form-control w-50"
                                placeholder="Search By Title"
                                style={{ borderRadius: '0 0 0 0', flex: '0 1 auto' }}
                                value={searchQuery}
                                onChange={handleSearchChange}
                            />
                            <button onClick={handleSearchSubmit} style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }} className="btn">
                                <Search />
                            </button>
                        </div>
                    )}
                    {activeButton === 'By subject keywords' &&
                        (
                            <div className="d-flex mb-4 w-50" style={{ padding: '' }}>
                                <TagsInput
                                    style={{ borderRadius: '0 0 0 0' }}
                                    value={selectedKeywords}
                                    onChange={handleKeywordSelectChange}
                                    placeHolder="Enter keywords separated by Enter Button"
                                />
                                <button onClick={handleSearchSubmit} style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }} className="btn">
                                    <Search />
                                </button>
                            </div>
                        )
                    }
                    {activeButton === 'By subject category' && <p>Content for By subject category</p>}
                    {items.length > 0 ? (
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

                                </tr>
                            ))}
                        </tbody>
                    </table>
                    <nav>
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
                    </nav>
                </div>
            </div>
        </div>
    );
};
export default CollectionList;
