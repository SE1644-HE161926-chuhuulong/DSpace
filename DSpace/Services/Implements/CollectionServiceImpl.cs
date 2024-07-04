using Application.CollectionGroups;
using Application.Collections;
using Application.Collections;
using Application.Responses;
using AutoMapper;
using Domain;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Services.Implements
{
   public class CollectionServiceImpl : CollectionService
   {
      public IMapper _mapper;
      private readonly DSpaceDbContext _context;
      private static readonly string[] Scopes = new[] { DriveService.Scope.DriveFile };
      protected ResponseDTO _response;
      private GroupPeopleService _groupPeopleService;
      private CollectionGroupService _collectionGroupService;
      private SubcribeService _subcribeService;

      public CollectionServiceImpl(IMapper mapper, DSpaceDbContext context, GroupPeopleService groupPeopleService, CollectionGroupService collectionGroupService, SubcribeService subcribeService)
      {
         _context = context;
         _response = new ResponseDTO();
         _mapper = mapper;
         _groupPeopleService = groupPeopleService;
         _collectionGroupService = collectionGroupService;
         _subcribeService = subcribeService;
      }
      public async Task<ResponseDTO> CreateCollection(CollectionDTOForCreateOrUpdate collectionDTO, int userCreateId)
      {
         try
         {
            Collection collection = new Collection();
            collection.CollectionName = collectionDTO.CollectionName;
            collection.LogoUrl = collectionDTO.LogoUrl;
            collection.CommunityId = collectionDTO.CommunityId;

            collection.ShortDescription = collectionDTO.ShortDescription;
            collection.CreateBy = userCreateId;
            collection.UpdateBy = userCreateId;
            collection.isActive = collectionDTO.isActive;
            collection.License = collectionDTO.License;
            collection.EntityTypeId = collectionDTO.EntityTypeId;
            collection.FolderId = await CreateFolder(GetUserCredential(), collectionDTO.CollectionName);
            collection.CreateTime = DateTime.Now;
            collection.UpdateTime = DateTime.Now;
            await _context.Collections.AddAsync(collection);
            await _context.SaveChangesAsync();

            _response.IsSuccess = true;
            _response.Message = "Add Collection success";
            //_response.ObjectResponse = _mapper.Map<CollectionDTOForDetail>(collection);

         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<string> CreateFolder(Task<UserCredential> credential, string FolderName)
      {
         DriveService service = new DriveService(new BaseClientService.Initializer()
         {
            HttpClientInitializer = await credential
         });

         Google.Apis.Drive.v3.Data.File FileMetaData = new Google.Apis.Drive.v3.Data.File()
         {
            Name = FolderName,
            MimeType = "application/vnd.google-apps.folder",
            Parents = new List<string>()
            {
               "19db_KTeQlQcl4fre2BMyRc80-ydKd7gk"
            }
         };

         var request = service.Files.Create(FileMetaData);
         request.Fields = "id";
         var folder = await request.ExecuteAsync();
         return folder.Id;
      }

      public async Task<UserCredential> GetUserCredential()
      {
         UserCredential credential = null;
         using (var stream =
                new FileStream(
                   "client_secret_336699035226-8c0s3uv2pee71adqarpuiicagplfpkvj.apps.googleusercontent.com.json",
                   FileMode.Open, FileAccess.ReadWrite))
         {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
               GoogleClientSecrets.FromStream(stream).Secrets,
               Scopes,
               "user",
               CancellationToken.None,
               new FileDataStore("DSpace v2.0", true));
         }

         return credential;
      }

      public async Task<ResponseDTO> DeleteCollection(int collectionId)
      {
         try
         {
            var check = await _context.Collections.Include(x => x.Items).SingleOrDefaultAsync(x => x.CollectionId == collectionId);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Collection doesnot exist";
            }
            else
            {
               if (check.Items.Count != 0)
               {
                  _response.IsSuccess = false;
                  _response.Message = "Collection have item, if you want delete collection please delete all item in the collection";
               }
               else
               {
                  _context.Collections.Remove(check);
                  await _context.SaveChangesAsync();
                  _response.IsSuccess = true;
                  _response.Message = "Delete Collection success";
               }
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCollectionByID(int id)
      {
         try
         {
            var collection = await _context.Collections
               .Include(x => x.EntityType)
               .Include(x => x.Community)
               .Include(x => x.People)
               .ThenInclude(x => x.User)
               .Include(x => x.Items)
               .Include(x => x.CollectionGroups)
               .ThenInclude(x=>x.Group)
               .SingleOrDefaultAsync(x => x.CollectionId == id);
            if (collection == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Collection with id " + id + " does not exist";
            }
            else
            {
               var result = _mapper.Map<CollectionDTOForDetail>(collection);
               result.CollectionGroupDTOForDetails = _mapper.Map<List<CollectionGroupDTOForDetail>>(collection.CollectionGroups);

               _response.IsSuccess = true;
               _response.Message = "Get Collection success";
               _response.ObjectResponse = result;
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCollectionByName(string name)
      {
         try
         {
            IEnumerable<Collection> objList = await _context.Collections.Where(x => x.CollectionName.Contains(name)).Include(x => x.Community).Include(x => x.Items).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CollectionDTOForSelectList>>(objList);
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> UpdateCollection(int id, CollectionDTOForCreateOrUpdate collectionDTO, int userUpdateId)
      {
         try
         {
            var check = await _context.Collections.FindAsync(id);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Collection with id " + id + " does not exist";
            }
            else
            {
               check.CollectionName = collectionDTO.CollectionName;
               check.LogoUrl = collectionDTO.LogoUrl;
               check.UpdateTime = DateTime.Now;
               check.ShortDescription = collectionDTO.ShortDescription;
               check.UpdateBy = userUpdateId;
               check.isActive = collectionDTO.isActive;
               check.License = collectionDTO.License;
               check.CommunityId = collectionDTO.CommunityId;
               _context.Collections.Update(check);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "update Collection success";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetAllCollection()
      {
         try
         {
            IEnumerable<Collection> objList = await _context.Collections.Include(x => x.Community).Include(x => x.People).ThenInclude(x => x.User).Include(x => x.Items).ToListAsync();

            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CollectionDTOForSelectList>>(objList);

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCollectionByCommunityId(int communityId)
      {
         try
         {
            IEnumerable<Collection> objList = await _context.Collections
               .Where(x => x.CommunityId == communityId)
               .Include(x => x.Items)
               .Include(x => x.Community)
               .Include(x => x.EntityType)
               .Include(x => x.People)


               .ThenInclude(x => x.User).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CollectionDTOForSelectList>>(objList);
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCollectionByIDForUser(int id, string email)
      {
         try
         {
            var Collection = await _context.Collections.
               Where(x => x.isActive)

               .SingleOrDefaultAsync(x => x.CollectionId == id);
            if (Collection == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Collection with id " + id + " does not exist";
            }
            else
            {
               _response.IsSuccess = true;
               _response.Message = "Get Collection success";
               var objMap = _mapper.Map<CollectionDTOForSelectOfUser>(Collection);
               var subcribeCheck = await _subcribeService.CheckSubcribe(id, email);
               objMap.IsSubcribed = subcribeCheck;
               _response.ObjectResponse = objMap;
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCollectionByNameForUser(string name, string email)
      {
         try
         {
            IEnumerable<Collection> objList = await _context.Collections.Where(x => x.CollectionName.Contains(name) && x.isActive).Include(x => x.Community).ToListAsync();
            var objMap = _mapper.Map<List<CollectionDTOForSelectOfUser>>(objList);
            foreach (var collectionMap in objMap) {
               collectionMap.IsSubcribed = await _subcribeService.CheckSubcribe(collectionMap.CollectionId, email);
            }
            _response.IsSuccess = true;
            _response.ObjectResponse = objMap;
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetAllCollectionForUser(string email)
      {
         try
         {
            IEnumerable<Collection> objList = await _context.Collections.Where(x => x.isActive).Include(x => x.Community).ToListAsync();
            var objMap = _mapper.Map<List<CollectionDTOForSelectOfUser>>(objList);
            foreach (var collectionMap in objMap)
            {
               collectionMap.IsSubcribed = await _subcribeService.CheckSubcribe(collectionMap.CollectionId, email);
            }
            
            _response.IsSuccess = true;
            _response.ObjectResponse = objMap;

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCollectionByCommunityIdForUser(int communityId, string email)
      {
         try
         {
            IEnumerable<Collection> objList = await _context.Collections.Where(x => x.CommunityId == communityId && x.isActive).Include(x => x.Community).ToListAsync();
            var objMap = _mapper.Map<List<CollectionDTOForSelectOfUser>>(objList);
            foreach (var collectionMap in objMap)
            {
               collectionMap.IsSubcribed = await _subcribeService.CheckSubcribe(collectionMap.CollectionId, email);
            }

            _response.IsSuccess = true;
            _response.ObjectResponse = objMap;
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCollectionByPeopleId(int peopleId)
      {
         try
         {
            List<Group> groups = await _groupPeopleService.GetListGroupByPeopleId(peopleId);
            List<Collection> collections = new List<Collection>();
            foreach (Group group in groups)
            {
               collections.AddRange(await _collectionGroupService.GetListCollectionByGroupId(group.GroupId));

            }
            if (collections.Count() == 0)
            {
               _response.IsSuccess = true;
               _response.Message = "Not Found";
            }
            else
            {
               _response.IsSuccess = true;
               _response.ObjectResponse = _mapper.Map<List<CollectionDTOForSelectList>>(collections);
            }
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCollectionByCollectionIdAndPeopleId(int collectionId, int peopleId)
      {
         try
         {
            List<Group> groups = await _groupPeopleService.GetListGroupByPeopleId(peopleId);
            List<Collection> collections = new List<Collection>();
            foreach (Group group in groups)
            {
               collections.AddRange(await _collectionGroupService.GetListCollectionByGroupId(group.GroupId));

            }
            if (collections.SingleOrDefault(x=>x.CollectionId==collectionId)==null)
            {
               _response.IsSuccess = false;
               _response.Message = "You can not access this collection";
            }
            else
            {
               _response.IsSuccess = true;
               _response.ObjectResponse = _mapper.Map<CollectionDTOForDetail>(collections.SingleOrDefault(x => x.CollectionId == collectionId));
            }
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }
   }
}