import Login from "./components/login";
import SSS from "./components/sss";
import HomepageAdmin from "./Admin/homepage";
import HomepageStaff from "./Staff/homepageStaff";
import HomepageUser from "./User/homepageUser";
import HomepageStu from "./StudentAndLecture/homepageStu";
import SubCommunity from "./StudentAndLecture/subCommunity";
import Items from "./StudentAndLecture/items";
import ListCollection from "./Admin/Items/listCollection";
import ListItems from "./Admin/Items/listItem";
import ListItemsSmall from "./Admin/Items/listItemSmall.js";
import ItemDetails from "./Admin/Items/itemDetails";
import EditItem from "./Admin/Items/editItem";
import CreateItem from "./Admin/Items/createItem";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import CollectionList from "./Admin/collectionManager/collectionList";
import CreateCollection from "./Admin/collectionManager/createCollection";
import CommunitiesList from "./Admin/communitiesManager/communitesList";
import MetadataList from "./Admin/metadataManager/metadataList";
import CreateMetadata from "./Admin/metadataManager/createMetadata";
import CreateCommunity from "./Admin/communitiesManager/createCommunites";
import GroupList from "./Admin/groupManager/groupList";
import CreateGroup from "./Admin/groupManager/createGroup";
import GroupDetails from "./Admin/groupManager/groupDetail";
import GroupStaffDetails from "./Staff/groupStaffDetails.js";
import CollectionDetails from "./Admin/collectionManager/collectionDetail";
import EditGroup from "./Admin/groupManager/editGroup";
import EditCollection from "./Admin/collectionManager/editCollection";
import CommunityDetails from "./Admin/communitiesManager/communitesDetail";
import EditCommunity from "./Admin/communitiesManager/editCommunities";
import MetadataDetails from "./Admin/metadataManager/metadataDetail";
import EditMetadata from "./Admin/metadataManager/editMetadata";
import ListAuthor from "./Admin/authorManager/listAuthor.js";
import Addauthor from "./Admin/authorManager/addAuthor.js";
import EditAuthor from "./Admin/authorManager/editAuthor.js";
import AuthorDetails from "./Admin/authorManager/detailAuthor.js";
import ListUser from "./Admin/peopleManager/listPeople.js";
import AddUser from "./Admin/peopleManager/addPeople.js";
import EditUser from "./Admin/peopleManager/editPeople.js";
import UserDetails from "./Admin/peopleManager/detailsPeople.js";
import AddUserToGroup from "./Admin/groupManager/addPeopletoGroup.js";
import PdfViewer from "./components/pdfViewer.js";
import ModifyItem from "./Admin/Items/modifyItemWithMetadata.js";
import GroupListStaff from "./Staff/groupStaff.js";
import axios from 'axios';
import React , { useEffect, useState } from 'react';
import HomepageReader from "./StudentAndLecture/homepageReader.js";
function App() {

  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);

