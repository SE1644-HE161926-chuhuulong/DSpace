using DSpace.Services.Implements;

namespace DSpace.Services;

public static class AddScope
{
   public static IServiceCollection AddScopedCollection(this IServiceCollection serviceCollection)
   {
      serviceCollection.AddScoped<PeopleService, PeopleServiceImpl>();
      serviceCollection.AddScoped<GroupService, GroupServiceImpl>();
      serviceCollection.AddScoped<ItemService, ItemServiceImpl>();
      serviceCollection.AddScoped<FileUploadService, FileUploadServiceImpl>();
      serviceCollection.AddScoped<GroupPeopleService, GroupPeopleServiceImpl>();
      serviceCollection.AddScoped<EmailService, EmailServiceImpl>();
      serviceCollection.AddScoped<CommunityService, CommunityServiceImpl>();
      serviceCollection.AddScoped<AuthorService, AuthorServiceImpl>();
      serviceCollection.AddScoped<CollectionService, CollectionServiceImpl>();
      serviceCollection.AddScoped<JwtTokenService, JwtTokenServiceImpl>();
      serviceCollection.AddScoped<UserService, UserServiceImpl>();
      serviceCollection.AddScoped<EntityCollectionService, EntityCollectionServiceImpl>();
      serviceCollection.AddScoped<MetadataFieldRegistryService, MetadataFieldRegistryServiceImpl>();
      serviceCollection.AddScoped<MetadataValueService, MetadataValueServiceImpl>();
      serviceCollection.AddScoped<CommunityGroupService, CommunityGroupServiceImpl>();
      serviceCollection.AddScoped<CollectionGroupService, CollectionGroupServiceImpl>();
      serviceCollection.AddScoped<StatisticService, StatisticServiceImpl>();
      serviceCollection.AddScoped<SubcribeService, SubcribeServiceImpl>();



      return serviceCollection;
   }
}