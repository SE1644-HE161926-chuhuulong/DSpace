using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;

namespace DSpace.Configurations;

public static class IdentityStartup
{
    public static IApplicationBuilder UseApplicationIdentity(this IApplicationBuilder builder,
        IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            SeedRoles(roleManager).Wait();
            SeedUsersAndOtherEntity(
                    userManager, serviceProvider.GetRequiredService<DSpaceDbContext>(),
                    serviceProvider.GetRequiredService<IPasswordHasher<User>>()
                )
                .Wait();
            SeedUserRoles(userManager).Wait();
            SeedMetadataField(serviceProvider.GetRequiredService<DSpaceDbContext>()).Wait();
        }

        return builder;
    }

    private static async Task SeedUserRoles(UserManager<User> userManager)
    {
        foreach (var (id, roles) in UserRoles()) {
            var user = await userManager.FindByIdAsync(id);
            await userManager.AddToRolesAsync(user, roles);
        }
    }

    private static async Task SeedMetadataField(DSpaceDbContext dbContext)
    {
        foreach (var metadataField in MetadataFieldRegistries())
        {
            if (await dbContext.MetadataFieldRegistries.FirstOrDefaultAsync(x => 
                    x.Element == metadataField.Element 
                    && x.Qualifier == metadataField.Qualifier) != null)
            {
                continue;
            }

            await dbContext.MetadataFieldRegistries.AddAsync(metadataField);
        }
        await dbContext.SaveChangesAsync();

    }
    
    private static async Task SeedUsersAndOtherEntity(UserManager<User> userManager, DSpaceDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
        foreach (var user in Users()) {
            user.PasswordHash = passwordHasher.HashPassword(null!, "123456");
            var dbUser = await userManager.FindByIdAsync(user.Id);
            if (dbUser == null) {
                await userManager.CreateAsync(user);
            } else {
                await userManager.UpdateAsync(dbUser);
            }
            if (await dbContext.Peoples.FirstOrDefaultAsync(x => x.UserId == user.Id) != null ) {
                continue;
            }
            await dbContext.Peoples.AddAsync(
                new People()
                {
                    Address = "",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    UserId = user.Id
                });
            if(await dbContext.EntityCollectionTypes.FirstOrDefaultAsync(x =>
                x.EntityType.Equals("Publication") || x.EntityType.Equals("Publication")) == null && await dbContext.Communities.FirstOrDefaultAsync(x =>
                x.CommunityName.Equals("Dissertations") || x.CommunityName.Equals("E-textbooks")) == null)
            {
                await dbContext.EntityCollectionTypes.AddAsync(
                    new EntityCollectionType()
                    {
                        EntityType = "Publication"
                    });
                await dbContext.EntityCollectionTypes.AddAsync(
                    new EntityCollectionType()
                    {
                        EntityType = "Author"
                    });
                await dbContext.Communities.AddAsync(
                    new Community()
                    {
                        LogoUrl = "",
                        CommunityName = "Dissertations",
                        ShortDescription = "",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        isActive = true,
                    });
                await dbContext.Communities.AddAsync(
                    new Community()
                    {
                        LogoUrl = "",
                        CommunityName = "E-textbooks",
                        ShortDescription = "",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        isActive = true,
                    });
                await dbContext.SaveChangesAsync();
            }
            
        }
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedRoles(RoleManager<Role> roleManager) {
        foreach (var role in Roles()) {
            var dbRole = await roleManager.FindByNameAsync(role.Name);
            if (dbRole == null) {
                await roleManager.CreateAsync(role);
            } else {
                await roleManager.UpdateAsync(dbRole);
            }
        }
    }
    
    private static IDictionary<string, string[]> UserRoles() {
        return new Dictionary<string, string[]> {
            { "186ec6d2-3b3c-4dfd-a05a-ece673298696", new[] { "ADMIN" } },
            { "475a5fb5-6344-4853-a545-f6257766a645", new[] { "ADMIN" } },
            { "870bf2dc-feb6-4863-be0f-982adf51cdbb", new[] { "ADMIN" } },
            { "1e644e26-b4c3-40c7-91a0-8e8550972d88", new[] { "ADMIN" } },
            { "ee65f727-43e4-409d-bdb6-2056cf26a9b2", new[] { "ADMIN" } },
        };
    }
    
        private static IEnumerable<User> Users() {
        return new List<User> {
            new User {
                Id = "186ec6d2-3b3c-4dfd-a05a-ece673298696",
                UserName = "admin1",
                FirstName = "Bui Hoai Nam",
                LastName = "(K16_HL)",
                PasswordHash = "$2a$11$lds3PNUxyTkGtlpwANmOFuq7QC7vbKRgkMPJjvvElzZbysndCye4y",
                Email = "nambhhe163952@fpt.edu.vn",
                isActive = true,
            },
            new User {
                Id = "475a5fb5-6344-4853-a545-f6257766a645",
                UserName = "admin2",
                FirstName = "Nguyen Anh Tuan",
                LastName = "(K16_HL)",
                PasswordHash = "$2a$11$lds3PNUxyTkGtlpwANmOFuq7QC7vbKRgkMPJjvvElzZbysndCye4y",
                Email = "tuannahe163052@fpt.edu.vn",
                isActive = true,
            },
            new User {
                Id = "870bf2dc-feb6-4863-be0f-982adf51cdbb",
                UserName = "admin3",
                FirstName = "Bui Tran Trong Thang",
                LastName = "(K16_HL)",
                PasswordHash = "$2a$11$lds3PNUxyTkGtlpwANmOFuq7QC7vbKRgkMPJjvvElzZbysndCye4y",
                Email = "buitrantrongthang.2002@gmail.com",
                isActive = true,
            },
            new User {
                Id = "1e644e26-b4c3-40c7-91a0-8e8550972d88",
                UserName = "admin4",
                FirstName = "Chu Huu Long",
                LastName = "(K16_HL)",
                PasswordHash = "$2a$11$lds3PNUxyTkGtlpwANmOFuq7QC7vbKRgkMPJjvvElzZbysndCye4y",
                Email = "longchhe161926@fpt.edu.vn",
                isActive = true,
            },
            new User {
                Id = "ee65f727-43e4-409d-bdb6-2056cf26a9b2",
                UserName = "admin5",
                FirstName = "Tran Huu Minh",
                LastName = "(K16_HL)",
                PasswordHash = "$2a$11$lds3PNUxyTkGtlpwANmOFuq7QC7vbKRgkMPJjvvElzZbysndCye4y",
                Email = "minhth922@gmail.com",
                isActive = true,
            },
        };
    }
    
    private static IEnumerable<Role> Roles() {
        return new List<Role> {
            new Role { Id = "admin", Name = RolesConstants.ADMIN },
            new Role { Id = "staff", Name = RolesConstants.STAFF },
            new Role { Id = "lecturer", Name = RolesConstants.LECTURER },
            new Role { Id = "student", Name = RolesConstants.STUDENT },
        };
    }
    
    private static IEnumerable<MetadataFieldRegistry> MetadataFieldRegistries()
    {
        return new List<MetadataFieldRegistry>
        {
            new MetadataFieldRegistry
            {
                Element = "contributor",
                Qualifier = "author",
                ScopeNote =
                    "Use for author."
            },
            new MetadataFieldRegistry
            {
                Element = "contributor",
                Qualifier = "advisor",
                ScopeNote =
                    "Use primarily for thesis advisor."
            },
            new MetadataFieldRegistry
            {
                Element = "contributor",
                Qualifier = "editor",
                ScopeNote =
                    ""
            },
            new MetadataFieldRegistry
            {
                Element = "contributor",
                Qualifier = "illustrator",
                ScopeNote =
                    ""
            },
            new MetadataFieldRegistry
            {
                Element = "contributor",
                Qualifier = "other",
                ScopeNote =
                    "As of 07/07 this field will not be used for reference to author. It can be used for scanning/hosting agencies who scan the material."
            },
            new MetadataFieldRegistry
            {
                Element = "coverage",
                Qualifier = "spatial",
                ScopeNote =
                    "Spatial characteristics of content."
            },
            new MetadataFieldRegistry
            {
                Element = "coverage",
                Qualifier = "temporal",
                ScopeNote =
                    "Temporal characteristics of content."
            },
            new MetadataFieldRegistry
            {
                Element = "date",
                Qualifier = "accessioned",
                ScopeNote =
                    "Date DSpace takes possession of item"
            },
            new MetadataFieldRegistry
            {
                Element = "date",
                Qualifier = "available",
                ScopeNote =
                    "Date or date range item became available to the public."
            },
            new MetadataFieldRegistry
            {
                Element = "date",
                Qualifier = "copyright",
                ScopeNote =
                    "Date of copyright."
            },
            new MetadataFieldRegistry
            {
                Element = "date",
                Qualifier = "created",
                ScopeNote =
                    "Date of creation or manufacture of intellectual content if different from date. issued."
            },
            new MetadataFieldRegistry
            {
                Element = "date",
                Qualifier = "issued",
                ScopeNote =
                    "Date of publication or distribution."
            },
            new MetadataFieldRegistry
            {
                Element = "date",
                Qualifier = "submitted",
                ScopeNote =
                    "Recommend for theses/dissertations."
            },
            new MetadataFieldRegistry
            {
                Element = "description",
                Qualifier = "",
                ScopeNote =
                    "Catch-all for any description not defined by qualifiers."
            },
            new MetadataFieldRegistry
            {
                Element = "description",
                Qualifier = "abstract",
                ScopeNote =
                    "Abstract or summary."
            },
            new MetadataFieldRegistry
            {
                Element = "description",
                Qualifier = "provenance",
                ScopeNote =
                    "The history of custody of the item since its creation, including any changes successive custodians made to it."
            },
            new MetadataFieldRegistry
            {
                Element = "description",
                Qualifier = "sponsorship",
                ScopeNote =
                    "Information about sponsoring agencies, individuals, or contractual arrangements for the item."
            },
            new MetadataFieldRegistry
            {
                Element = "description",
                Qualifier = "statementofresponsibility",
                ScopeNote =
                    "To preserve statement of responsibility from MARC records."
            },
            new MetadataFieldRegistry
            {
                Element = "description",
                Qualifier = "tableofcontents",
                ScopeNote =
                    "A table of contents for this item."
            },
            new MetadataFieldRegistry
            {
                Element = "description",
                Qualifier = "uri",
                ScopeNote =
                    "Uniform Resource Identifier pointing to description of this item."
            },
            new MetadataFieldRegistry
            {
                Element = "format",
                Qualifier = "",
                ScopeNote =
                    "Catch-all for any format information not defined by qualifiers."
            },
            new MetadataFieldRegistry
            {
                Element = "format",
                Qualifier = "extent",
                ScopeNote =
                    "Size or duration."
            },
            new MetadataFieldRegistry
            {
                Element = "format",
                Qualifier = "medium",
                ScopeNote =
                    "Physical medium."
            },
            new MetadataFieldRegistry
            {
                Element = "format",
                Qualifier = "creation",
                ScopeNote =
                    "Used for the NDNP to record provenance information about the scanning and microfilming of the newspapers. Based on Western States Digital Standards Group Metadata Best Practices, v. 2.0, 2005."
            },
            new MetadataFieldRegistry
            {
                Element = "format",
                Qualifier = "mimetype",
                ScopeNote =
                    "Registered MIME type identifiers."
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "citation",
                ScopeNote =
                    "Bibliographic citation for works that have been published as a part of a larger work, e.g. journal articles, book chapters."
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "govdoc",
                ScopeNote =
                    "Government document number"
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "isbn",
                ScopeNote =
                    "International Standard Book Number"
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "issn",
                ScopeNote =
                    "International Standard Serial Number"
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "sici",
                ScopeNote =
                    "Serial Item and Contribution Identifier"
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "ismn",
                ScopeNote =
                    "International Standard Music Number"
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "other",
                ScopeNote =
                    "A known identifier type common to a local collection."
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "lccn",
                ScopeNote =
                    "Library of Congress Call Number. We added this in January 2009 to accommodate the National Digital Newspaper Project, whose information will be added to ScholarsSpace"
            },
            new MetadataFieldRegistry
            {
                Element = "identifier",
                Qualifier = "uri",
                ScopeNote =
                    "Uniform Resource Identifier"
            },
            new MetadataFieldRegistry
            {
                Element = "language",
                Qualifier = "iso",
                ScopeNote =
                    "Current ISO standard for language of intellectual content, including country codes (e.g. \"en_US\")."
            },
            new MetadataFieldRegistry
            {
                Element = "publisher",
                Qualifier = "",
                ScopeNote =
                    "Entity responsible for publication, distribution, or imprint. Repeat this element for co-publisher (added June, 2008)."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "isformatof",
                ScopeNote =
                    "References additional physical form."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "ispartof",
                ScopeNote =
                    "References physically or logically containing item."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "ispartofseries",
                ScopeNote =
                    "Series name and number within that series, if available."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "haspart",
                ScopeNote =
                    "References physically or logically contained item."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "isversionof",
                ScopeNote =
                    "References earlier version."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "hasversion",
                ScopeNote =
                    "References later version."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "isbasedon",
                ScopeNote =
                    "References source."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "isreferencedby",
                ScopeNote =
                    "Pointed to by referenced resource."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "requires",
                ScopeNote =
                    "Reference resource is required to support function, delivery, or coherence of item."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "replaces",
                ScopeNote =
                    "References preceding item."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "isreplacedby",
                ScopeNote =
                    "References succeeding item."
            },
            new MetadataFieldRegistry
            {
                Element = "relation",
                Qualifier = "uri",
                ScopeNote =
                    "References Uniform Resource Identifier for related item."
            },
            new MetadataFieldRegistry
            {
                Element = "rights",
                Qualifier = "uri",
                ScopeNote =
                    "References terms governing use and reproduction."
            },
            new MetadataFieldRegistry
            {
                Element = "source",
                Qualifier = "uri",
                ScopeNote =
                    "Do not use; only for harvested metadata."
            },
            new MetadataFieldRegistry
            {
                Element = "subject",
                Qualifier = "classification",
                ScopeNote =
                    "Catch-all for value from local classification system; global classification systems will receive specific qualifier."
            },
            new MetadataFieldRegistry
            {
                Element = "subject",
                Qualifier = "ddc",
                ScopeNote =
                    "Dewey Decimal Classification Number"
            },
            new MetadataFieldRegistry
            {
                Element = "subject",
                Qualifier = "lcc",
                ScopeNote =
                    "Library of Congress Classification Number"
            },
            new MetadataFieldRegistry
            {
                Element = "subject",
                Qualifier = "lcsh",
                ScopeNote =
                    "Library of Congress Subject Heading"
            },
            new MetadataFieldRegistry
            {
                Element = "subject",
                Qualifier = "mesh",
                ScopeNote =
                    "Medical Subject Headings"
            },
            new MetadataFieldRegistry
            {
                Element = "subject",
                Qualifier = "other",
                ScopeNote =
                    "Local controlled vocabulary."
            },
            new MetadataFieldRegistry
            {
                Element = "title",
                Qualifier = "",
                ScopeNote =
                    ""
            },
            new MetadataFieldRegistry
            {
                Element = "title",
                Qualifier = "alternative",
                ScopeNote =
                    "Varying (or substitute) form of title proper appearing in item, e.g. abbreviation or translation."
            },
            new MetadataFieldRegistry
            {
                Element = "type",
                Qualifier = "",
                ScopeNote =
                    ""
            },
        };
    }
    
    

    
}