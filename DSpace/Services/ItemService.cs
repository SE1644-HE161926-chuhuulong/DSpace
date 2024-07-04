using Application.Items;
using Application.Metadatas;
using Application.Responses;

namespace DSpace.Services;

public interface ItemService
{
    public Task<ResponseDTO> GetAllItem(int pageIndex,int pageSize);
    public Task<ResponseDTO> GetAllItemInOneCollection(int collectionId, int pageIndex, int pageSize);
    public Task<ResponseDTO> DeleteItem(int itemId);
   public Task<ResponseDTO> CreateSimpleItem(ItemDTOForCreateSimple itemDTO, List<IFormFile> multipleFiles, int submmitterId);
   public Task<ResponseDTO> GetItemSimpleById(int itemId);
   public Task<ResponseDTO> GetItemFullById(int itemId);
   public Task<ResponseDTO> ModifyItem(MetadataValueDTOForModified metadataValueDTOForModified,int submmitter,int itemId);
   public Task<ResponseDTO> GetAllItemByCollectionIdForUser(int collectionId, int pageIndex, int pageSize);
   public Task<ResponseDTO> GetItemSimpleByIdForUser(int itemId);
   public Task<ResponseDTO> GetItemFullByIdForUser(int itemId);
   public Task<ResponseDTO> GetListItemRecentForUser(string userEmail);
   public Task<ResponseDTO> GetAllItemByPeopleId(int PeopleId, int pageIndex, int pageSize);
   public Task<ResponseDTO> CreateSimpleItemByStaff(ItemDTOForCreateSimple itemDTO, int submmitterId);
   public Task<ResponseDTO> GetItemSimpleByIdByStaff(int itemId,int staffId);
   public Task<ResponseDTO> GetItemFullByIdByStaff(int itemId,int staffId);
   public Task<ResponseDTO> ModifyItemByStaff(MetadataValueDTOForModified metadataValueDTOForModified, int submmitter, int itemId);
   public Task<ResponseDTO> UpdateStatus(int itemId,bool discoverable,int peopleId); 
   public Task<ResponseDTO> UpdateCollection(int itemId,int collectionId,int peopleId);
   public Task<ResponseDTO> SearchItem(ItemDtoForSearch itemDtoForSearch, int pageIndex, int pageSize);
   public Task<ResponseDTO> SearchItemForUser(ItemDtoForSearch itemDtoForSearch, int pageIndex, int pageSize);
   public Task<ResponseDTO> SearchItemByTitle(string title);
   public Task<ResponseDTO> SearchItemByTitleForUser(string title);
   public Task<ResponseDTO> Get5ItemRecentlyInOneCollection(int collectionId);
   public Task<ResponseDTO> Get5ItemRecentlyInOneCollectionForUser(int collectionId);
}