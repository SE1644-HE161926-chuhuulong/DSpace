using Application.Authors;
using Application.Collections;
using Application.Emails;
using Application.FileUploads;
using Application.Items;
using Application.Metadatas;
using Application.Responses;
using AutoMapper;
using Domain;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace DSpace.Services.Implements;

public class ItemServiceImpl : ItemService
{
   private readonly DSpaceDbContext _context;
   private readonly IMapper _mapper;
   protected ResponseDTO _response;
   private CollectionService _collectionService;
   private MetadataValueService _metadataValueService;
   private CollectionGroupService _collectionGroupService;
   private GroupPeopleService _groupPeopleService;
   private StatisticService _statisticService;
   private SubcribeService _subcribeService;
   private EmailService _emailService;
   private static readonly string[] Scopes = new[] { DriveService.Scope.DriveFile };

   public ItemServiceImpl(DSpaceDbContext context,
      IMapper mapper,
      MetadataValueService metadataValueService,
      GroupPeopleService groupPeopleService,
      CollectionGroupService collectionGroupService,
      CollectionService collectionService,
      StatisticService statisticService,
      SubcribeService subcribeService,
      EmailService emailService)
   {
      _context = context;
      _mapper = mapper;
      _response = new ResponseDTO();
      _metadataValueService = metadataValueService;
      _groupPeopleService = groupPeopleService;
      _collectionGroupService = collectionGroupService;
      _collectionService = collectionService;
      _statisticService = statisticService;
      _subcribeService = subcribeService;
      _emailService = emailService;
   }

