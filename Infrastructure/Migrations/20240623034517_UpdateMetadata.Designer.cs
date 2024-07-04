﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DSpaceDbContext))]
    [Migration("20240623034517_UpdateMetadata")]
    partial class UpdateMetadata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAccessioned")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAvailable")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Domain.AuthorItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AuthorItems");
                });

            modelBuilder.Entity("Domain.Collection", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollectionId"));

                    b.Property<string>("CollectionName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CommunityId")
                        .HasColumnType("int");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntityTypeId")
                        .HasColumnType("int");

                    b.Property<string>("FolderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("License")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("CollectionId");

                    b.HasIndex("CommunityId");

                    b.HasIndex("CreateBy");

                    b.HasIndex("EntityTypeId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("Domain.Community", b =>
                {
                    b.Property<int>("CommunityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommunityId"));

                    b.Property<string>("CommunityName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ParentCommunityId")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("CommunityId");

                    b.HasIndex("CreateBy");

                    b.HasIndex("ParentCommunityId");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("Domain.CommunityGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<int>("CommunityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PolicyType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("canEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("canReview")
                        .HasColumnType("bit");

                    b.Property<bool>("canSubmit")
                        .HasColumnType("bit");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.HasIndex("GroupId");

                    b.ToTable("CommunitiesGroups");
                });

            modelBuilder.Entity("Domain.EntityCollectionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EntityType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("EntityCollectionTypes");
                });

            modelBuilder.Entity("Domain.FileUpload", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileKeyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Kind")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MimeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("FileId");

                    b.HasIndex("ItemId");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("Domain.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Domain.GroupPeople", b =>
                {
                    b.Property<int>("PeopleId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("PeopleId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupPeoples");
                });

            modelBuilder.Entity("Domain.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<int>("CollectionId")
                        .HasColumnType("int");

                    b.Property<bool>("Discoverable")
                        .HasColumnType("bit");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("MetadataId")
                        .HasColumnType("int");

                    b.Property<int>("SubmitterId")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("CollectionId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("MetadataId")
                        .IsUnique();

                    b.HasIndex("SubmitterId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Domain.ItemKeyword", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("KeywordId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("ItemKeywords");
                });

            modelBuilder.Entity("Domain.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("KeywordName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("Domain.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LanguageCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Domain.Metadata", b =>
                {
                    b.Property<int>("MetadataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetadataId"));

                    b.Property<string>("Contributor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coverage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Format")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rights")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetadataId");

                    b.ToTable("Metadatas");
                });

            modelBuilder.Entity("Domain.MetadataFieldRegistry", b =>
                {
                    b.Property<int>("MetadataFieldId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetadataFieldId"));

                    b.Property<string>("Element")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScopeNote")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetadataFieldId");

                    b.ToTable("MetadataFieldRegistries");
                });

            modelBuilder.Entity("Domain.MetadataValue", b =>
                {
                    b.Property<int>("MetadataValueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetadataValueId"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("MetadataFieldId")
                        .HasColumnType("int");

                    b.Property<string>("TextLang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetadataValueId");

                    b.HasIndex("ItemId");

                    b.HasIndex("MetadataFieldId");

                    b.ToTable("MetadataValues");
                });

            modelBuilder.Entity("Domain.People", b =>
                {
                    b.Property<int>("PeopleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PeopleId"));

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar");

                    b.HasKey("PeopleId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Peoples");
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit")
                        .HasColumnName("activated");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Domain.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.AuthorItem", b =>
                {
                    b.HasOne("Domain.Author", "Author")
                        .WithMany("AuthorItems")
                        .HasForeignKey("AuthorId");

                    b.HasOne("Domain.Item", "Item")
                        .WithMany("AuthorItems")
                        .HasForeignKey("ItemId");

                    b.Navigation("Author");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Domain.Collection", b =>
                {
                    b.HasOne("Domain.Community", "Community")
                        .WithMany("Collections")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.People", "People")
                        .WithMany("Collection")
                        .HasForeignKey("CreateBy");

                    b.HasOne("Domain.EntityCollectionType", "EntityType")
                        .WithMany("Collection")
                        .HasForeignKey("EntityTypeId");

                    b.Navigation("Community");

                    b.Navigation("EntityType");

                    b.Navigation("People");
                });

            modelBuilder.Entity("Domain.Community", b =>
                {
                    b.HasOne("Domain.People", "People")
                        .WithMany("Community")
                        .HasForeignKey("CreateBy");

                    b.HasOne("Domain.Community", "ParentCommunity")
                        .WithMany("SubCommunities")
                        .HasForeignKey("ParentCommunityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCommunity");

                    b.Navigation("People");
                });

            modelBuilder.Entity("Domain.CommunityGroup", b =>
                {
                    b.HasOne("Domain.Community", "Community")
                        .WithMany("CommunityGroups")
                        .HasForeignKey("CommunityId");

                    b.HasOne("Domain.Group", "Group")
                        .WithMany("CommunityGroups")
                        .HasForeignKey("GroupId");

                    b.Navigation("Community");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Domain.FileUpload", b =>
                {
                    b.HasOne("Domain.Item", "Item")
                        .WithMany("File")
                        .HasForeignKey("ItemId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Domain.GroupPeople", b =>
                {
                    b.HasOne("Domain.Group", "Group")
                        .WithMany("GroupPeoples")
                        .HasForeignKey("GroupId");

                    b.HasOne("Domain.People", "People")
                        .WithMany("GroupPeoples")
                        .HasForeignKey("PeopleId");

                    b.Navigation("Group");

                    b.Navigation("People");
                });

            modelBuilder.Entity("Domain.Item", b =>
                {
                    b.HasOne("Domain.Collection", "Collection")
                        .WithMany("Items")
                        .HasForeignKey("CollectionId");

                    b.HasOne("Domain.Language", "Language")
                        .WithMany("Item")
                        .HasForeignKey("LanguageId");

                    b.HasOne("Domain.Metadata", "Metadata")
                        .WithOne("Item")
                        .HasForeignKey("Domain.Item", "MetadataId");

                    b.HasOne("Domain.People", "People")
                        .WithMany("Items")
                        .HasForeignKey("SubmitterId");

                    b.Navigation("Collection");

                    b.Navigation("Language");

                    b.Navigation("Metadata");

                    b.Navigation("People");
                });

            modelBuilder.Entity("Domain.ItemKeyword", b =>
                {
                    b.HasOne("Domain.Item", "Item")
                        .WithMany("ItemKeywords")
                        .HasForeignKey("ItemId");

                    b.HasOne("Domain.Keyword", "Keyword")
                        .WithMany("ItemKeywords")
                        .HasForeignKey("KeywordId");

                    b.Navigation("Item");

                    b.Navigation("Keyword");
                });

            modelBuilder.Entity("Domain.MetadataValue", b =>
                {
                    b.HasOne("Domain.Item", "Item")
                        .WithMany("MetadataValue")
                        .HasForeignKey("ItemId");

                    b.HasOne("Domain.MetadataFieldRegistry", "MetadataFieldRegistry")
                        .WithMany("MetadataValue")
                        .HasForeignKey("MetadataFieldId");

                    b.Navigation("Item");

                    b.Navigation("MetadataFieldRegistry");
                });

            modelBuilder.Entity("Domain.People", b =>
                {
                    b.HasOne("Domain.People", "PeopleParent")
                        .WithMany("ListPeopleCreated")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.User", "User")
                        .WithOne("People")
                        .HasForeignKey("Domain.People", "UserId");

                    b.Navigation("PeopleParent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.UserRole", b =>
                {
                    b.HasOne("Domain.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Author", b =>
                {
                    b.Navigation("AuthorItems");
                });

            modelBuilder.Entity("Domain.Collection", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Community", b =>
                {
                    b.Navigation("Collections");

                    b.Navigation("CommunityGroups");

                    b.Navigation("SubCommunities");
                });

            modelBuilder.Entity("Domain.EntityCollectionType", b =>
                {
                    b.Navigation("Collection");
                });

            modelBuilder.Entity("Domain.Group", b =>
                {
                    b.Navigation("CommunityGroups");

                    b.Navigation("GroupPeoples");
                });

            modelBuilder.Entity("Domain.Item", b =>
                {
                    b.Navigation("AuthorItems");

                    b.Navigation("File");

                    b.Navigation("ItemKeywords");

                    b.Navigation("MetadataValue");
                });

            modelBuilder.Entity("Domain.Keyword", b =>
                {
                    b.Navigation("ItemKeywords");
                });

            modelBuilder.Entity("Domain.Language", b =>
                {
                    b.Navigation("Item");
                });

            modelBuilder.Entity("Domain.Metadata", b =>
                {
                    b.Navigation("Item")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.MetadataFieldRegistry", b =>
                {
                    b.Navigation("MetadataValue");
                });

            modelBuilder.Entity("Domain.People", b =>
                {
                    b.Navigation("Collection");

                    b.Navigation("Community");

                    b.Navigation("GroupPeoples");

                    b.Navigation("Items");

                    b.Navigation("ListPeopleCreated");
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("People");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
