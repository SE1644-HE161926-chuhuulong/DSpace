using Application.CollectionGroups;
using Application.CommunityGroups;
using Application.GroupPeoples;
using Application.Responses;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Services.Implements
{
   public class CollectionGroupServiceImpl : CollectionGroupService
   {
      public IMapper _mapper;
      private readonly DSpaceDbContext _context;


      protected ResponseDTO _response;

      public CollectionGroupServiceImpl(IMapper mapper, DSpaceDbContext context)
      {
         _mapper = mapper;
         _context = context;
         _response = new ResponseDTO();
      }

      public async Task<ResponseDTO> AddCollectionManageGroup(CollectionGroupDTOForCreate CollectionGroupDTO)
      {
         try
         {
            var groupCheck = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId.Equals(CollectionGroupDTO.GroupId));
            var CollectionCheck = await _context.Collections.SingleOrDefaultAsync(x => x.CollectionId.Equals(CollectionGroupDTO.CollectionId));
            if (groupCheck == null || CollectionCheck == null)
            {
               _response.IsSuccess = true;
               if (groupCheck == null)
               {
                  _response.Message = "Group " + CollectionGroupDTO.GroupId + " does not exist";
               }
               if (CollectionCheck == null)
               {
                  _response.Message = "Collection " + CollectionGroupDTO.CollectionId + " does not exist";
               }
            }
            else
            {
               CollectionGroup CollectionGroup = _mapper.Map<CollectionGroup>(CollectionGroupDTO);
               await _context.CollectionGroups.AddAsync(CollectionGroup);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Add success";
            }

         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }
      public async Task<ResponseDTO> UpdateCollectionManageGroup(CollectionGroupDTOForUpdate collectionGroupDTO)
      {
         try
         {
            var groupCheck = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId.Equals(collectionGroupDTO.GroupId));
            var collectionCheck = await _context.Collections.SingleOrDefaultAsync(x => x.CollectionId.Equals(collectionGroupDTO.CollectionId));
            var collectionGroupCheck = await _context.CollectionGroups.SingleOrDefaultAsync(x => x.Id.Equals(collectionGroupDTO.Id));
            if (groupCheck == null || collectionCheck == null || collectionGroupCheck == null)
            {
               _response.IsSuccess = false;
               if (groupCheck == null)
               {
                  _response.Message = "Group " + collectionGroupDTO.GroupId + " does not exist";
               }
               if (collectionCheck == null)
               {
                  _response.Message = "Collection " + collectionGroupDTO.CollectionId + " does not exist";
               }
               if (collectionGroupCheck == null)
               {
                  _response.Message = "Collection Group" + collectionGroupDTO.Id + " does not exist";

               }
            }
            else
            {
               _context.Entry(collectionGroupCheck).CurrentValues.SetValues(collectionGroupDTO);

               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Update success";
            }

         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> DeleteCollectionManageInGroup(int id)
      {
         try
         {
            var CollectionGroupCheck = await _context.CollectionGroups.SingleOrDefaultAsync(x => x.Id == id);
            if (CollectionGroupCheck == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Group does not manage this Collection";
            }
            else
            {
               _context.CollectionGroups.Remove(CollectionGroupCheck);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Delete group manage Collection success";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }
      public async Task<ResponseDTO> GetListCollectionGroup()
      {
         try
         {
            IEnumerable<CollectionGroup> collectionGroups = await _context.CollectionGroups.Include(x=>x.Group).Include(x=>x.Collection).ToListAsync();

            var result = _mapper.Map<List<CollectionGroupDTOForDetail>>(collectionGroups);
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
      public async Task<List<Group>> GetListGroupByCollectionId(int CollectionId)
      {
         try
         {
            List<Group> groups = await _context.CollectionGroups.Where(x => x.CollectionId == CollectionId).Select(x => x.Group).ToListAsync();
            return groups;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }

      public async Task<List<Collection>> GetListCollectionByGroupId(int groupId)
      {
         try
         {
            List<Collection> collections = await _context.CollectionGroups.Where(x => x.GroupId == groupId).Select(x => x.Collection).ToListAsync();
            return collections;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }

      public async Task<CollectionGroup> GetCollectionGroupByGroupIdAndCollectionId(int collectionId, int groupId)
      {
         try
         {
           var collections = await _context.CollectionGroups.SingleOrDefaultAsync(x => x.GroupId == groupId && x.CollectionId == collectionId);
            return collections;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }
   }
}