   public async Task<ResponseDTO> GetAllItemInOneCollection(int collectionId, int pageIndex, int pageSize)
   {
      try
      {
         var totalAmount = _context.Items
             .Where(x => x.CollectionId == collectionId)
             .Count();
         var objList = await _context.Items
             .Include(i => i.Collection)
             .Include(x => x.MetadataValue)
             .Where(x => x.CollectionId == collectionId)
             .Skip((pageIndex - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();
         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(objList);
         var totalPage = Math.Ceiling((decimal)totalAmount / pageSize);

         var result = new
         {
            totalAmount,
            pageIndex,
            pageSize,
            totalPage,
            objmap

         };


         
         _response.IsSuccess = true;
         _response.ObjectResponse = result;
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }

      return _response;
   }

   public async Task<ResponseDTO> GetAllItem(int pageIndex,int pageSize)
   {
      try
      {
         var totalAmount = _context.Items.Count();
        

         var objList = await _context.Items
             .Include(i => i.Collection)
             .Include(x => x.MetadataValue)
             .ThenInclude(x => x.MetadataFieldRegistry)
             .Skip((pageIndex - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();
         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(objList);
         var totalPage = Math.Ceiling((decimal)totalAmount / pageSize);

         var result = new
         {
            totalAmount,
            pageIndex,
            pageSize,
            totalPage,
            objmap

         };

         _response.IsSuccess = true;
         _response.ObjectResponse = result;
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }

      return _response;
   }

   public async Task<ResponseDTO> DeleteItem(int itemId)
   {
      try
      {
         var item = await _context.Items.Include(x => x.MetadataValue).SingleOrDefaultAsync(x => x.ItemId.Equals(itemId));
         if (item == null)
         {
            _response.IsSuccess = true;
            _response.Message = "Item does not exist";
         }
         else
         {
            _context.MetadataValues.RemoveRange(item.MetadataValue);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            _response.IsSuccess = true;
            _response.Message = "Delete Item success";
         }
      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
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

   public async Task<bool> UploadFile(IList<IFormFile> multipleFile, Task<UserCredential> credential, Item item, int collectionId)
   {
      try
      {
         var collection = await _context.Collections.FindAsync(collectionId);
         if (collection == null)
         {
            return false;
         }
         List<FileUpload> fileUploads = new List<FileUpload>();
         DriveService service = new DriveService(new BaseClientService.Initializer()
         {
            HttpClientInitializer = await credential
         });
         foreach (IFormFile oneFileOnly in multipleFile)
         {
            var insertFile = service.Files.Create(
                new Google.Apis.Drive.v3.Data.File
                {
                   Name = oneFileOnly.FileName,
                   Parents = new List<string>()
                    {
                            collection.FolderId
                    }
                },
                oneFileOnly.OpenReadStream(), oneFileOnly.ContentType);
            insertFile.ChunkSize = Google.Apis.Drive.v3.FilesResource.CreateMediaUpload.MinimumChunkSize * 2;
            insertFile.Body.CopyRequiresWriterPermission = true;
            await insertFile.UploadAsync();
            var permission = new Google.Apis.Drive.v3.Data.Permission()
            {
               Type = "anyone",
               Role = "reader"
            };
            await service.Permissions.Create(permission, insertFile.ResponseBody.Id).ExecuteAsync();
            var fileUpload = new ModifyFileUploadDto()
            {
               FileUrl = Path.Combine(Path.GetTempPath(), insertFile.ResponseBody.Name),
               FileKeyId = insertFile.ResponseBody.Id,
               MimeType = insertFile.ResponseBody.MimeType,
               Kind = insertFile.ResponseBody.Kind,
               FileName = insertFile.ResponseBody.Name,
               CreationTime = DateTime.Now,
               isActive = true,
               ItemId = item.ItemId
            };
            var file = _mapper.Map<FileUpload>(fileUpload);
            fileUploads.Add(file);
         }

         if (fileUploads.Count > 0)
         {
            await _context.FileUploads.AddRangeAsync(fileUploads);
            await _context.SaveChangesAsync();
            return true;
         }
         else
         {
            return false;
         }
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }

   public async Task<ResponseDTO> CreateSimpleItem(ItemDTOForCreateSimple itemDTO, List<IFormFile> multipleFiles, int submitterId)
   {
      try
      {
         Item itemNew = new Item();
         itemNew.LastModified = DateTime.Now;
         itemNew.Discoverable = true;
         itemNew.SubmitterId = submitterId;
         itemNew.CollectionId = itemDTO.CollectionId;

         _context.Items.Add(itemNew);
         _context.SaveChanges();
         itemNew.MetadataValue = new List<MetadataValue>();

         if (itemDTO.Author != null)
         {
            foreach (string author in itemDTO.Author)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = author,
                  TextLang = "",
                  MetadataFieldId = 1,
                  ItemId = itemNew.ItemId
               });
            }
         }

         itemNew.MetadataValue.Add(new MetadataValue
         {
            TextValue = itemDTO.Title,
            TextLang = "",
            MetadataFieldId = 57,
            ItemId = itemNew.ItemId
         });
         if (itemDTO.OtherTitle != null)
         {
            foreach (string otherTitle in itemDTO.OtherTitle)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = otherTitle,
                  TextLang = "",
                  MetadataFieldId = 58,
                  ItemId = itemNew.ItemId
               });
            }
         }
         itemNew.MetadataValue.Add(new MetadataValue
         {
            TextValue = itemDTO.DateOfIssue.ToString(),
            TextLang = "",
            MetadataFieldId = 12,
            ItemId = itemNew.ItemId
         });
         if (itemDTO.Publisher != null)
         {
            itemNew.MetadataValue.Add(new MetadataValue
            {
               TextValue = itemDTO.Publisher,
               TextLang = "",
               MetadataFieldId = 36,
               ItemId = itemNew.ItemId
            });
         }
         if (itemDTO.Citation != null)
         {
            itemNew.MetadataValue.Add(new MetadataValue
            {
               TextValue = itemDTO.Citation,
               TextLang = "",
               MetadataFieldId = 26,
               ItemId = itemNew.ItemId
            });
         }
         if (itemDTO.SeriesNo != null)
         {
            foreach (string serieNo in itemDTO.SeriesNo)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = serieNo,
                  TextLang = "",
                  MetadataFieldId = 39,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.IdentifiersISBN != null)
         {
            foreach (string identifiersISBN in itemDTO.IdentifiersISBN)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = identifiersISBN,
                  TextLang = "",
                  MetadataFieldId = 28,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.IdentifiersISSN != null)
         {
            foreach (string identifiersISSN in itemDTO.IdentifiersISSN)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = identifiersISSN,
                  TextLang = "",
                  MetadataFieldId = 29,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.IdentifiersSICI != null)
         {
            foreach (string identifiersSICI in itemDTO.IdentifiersSICI)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = identifiersSICI,
                  TextLang = "",
                  MetadataFieldId = 30,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.IdentifiersISMN != null)
         {
            foreach (string identifiersISMN in itemDTO.IdentifiersISMN)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = identifiersISMN,
                  TextLang = "",
                  MetadataFieldId = 31,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.IdentifiersOther != null)
         {
            foreach (string identifiersOther in itemDTO.IdentifiersOther)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = identifiersOther,
                  TextLang = "",
                  MetadataFieldId = 32,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.IdentifiersLCCN != null)
         {
            foreach (string identifiersLCCN in itemDTO.IdentifiersLCCN)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = identifiersLCCN,
                  TextLang = "",
                  MetadataFieldId = 33,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.Type != null)
         {
            foreach (string type in itemDTO.Type)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = type,
                  TextLang = "",
                  MetadataFieldId = 59,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.Language != null)
         {
            itemNew.MetadataValue.Add(new MetadataValue
            {
               TextValue = itemDTO.Language,
               TextLang = "",
               MetadataFieldId = 35,
               ItemId = itemNew.ItemId
            });
         }
         if (itemDTO.SubjectKeywords != null)
         {
            foreach (string subjectKeyword in itemDTO.SubjectKeywords)
            {
               itemNew.MetadataValue.Add(new MetadataValue
               {
                  TextValue = subjectKeyword,
                  TextLang = "",
                  MetadataFieldId = 51,
                  ItemId = itemNew.ItemId
               });
            }
         }
         if (itemDTO.Abstract != null)
         {
            itemNew.MetadataValue.Add(new MetadataValue
            {
               TextValue = itemDTO.Abstract,
               TextLang = "",
               MetadataFieldId = 15,
               ItemId = itemNew.ItemId
            });
         }
         if (itemDTO.Sponsors != null)
         {
            itemNew.MetadataValue.Add(new MetadataValue
            {
               TextValue = itemDTO.Sponsors,
               TextLang = "",
               MetadataFieldId = 17,
               ItemId = itemNew.ItemId
            });
         }
         if (itemDTO.Description != null)
         {
            itemNew.MetadataValue.Add(new MetadataValue
            {
               TextValue = itemDTO.Description,
               TextLang = "",
               MetadataFieldId = 14,
               ItemId = itemNew.ItemId
            });
         }

         itemNew.MetadataValue.Add(new MetadataValue
         {
            TextValue = itemDTO.IdentifiersURI + itemNew.ItemId,
            TextLang = "",
            MetadataFieldId = 34,
            ItemId = itemNew.ItemId
         });

         await _context.MetadataValues.AddRangeAsync(itemNew.MetadataValue);
         await _context.SaveChangesAsync();
         var listEmailSubcribed = await _subcribeService.GetListUserEmailSubcribeCollection(itemDTO.CollectionId);
         _emailService.SendEmailBcc(listEmailSubcribed.ToList(), EmailConstants.EMAIL_CREATE_NEW_ITEM + itemDTO.Title, new MimeKit.TextPart(""));

         bool result = await UploadFile(multipleFiles, GetUserCredential(), itemNew, itemNew.CollectionId);
         if (result == true)
         {
            _response.Message = "Add new item and upload successful";
            _response.IsSuccess = true;
            _response.ObjectResponse = itemNew;
         }
         else
         {
            _response.Message = "Encounter an error when add new item";
            _response.IsSuccess = false;
         }



      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;


   }

   public async Task<ResponseDTO> GetItemSimpleById(int itemId)
   {
      try
      {
         var item = await _context.Items.Include(x => x.MetadataValue).Include(x => x.Collection).FirstOrDefaultAsync(x => x.ItemId == itemId);
         if (item == null)
         {
            _response.IsSuccess = true;
            _response.Message = "Not found Item";

         }
         else
         {
            var objmap = _mapper.Map<ItemDTOForSelectDetailSimple>(item);
            _response.IsSuccess = true;
            _response.Message = "Founed";
            _response.ObjectResponse = objmap;
         }
      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
   }
   public async Task<ResponseDTO> ModifyItem(MetadataValueDTOForModified metadataValueDTOForModified, int submitterId, int itemId)
   {
      try
      {
         var item = await _context.Items.SingleOrDefaultAsync(x => x.ItemId == itemId);
         if (item == null)
         {
            throw new Exception("Not found item");

         }
         item.SubmitterId = submitterId;
         item.LastModified = DateTime.Now;
         if (metadataValueDTOForModified.Add != null)
         {
            var responseAdd = await _metadataValueService.AddListMetadataValue(metadataValueDTOForModified.Add);
            if (!responseAdd)
            {
               throw new Exception("Add failed");
            }
         }

         if (metadataValueDTOForModified.Update != null)
         {
            var responseUpdate = await _metadataValueService.UpdateListMetadataValue(metadataValueDTOForModified.Update);
            if (!responseUpdate)
            {
               throw new Exception("Update failed");
            }
         }
         if (metadataValueDTOForModified.Delete != null)
         {
            var responseDelete = await _metadataValueService.DeleteListMetadataValue(metadataValueDTOForModified.Delete);
            if (!responseDelete)
            {
               throw new Exception("Delete failed");
            }
         }

         _response.IsSuccess = true;
         _response.Message = "Update item successful";

      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
   }

   public async Task<ResponseDTO> GetItemFullById(int itemId)
   {
      try
      {
         var item = await _context.Items.Include(x => x.MetadataValue).ThenInclude(x => x.MetadataFieldRegistry).SingleOrDefaultAsync(x => x.ItemId == itemId);
         if (item != null)
         {
            var objectResponse = _mapper.Map<ItemDTOForSelectDetailFull>(item);
            objectResponse.ListMetadataValueDTOForSelect = _mapper.Map<List<MetadataValueDTOForSelect>>(item.MetadataValue);

            _response.IsSuccess = true;
            _response.Message = "Find item Success";
            _response.ObjectResponse = objectResponse;
         }
         else
         {
            _response.IsSuccess = false;
            _response.Message = "Not found item";

         }

      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
   }

   public async Task<ResponseDTO> GetAllItemByCollectionIdForUser(int collectionId, int pageIndex, int pageSize)
   {
      try
      {
         var totalAmount = _context.Items.Where(x => x.CollectionId == collectionId).Count();  
         var objList = await _context.Items.Include(x => x.MetadataValue).Where(x => x.CollectionId == collectionId).ToListAsync();
         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(objList);
         var totalPage = Math.Ceiling((decimal)totalAmount / pageSize);

         var result = new
         {
            totalAmount,
            pageIndex,
            pageSize,
            totalPage,
            objmap
         };

         _response.IsSuccess = true;
         _response.ObjectResponse = result;
      

      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
   }

   public async Task<ResponseDTO> GetItemSimpleByIdForUser(int itemId)
   {
      try
      {
         var item = await _context.Items.Include(x => x.MetadataValue).Include(x => x.Collection).FirstOrDefaultAsync(x => x.ItemId == itemId && x.Discoverable);
         if (item == null)
         {
            _response.IsSuccess = true;
            _response.Message = "Not found Item";

         }
         else
         {
            var objMap = _mapper.Map<ItemDTOForSelectDetailSimpleForUser>(item);
            _response.IsSuccess = true;
            _response.Message = "Founed";
            _response.ObjectResponse = objMap;

            await _statisticService.IncreaseView(item.ItemId, DateTime.Now);


         }

      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
   }

   public async Task<ResponseDTO> GetItemFullByIdForUser(int itemId)
   {
      try
      {
         var item = await _context.Items.Include(x => x.MetadataValue).ThenInclude(x => x.MetadataFieldRegistry).SingleOrDefaultAsync(x => x.ItemId == itemId && x.Discoverable);
         if (item != null)
         {
            var objectResponse = _mapper.Map<ItemDTOForSelectDetailFull>(item);
            objectResponse.ListMetadataValueDTOForSelect = _mapper.Map<List<MetadataValueDTOForSelect>>(item.MetadataValue);

            _response.IsSuccess = true;
            _response.Message = "Find item Success";
            _response.ObjectResponse = objectResponse;
         }
         else
         {
            _response.IsSuccess = false;
            _response.Message = "Not found item";

         }

      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
   }

  public async Task<ResponseDTO> GetAllItemByPeopleId(int peopleId,int pageIndex,int pageSize)
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
            var listItem = new List<ItemDTOForSelectList>();
            foreach (Collection collection in collections)
            {
               var objList = await _context.Items
                   .Include(i => i.Collection)
                   .Include(x => x.MetadataValue)
                   .ThenInclude(x => x.MetadataFieldRegistry)
                   .Where(x => x.CollectionId == collection.CollectionId)
                   .ToListAsync();
               listItem.AddRange(_mapper.Map<List<ItemDTOForSelectList>>(objList));

            }
            var totalAmount = listItem.Count();
            var totalPage = Math.Ceiling((decimal)totalAmount / pageSize);

            var result = new
            {
               totalAmount,
               pageIndex,
               pageSize,
               totalPage,
               objmap = listItem
            };

            _response.IsSuccess = true;
            _response.ObjectResponse = result;
          

         }

      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }
      return _response;
   }

   public async Task<ResponseDTO> CreateSimpleItemByStaff(ItemDTOForCreateSimple itemDTO, int submmitterId)
   {
      try
      {
         var collectionGroup = await _context.CollectionGroups
            .SingleOrDefaultAsync(x => x.CollectionId == itemDTO.CollectionId && x.Group.GroupPeoples.Select(x => x.PeopleId).Contains(submmitterId));
         if (collectionGroup == null || !collectionGroup.canSubmit)
         {
            _response.IsSuccess = false;
            _response.Message = "you not have permission in this collection";

         }
         else
         {
            _response = await CreateSimpleItem(itemDTO,new List<IFormFile>(), submmitterId);
         }
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }
      return _response;
   }

   public async Task<ResponseDTO> GetItemSimpleByIdByStaff(int itemId, int staffId)
   {
      try
      {
         var item = await _context.Items.Include(x => x.Collection).ThenInclude(x => x.CollectionGroups)
            .SingleOrDefaultAsync(x => x.ItemId == itemId && x.Collection.CollectionGroups.Select(x => x.Group.GroupPeoples.Select(x => x.PeopleId).Contains(staffId)).Contains(true));
         if (item == null)
         {
            _response.IsSuccess = false;
            _response.Message = "you not have permission in this Item";

         }
         else
         {
            var collectionGroup = await _context.CollectionGroups
            .SingleOrDefaultAsync(x => x.CollectionId == item.CollectionId && x.Group.GroupPeoples.Select(x => x.PeopleId).Contains(staffId));
            if (collectionGroup == null || !collectionGroup.canReview)
            {
               _response.IsSuccess = false;
               _response.Message = "you not have permission in this collection";

            }
            _response = await GetItemSimpleById(itemId);
         }
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }
      return _response;

   }

   public async Task<ResponseDTO> GetItemFullByIdByStaff(int itemId, int staffId)
   {
      try
      {
         var item = await _context.Items.Include(x => x.Collection).ThenInclude(x => x.CollectionGroups)
            .SingleOrDefaultAsync(x => x.ItemId == itemId && x.Collection.CollectionGroups.Select(x => x.Group.GroupPeoples.Select(x => x.PeopleId).Contains(staffId)).Contains(true));
         if (item == null)
         {
            _response.IsSuccess = false;
            _response.Message = "you not have permission in this Item";

         }
         else
         {
            var collectionGroup = await _context.CollectionGroups
            .SingleOrDefaultAsync(x => x.CollectionId == item.CollectionId && x.Group.GroupPeoples.Select(x => x.PeopleId).Contains(staffId));
            if (collectionGroup == null || !collectionGroup.canReview)
            {
               _response.IsSuccess = false;
               _response.Message = "you not have permission in this collection";

            }
            _response = await GetItemFullById(itemId);
         }
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }
      return _response;
   }

   public async Task<ResponseDTO> ModifyItemByStaff(MetadataValueDTOForModified metadataValueDTOForModified, int submmitterId, int itemId)
   {
      try
      {
         var item = await _context.Items.Include(x => x.Collection).SingleOrDefaultAsync(x => x.ItemId == itemId);
         var collectionGroup = await _context.CollectionGroups
            .SingleOrDefaultAsync(x => x.CollectionId == item.Collection.CollectionId && x.Group.GroupPeoples.Select(x => x.PeopleId).Contains(submmitterId));
         if (collectionGroup == null || !collectionGroup.canSubmit)
         {
            _response.IsSuccess = false;
            _response.Message = "you not have permission in this collection";
         }
         else
         {
            _response = await ModifyItem(metadataValueDTOForModified, submmitterId, itemId);
         }
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }
      return _response;
   }

   public async Task<ResponseDTO> UpdateStatus(int itemId, bool discoverable, int peopleId)
   {
      try
      {
         var item = await _context.Items.SingleOrDefaultAsync(x => x.ItemId == itemId);
         if (item == null)
         {
            _response.IsSuccess = false;
            _response.Message = "Not found item update";
         }
         else
         {
            item.Discoverable = discoverable;
            item.SubmitterId = peopleId;
            item.LastModified = DateTime.Now;
            _context.Items.Update(item);
            _context.SaveChanges();
            _response.IsSuccess = true;
            _response.Message = "update status of item success";
         }

      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
   }

   public async Task<ResponseDTO> UpdateCollection(int itemId, int collectionId, int peopleId)
   {
      try
      {
         var item = await _context.Items.SingleOrDefaultAsync(x => x.ItemId == itemId);
         if (item == null)
         {
            _response.IsSuccess = false;
            _response.Message = "Not found item update";
         }
         else
         {

            var collectionCheck = await _context.Collections.SingleOrDefaultAsync(x => x.CollectionId == collectionId);


            if (collectionCheck == null)
            {
               _response.IsSuccess = false;
               _response.Message = "Not found collection to update";
            }
            else
            {
               item.CollectionId = collectionId;
               item.SubmitterId = peopleId;
               item.LastModified = DateTime.Now;
               _context.Items.Update(item);
               _context.SaveChanges();
               _response.IsSuccess = true;
               _response.Message = "update status of item success";
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

   public async Task<ResponseDTO> GetListItemRecentForUser(string userEmail)
   {
      try
      {

         //var listItems = _context.Items
         //   .Where(x => listCollectionId.Any(collectionId => x.CollectionId == collectionId))
         //   .Select(x=>x.MetadataValue).SingleOrDefault(x=>)
         //   .OrderByDescending(x=>x.MetadataValue.Select(x=>x.TextValue).SingleOrDefault(x=>x.MetadataFieldId == 57));
         List<Item> items = new List<Item>();
         var listCollectionId = await _subcribeService.GetAllCollectionIdByUserEmail(userEmail);
         foreach (int collectionId in listCollectionId)
         {
            var list = await _context.Items.Include(x => x.MetadataValue).Where(x => x.CollectionId == collectionId && x.Discoverable).ToListAsync();
            items.AddRange(list);

         }

         var itemMaps = _mapper.Map<List<ItemDTOForSelectList>>(items).OrderByDescending(x => x.DateOfIssue);

         var result = itemMaps.Take(5);

         _response.IsSuccess = true;
         _response.ObjectResponse = result;

      }
      catch (Exception ex)
      {
         _response.IsSuccess = false;
         _response.Message = ex.Message;
      }
      return _response;
   }

   public async Task<ResponseDTO> SearchItemForUser(ItemDtoForSearch itemDtoForSearch,int pageIndex,int pageSize)
   {
      try
      {
         var listItem = new List<Item>();
         if (itemDtoForSearch.CollectionId == 0)
         {
            listItem = await _context.Items.Include(x => x.MetadataValue).Where(x => x.Discoverable).ToListAsync();
         }
         else
         {
            listItem = await _context.Items.Include(x => x.MetadataValue).Where(x => x.Discoverable && x.CollectionId == itemDtoForSearch.CollectionId).ToListAsync();
         }
         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(listItem);
         if (itemDtoForSearch.Title != null)
         {
            objmap = objmap.Where(x => x.Title.Contains(itemDtoForSearch.Title)).ToList();
         }
         if (itemDtoForSearch.Year != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Year == itemDtoForSearch.Year).ToList();
         }
         if (itemDtoForSearch.Month != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Month == itemDtoForSearch.Month).ToList();
         }
         if (itemDtoForSearch.Day != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Day == itemDtoForSearch.Day).ToList();
         }


         if (itemDtoForSearch.Authors != null)
         {
            foreach (string author in itemDtoForSearch.Authors)
            {
               objmap = objmap.Where(x => x.Authors.Any(y => y.Contains(author))).ToList();
            }
         }
         if (itemDtoForSearch.Keywords != null)
         {
            foreach (string keyword in itemDtoForSearch.Keywords)
            {
               objmap = objmap.Where(x => x.SubjectKeywords.Any(y => y.Contains(keyword))).ToList();
            }
         }
         if (itemDtoForSearch.Publisher != null)
         {
            objmap = objmap.Where(x => x.Publisher.Contains(itemDtoForSearch.Publisher)).ToList();
         }
         if (itemDtoForSearch.Types != null)
         {
            foreach (string type in itemDtoForSearch.Types)
            {
               objmap = objmap.Where(x => x.Types.Any(y => y.Contains(type))).ToList();
            }
         }

         if (itemDtoForSearch.Year != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Year == itemDtoForSearch.Year).ToList();
         }
         if (itemDtoForSearch.Month != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Month == itemDtoForSearch.Month).ToList();
         }
         if (itemDtoForSearch.Day != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Day == itemDtoForSearch.Day).ToList();
         }
         var totalAmount = objmap.Count();
         var totalPage = Math.Ceiling((decimal)totalAmount / pageSize);

         var result = new
         {
            totalAmount,
            pageIndex,
            pageSize,
            totalPage,
            objmap

         };

         _response.IsSuccess = true;
         _response.ObjectResponse = result;



        
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }

      return _response;
   }
   public async Task<ResponseDTO> SearchItem(ItemDtoForSearch itemDtoForSearch,int pageIndex,int pageSize)
   {
      try
      {
         var listItem = new List<Item>();
         if (itemDtoForSearch.CollectionId == 0)
         {

            listItem = await _context.Items.Include(x => x.MetadataValue).ToListAsync();
         }
         else
         {
            listItem = await _context.Items.Include(x => x.MetadataValue).Where(x => x.CollectionId == itemDtoForSearch.CollectionId).ToListAsync();

         }
         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(listItem);

         if (itemDtoForSearch.Title != null)
         {
            objmap = objmap.Where(x => x.Title.ToLower().Replace(" ", "").Contains(itemDtoForSearch.Title)).ToList();
         }
         if (itemDtoForSearch.Year != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Year == itemDtoForSearch.Year).ToList();
         }
         if (itemDtoForSearch.Month != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Month == itemDtoForSearch.Month).ToList();
         }
         if (itemDtoForSearch.Day != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Day == itemDtoForSearch.Day).ToList();
         }
         if (itemDtoForSearch.Authors != null)
         {
            foreach (string author in itemDtoForSearch.Authors)
            {
               objmap = objmap.Where(x => x.Authors.Any(y => y.Contains(author))).ToList();
            }
         }
         if (itemDtoForSearch.Keywords != null)
         {
            foreach (string keyword in itemDtoForSearch.Keywords)
            {
               objmap = objmap.Where(x => x.SubjectKeywords.Any(y => y.Contains(keyword))).ToList();
            }
         }
         if (itemDtoForSearch.Publisher != null)
         {
            objmap = objmap.Where(x => x.Publisher.Contains(itemDtoForSearch.Publisher)).ToList();
         }
         if (itemDtoForSearch.Types != null)
         {
            foreach (string type in itemDtoForSearch.Types)
            {
               objmap = objmap.Where(x => x.Types.Any(y => y.Contains(type))).ToList();
            }
         }
         if (itemDtoForSearch.Year != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Year == itemDtoForSearch.Year).ToList();
         }
         if (itemDtoForSearch.Month != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Month == itemDtoForSearch.Month).ToList();
         }
         if (itemDtoForSearch.Day != null)
         {

            objmap = objmap.Where(x => x.DateOfIssue.Day == itemDtoForSearch.Day).ToList();
         }
         var totalAmount = objmap.Count();
         var totalPage = Math.Ceiling((decimal)totalAmount / pageSize);

         var result = new
         {
            totalAmount,
            pageIndex,
            pageSize,
            totalPage,
            objmap

         };

         _response.IsSuccess = true;
         _response.ObjectResponse = result;
         
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }

