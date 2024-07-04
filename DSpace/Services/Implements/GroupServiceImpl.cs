using AutoMapper;
using Domain;
using Application.Groups;
using Application.Responses;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application.Peoples;
using Application.CommunityGroups;
using Application.CollectionGroups;

namespace DSpace.Services.Implements
{
   public class GroupServiceImpl : GroupService
   {
      public IMapper _mapper;
      private readonly DSpaceDbContext _context;

      protected ResponseDTO _response;

      public GroupServiceImpl(IMapper mapper, DSpaceDbContext context)
      {
         _context = context;
         _response = new ResponseDTO();
         _mapper = mapper;
      }

      public async Task<ResponseDTO> CreateGroup(GroupDTOForCreateUpdate groupDTO)
      {
         try
         {
            Group group = new Group();
            group.Title = groupDTO.Title;
            group.Description = groupDTO.Description;
            group.isActive = groupDTO.isActive;
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            _response.IsSuccess = true;
            _response.Message = "Add Group success";
            _response.ObjectResponse = _mapper.Map<GroupDTOForSelect>(group);

         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }
      public async Task<ResponseDTO> DeleteGroup(int groupId)
      {
         try
         {
            var check = await _context.Groups.Include(x => x.GroupPeoples).Include(x => x.CommunityGroups).SingleOrDefaultAsync(x => x.GroupId == groupId);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Group doesnot exist";
            }
            else
            {
               if (check.CommunityGroups.Count != 0)
               {
                  _response.IsSuccess = false;
                  _response.Message = "group is manage Community";
               }
               else if (check.GroupPeoples.Count != 0)
               {
                  _response.IsSuccess = false;
                  _response.Message = "group is exist people";
               }
               else
               {
                  _context.Groups.Remove(check);
                  await _context.SaveChangesAsync();
                  _response.IsSuccess = true;
                  _response.Message = "Delete Group success";
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

      public async Task<ResponseDTO> GetGroupByID(int id)
      {
         try
         {
            var group = await _context.Groups.Include(x=>x.CollectionGroups).ThenInclude(x=>x.Collection).Include(x => x.GroupPeoples).ThenInclude(x => x.People).ThenInclude(x=>x.User).Include(x=>x.CommunityGroups).ThenInclude(x=>x.Community).SingleOrDefaultAsync(x => x.GroupId == id);
            if (group == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Group with id " + id + " does not exist";
            }
            else
            {
               GroupDTOForDetail groupResponse = _mapper.Map<GroupDTOForDetail>(group);
               groupResponse.listCommunityGroup = _mapper.Map<List<CommunityGroupDTOForSelectList>>(group.CommunityGroups);
               groupResponse.listCollectionGroup = _mapper.Map<List<CollectionGroupDTOForDetail>>(group.CollectionGroups);
               _response.IsSuccess = true;
               _response.Message = "Add Group success";
               _response.ObjectResponse = groupResponse;
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetGroupByTitle(string title)
      {
         try
         {
            //IEnumerable<Group> objList = await _context.Groups.Where(x => x.Title==title).ToListAsync();

            IEnumerable<Group> objList = await _context.Groups.Where(x => x.Title.Contains(title)).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<GroupDTOForSelect>>(objList);
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> UpdateGroup(int id, GroupDTOForCreateUpdate groupDTO)
      {
         try
         {
            var check = await _context.Groups.FindAsync(id);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Group with id " + id + " does not exist";
            }
            else
            {
               check.Title = groupDTO.Title;
               check.Description = groupDTO.Description;
               check.isActive = groupDTO.isActive;
               _context.Groups.Update(check);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Update group successful";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetAllGroup()
      {
         try
         {
            IEnumerable<Group> objList = await _context.Groups.ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<GroupDTOForSelect>>(objList);

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetGroupByPeopleId(int peopleId)
      {
         try
         {
            IEnumerable<GroupPeople> groupPeoples = await _context.GroupPeoples.Where(x=>x.PeopleId == peopleId).ToListAsync();
            List<Group> objList = new List<Group>();
            foreach (GroupPeople groupPeople in groupPeoples) {
               var group = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId == groupPeople.GroupId);
               objList.Add(group);
            }
           
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<GroupDTOForSelect>>(objList);

         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetGroupByIDForStaff(int groupId, int peopleId)
      {
         try
         {
            var group = await _context.Groups.Include(x => x.CollectionGroups).ThenInclude(x => x.Collection).Include(x => x.GroupPeoples).ThenInclude(x => x.People).ThenInclude(x => x.User).Include(x => x.CommunityGroups).ThenInclude(x => x.Community).SingleOrDefaultAsync(x => x.GroupId == groupId);
            if (group == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Group with id " + groupId + " does not exist";
            }
            else
            {
               if (group.GroupPeoples.Any(x => x.PeopleId == peopleId))
               {
                  GroupDTOForDetail groupResponse = _mapper.Map<GroupDTOForDetail>(group);
                  groupResponse.listCommunityGroup = _mapper.Map<List<CommunityGroupDTOForSelectList>>(group.CommunityGroups);
                  groupResponse.listCollectionGroup = _mapper.Map<List<CollectionGroupDTOForDetail>>(group.CollectionGroups);
                  _response.IsSuccess = true;
                  _response.Message = "Add Group success";
                  _response.ObjectResponse = groupResponse;
               }
               else { 
                  _response.IsSuccess = false;
                  _response.Message = "You not in this group";
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
   }
}

