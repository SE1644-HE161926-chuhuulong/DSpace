import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import {
  List, ListItem, ListItemIcon, ListItemText, Collapse
} from '@material-ui/core';
import {
  ExpandLess, ExpandMore, Add as NewIcon, Edit as EditIcon,
  ImportExport as ImportIcon, GetApp as ExportIcon, Lock as AccessControlIcon,
  Search as AdminSearchIcon, Book as RegistriesIcon,
  Assignment as CurationTaskIcon, Settings as ProcessesIcon,
  Build as WorkflowAdminIcon, LocalHospital as HealthIcon,
  NotificationImportant as SystemWideAlertIcon
} from '@material-ui/icons';

const useStyles = makeStyles({
  root: {
    display: 'flex',
    flexDirection: 'column',
    height: '100%',
    backgroundColor: '#f5f5f5',
    padding: '20px',
    width: '250px', // Automatically adjust width on hover

  },
  listItem: {
    display: 'flex',
    alignItems: 'center',
    padding: '10px',
    marginBottom: '10px',
    borderRadius: '5px',
    cursor: 'pointer',
  },
  listItemIcon: {
    marginRight: '10px',
  },
  listItemText: {
    opacity: 1, // Show text immediately
  },

});

const ManagementMenu = () => {
  const classes = useStyles();
  const [openAccessControl, setOpenAccessControl] = useState(false);

  const handleAccessControlClick = () => {
    setOpenAccessControl(!openAccessControl);
  };

  return (
    <div className={classes.root}>
      <List>
        <ListItem button onClick={handleAccessControlClick} className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <AccessControlIcon />
          </ListItemIcon>
          <ListItemText primary="Access Control" className={classes.listItemText} />
          {openAccessControl ? <ExpandLess /> : <ExpandMore />}
        </ListItem>
        <Collapse in={openAccessControl} timeout="auto" unmountOnExit>
          <List component="div" disablePadding>
            {localStorage.getItem("Role") == "ADMIN" && (<ListItem button component={NavLink} to="/Dspace/User/ListOfUsers" className={`${classes.listItem} ${classes.nested}`}>
              <ListItemText primary="People" className={classes.listItemText} />
            </ListItem>)}

            {localStorage.getItem("Role") == "ADMIN" && (<ListItem button component={NavLink} to="/Dspace/Group/ListOfGroup" className={`${classes.listItem} ${classes.nested}`}>
              <ListItemText primary="Groups" className={classes.listItemText} />
            </ListItem>)}

            <ListItem button component={NavLink} to="/Dspace/Author/ListOfAuthor" className={`${classes.listItem} ${classes.nested}`}>
              <ListItemText primary="Author" className={classes.listItemText} />
            </ListItem>
            <ListItem button component={NavLink} to="/Dspace/Communities/ListOfCommunities" className={`${classes.listItem} ${classes.nested}`}>
              <ListItemText primary="Community" className={classes.listItemText} />
            </ListItem>
            <ListItem button component={NavLink} to="/Dspace/Collection/ListOfCollection" className={`${classes.listItem} ${classes.nested}`}>
              <ListItemText primary="Collection" className={classes.listItemText} />
            </ListItem>
            
            <ListItem button component={NavLink} to="/Dspace/Collection/ListOfCollectionCreateItems" className={`${classes.listItem} ${classes.nested}`}>
              <ListItemText primary="Item" className={classes.listItemText} />
            </ListItem>
            <ListItem button component={NavLink} to="/Dspace/Metadata/ListOfMetadata" className={`${classes.listItem} ${classes.nested}`}>
              <ListItemText primary="Metadata" className={classes.listItemText} />
            </ListItem>
          </List>
        </Collapse>
        <ListItem button component={NavLink} to="/import" className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <ImportIcon />
          </ListItemIcon>
          <ListItemText primary="Import" className={classes.listItemText} />
        </ListItem>
        <ListItem button component={NavLink} to="/export" className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <ExportIcon />
          </ListItemIcon>
          <ListItemText primary="Export" className={classes.listItemText} />
        </ListItem>
        <ListItem button component={NavLink} to="/admin-search" className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <AdminSearchIcon />
          </ListItemIcon>
          <ListItemText primary="Admin Search" className={classes.listItemText} />
        </ListItem>
        <ListItem button component={NavLink} to="/registries" className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <RegistriesIcon />
          </ListItemIcon>
          <ListItemText primary="Registries" className={classes.listItemText} />
        </ListItem>
        <ListItem button component={NavLink} to="/curation-task" className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <CurationTaskIcon />
          </ListItemIcon>
          <ListItemText primary="Curation Task" className={classes.listItemText} />
        </ListItem>
        <ListItem button component={NavLink} to="/processes" className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <ProcessesIcon />
          </ListItemIcon>
          <ListItemText primary="Processes" className={classes.listItemText} />
        </ListItem>
        <ListItem button component={NavLink} to="/workflow-administrator" className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <WorkflowAdminIcon />
          </ListItemIcon>
          <ListItemText primary="Workflow Administrator" className={classes.listItemText} />
        </ListItem>
        <ListItem button component={NavLink} to="/system-wide-alert" className={classes.listItem}>
          <ListItemIcon className={classes.listItemIcon}>
            <SystemWideAlertIcon />
          </ListItemIcon>
          <ListItemText primary="System-wide Alert" className={classes.listItemText} />
        </ListItem>
      </List>
    </div>
  );
};

export default ManagementMenu;