if(localStorage.getItem("Role") != null){
  if(localStorage.getItem("Role") === "STUDENT"){
    return (
      <Router>
        <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/homepage" element={<HomepageReader />} />
        <Route path="/homepageUser" element={<HomepageUser />} />
        <Route path="/homepageStu" element={<HomepageStu />} />
        <Route path="/Dspace/Communities/ListOfStuCom/" element={<SubCommunity />} />
        <Route path="/Dspace/Communities/ListOfStuCom/Items" element={<Items />} />
        <Route path="/DSpace/ItemDetails/:itemId" element={<ItemDetails />} />
        <Route path="/Dspace/Collection/listItem/:collectionId" element={<ListItems />} />
        <Route path="/Dspace/Collection/listItemsInCollection/:collectionId" element={<ListItemsSmall />} />
        <Route path="/Dspace/Metadata/getMetadata/:metadataId" element={<MetadataDetails/>} />
        <Route path="/DSpace/PdfViewer/:fileId" element={<PdfViewer />} />
        </Routes>
    </Router>
  );
  }else if(localStorage.getItem("Role")=== "STAFF"){
    return (
      <Router>
        <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/homepage" element={<HomepageReader />} />
        <Route path="/homepageUser" element={<HomepageUser />} />
        <Route path="/homepageStaff" element={<HomepageStaff />} />
        <Route path="/Dspace/Communities/ListOfStuCom/" element={<SubCommunity />} />
        <Route path="/Dspace/Communities/ListOfStuCom/Items" element={<Items />} />
        <Route path="/Dspace/Collection/ListOfCollectionCreateItems" element={<ListCollection />} />
        <Route path="/Dspace/Collection/listItem/:collectionId" element={<ListItems />} />
        <Route path="/Dspace/Collection/listItemsInCollection/:collectionId" element={<ListItemsSmall />} />
        <Route path="/DSpace/ItemDetails/:itemId" element={<ItemDetails />} />
        <Route path="/DSpace/EditItem/:itemId" element={<EditItem />} />
        <Route path="/DSpace/CreateItem/:collectionId" element={<CreateItem />} />
        <Route path='/Dspace/Collection/ListOfCollection' element={<CollectionList/>} />
        <Route path='/Dspace/Collection/createCollection' element={<CreateCollection/>} />
        <Route path="/Dspace/Collection/getCollection/:collectionId" element={<CollectionDetails/>} />
        <Route path="/Dspace/Collection/editCollection/:collectionId" element={<EditCollection/>} />
        <Route path="/Dspace/Communities/ListOfCommunities" element={<CommunitiesList/>} />
        <Route path="/Dspace/Communities/createCommunities" element={<CreateCommunity/>} />
        <Route path="/Dspace/Communities/getCommunity/:communityId" element={<CommunityDetails/>} />
        <Route path="/Dspace/Communities/editCommunity/:communityId" element={<EditCommunity/>} />
        <Route path="/Dspace/Metadata/ListOfMetadata" element={<MetadataList/>} />
        <Route path="/Dspace/Metadata/createMetadata" element={<CreateMetadata/>} />
        <Route path="/Dspace/Metadata/editMetadata/:metadataId" element={<EditMetadata/>} />
        <Route path="/Dspace/Metadata/getMetadata/:metadataId" element={<MetadataDetails/>} />
        <Route path="/Dspace/Group/ListOfGroup" element={<GroupList/>} />
        <Route path="/Dspace/Group/ListOfGroupStaff" element={<GroupListStaff/>} />
        <Route path="/Dspace/Group/createGroup" element={<CreateGroup/>} />
        <Route path="/Dspace/Group/getGroupStaff/:groupId" element={<GroupStaffDetails />} />
        <Route path="/Dspace/Group/editGroup/:groupId" element={<EditGroup/>} />
        <Route path="/Dspace/Author/ListOfAuthor" element={<ListAuthor />} />
        <Route path="/Dspace/Author/AddAuthor" element={<Addauthor />} />
        <Route path="/Dspace/Author/editAuthor/:authorId" element={<EditAuthor />} />
        <Route path="/Dspace/Author/AuthorDetails/:authorId" element={<AuthorDetails />} />
        <Route path="/DSpace/PdfViewer/:fileId" element={<PdfViewer />} />
        </Routes>
    </Router>
  );
    
  }else if(localStorage.getItem("Role") === "ADMIN"){
    return (
      <Router>
        <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/homepageUser" element={<HomepageUser />} />
        <Route path="/modifyItem/:itemId" element={<ModifyItem/>} />
        <Route path="/homepageAdmin" element={<HomepageAdmin />} />
        <Route path="/Dspace/Communities/ListOfStuCom/" element={<SubCommunity />} />
        <Route path="/Dspace/Communities/ListOfStuCom/Items" element={<Items />} />
        <Route path="/Dspace/Collection/ListOfCollectionCreateItems" element={<ListCollection />} />
        <Route path="/Dspace/Collection/listItem/:collectionId" element={<ListItems />} />
        <Route path="/Dspace/Collection/listItemsInCollection/:collectionId" element={<ListItemsSmall />} />
        <Route path="/DSpace/ItemDetails/:itemId" element={<ItemDetails />} />
        <Route path="/DSpace/EditItem/:itemId" element={<EditItem />} />
        <Route path="/DSpace/CreateItem/:collectionId" element={<CreateItem />} />
        <Route path='/Dspace/Collection/ListOfCollection' element={<CollectionList/>} />
        <Route path='/Dspace/Collection/createCollection' element={<CreateCollection/>} />
        <Route path="/Dspace/Collection/getCollection/:collectionId" element={<CollectionDetails/>} />
        <Route path="/Dspace/Collection/editCollection/:collectionId" element={<EditCollection/>} />
        <Route path="/Dspace/Communities/ListOfCommunities" element={<CommunitiesList/>} />
        <Route path="/Dspace/Communities/createCommunities" element={<CreateCommunity/>} />
        <Route path="/Dspace/Communities/getCommunity/:communityId" element={<CommunityDetails/>} />
        <Route path="/Dspace/Communities/editCommunity/:communityId" element={<EditCommunity/>} />
        <Route path="/Dspace/Metadata/ListOfMetadata" element={<MetadataList/>} />
        <Route path="/Dspace/Metadata/createMetadata" element={<CreateMetadata/>} />
        <Route path="/Dspace/Metadata/editMetadata/:metadataId" element={<EditMetadata/>} />
        <Route path="/Dspace/Metadata/getMetadata/:metadataId" element={<MetadataDetails/>} />
        <Route path="/Dspace/Group/ListOfGroup" element={<GroupList/>} />
        <Route path="/Dspace/Group/createGroup" element={<CreateGroup/>} />
        <Route path="/Dspace/Group/getGroup/:groupId" element={<GroupDetails />} />
        <Route path="/Dspace/Group/editGroup/:groupId" element={<EditGroup/>} />
        <Route path="/Dspace/Group/addUsertoGroup/:groupId" element={<AddUserToGroup/>} />
        <Route path="/Dspace/Author/ListOfAuthor" element={<ListAuthor />} />
        <Route path="/Dspace/Author/AddAuthor" element={<Addauthor />} />
        <Route path="/Dspace/Author/editAuthor/:authorId" element={<EditAuthor />} />
        <Route path="/Dspace/Author/AuthorDetails/:authorId" element={<AuthorDetails />} />
        <Route path="/Dspace/User/ListOfUsers" element={<ListUser />} />
        <Route path="/Dspace/User/Adduser" element={<AddUser />} />
        <Route path="/Dspace/User/UserDetails/:peopleId" element={<UserDetails />} />
        <Route path="/Dspace/User/editUser/:peopleId" element={<EditUser />} />
        <Route path="/DSpace/PdfViewer/:fileId" element={<PdfViewer />} />
        </Routes>
    </Router>
  );
  }
}


  return (
    <Router>
      <Routes>
      <Route path="/homepage" element={<HomepageReader/>} />
        <Route path="/" element={<Login />} />
        <Route path="/sss" element={<SSS />} />
        <Route path="/modifyItem/:itemId" element={<ModifyItem/>} />
        <Route path="/homepageAdmin" element={<HomepageAdmin />} />
        <Route path="/homepageStaff" element={<HomepageStaff />} />
        <Route path="/homepageUser" element={<HomepageUser />} />
        <Route path="/homepageStu" element={<HomepageStu />} />
        <Route path="/Dspace/Communities/ListOfStuCom/" element={<SubCommunity />} />
        <Route path="/Dspace/Communities/ListOfStuCom/Items" element={<Items />} />
        <Route path="/Dspace/Collection/ListOfCollectionCreateItems" element={<ListCollection />} />
        <Route path="/Dspace/Collection/listItem/:collectionId" element={<ListItems />} />
        <Route path="/DSpace/ItemDetails/:itemId" element={<ItemDetails />} />
        <Route path="/DSpace/EditItem/:itemId" element={<EditItem />} />
        <Route path="/DSpace/CreateItem/:collectionId" element={<CreateItem />} />
        <Route path='/Dspace/Collection/ListOfCollection' element={<CollectionList/>} />
        <Route path='/Dspace/Collection/createCollection' element={<CreateCollection/>} />
        <Route path="/Dspace/Collection/getCollection/:collectionId" element={<CollectionDetails/>} />
        <Route path="/Dspace/Collection/editCollection/:collectionId" element={<EditCollection/>} />
        <Route path="/Dspace/Communities/ListOfCommunities" element={<CommunitiesList/>} />
        <Route path="/Dspace/Communities/createCommunities" element={<CreateCommunity/>} />
        <Route path="/Dspace/Communities/getCommunity/:communityId" element={<CommunityDetails/>} />
        <Route path="/Dspace/Communities/editCommunity/:communityId" element={<EditCommunity/>} />
        <Route path="/Dspace/Metadata/ListOfMetadata" element={<MetadataList/>} />
        <Route path="/Dspace/Metadata/createMetadata" element={<CreateMetadata/>} />
        <Route path="/Dspace/Metadata/editMetadata/:metadataId" element={<EditMetadata/>} />
        <Route path="/Dspace/Metadata/getMetadata/:metadataId" element={<MetadataDetails/>} />
        <Route path="/Dspace/Group/ListOfGroup" element={<GroupList/>} />
        <Route path="/Dspace/Group/createGroup" element={<CreateGroup/>} />
        <Route path="/Dspace/Group/getGroup/:groupId" element={<GroupDetails />} />
        <Route path="/Dspace/Group/editGroup/:groupId" element={<EditGroup/>} />
        <Route path="/Dspace/Author/ListOfAuthor" element={<ListAuthor />} />
        <Route path="/Dspace/Author/AddAuthor" element={<Addauthor />} />
        <Route path="/Dspace/Author/editAuthor/:authorId" element={<EditAuthor />} />
        <Route path="/Dspace/Author/AuthorDetails/:authorId" element={<AuthorDetails />} />
        <Route path="/Dspace/User/ListOfUsers" element={<ListUser />} />
        <Route path="/Dspace/User/Adduser" element={<AddUser />} />
        <Route path="/Dspace/User/UserDetails/:peopleId" element={<UserDetails />} />
        <Route path="/Dspace/User/editUser/:peopleId" element={<EditUser />} />
        <Route path="/DSpace/PdfViewer/:fileId" element={<PdfViewer />} />
      </Routes>
    </Router>
  );
}
export default App;