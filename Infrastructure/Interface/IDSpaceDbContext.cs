using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interface;

public interface IDSpaceDbContext
{
    public DbSet<Community> Communities { get; set; }
    public DbSet<CommunityGroup> CommunitieGroups { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<EntityCollectionType> EntityCollectionTypes { get; set; }
    public DbSet<People> Peoples { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupPeople> GroupPeoples { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<FileUpload> FileUploads { get; set; }
}