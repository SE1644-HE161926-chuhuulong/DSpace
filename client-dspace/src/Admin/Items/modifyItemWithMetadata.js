import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/DeleteOutlined';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Close';
import {
    GridRowModes,
    DataGrid,
    GridToolbarContainer,
    GridActionsCellItem,
    GridRowEditStopReasons,
} from '@mui/x-data-grid';
import {
    randomId,
} from '@mui/x-data-grid-generator';
import Autocomplete from '@mui/material/Autocomplete';
import TextField from '@mui/material/TextField';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import MetadataField from '../../components/metadataFields'
import Header from '../../components/Header/Header';
import { Container, Typography } from '@mui/material';
import { Tabs, Tab } from 'react-bootstrap';
import Save from '@mui/icons-material/Save';
import Close from '@mui/icons-material/Close';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

function getMetadataValue(key) {
    const field = MetadataField.find(item => item.metadataFieldName === key);
    return field ? field.metadataFieldId : null;
}

function MetadataSelect(props) {
    const { id, field, value, api, colDef, row } = props;
    const fieldForId = 'metadataFieldId'
    console.log(props)
    const handleChange = (event, newValue) => {
        if (newValue !== null) {
            api.setEditCellValue({ id, field, value: newValue.metadataFieldName }, event);
        }
        console.log(newValue)
    };
    return (
        <Autocomplete
            sx={{
                width: '100%'
            }}
            options={MetadataField}
            getOptionLabel={(option) => `${option.metadataFieldName}`}
            id="metadata-customized-option-demo"
            onChange={handleChange}
            renderInput={(params) => (
                <TextField {...params} label="Choose a metadata" variant="standard" />
            )}
        />
    );
}


function EditToolbar(props) {
    const { setModifyFields, setRowModesModel, setModifyStatus } = props;

    const handleClick = () => {
        const metadataValueId = randomId();
        setModifyFields((oldRows) => [...oldRows, { metadataValueId, metadataFieldName: '', textValue: '', textLang: '' }]);
        setRowModesModel((oldModel) => ({
            ...oldModel,
            [metadataValueId]: { mode: GridRowModes.Edit, fieldToFocus: 'metadataFieldName' },
        }));
        setModifyStatus('ADD');
    };

    return (
        <GridToolbarContainer>
            <Button color="primary" startIcon={<AddIcon />} onClick={handleClick}>
                Add record
            </Button>
        </GridToolbarContainer>
    );
}

