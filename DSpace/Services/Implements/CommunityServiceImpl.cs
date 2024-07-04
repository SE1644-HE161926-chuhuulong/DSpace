using Application.Communities;
using Application.Responses;
using Domain;
using AutoMapper;
using Infrastructure;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Application.CommunityGroups;

namespace DSpace.Services.Implements
{
   public class CommunityServiceImpl : CommunityService
   {
      protected IMapper _mapper;
      private readonly DSpaceDbContext _context;
      private GroupPeopleService _groupPeopleService;
      private CommunityGroupService _communityGroupService;


      protected ResponseDTO _response;

      public CommunityServiceImpl(IMapper mapper, DSpaceDbContext context,GroupPeopleService groupPeopleService,CommunityGroupService communityGroupService)
      {
         _context = context;
         _response = new ResponseDTO();
         _mapper = mapper;
         _groupPeopleService = groupPeopleService;
         _communityGroupService = communityGroupService;
      }

      public async Task<ResponseDTO> CreateCommunity(CommunityDTOForCreateOrUpdate communityDTO, int userCreateId)
      {
         try
         {
            Community community = new Community();
            community.CommunityName = communityDTO.CommunityName;
            community.LogoUrl = communityDTO.LogoUrl;
            community.ShortDescription = communityDTO.ShortDescription;
            community.CreateBy = userCreateId;
            community.isActive = communityDTO.isActive;
            community.ParentCommunityId = communityDTO.ParentCommunityId;
            community.CreateTime = DateTime.Now;
            community.UpdateTime = DateTime.Now;
            await _context.Communities.AddAsync(community);
            await _context.SaveChangesAsync();

            _response.IsSuccess = true;
            _response.Message = "Add Community success";
            _response.ObjectResponse = _mapper.Map<CommunityDTOForSelect>(community);

         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> DeleteCommunity(int communityId)
      {
         try
         {
            var check = await _context.Communities.Include(x=>x.Collections).Include(x=>x.SubCommunities).SingleOrDefaultAsync(x=>x.CommunityId==communityId);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Community doesnot exist";
            }
            else
            {
               if (check.Collections.Count!=0 || check.SubCommunities.Count != 0)
               {
                  _response.IsSuccess = false;
                  _response.Message = "Community have collection or sub community, if you want delete this community please delete all collection or subcommunity in the community";
               }
               else
               {
                  _context.Communities.Remove(check);
                  await _context.SaveChangesAsync();
                  _response.IsSuccess = true;
                  _response.Message = "Delete Community success";
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

      public async Task<ResponseDTO> GetCommunityByID(int id)
      {
         try
         {
            var community = await _context.Communities.
               Include(x=>x.CommunityGroups)
               .ThenInclude(x=>x.Group)
               .Include(x=>x.People)
               .ThenInclude(x=>x.User)
               .SingleOrDefaultAsync(x=>x.CommunityId== id);
            if (community == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Community with id " + id + " does not exist";
            }
            else
            {

               var result = _mapper.Map<CommunityDTOForDetail>(community);
               result.communityGroupDTOForDetails = _mapper.Map<List<CommunityGroupDTOForDetail>>(community.CommunityGroups);
               _response.IsSuccess = true;
               _response.Message = "Get Community success";
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

      public async Task<ResponseDTO> GetCommunityByName(string name)
      {
         try
         {
            IEnumerable<Community> objList = await _context.Communities.Where(x => x.CommunityName.Contains(name)).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CommunityDTOForSelect>>(objList);
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> UpdateCommunity(int id, CommunityDTOForCreateOrUpdate communityDTO, int userUpdateId)
      {
         try
         {
            var check = await _context.Communities.FindAsync(id);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Community with id " + id + " does not exist";
            }
            else
            {
               check.CommunityName = communityDTO.CommunityName;
               check.LogoUrl = communityDTO.LogoUrl;
               check.UpdateTime = DateTime.Now;
               check.ShortDescription = communityDTO.ShortDescription;
               check.UpdateBy = userUpdateId;
               check.isActive = communityDTO.isActive;
               check.ParentCommunityId = communityDTO.ParentCommunityId;

               _context.Communities.Update(check);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "update community success";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetAllCommunity()
      {
         try
         {
            IEnumerable<Community> objList = await _context.Communities.Include(x => x.People).ThenInclude(x => x.User).ToListAsync();

            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CommunityDTOForSelect>>(objList);

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetAllCommunityParent()
      {
         try
         {
            IEnumerable<Community> objList = await _context.Communities
               .Where(x=>x.ParentCommunityId==null)
               .ToListAsync();
               

            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CommunityDTOForSelectOfUser>>(objList);

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCommunityByParentId(int communityParentId)
      {
         try
         {
            IEnumerable<Community> objList = await _context.Communities.Where(x => x.ParentCommunityId == communityParentId).Include(x=>x.People).ThenInclude(x=>x.User).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CommunityDTOForSelect>>(objList);
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetAllCommunityForUser()
      {
         try
         {
            IEnumerable<Community> objList = await _context.Communities.Where(x => x.isActive).ToListAsync();

            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CommunityDTOForSelectOfUser>>(objList);

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCommunityByParentIdForUser(int communityParentId)
      {
         try
         {
            IEnumerable<Community> objList = await _context.Communities.Where(x => x.ParentCommunityId == communityParentId && x.isActive).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CommunityDTOForSelectOfUser>>(objList);
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }
      public async Task<ResponseDTO> GetCommunityByIDForUser(int id)
      {
         try
         {
            var community = await _context.Communities.Where(x => x.isActive).SingleOrDefaultAsync(x => x.CommunityId == id);
            if (community == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Community with id " + id + " does not exist";
            }
            else
            {
               _response.IsSuccess = true;
               _response.Message = "Get Community success";
               _response.ObjectResponse = _mapper.Map<CommunityDTOForSelectOfUser>(community);
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCommunityByNameForUser(string name)
      {
         try
         {
            IEnumerable<Community> objList = await _context.Communities.Where(x => x.CommunityName.Contains(name) && x.isActive).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<CommunityDTOForSelectOfUser>>(objList);
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetCommunityByPeopleId(int peopleId)
      {
         try
         {
           List<Group>  groups = await _groupPeopleService.GetListGroupByPeopleId(peopleId);
            List<Community> communities = new List<Community>();
            foreach(Group group in groups)
            {
               communities.AddRange(await _communityGroupService.GetListCommunityByGroupId(group.GroupId));

            }
            if (communities.Count() == 0) { 
               _response.IsSuccess = true;
               _response.Message = "Not Found";
            }
            else {
               _response.IsSuccess = true;
               _response.ObjectResponse = _mapper.Map<List<CommunityDTOForSelect>>(communities);
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