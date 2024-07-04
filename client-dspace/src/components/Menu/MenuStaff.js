import React from 'react';
import { useState } from 'react';
import { Link } from 'react-admin';
import { makeStyles, ThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import Drawer from '@material-ui/core/Drawer';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import PeopleIcon from '@material-ui/icons/People';
import GroupIcon from '@material-ui/icons/Group';
import CollectionsIcon from '@material-ui/icons/Collections';
import DashboardIcon from '@material-ui/icons/Dashboard';
import AccountCircleIcon from '@material-ui/icons/AccountCircle';
import BusinessIcon from '@material-ui/icons/Business';
import LibraryBooksIcon from '@material-ui/icons/LibraryBooks';
import PersonIcon from '@material-ui/icons/Person';
import StorageIcon from '@material-ui/icons/Storage';
import HelpOutlineIcon from '@material-ui/icons/HelpOutline';
import { Slide, Typography } from '@material-ui/core';

const theme = createMuiTheme({
  palette: {
    primary: {
      main: '#2243a4', 
    },
  },
});

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(1),
  },
  title: {
    flexGrow: 1,
    textAlign: 'center',
    color: 'black',
  },
  drawer: {
    width: 250,
  },
  listItemText: {
    color: 'black',
  },
  drawerTitle: {
    textAlign: 'center',
    padding: theme.spacing(2),
    fontWeight: 'bold',
  },
  listItem: {
    marginTop: theme.spacing(2),
    padding: theme.spacing(3),
  },
 
}));

const Menu = () => {
  const classes = useStyles();
  const [drawerOpen, setDrawerOpen] = useState(false);

  const toggleDrawer = (open) => (event) => {
    if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
      return;
    }
    setDrawerOpen(open);
  };

  return (
    <ThemeProvider theme={theme}>
      <div className={classes.root}>
        <AppBar position="static" elevation={0}>
          <Toolbar>
            <IconButton edge="start" className={classes.menuButton} color="inherit" aria-label="menu" onClick={toggleDrawer(true)}>
              <MenuIcon />
            </IconButton>
          </Toolbar>
        </AppBar>
        <Drawer open={drawerOpen} onClose={toggleDrawer(false)}>
          <div className={classes.drawer} role="presentation" onClick={toggleDrawer(false)} onKeyDown={toggleDrawer(false)}>
            <Typography variant="h6" className={classes.drawerTitle}>
              Admin Dashboard
            </Typography>
            <List>
              <Slide direction="right" in={drawerOpen} mountOnEnter unmountOnExit>
                <div>
                  {/* <ListItem button component={Link} to="/Dspace/User/ListOfUsers" className={classes.listItem}>
                    <ListItemIcon><AccountCircleIcon /></ListItemIcon>
                    <ListItemText primary="User Management" classes={{ primary: classes.listItemText }} />
                  </ListItem> */}
                  <ListItem button component={Link} to="/Dspace/Communities/ListOfCommunities" className={classes.listItem}>
                    <ListItemIcon><BusinessIcon /></ListItemIcon>
                    <ListItemText primary="Communities Management" classes={{ primary: classes.listItemText }} />
                  </ListItem>
                  <ListItem button component={Link} to="/Dspace/Collection/ListOfCollection" className={classes.listItem}>
                    <ListItemIcon><CollectionsIcon /></ListItemIcon>
                    <ListItemText primary="Collection Management" classes={{ primary: classes.listItemText }} />
                  </ListItem>
                  <ListItem button component={Link} to="/Dspace/Collection/listItem/-1" className={classes.listItem}>
                    <ListItemIcon><LibraryBooksIcon /></ListItemIcon>
                    <ListItemText primary="Items Management" classes={{ primary: classes.listItemText }} />
                  </ListItem>
                  {/* <ListItem button component={Link} to="/Dspace/Author/ListOfAuthor" className={classes.listItem}>
                    <ListItemIcon><PersonIcon /></ListItemIcon>
                    <ListItemText primary="Author Management" classes={{ primary: classes.listItemText }} />
                  </ListItem> */}
                  {/* <ListItem button component={Link} to="/Dspace/Metadata/ListOfMetadata" className={classes.listItem}>
                    <ListItemIcon><StorageIcon /></ListItemIcon>
                    <ListItemText primary="Metadata Management" classes={{ primary: classes.listItemText }} />
                  </ListItem> */}
                  <ListItem button component={Link} to="/Dspace/Group/ListOfGroupStaff" className={classes.listItem}>
                    <ListItemIcon><GroupIcon /></ListItemIcon>
                    <ListItemText primary="Group Info" classes={{ primary: classes.listItemText }} />
                  </ListItem>
                  <ListItem button component={Link} to="" className={classes.listItem}>
                    <ListItemIcon><HelpOutlineIcon /></ListItemIcon>
                    <ListItemText primary="Tool and Tip" classes={{ primary: classes.listItemText }} />
                  </ListItem>
                </div>
              </Slide>
            </List>
          </div>
        </Drawer>
      </div>
    </ThemeProvider>
  );
};

export default Menu;