export default function ModifyItem() {
    const { itemId } = useParams();
    const [addFields, setAddFields] = React.useState([]);
    const [updateFields, setUpdateFields] = React.useState([]);
    const [deleteFields, setDeleteFields] = React.useState([]);
    const [items, setItems] = React.useState([]);
    const [error, setError] = React.useState(null);
    const [loading, setLoading] = React.useState(true);
    const token = localStorage.getItem('Token');
    const header = `Bearer ${token}`;
    const [modifyMetadata, setModifyFields] = React.useState([]);
    const [modifyStatus, setModifyStatus] = React.useState('');

    const handleAddChange = (textValue, textLang, metadataFieldId) => {
        const newField = { textValue, textLang, metadataFieldId, itemId };
        addFields.push(newField);
        const requestBody = {
            add: addFields,
            update: updateFields,
            delete: deleteFields,
        };
        console.log(requestBody);
    };

    const handleUpdateChange = (metadataValueId, textValue, textLang, metadataFieldId) => {
        const newField = { metadataValueId, textValue, textLang, metadataFieldId };
        updateFields.push(newField);
        const requestBody = {
            add: addFields,
            update: updateFields,
            delete: deleteFields,
        };
        console.log(requestBody);
    };

    const handleDeleteChange = (metadataValueId) => {
        const newField = { metadataValueId }
        deleteFields.push(newField);
        const requestBody = {
            add: addFields,
            update: updateFields,
            delete: deleteFields,
        };
        console.log(requestBody);
    };

    React.useEffect(() => {
        if (itemId != 0) {
            axios.get(`https://localhost:7200/api/Item/GetItemFullById/${itemId}`)
                .then(({ data }) => {
                    setItems(data);
                    setModifyFields(data.listMetadataValueDTOForSelect)
                    setLoading(false);
                })
                .catch(error => {
                    setError(error.message);
                    setLoading(false);
                });
        }
    }, [itemId]);

    const [rowModesModel, setRowModesModel] = React.useState({});

    const handleRowEditStop = (params, event) => {
        if (params.reason === GridRowEditStopReasons.rowFocusOut) {
            event.defaultMuiPrevented = true;
        }
    };

    const handleEditClick = (metadataValueId) => () => {
        setRowModesModel({ ...rowModesModel, [metadataValueId]: { mode: GridRowModes.Edit } });
        setModifyStatus('EDIT');
    };

    const handleSaveClick = (metadataValueId) => () => {
        setRowModesModel({ ...rowModesModel, [metadataValueId]: { mode: GridRowModes.View } });
    };

    const handleDeleteClick = (metadataValueId) => () => {
        handleDeleteChange(metadataValueId)
        setModifyFields(modifyMetadata.filter((row) => row.metadataValueId !== metadataValueId));
    };

    const handleCancelClick = (metadataValueId) => () => {
        setRowModesModel({
            ...rowModesModel,
            [metadataValueId]: { mode: GridRowModes.View, ignoreModifications: true },
        });

        if (modifyStatus.localeCompare('ADD') === 0) {
            setModifyFields(modifyMetadata.filter((row) => row.metadataValueId !== metadataValueId));
        }
    };

    const processRowUpdate = (newRow) => {
        const updatedRow = { ...newRow };
        const fieldMetadataIdGet = getMetadataValue(newRow.metadataFieldName);
        updatedRow.metadataFieldId = fieldMetadataIdGet;
        console.log(updatedRow)
        if (modifyStatus.localeCompare('ADD') === 0) {
            if (modifyMetadata.find((row) => row.metadataFieldName === updatedRow.metadataFieldName
                && row.textValue === updatedRow.textValue
                && row.textLang === updatedRow.textLang) === undefined) {
                const fieldMetadataIdGet = getMetadataValue(updatedRow.metadataFieldName)
                console.log(fieldMetadataIdGet);
                handleAddChange(updatedRow.textValue, updatedRow.textLang, fieldMetadataIdGet)
                setModifyFields(modifyMetadata.map((row) => (row.metadataValueId === updatedRow.metadataValueId ? updatedRow : row)));
            } else {
                console.log("Exist row, no add");
                setModifyFields(modifyMetadata.filter((row) => row.metadataValueId !== updatedRow.metadataValueId));
                return { ...updatedRow, _action: 'delete' };
            }
        } else if (modifyStatus.localeCompare('EDIT') === 0) {
            if (modifyMetadata.find((row) => row.metadataFieldId === updatedRow.metadataFieldId) !== null) {
                setModifyFields(modifyMetadata.filter((row) => row.metadataValueId !== updatedRow.metadataFieldId));
                console.log("Exist row, delete row and update again");
            }
            handleUpdateChange(updatedRow.metadataValueId, updatedRow.textValue, updatedRow.textLang, updatedRow.metadataFieldId)
            setModifyFields(modifyMetadata.map((row) => (row.metadataValueId === updatedRow.metadataValueId ? updatedRow : row)));
        }
        return updatedRow;
    };

    const handleRowModesModelChange = (newRowModesModel) => {
        setModifyStatus('EDIT');
        setRowModesModel(newRowModesModel);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        const requestBody = {
            add: addFields,
            update: updateFields,
            delete: deleteFields,
        };

        try {
            const response = await fetch(`https://localhost:7200/api/Item/ModifyItem/${itemId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: header
                },
                body: JSON.stringify(requestBody),
            });

            const data = await response.json();
            console.log(data);
        } catch (error) {
            console.error('Error:', error);
        }
    };

    const columns = [
        {
            field: 'metadataFieldId',
            headerName: 'Field ID',
            width: 180,
            hideable: true

        },
        {
            field: 'metadataFieldName',
            headerName: 'Field',
            width: 240,
            editable: true,
            renderEditCell: (params) => (
                <MetadataSelect {...params} />
            )
        },
        {
            field: 'textValue',
            headerName: 'Value',
            width: 420,
            editable: true
        },
        {
            field: 'textLang',
            headerName: 'Lang',
            width: 120,
            editable: true
        },
        // {
        //     field: 'role',
        //     headerName: 'Department',
        //     width: 220,
        //     editable: true,
        //     type: 'singleSelect',
        //     valueOptions: ['Market', 'Finance', 'Development'],
        // },
        {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            width: 100,
            cellClassName: 'actions',
            getActions: ({ id }) => {
                const isInEditMode = rowModesModel[id]?.mode === GridRowModes.Edit;

                if (isInEditMode) {
                    return [
                        <GridActionsCellItem
                            icon={<SaveIcon />}
                            label="Save"
                            sx={{
                                color: 'primary.main',
                            }}
                            onClick={handleSaveClick(id)}
                        />,
                        <GridActionsCellItem
                            icon={<CancelIcon />}
                            label="Cancel"
                            className="textPrimary"
                            onClick={handleCancelClick(id)}
                            color="inherit"
                        />,
                    ];
                }

                return [
                    <GridActionsCellItem
                        icon={<EditIcon />}
                        label="Edit"
                        className="textPrimary"
                        onClick={handleEditClick(id)}
                        color="inherit"
                    />,
                    <GridActionsCellItem
                        icon={<DeleteIcon />}
                        label="Delete"
                        onClick={handleDeleteClick(id)}
                        color="inherit"
                    />,
                ];
            },
        },
    ];

    return (
        <div>
            <Header />
            <Container>
                <Typography
                    variant="h4"
                    component="h4"
                    sx={{ textAlign: 'left', mt: 3 }}>
                    Edit Item
                </Typography>
                <hr
                    style={{
                        color: 'black',
                        backgroundColor: 'black',
                        height: 1
                    }}
                />
                <Tabs defaultActiveKey="metadata">
                    <Tab eventKey="status" title="Status">
                        {/* Tab content */}
                    </Tab>
                    <Tab eventKey="metadata" title="Metadata">
                        <Box
                            sx={{
                                height: '100%',
                                width: '100%',
                                marginTop: 2,
                                '& .actions': {
                                    color: 'text.secondary',
                                },
                                '& .textPrimary': {
                                    color: 'text.primary',
                                },
                            }}
                        >
                            <DataGrid
                                rows={modifyMetadata}
                                columns={columns}
                                editMode="row"
                                rowModesModel={rowModesModel}
                                onRowModesModelChange={handleRowModesModelChange}
                                onRowEditStop={handleRowEditStop}
                                processRowUpdate={processRowUpdate}
                                getRowId={(row) => row.metadataValueId}
                                slots={{
                                    toolbar: EditToolbar,
                                }}
                                slotProps={{
                                    toolbar: { setModifyFields, setRowModesModel, setModifyStatus },
                                }}
                                columnVisibilityModel={{
                                    metadataFieldId: false
                                }}
                                initialState={{
                                    pagination: {
                                        paginationModel: { pageSize: 10, page: 0 },
                                    },
                                }}
                                pageSizeOptions={[10, 15, 25]}
                            />
                        </Box>
                        <div className='d-flex flex-column align-items-end'>
                            <div>
                                <Button variant="secondary" startIcon={<Save />} className="ms-1 mt-2 bg-secondary text-white" onClick={handleSubmit}>
                                    Save
                                </Button>
                                <Button className="ms-2 mt-2 bg-danger bg-gradient text-white" startIcon={<Close />} >
                                    Discard
                                </Button>
                            </div>
                            <Button className="mr-2 text-black mt-2" startIcon={<ArrowBackIcon />} >
                                Back
                            </Button>
                        </div>

                    </Tab>
                    <Tab eventKey="relationships" title="Relationships">{/* Tab content */}</Tab>
                    <Tab eventKey="version-history" title="Version History">{/* Tab content */}</Tab>
                    <Tab eventKey="collection-mapper" title="Collection Mapper">{/* Tab content */}</Tab>
                </Tabs>


            </Container>
        </div>

    );
}