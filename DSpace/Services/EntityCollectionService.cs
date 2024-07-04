using Application.Responses;

namespace DSpace.Services;

public interface EntityCollectionService
{
    public Task<ResponseDTO> GetEntityCollectionByID(int entityId);

    public Task<ResponseDTO> GetAllEntityCollections();
}