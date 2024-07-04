import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link, useSearchParams, useLocation, useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../components/Header/Header';
import { links } from './homepageStu.js';
import Select from 'react-select';
import { FormatListBulleted, GridOn, Search } from '@material-ui/icons';
import { useRef } from 'react';
import e from 'cors';

const SubCommunity = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [searchParams] = useSearchParams();
    const communityIdRef = useRef(searchParams.getAll('cid'));
    const collectionId = searchParams.get('colid') ?? "0";
    const [breadcrumbLinks, setBreadcrumbLinks] = useState([]);
    const [links2, setLinks] = useState(Object.values(links));
    const [community, setCommunity] = useState({});
    const [collections, setCollections] = useState([]);
    const [collection, setCollection] = useState({});
    const [communities, setCommunities] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [visibleCommunityCount, setVisibleCommunityCount] = useState(3);
    const [visibleCollectionCount, setVisibleCollectionCount] = useState(3);
    const queryParams = new URLSearchParams(location.search);
    const initialButton = queryParams.get('button') || 'Recent submissions';
    const colName = searchParams.get('colName');
    const [activeButton, setActiveButton] = useState(initialButton);
    const [selectedYear, setSelectedYear] = useState('');
    const [selectedMonth, setSelectedMonth] = useState('');
    const [selectedDate, setSelectedDate] = useState('');
    const [years, setYears] = useState([]);
    const handleDateChange = (e) => setSelectedDate(e.target.value);
    const [selectedNames, setSelectedNames] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const filterData = (year, month, date) => {
        // Implement your filtering logic here
        // Example: filter communities based on selected criteria
        let filteredCommunities = communities.filter(community => {
            const createDate = new Date(community.createDate);
            if (
                (year === '' || createDate.getFullYear() === parseInt(year, 10)) &&
                (month === '' || createDate.getMonth() + 1 === parseInt(month, 10)) &&
                (date === '' || createDate.getDate() === parseInt(date, 10))
            ) {
                return true;
            }
            return false;
        });

        setCommunities(filteredCommunities);
    };
    const buttons = [
        'Recent submissions',
        // 'By issue date',
        'By Name',
        'By Description'
    ];

    useEffect(() => {
        // Logic to be executed when component mounts or location.search changes
        communityIdRef.current = searchParams.getAll('cid');
    }, [location.search]);

    const months = [
        'January', 'February', 'March', 'April', 'May', 'June',
        'July', 'August', 'September', 'October', 'November', 'December'
    ];
    useEffect(() => {
        const uniqueYears = Array.from(new Set(communities.map(community => new Date(community.createTime).getFullYear())));
        uniqueYears.sort((a, b) => b - a);
        setYears(uniqueYears);
    }, [community]);
    useEffect(() => {
        setActiveButton(initialButton);
    }, [initialButton]);


    useEffect(() => {
        axios.get(`https://localhost:7200/api/Collection/getListOfCollectionByCommunityId/${communityIdRef?.current[communityIdRef?.current.length - 1]}`)
            .then(response => {
                setCollections(response.data);
            })
            .catch(error => {
                console.error('Error fetching collections:', error);
            });
    }, [communityIdRef]);

    useEffect(() => {
        axios.get(`https://localhost:7200/api/Collection/getCollection/${collectionId}`)
            .then(response => {
                setCollection(response.data);
            })
            .catch(error => {
                console.error('Error fetching collection:', error);
            });
    }, [collectionId]);

    useEffect(() => {
        axios.get(`https://localhost:7200/api/Community/getListOfCommunitiesByParentId/${communityIdRef?.current[communityIdRef?.current.length - 1]}`)
            .then(response => {
                setCommunities(response.data);
            })
            .catch(error => {
                console.error('Error fetching communities:', error);
            });
    }, [communityIdRef]);

    useEffect(() => {
        axios.get(`https://localhost:7200/api/Community/getCommunity/${communityIdRef?.current[communityIdRef?.current.length - 1]}`)
            .then(response => {
                setCommunity(response.data);
                const updatedLinks = [
                    ...links2,
                    { label: response.data.communityName, url: `/Dspace/Communities/ListOfStuCom?cid=${response.data.communityId}` },
                ];
                setBreadcrumbLinks(updatedLinks);
            })
            .catch(error => {
                console.error('Error fetching community:', error);
            });
    }, [communityIdRef]);


    const handleCommunityClick = (community) => {
        setLinks([
            ...links2,
            { label: community.communityName, url: `/Dspace/Communities/ListOfStuCom?cid=${community.communityId}` },
        ]);
        navigate(`/Dspace/Communities/ListOfStuCom?cid=${community.communityId}`);
    };

    const handleCollectionClick = (collection) => {
        navigate(`/Dspace/Collection/listItemsInCollection/${collection.collectionId}?colName=${collection.collectionName}`);
    };

    const handleShowMoreCommunities = () => {
        setVisibleCommunityCount(prevCount => prevCount + 3);
    };

    const handleShowMoreCollections = () => {
        setVisibleCollectionCount(prevCount => prevCount + 3);
    };
    // Xử lý sự kiện thay đổi năm
    const handleYearChange = (e) => {
        const year = e.target.value;
        setSelectedYear(year);

        // Lọc dữ liệu ngay khi người dùng chọn năm và tháng
        filterData(year, selectedMonth, selectedDate);
    };

    // Xử lý sự kiện thay đổi tháng
    const handleMonthChange = (e) => {
        const month = e.target.value;
        setSelectedMonth(month);

        // Lọc dữ liệu ngay khi người dùng chọn năm và tháng
        filterData(selectedYear, month, selectedDate);
    };
    const performSearch = () => {
        const normalizedSearchQuery = searchQuery.replace(/\s+/g, '').toLowerCase();

        if (activeButton === 'By Name') {
            setCommunities(prevCommunities =>
                prevCommunities.filter(community =>
                    community.communityName.toLowerCase().includes(normalizedSearchQuery)
                )
            );
            setCollections(prevCollections =>
                prevCollections.filter(collection =>
                    collection.collectionName.toLowerCase().includes(normalizedSearchQuery)
                )
            );
        } else if (activeButton === 'By Description') {
            setCommunities(prevCommunities =>
                prevCommunities.filter(community =>
                    community.shortDescription.toLowerCase().includes(normalizedSearchQuery)
                )
            );
            setCollections(prevCollections =>
                prevCollections.filter(collection =>
                    collection.shortDescription.toLowerCase().includes(normalizedSearchQuery)
                )
            );
        }
        else if (activeButton === 'By issue date') {
            let filteredCommunities = communities.filter(community => {
                const createDate = new Date(community.createDate); // Thay thế bằng trường tương ứng từ API
                if (
                    (selectedYear === '' || createDate.getFullYear() === parseInt(selectedYear, 10)) &&
                    (selectedMonth === '' || createDate.getMonth() + 1 === parseInt(selectedMonth, 10)) &&
                    (selectedDate === '' || createDate.getDate() === parseInt(selectedDate, 10))
                ) {
                    return true;
                }
                return false;
            });

            setCommunities(filteredCommunities);
        }


    };

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
    };


    const handleButtonClick = (buttonName) => {
        setActiveButton(buttonName);
        navigate(`?cid=${community.communityId}&button=${buttonName}`);
        setSearchQuery('');
        window.location.reload();
    };


    const NameSelect = ({ Names, selectedNames, handleNameSelectChange }) => {
        const options = Names.map(Name => ({
            value: Name.NameId,
            label: Name.firstName
        }));
        const selectedOptions = options.filter(option =>
            selectedNames.includes(option.value)
        );

        return (
            <Select
                isMulti
                options={options}
                value={selectedOptions}
                onChange={selectedOptions => handleNameSelectChange(selectedOptions ? selectedOptions.map(option => option.value) : [])}
                placeholder="Search and select Names..."
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



    return (
        <div>
            <Header />
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    {breadcrumbLinks.map((link, index) => (
                        <li key={index} className={`breadcrumb-item ${index === breadcrumbLinks.length - 1 ? 'active' : ''}`} aria-current={index === breadcrumbLinks.length - 1 ? 'page' : ''}>
                            <Link to={link.url}>{link.label}</Link>
                        </li>
                    ))}
                </ol>
            </nav>

            <div className="container mt-5">
                <h1 className="mb-4">{community.communityName} Community</h1>
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
                {activeButton === 'By Name' && (
                    <div className="d-flex mb-4">
                        <input
                            type="text"
                            className="form-control w-50"
                            placeholder="Search By Name"
                            style={{ borderRadius: '0 0 0 0', flex: '0 1 auto' }}
                            value={searchQuery}
                            onChange={handleSearchChange}
                        />
                        <button
                            style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }}
                            className="btn"
                            onClick={performSearch}
                        >
                            <Search />
                        </button>
                    </div>
                )}
                {activeButton === 'By Description' && (
                    <div className="d-flex mb-4">
                        <input
                            type="text"
                            className="form-control w-50"
                            placeholder="Search By Description"
                            style={{ borderRadius: '0 0 0 0', flex: '0 1 auto' }}
                            value={searchQuery}
                            onChange={handleSearchChange}
                        />
                        <button
                            style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }}
                            className="btn"
                            onClick={performSearch}
                        >
                            <Search />
                        </button>
                    </div>
                )}
                {/* {activeButton === 'By issue date' && (
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
                            <button
                                style={{ backgroundColor: '#43515F', color: 'white', borderRadius: '0 6px 6px 0' }}
                                className="btn"
                            >
                                <Search />
                            </button>
                        </div>
                    </>
                )} */}

                <h3>Sub-Communities of {community.communityName}</h3>
                <ul className="list-group">
                    {communities.slice(0, visibleCommunityCount).map((community, index) => (
                        <li key={community.communityId} className="list-group-item clickable-row border-0">
                            <div className="d-flex justify-content-between align-items-center" onClick={() => handleCommunityClick(community)}>
                                <div className="d-flex align-items-center">
                                    <img src={community.logoUrl} alt="logo" style={{ width: '200px', height: '150px', marginRight: '100px', cursor: 'pointer' }} />
                                    <div style={{ marginBottom: '80px' }}>
                                        <span className="font-weight-bold h4" style={{ cursor: 'pointer' }}>Name: {community.communityName}</span>
                                        <p className="mb-0">Description: {community.shortDescription}</p>
                                        <span className="font-weight-bold">Total Items: </span>{community.totalItems}
                                    </div>
                                </div>
                                <div>
                                </div>
                            </div>
                        </li>
                    ))}
                </ul>
                {communities.length > visibleCommunityCount && (
                    <button className="btn btn-primary mt-3" onClick={handleShowMoreCommunities}>Show more</button>
                )}
            </div>

            <div className="container mt-5">
                <h3>Collections of {community.communityName}</h3>
                <ul className="list-group">
                    {collections.slice(0, visibleCollectionCount).map((collection, index) => (
                        <li key={collection.collectionId} className="list-group-item clickable-row border-0">
                            <div className="d-flex justify-content-between align-items-center" onClick={() => handleCollectionClick(collection)}>
                                <div className="d-flex align-items-center">
                                    <img src={collection.logoUrl} alt="logo" style={{ width: '200px', height: '150px', marginRight: '100px', cursor: 'pointer' }} />
                                    <div style={{ marginBottom: '80px' }}>
                                        <span className="font-weight-bold h4" style={{ cursor: 'pointer' }}>Name: {collection.collectionName}</span>
                                        <p className="mb-0">Description: {collection.shortDescription}</p>
                                        <span className="font-weight-bold">Total Items: </span>{collection.totalItems}
                                    </div>
                                </div>
                            </div>
                        </li>
                    ))}
                </ul>
                {collections.length > visibleCollectionCount && (
                    <button className="btn btn-primary mt-3" onClick={handleShowMoreCollections}>Show more</button>
                )}
            </div>
        </div>
    );
};

export default SubCommunity;