      return _response;
   }

   public async Task<ResponseDTO> SearchItemByTitle(string title)
   {
      try
      {
         var listItem = await _context.MetadataValues.Include(x => x.Item).ThenInclude(x => x.MetadataValue).Where(x => x.MetadataFieldId == 57 && x.TextValue.Contains(title)).Select(x => x.Item).Distinct().ToListAsync();
         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(listItem);

         _response.IsSuccess = true;
         _response.ObjectResponse = objmap;
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }

      return _response;
   }

   public async Task<ResponseDTO> SearchItemByTitleForUser(string title)
   {
      try
      {
         //var listItemId = await _context.MetadataValues.Where(x => x.MetadataFieldId == 57 && x.TextValue.Contains(title)).Select(x => x.ItemId).Distinct().ToListAsync();
         //var listItem = new List<Item>();
         //foreach (int itemId in listItemId)
         //{
         //   var item = await _context.Items.Include(x => x.MetadataValue).SingleOrDefaultAsync(x => x.ItemId == itemId && x.Discoverable);
         //   if (item != null) {
         //      listItem.Add(item);
         //   }
         //}

         var listItem = await _context.MetadataValues.Include(x => x.Item).ThenInclude(x => x.MetadataValue).Where(x => x.MetadataFieldId == 57 && x.TextValue.Contains(title) && x.Item.Discoverable).Select(x => x.Item).Distinct().ToListAsync();



         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(listItem);

         _response.IsSuccess = true;
         _response.ObjectResponse = objmap;
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }

      return _response;
   }
   public async Task<ResponseDTO> Get5ItemRecentlyInOneCollection(int collectionId)
   {
      try
      {
         var objList = await _context.Items
             .Include(i => i.Collection)
             .Include(x => x.MetadataValue)
             .Where(x => x.CollectionId == collectionId)

             .ToListAsync();

         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(objList);

         objmap = objmap.OrderBy(x => x.DateOfIssue).TakeLast(5).ToList();
         _response.IsSuccess = true;
         _response.ObjectResponse = objmap;
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }

      return _response;
   }
   public async Task<ResponseDTO> Get5ItemRecentlyInOneCollectionForUser(int collectionId)
   {
      try
      {
         var objList = await _context.Items
             .Include(i => i.Collection)
             .Include(x => x.MetadataValue)
             .Where(x => x.CollectionId == collectionId && x.Discoverable)

             .ToListAsync();

         var objmap = _mapper.Map<List<ItemDTOForSelectList>>(objList);

         objmap = objmap.TakeLast(5).ToList();
         _response.IsSuccess = true;
         _response.ObjectResponse = objmap;
      }
      catch (Exception ex)
      {
         _response.Message = ex.Message;
         _response.IsSuccess = false;
      }

      return _response;
   }
}