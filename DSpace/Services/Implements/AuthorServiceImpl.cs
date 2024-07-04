using Application.Authors;
using Domain;
using Application.Responses;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Auth.OAuth2;

namespace DSpace.Services.Implements
{
   public class AuthorServiceImpl : AuthorService
   {
      public IMapper _mapper;
      private readonly DSpaceDbContext _context;

      protected ResponseDTO _response;

      public AuthorServiceImpl(IMapper mapper, DSpaceDbContext context)
      {
         _context = context;
         _response = new ResponseDTO();
         _mapper = mapper;
      }

      public async Task<ResponseDTO> CreateAuthor(AuthorDTOForCreateUpdate authorDTO)
      {
         try
         {
            Author author = new Author();
            author.FullName = authorDTO.FullName;
            author.JobTitle = authorDTO.JobTitle;
            author.DateAccessioned = authorDTO.DateAccessioned;
            author.DateAvailable = authorDTO.DateAvailable;
            
            author.Type = authorDTO.Type;
            await _context.Authors.AddAsync(author);

            await _context.SaveChangesAsync();
            author.Uri = authorDTO.Uri+author.AuthorId;
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();


            _response.IsSuccess = true;
            _response.Message = "Add Author success";
            _response.ObjectResponse = _mapper.Map<AuthorDTOForSelect>(author);
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;

         }
         return _response;
      }

      public async Task<ResponseDTO> DeleteAuthor(int authorId)
      {
         try
         {
            var check = await _context.Authors.Include(x => x.AuthorItems).SingleOrDefaultAsync(x => x.AuthorId == authorId);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Author does not exist";
            }
            else
            {
               if (check.AuthorItems.Count !=0)
               {
                  _response.IsSuccess = false;
                  _response.Message = "author " + check.FullName + "is exist in a item";
               }
               else
               {
                  _context.Authors.Remove(check);
                  await _context.SaveChangesAsync();
                  _response.IsSuccess = true;
                  _response.Message = "Delete Author success";
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

      public async Task<ResponseDTO> GetAuthorByID(int authorId)
      {
         try
         {
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Author with id " + authorId + " does not exist";
            }
            else
            {
               _response.IsSuccess = true;
               _response.Message = " Author exsit";
               _response.ObjectResponse = _mapper.Map<AuthorDTOForSelect>(author);
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetAuthorByName(string name)
      {
         try
         {
            IEnumerable<Author> objList = await _context.Authors.Where(x => (x.FullName).Contains(name)).ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<AuthorDTOForSelect>>(objList);
         }
         catch (Exception ex)
         {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
         }
         return _response;
      }

      public async Task<ResponseDTO> UpdateAuthor(int id, AuthorDTOForCreateUpdate authorDTO)
      {
         try
         {
            var check = await _context.Authors.FindAsync(id);
            if (check == null)
            {
               _response.IsSuccess = true;
               _response.Message = "Author with id " + id + " does not exist";
            }
            else
            {
               check.FullName = authorDTO.FullName;
               check.JobTitle = authorDTO.JobTitle;
               check.DateAccessioned = authorDTO.DateAccessioned;
               check.DateAvailable = authorDTO.DateAvailable;
               check.Uri = authorDTO.Uri;
               check.Type = authorDTO.Type;
               _context.Authors.Update(check);
               await _context.SaveChangesAsync();
               _response.IsSuccess = true;
               _response.Message = "Update author successful";
            }
         }
         catch (Exception ex)
         {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
         }
         return _response;
      }

      public async Task<ResponseDTO> GetAllAuthor()
      {
         try
         {
            IEnumerable<Author> objList = await _context.Authors.ToListAsync();
            _response.IsSuccess = true;
            _response.ObjectResponse = _mapper.Map<List<AuthorDTOForSelect>>(objList);

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

