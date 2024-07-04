using AutoMapper;
using Domain;
using Application.GroupPeoples;
using Application.Responses;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Services.Implements
{
   public class GroupPeopleServiceImpl : GroupPeopleService
   {
      public IMapper _mapper;
      private readonly DSpaceDbContext _context;


      protected ResponseDTO _response;

      public GroupPeopleServiceImpl(IMapper mapper, DSpaceDbContext context)
      {
         _context = context;
         _response = new ResponseDTO();
         _mapper = mapper;
      }
      public async Task<ResponseDTO> AddPeopleInGroup(GroupPeopleDTO groupPeopleDTO)
      {
         try
         {
            var groupCheck = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId.Equals(groupPeopleDTO.GroupId));
            var peopleCheck = await _context.Peoples.SingleOrDefaultAsync(x => x.PeopleId.Equals(groupPeopleDTO.PeopleId));
            if (groupCheck == null || peopleCheck == null)
            {
               _response.IsSuccess = true;
               if (groupCheck == null)
               {
                  _response.Message = "Group " + groupPeopleDTO.GroupId + " does not exist";
               }
               if (peopleCheck == null)
               {
                  _response.Message = "Group " + groupPeopleDTO.PeopleId + " does not exist";
               }
            }
            else
            {
               GroupPeople groupPeople = _mapper.Map<GroupPeople>(groupPeopleDTO);
               await _context.GroupPeoples.AddAsync(groupPeople);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Add people success";
            }

         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }
      public async Task<ResponseDTO> DeletePeopleInGroup(GroupPeopleDTO groupPeopleDTO)
      {
         try
         {
            var groupPeopleCheck = await _context.GroupPeoples.SingleOrDefaultAsync(x => x.GroupId.Equals(groupPeopleDTO.GroupId) && x.PeopleId.Equals(groupPeopleDTO.PeopleId));
            if (groupPeopleCheck == null)
            {
               _response.IsSuccess = true;
               _response.Message = "People does not exist in group";
            }
            else
            {
               _context.GroupPeoples.Remove(groupPeopleCheck);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Delete people in group success";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> AddListPeopleInGroup(GroupPeopleListDTOForCreateUpdate groupPeopleListDTO)
      {
         try
         {
            var groupCheck = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId.Equals(groupPeopleListDTO.GroupId));

            if (groupCheck == null)
            {
               throw new Exception("Group does not exist");
            }

            List<GroupPeople> listGroupPeople = new List<GroupPeople>();
            foreach (int peopleId in groupPeopleListDTO.ListPeopleId)
            {
               var peopleCheck = await _context.Peoples.SingleOrDefaultAsync(x => x.PeopleId == peopleId);
               if (peopleCheck == null)
               {
                  throw new Exception("people with id " + peopleId + " does not exist");
               }
               listGroupPeople.Add(new GroupPeople
               {
                  GroupId = groupPeopleListDTO.GroupId,
                  PeopleId = peopleId,
               });
            }

            await _context.GroupPeoples.AddRangeAsync(listGroupPeople);
            await _context.SaveChangesAsync();
            _response.IsSuccess = true;
            _response.Message = "Add list people in Group success";
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> UpdateListPeopleInGroup(GroupPeopleListDTOForCreateUpdate groupPeopleListDTO)
      {
         try
         {
            var groupCheck = await _context.Groups.SingleOrDefaultAsync(x => x.GroupId.Equals(groupPeopleListDTO.GroupId));

            if (groupCheck == null)
            {
               throw new Exception("Group does not exist");
            }

            List<GroupPeople> listGroupPeople = new List<GroupPeople>();
            

            await _context.GroupPeoples.AddRangeAsync(listGroupPeople);
            await _context.SaveChangesAsync();
            _response.IsSuccess = true;
            _response.Message = "Add list people in Group success";
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> DeleteListPeopleInGroup(int groupId)
      {
         try
         {
            List<GroupPeople> groupPeopleCheck = await _context.GroupPeoples.Where(x => x.GroupId.Equals(groupId)).ToListAsync();
            if (groupPeopleCheck.Count() == 0)
            {
               _response.IsSuccess = true;
               _response.Message = "group does not exist any people";
            }
            else
            {
               _context.GroupPeoples.RemoveRange(groupPeopleCheck);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Delete people in group success";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<List<Group>> GetListGroupByPeopleId(int peopleId)
      {
         try
         {
            List<Group> groups = await _context.GroupPeoples.Where(x => x.PeopleId==peopleId).Select(x => x.Group).ToListAsync();
            return groups;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }

      public async Task<List<People>> GetListPeopleByGroupId(int groupId)
      {
         try
         {
            List<People> peoples = await _context.GroupPeoples.Where(x => x.GroupId == groupId).Select(x => x.People).ToListAsync();
            return peoples;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }
   }
}
