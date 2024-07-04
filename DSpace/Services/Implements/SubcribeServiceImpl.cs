using Application.Collections;
using Application.CollectionSubcribes;
using Application.Responses;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Services.Implements
{
   public class SubcribeServiceImpl : SubcribeService
   {
      private readonly DSpaceDbContext _context;
      private IMapper _mapper;
      private UserService _userService;
      private ResponseDTO _response;

      public SubcribeServiceImpl(DSpaceDbContext context, IMapper mapper, UserService userService)
      {
         _context = context;
         _mapper = mapper;
         _userService = userService;
         _response = new ResponseDTO();
      }

      public async Task<ResponseDTO> UnSubcribeCollection(int collectionId, string userEmail)
      {
         try
         {
            var userCheck = await _userService.getUserByEmail(userEmail);
            var subcribeCheck = await _context.Subscribes.SingleOrDefaultAsync(x => x.CollectionId.Equals(collectionId) && x.UserId == userCheck.Id);
            if (subcribeCheck == null)
            {
               _response.IsSuccess = true;
               _response.Message = "you had unsubcribe collection";
            }
            else
            {
               _context.Subscribes.Remove(subcribeCheck);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "you have unsubcribed collection";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> SubcribeCollection(int collectionId, string userEmail)
      {
         try
         {
            var collectionCheck = await _context.Collections.SingleOrDefaultAsync(x => x.CollectionId.Equals(collectionId));
            var userCheck = await _userService.getUserByEmail(userEmail);
            var subcribeCheck = await CheckSubcribe(collectionId, userEmail);
            if (collectionCheck == null || userCheck == null || subcribeCheck)
            {
               _response.IsSuccess = false;
               if (collectionCheck == null)
               {
                  _response.Message = "collection " + collectionId + " does not exist";
               }
               if (userCheck == null)
               {
                  _response.Message = "email " + userEmail + " does not log in ";
               }
               if (subcribeCheck)
               {
                  _response.IsSuccess = true;
                  _response.Message = "you subcribed this collection";
               }
            }
            else
            {
               Subscribe subscribeNew = new Subscribe();
               subscribeNew.CollectionId = collectionId;
               subscribeNew.UserId = userCheck.Id;
               await _context.Subscribes.AddAsync(subscribeNew);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "you subcribed collection" + collectionCheck.CollectionName;
            }

         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> ViewListSubcribe()
      {
         try
         {
            var subcribes = await _context.Collections.Include(x => x.Subscribes).Select(y => new SubcribeDTO
            {
               CollectionId = y.CollectionId,
               CollectionName = y.CollectionName,
               SubcribeAmount = y.Subscribes.Count()
            }).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = subcribes;
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<List<int>> GetAllCollectionIdByUserEmail(string userEmail)
      {
         try
         {
            var userCheck = await _userService.getUserByEmail(userEmail);
            var subcribes = await _context.Subscribes.Where(x => x.UserId == userCheck.Id).Select(x => x.CollectionId).ToListAsync();
            return subcribes;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }

      }

      public async Task<bool> CheckSubcribe(int collectionId, string email)
      {
         try
         {
            var userCheck = await _userService.getUserByEmail(email);
            var subcribes = await _context.Subscribes.SingleOrDefaultAsync(x => x.UserId == userCheck.Id && x.CollectionId == collectionId);
            if (subcribes == null)
            {
               return false;
            }
            return true;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }

      }

      public async Task<ResponseDTO> ViewListSubcribeForUser(string email)
      {
         try
         {
            var userCheck = await _userService.getUserByEmail(email);
            var collections = await _context.Subscribes.Where(x => x.UserId == userCheck.Id && x.Collection.isActive).Select(x => x.Collection).ToListAsync();
            var objMap = _mapper.Map<List<CollectionDTOForSelectOfUser>>(collections);

            _response.IsSuccess= true;
            _response.ObjectResponse= objMap;


         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<IEnumerable<string>> GetListUserEmailSubcribeCollection(int collectionId)
      {
         try
         {
            IEnumerable<string> listEmailSubcribed = await _context.Subscribes.Include(x => x.User).Where(x => x.CollectionId == collectionId).Select(x => x.User.Email).ToListAsync();
            return listEmailSubcribed;

         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }

      }
   }
}
