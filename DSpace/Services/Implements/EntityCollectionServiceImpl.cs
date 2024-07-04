using Application.Responses;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DSpace.Services.Implements;

public class EntityCollectionServiceImpl : EntityCollectionService
{
    private readonly DSpaceDbContext _context;

    protected ResponseDTO _response;
    public EntityCollectionServiceImpl(DSpaceDbContext context)
    {
        _context = context;
        _response = new ResponseDTO();
    }

    public async Task<ResponseDTO> GetEntityCollectionByID(int entityId)
    {
        try
        {
            var entity = await _context.EntityCollectionTypes.FindAsync(entityId);
            if (entity == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Entity with id " + entityId + " does not exist";
            }
            else
            {
                _response.IsSuccess = true;
                _response.Message = "Found entity";
                _response.ObjectResponse = entity;
            }
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    public async Task<ResponseDTO> GetAllEntityCollections()
    {
        try
        {
            var objList = await _context.EntityCollectionTypes.ToListAsync();
            _response.IsSuccess = true;
            _response.Message = "Found list of entity";
            _response.ObjectResponse = objList;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.Message = e.Message;
        }
        return _response;
    }
}