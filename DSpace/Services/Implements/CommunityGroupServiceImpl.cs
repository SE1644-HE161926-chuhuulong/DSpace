using Application.CommunityGroups;
using Application.GroupPeoples;
using Application.Responses;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Services.Implements
{
   public class CommunityGroupServiceImpl : CommunityGroupService
   {
      public IMapper _mapper;
      private readonly DSpaceDbContext _context;


      protected ResponseDTO _response;

      public CommunityGroupServiceImpl(IMapper mapper, DSpaceDbContext context)
      {
         _mapper = mapper;
         _context = context;
         _response = new ResponseDTO();
      }

      public async Task<ResponseDTO> AddCommunityManageGroup(CommunityGroupDTOForCreate communityGroupDTO)
      {
         try
         {
            var groupCheck = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId.Equals(communityGroupDTO.GroupId));
            var communityCheck = await _context.Communities.SingleOrDefaultAsync(x => x.CommunityId.Equals(communityGroupDTO.CommunityId));
            if (groupCheck == null || communityCheck == null)
            {
               _response.IsSuccess = true;
               if (groupCheck == null)
               {
                  _response.Message = "Group " + communityGroupDTO.GroupId + " does not exist";
               }
               if (communityCheck == null)
               {
                  _response.Message = "Community " + communityGroupDTO.CommunityId + " does not exist";
               }
            }
            else
            {
               CommunityGroup communityGroup = _mapper.Map<CommunityGroup>(communityGroupDTO);
               await _context.CommunitiesGroups.AddAsync(communityGroup);
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
      public async Task<ResponseDTO> UpdateCommunityManageGroup(CommunityGroupDTOForUpdate communityGroupDTO)
      {
         try
         {
            var groupCheck = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId.Equals(communityGroupDTO.GroupId));
            var communityCheck = await _context.Communities.SingleOrDefaultAsync(x => x.CommunityId.Equals(communityGroupDTO.CommunityId));
            var communityGroupCheck = await _context.CommunitiesGroups.SingleOrDefaultAsync(x => x.Id.Equals(communityGroupDTO.Id));
            if (groupCheck == null || communityCheck == null|| communityGroupCheck==null)
            {
               _response.IsSuccess = false;
               if (groupCheck == null)
               {
                  _response.Message = "Group " + communityGroupDTO.GroupId + " does not exist";
               }
               if (communityCheck == null)
               {
                  _response.Message = "Community " + communityGroupDTO.CommunityId + " does not exist";
               }
               if (communityGroupCheck == null) { 
                  _response.Message = "Community Group" + communityGroupDTO.Id + " does not exist";

               }
            }
            else
            {
               _context.Entry(communityGroupCheck).CurrentValues.SetValues(communityGroupDTO);
              
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

      public async Task<ResponseDTO> DeleteCommunityManageInGroup(int id)
      {
         try
         {
            var communityGroupCheck = await _context.CommunitiesGroups
               .SingleOrDefaultAsync(x => x.Id.Equals(id));
            if (communityGroupCheck == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Group does not manage this community";
            }
            else
            {
               _context.CommunitiesGroups.Remove(communityGroupCheck);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Delete group manage community success";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetListCommunityGroup()
      {
         try
         {
            IEnumerable<CommunityGroup> communityGroups = await _context.CommunitiesGroups
               .Include(x=>x.Group)
               .Include(x=>x.Community)
               .ToListAsync();
            var result = _mapper.Map<List<CommunityGroupDTOForDetail>>(communityGroups);
            _response.IsSuccess = true;
            _response.ObjectResponse= result;
            
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<List<Group>> GetListGroupByCommunityId(int communityId)
      {
         try {
            List<Group> groups = await _context.CommunitiesGroups
               .Where(x => x.CommunityId == communityId)
               .Select(x=>x.Group)
               .ToListAsync();
            return groups;
         } catch (Exception ex) {
            throw new Exception(ex.Message);
         }
      }

      public async Task<List<Community>> GetListCommunityByGroupId(int groupId)
      {

         try
         {
            List<Community> communities = await _context.CommunitiesGroups
               .Where(x => x.GroupId == groupId)
               .Select(x => x.Community)
               .ToListAsync();
            return communities;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }
   }
}
