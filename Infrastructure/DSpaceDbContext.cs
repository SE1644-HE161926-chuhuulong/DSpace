using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DSpaceDbContext : IdentityDbContext<
    User,Role,string,
    IdentityUserClaim<string>,
    UserRole, 
    IdentityUserLogin<string>,
    IdentityRoleClaim<string>,
    IdentityUserToken<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DSpaceDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public DbSet<Community> Communities { get; set; }
    public DbSet<CommunityGroup> CommunitiesGroups { get; set; }
    public DbSet<CollectionGroup> CollectionGroups { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<EntityCollectionType> EntityCollectionTypes { get; set; }
    public DbSet<People> Peoples { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupPeople> GroupPeoples { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<MetadataValue> MetadataValues { get; set; }
    public DbSet<MetadataFieldRegistry> MetadataFieldRegistries { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<AuthorItem> AuthorItems { get; set; }
    public DbSet<FileUpload> FileUploads { get; set; }
    public DbSet<Statistic> Statistics { get; set; }
    public DbSet<Subscribe> Subscribes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        IdentityConfig(modelBuilder);
        CommunityConfig(modelBuilder);
        CommunityGroupConfig(modelBuilder);
        CollectionConfig(modelBuilder);
        CollectionGroupConfig(modelBuilder);
        EntityCollectionTypeConfig(modelBuilder);
        PeopleConfig(modelBuilder);
        GroupConfig(modelBuilder);
        GroupPeopleConfig(modelBuilder);
        ItemConfig(modelBuilder);
        MetadataValueConfig(modelBuilder);
        MetadataFieldRegistryConfig(modelBuilder);
        AuthorConfig(modelBuilder);
        FileUploadConfig(modelBuilder);
        AuthorItemConfig(modelBuilder);
        StatisticConfig(modelBuilder);
        SubscribeConfig(modelBuilder);
    }

    private void SubscribeConfig(ModelBuilder modelBuilder)
    {
        var subscribe = modelBuilder.Entity<Subscribe>();
        subscribe.HasKey(e => new { e.UserId, e.CollectionId });
        subscribe.HasOne(ik => ik.User)
            .WithMany(i => i.Subscribes)
            .HasForeignKey(ik => ik.UserId)
            .IsRequired(false);
        subscribe.HasOne(ik => ik.Collection)
            .WithMany(k => k.Subscribes)
            .HasForeignKey(ik => ik.CollectionId)
            .IsRequired(false);
    }

    private void StatisticConfig(ModelBuilder modelBuilder)
    {
        var statistic = modelBuilder.Entity<Statistic>();
        statistic.HasKey(e => e.StatisticId);
        statistic.Property(e => e.Month).IsRequired();
        statistic.Property(e => e.Year).IsRequired();
        statistic.Property(e => e.ViewCount).IsRequired();
        statistic.Property(e => e.ViewOnDay).IsRequired();
        statistic.HasOne(f => f.Item)
            .WithMany(i => i.Statistics)
            .HasForeignKey(c => c.ItemId)
            .IsRequired(false);
    }
    private void IdentityConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<UserRole>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        modelBuilder.Entity<UserRole>(
            userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
    }

    private void FileUploadConfig(ModelBuilder modelBuilder)
    {
        var fileUpload = modelBuilder.Entity<FileUpload>();
        fileUpload.HasKey(e => e.FileId);
        fileUpload.Property(e => e.FileUrl).IsRequired(false);
        fileUpload.Property(e => e.FileKeyId).IsRequired(false);
        fileUpload.Property(e => e.MimeType).IsRequired(false);
        fileUpload.Property(e => e.Kind).IsRequired(false);
        fileUpload.Property(e => e.FileName).IsRequired(false);
        fileUpload.Property(e => e.CreationTime)
            .HasColumnType("datetime2")
            .IsRequired();
        fileUpload.Property(e => e.isActive)
            .IsRequired();
        fileUpload.Property(e => e.DownloadCount)
            .IsRequired();
        fileUpload.HasOne(f => f.Item)
            .WithMany(i => i.File)
            .HasForeignKey(c => c.ItemId)
            .IsRequired(false);
    }

    private void AuthorItemConfig(ModelBuilder modelBuilder)
    {
        var authorItem = modelBuilder.Entity<AuthorItem>();
        authorItem.HasKey(e => new { e.ItemId, e.AuthorId });
        authorItem.HasOne(ai => ai.Item)
            .WithMany(i => i.AuthorItems)
            .HasForeignKey(ai => ai.ItemId)
            .IsRequired(false);

        authorItem.HasOne(ai => ai.Author)
            .WithMany(a => a.AuthorItems)
            .HasForeignKey(ai => ai.AuthorId)
            .IsRequired(false);
    }
    
    private void AuthorConfig(ModelBuilder modelBuilder)
    {
        var author = modelBuilder.Entity<Author>();
        author.HasKey(e => e.AuthorId);
        author.Property(e => e.AuthorId)
            .ValueGeneratedOnAdd();
        author.Property(e => e.FullName)
            .IsRequired(false);
        author.Property(e => e.JobTitle)
            .IsRequired(false);
        author.Property(e => e.DateAccessioned)
            .HasColumnType("datetime2")
            .IsRequired();
        author.Property(e => e.DateAvailable)
            .HasColumnType("datetime2")
            .IsRequired();
        author.Property(e => e.Uri)
            .IsRequired(false);
        author.Property(e => e.Type)
            .IsRequired(false);
        author.HasMany(e => e.AuthorItems)
            .WithOne(ai => ai.Author)
            .HasForeignKey(ai => ai.AuthorId)
            .IsRequired(false);
    }
    private void ItemConfig(ModelBuilder modelBuilder)
    {
        var item = modelBuilder.Entity<Item>();
        item.HasKey(e => e.ItemId);
        item.Property(e => e.ItemId)
            .ValueGeneratedOnAdd();
        item.Property(e => e.LastModified)
            .HasColumnType("datetime2")
            .IsRequired();
        item.Property(e => e.Discoverable)
            .IsRequired();
        item.HasOne(e => e.People)
            .WithMany(m => m.Items)
            .HasForeignKey(e => e.SubmitterId)
            .IsRequired(false);
        item.HasOne(e => e.Collection)
            .WithMany(m => m.Items)
            .HasForeignKey(e => e.CollectionId)
            .IsRequired(false);
        item.HasMany(e => e.MetadataValue)
            .WithOne(m => m.Item)
            .HasForeignKey(m => m.ItemId)
            .IsRequired(false);
        item.HasMany(e => e.AuthorItems)
            .WithOne(ai => ai.Item)
            .HasForeignKey(ai => ai.ItemId)
            .IsRequired(false);
    }
    
    private void MetadataValueConfig(ModelBuilder modelBuilder)
    {
        var metadataValue = modelBuilder.Entity<MetadataValue>();
        metadataValue.HasKey(e => e.MetadataValueId);
        metadataValue.Property(e => e.MetadataValueId)
            .ValueGeneratedOnAdd();
        metadataValue.Property(e => e.TextValue)
            .IsRequired(false);
        metadataValue.Property(e => e.TextLang)
            .IsRequired(false);
        metadataValue.HasOne(e => e.Item)
            .WithMany(i => i.MetadataValue)
            .HasForeignKey(e => e.ItemId)
            .IsRequired(false);
        metadataValue.HasOne(e => e.MetadataFieldRegistry)
            .WithMany(m => m.MetadataValue)
            .HasForeignKey(e => e.MetadataFieldId)
            .IsRequired(false);
    }

    private void MetadataFieldRegistryConfig(ModelBuilder modelBuilder)
    {
        var metadataField = modelBuilder.Entity<MetadataFieldRegistry>();
        metadataField.HasKey(e => e.MetadataFieldId);
        metadataField.Property(e => e.MetadataFieldId)
            .ValueGeneratedOnAdd();
        metadataField.Property(e => e.Element)
            .IsRequired(false);
        metadataField.Property(e => e.Qualifier)
            .IsRequired(false);
        metadataField.Property(e => e.ScopeNote)
            .IsRequired(false);
        metadataField.HasMany(e => e.MetadataValue)
            .WithOne(m => m.MetadataFieldRegistry)
            .HasForeignKey(m => m.MetadataFieldId)
            .IsRequired(false);
    }

    private void EntityCollectionTypeConfig(ModelBuilder modelBuilder)
    {
        var entityType = modelBuilder.Entity<EntityCollectionType>();
        entityType.HasKey(e => e.Id);
        entityType.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        entityType.Property(e => e.EntityType)
            .HasMaxLength(50)
            .IsRequired(false);
    }

    private void CommunityGroupConfig(ModelBuilder modelBuilder)
    {
        var communityGroup = modelBuilder.Entity<CommunityGroup>();
        communityGroup.HasKey(e => e.Id);
        communityGroup.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        communityGroup.Property(e => e.canSubmit)
            .IsRequired();
        communityGroup.Property(e => e.canReview)
            .IsRequired();
        communityGroup.Property(e => e.canEdit)
            .IsRequired();
        communityGroup.HasOne(e => e.Community)
            .WithMany(c => c.CommunityGroups)
            .HasForeignKey(e => e.CommunityId)
            .IsRequired(false);
        communityGroup.HasOne(e => e.Group)
            .WithMany(g => g.CommunityGroups)
            .HasForeignKey(e => e.GroupId)
            .IsRequired(false);
    }
    
    private void CollectionGroupConfig(ModelBuilder modelBuilder)
    {
        var collectionGroup = modelBuilder.Entity<CollectionGroup>();
        collectionGroup.HasKey(e => e.Id);
        collectionGroup.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        collectionGroup.Property(e => e.canSubmit)
            .IsRequired();
        collectionGroup.Property(e => e.canReview)
            .IsRequired();
        collectionGroup.Property(e => e.canEdit)
            .IsRequired();
        collectionGroup.HasOne(e => e.Collection)
            .WithMany(c => c.CollectionGroups)
            .HasForeignKey(e => e.CollectionId)
            .IsRequired(false);
        collectionGroup.HasOne(e => e.Group)
            .WithMany(g => g.CollectionGroups)
            .HasForeignKey(e => e.GroupId)
            .IsRequired(false);
    }

    private void GroupPeopleConfig(ModelBuilder modelBuilder)
    {
        var groupPeople = modelBuilder.Entity<GroupPeople>();
        groupPeople.HasKey(e => new { e.PeopleId, e.GroupId });
        groupPeople.HasOne(e => e.Group)
            .WithMany(e => e.GroupPeoples)
            .HasForeignKey(e => e.GroupId)
            .IsRequired(false);
        groupPeople.HasOne(e => e.People)
            .WithMany(e => e.GroupPeoples)
            .HasForeignKey(e => e.PeopleId)
            .IsRequired(false);
    }
    
    private void GroupConfig(ModelBuilder modelBuilder)
    {
        var group = modelBuilder.Entity<Group>();
        group.HasKey(e => e.GroupId);
        group.Property(e => e.GroupId)
            .ValueGeneratedOnAdd();
        group.Property(e => e.Title)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired(false);
        group.Property(e => e.Description)
            .HasColumnType("text")
            .IsRequired(false);
        group.Property(e => e.isActive)
            .IsRequired();
        group.HasMany(e => e.GroupPeoples)
            .WithOne(e => e.Group)
            .HasForeignKey(e => e.GroupId)
            .IsRequired(false);
        group.HasMany(g => g.CommunityGroups)
            .WithOne(cg => cg.Group)
            .HasForeignKey(cg => cg.GroupId)
            .IsRequired(false);
    }

    private void PeopleConfig(ModelBuilder modelBuilder)
    {
        var people = modelBuilder.Entity<People>();
        people.HasKey(e => e.PeopleId);
        people.Property(e => e.PeopleId)
            .ValueGeneratedOnAdd();
        people.Property(e => e.Address)
            .IsRequired(false)
            .HasMaxLength(100);
        people.Property(e => e.PhoneNumber)
            .IsRequired(false)
            .HasMaxLength(100);
        people.Property(e => e.CreatedDate)
            .HasColumnType("datetime2")
            .IsRequired();
        people.Property(e => e.LastModifiedBy)
            .IsRequired(false);
        people.Property(e => e.LastModifiedDate)
            .HasColumnType("datetime2")
            .IsRequired();
        people.Property(e => e.UserId)
            .HasColumnType("nvarchar")
            .HasMaxLength(450);
        people.HasOne(e => e.User)
            .WithOne(e => e.People)
            .HasForeignKey<People>(e => e.UserId)
            .IsRequired(false);
        people.HasOne(e => e.PeopleParent)
            .WithMany(e => e.ListPeopleCreated)
            .HasForeignKey(e => e.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
        people.HasMany(e => e.GroupPeoples)
            .WithOne(e => e.People)
            .HasForeignKey(e => e.PeopleId)
            .IsRequired(false);
    }
    
    private void CollectionConfig(ModelBuilder modelBuilder)
    {
        var collection = modelBuilder.Entity<Collection>();
        collection.HasKey(e => e.CollectionId);
        collection.Property(e => e.CollectionId)
            .ValueGeneratedOnAdd();
        collection.Property(e => e.LogoUrl)
            .IsRequired(false)
            .HasMaxLength(200);
        collection.Property(e => e.CollectionName)
            .IsRequired(false)
            .HasMaxLength(100);
        collection.Property(e => e.ShortDescription)
            .HasMaxLength(500)
            .IsRequired(false);
        collection.Property(e => e.CreateTime)
            .HasColumnType("datetime2")
            .IsRequired();
        collection.Property(e => e.UpdateTime)
            .HasColumnType("datetime2")
            .IsRequired();
        collection.Property(e => e.CreateBy)
            .IsRequired(false);
        collection.Property(e => e.UpdateBy)
            .IsRequired(false);
        collection.Property(e => e.isActive)
            .IsRequired();
        collection.Property(e => e.License)
            .IsRequired(false);
        collection.Property(e => e.FolderId)
            .IsRequired(false);
        collection.HasOne(e => e.People)
            .WithMany(e => e.Collection)
            .HasForeignKey(e => e.CreateBy)
            .IsRequired(false);
        collection.HasOne(e => e.EntityType)
            .WithMany(e => e.Collection)
            .HasForeignKey(e => e.EntityTypeId)
            .IsRequired(false);
        collection.HasOne(e => e.Community)
            .WithMany(c => c.Collections)
            .HasForeignKey(e => e.CommunityId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }

    private void CommunityConfig(ModelBuilder modelBuilder)
    {
        var community = modelBuilder.Entity<Community>();
        community.HasKey(e => e.CommunityId);
        community.Property(e => e.CommunityId)
            .ValueGeneratedOnAdd();
        community.Property(e => e.LogoUrl)
            .IsRequired(false)
            .HasMaxLength(200);
        community.Property(e => e.CommunityName)
            .IsRequired(false)
            .HasMaxLength(100);
        community.Property(e => e.ShortDescription)
            .HasMaxLength(500)
            .IsRequired(false);
        community.Property(e => e.CreateTime)
            .HasColumnType("datetime2")
            .IsRequired();
        community.Property(e => e.UpdateTime)
            .HasColumnType("datetime2")
            .IsRequired();
        community.Property(e => e.CreateBy)
            .IsRequired(false);
        community.Property(e => e.UpdateBy)
            .IsRequired(false);
        community.Property(e => e.isActive)
            .IsRequired();
        community.HasOne(e => e.People)
            .WithMany(e => e.Community)
            .HasForeignKey(e => e.CreateBy)
            .IsRequired(false);
        community.HasMany(c => c.CommunityGroups)
            .WithOne(cg => cg.Community)
            .HasForeignKey(cg => cg.CommunityId)
            .IsRequired(false);
        community.HasOne(e => e.ParentCommunity)
            .WithMany(c => c.SubCommunities)
            .HasForeignKey(e => e.ParentCommunityId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}