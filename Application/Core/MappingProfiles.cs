using Application.Authors;
using Application.Communities;
using Application.GroupPeoples;
using Application.Groups;
using Application.Metadatas;
using Application.Peoples;
using Application.Collections;
using Application.FileUploads;
using Application.Items;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Users;
using Application.CommunityGroups;
using Application.CollectionGroups;
using Application.Statistics;


namespace Application.Core;

public class MappingProfiles : Profile
{
   public MappingProfiles()
   {
      CreateMap<People, PeopleDTOForSelect>()
         .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.User.Email))
         .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.User.UserRoles.Select(x => x.Role)))
         .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
         .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.User.LastName))
         .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => x.User.isActive))
         .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(x => x.PeopleParent.User.Email))
         .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(x => x.PeopleParent.User.Email))
         .ReverseMap();
      //CreateMap<User,UserDTOForSelect>().ForMember(dest => dest.RoleName,opt=>opt.MapFrom(x=>x.UserRoles))
      CreateMap<ItemDTOForModify, Item>().ReverseMap();
      CreateMap<ModifyFileUploadDto, FileUpload>().ReverseMap();
      CreateMap<Item, ItemDto>().ReverseMap();
      CreateMap<AuthorDTOForCreateUpdate, Author>().ReverseMap();
      CreateMap<People, PeopleDTOForCreateUpdate>().ReverseMap();
      CreateMap<Group, GroupDTOForCreateUpdate>().ReverseMap();
      CreateMap<Group, GroupDTOForDetail>()
         .ForMember(dest=>dest.listPeopleInGroup,opt=>opt.MapFrom(x=>x.GroupPeoples.Select(y=>y.People)))
         .ReverseMap();
      CreateMap<GroupPeople, GroupPeopleDTO>().ReverseMap();
      CreateMap<Community, CommunityDTOForSelect>()
         .ForMember(dest => dest.CreateBy, opt => opt.MapFrom(x => x.People.User.Email))
         .ForMember(dest => dest.UpdateBy, opt => opt.MapFrom(x => x.People.User.Email))
         .ReverseMap();
      CreateMap<Community, CommunityDTOForSelectOfUser>().ReverseMap();
      CreateMap<Community, CommunityDTOForDetail>().ReverseMap();
      CreateMap<Group, GroupDTOForSelect>().ReverseMap();
      CreateMap<Author, AuthorDTOForSelect>().ReverseMap();
      CreateMap<Collection, CollectionDTOForDetail>()
         .ForMember(dest => dest.CommunityDTOForSelect, opt => opt.MapFrom(x => x.Community))
         .ForMember(dest => dest.CreateBy, opt => opt.MapFrom(x => x.People.User.Email))
         .ForMember(dest => dest.UpdateBy, opt => opt.MapFrom(x => x.People.User.Email))
         .ForMember(dest=>dest.EntityTypeName,opt=>opt.MapFrom(x=>x.EntityType.EntityType))
         .ReverseMap();
      CreateMap<Collection, CollectionDTOForSelectOfUser>()
         .ForMember(dest => dest.CommunityDTOForSelectOfUser, opt => opt.MapFrom(x => x.Community))
         .ReverseMap();
      CreateMap<Collection, CollectionDTOForSelectList>()
        .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(x => x.Items.Count()))
        .ForMember(dest=>dest.CommunityName,opt=>opt.MapFrom(x=>x.Community.CommunityName))
        .ReverseMap();
      CreateMap<Collection,CollectionDTOForGetListSubcribedForUser>().ReverseMap();
      CreateMap<MetadataValue,MetadataValueDTOForCreate>().ReverseMap();
      CreateMap<MetadataValue,MetadataValueDTOForUpdate>().ReverseMap();
      CreateMap<MetadataValue, MetadataValueDTOForSelect>()
         .ForMember(dest=>dest.MetadataFieldName,opt =>opt.MapFrom(x=>x.MetadataFieldRegistry.Qualifier==""?"dc." + x.MetadataFieldRegistry.Element: "dc." + x.MetadataFieldRegistry.Element+"."+x.MetadataFieldRegistry.Qualifier))
         .ReverseMap();
      CreateMap<MetadataValue, MetadataValueDTOForDelete>().ReverseMap();
      CreateMap<Item, ItemDTOForSelectDetailFull>()
         .ForMember(dest=>dest.CollectionName,opt=>opt.MapFrom(x=>x.Collection.CollectionName))
         .ReverseMap();
      CreateMap<Item, ItemDTOForSelectDetailFullForUser>()
         .ForMember(dest => dest.CollectionName, opt => opt.MapFrom(x => x.Collection.CollectionName))
         .ReverseMap();
      CreateMap<Item, ItemDTOForSelectList>()
            .ForMember(dest=>dest.Authors, opt=>opt.MapFrom(x=>x.MetadataValue.Where(x => x.MetadataFieldId == 1).Select(x => x.TextValue).ToList()))
            .ForMember(dest => dest.SubjectKeywords, opt => opt.MapFrom(x => x.MetadataValue.Where(x => x.MetadataFieldId == 51).Select(x => x.TextValue).ToList()))
            .ForMember(dest=>dest.Title,opt=>opt.MapFrom(x=>x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57)==null?"": x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57).TextValue))
            .ForMember(dest => dest.DateOfIssue, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 12) == null ? null : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 12).TextValue))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 36) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 36).TextValue))
            .ForMember(dest => dest.Abstract, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 15) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 15).TextValue))
            .ForMember(dest => dest.IdentifierUri, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 34) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 34).TextValue))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 14) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 14).TextValue))
            .ForMember(dest => dest.Types, opt => opt.MapFrom(x => x.MetadataValue.Where(x => x.MetadataFieldId == 59).Select(x => x.TextValue).ToList()))
         .ReverseMap();
      CreateMap<Item, ItemDTOForSelectDetailSimple>()
           .ForMember(dest => dest.Authors, opt => opt.MapFrom(x => x.MetadataValue.Where(x => x.MetadataFieldId == 1).Select(x => x.TextValue).ToList()))
           .ForMember(dest => dest.SubjectKeywords, opt => opt.MapFrom(x => x.MetadataValue.Where(x => x.MetadataFieldId == 51).Select(x => x.TextValue).ToList()))
           .ForMember(dest => dest.Title, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57).TextValue))
           .ForMember(dest => dest.DateOfIssue, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 12) == null ? null : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 12).TextValue))
           .ForMember(dest => dest.Publisher, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 36) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 36).TextValue))
           .ForMember(dest => dest.Abstract, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 15) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 15).TextValue))
           .ForMember(dest => dest.IdentifierUri, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 34) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 34).TextValue))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 14) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 14).TextValue))
           .ForMember(dest=>dest.CollectionName,opt=>opt.MapFrom(x=>x.Collection.CollectionName))
        .ReverseMap();
      CreateMap<Item, ItemDTOForSelectDetailSimpleForUser>()
           .ForMember(dest => dest.Authors, opt => opt.MapFrom(x => x.MetadataValue.Where(x => x.MetadataFieldId == 1).Select(x => x.TextValue).ToList()))
           .ForMember(dest => dest.SubjectKeywords, opt => opt.MapFrom(x => x.MetadataValue.Where(x => x.MetadataFieldId == 51).Select(x => x.TextValue).ToList()))
           .ForMember(dest => dest.Title, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 57).TextValue))
           .ForMember(dest => dest.DateOfIssue, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 12) == null ? null : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 12).TextValue))
           .ForMember(dest => dest.Publisher, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 36) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 36).TextValue))
           .ForMember(dest => dest.Abstract, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 15) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 15).TextValue))
           .ForMember(dest => dest.IdentifierUri, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 34) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 34).TextValue))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 14) == null ? "" : x.MetadataValue.FirstOrDefault(x => x.MetadataFieldId == 14).TextValue))
           .ForMember(dest => dest.CollectionName, opt => opt.MapFrom(x => x.Collection.CollectionName))
        .ReverseMap();
      CreateMap<MetadataFieldRegistry, MetadateFieldDTO>()
         .ForMember(dest => dest.MetadataFieldName, opt => opt.MapFrom(x => x.Qualifier == "" ? "dc." + x.Element : "dc." + x.Element + "." + x.Qualifier))
         .ReverseMap();
      CreateMap<CommunityGroup,CommunityGroupDTOForCreate>().ReverseMap();
      CreateMap<CommunityGroup, CommunityGroupDTOForSelectList>()
         .ReverseMap();
      CreateMap<CommunityGroup, CommunityGroupDTOForUpdate>().ReverseMap();
      CreateMap<CommunityGroup, CommunityGroupDTOForDetail>()
         .ForMember(dest=>dest.CommunityName,opt=>opt.MapFrom(x=>x.Community.CommunityName))
         .ForMember(dest=>dest.GroupName,opt=>opt.MapFrom(x=>x.Group.Title))
         .ReverseMap();
      CreateMap<CommunityGroup, CommunityGroupDTOForUpdate>().ReverseMap();
      CreateMap<CollectionGroup, CollectionGroupDTOForCreate>().ReverseMap();
      CreateMap<CollectionGroup, CollectionGroupDTOForSelectList>()
         .ReverseMap();
      CreateMap<CollectionGroup, CollectionGroupDTOForDetail>()
         .ForMember(dest=>dest.CollectionName,opt=>opt.MapFrom(x=>x.Collection.CollectionName))
         .ForMember(dest => dest.GroupName, opt => opt.MapFrom(x => x.Group.Title))
         .ReverseMap();
      CreateMap<Statistic, StatisticDTOForCreate>().ReverseMap();
      CreateMap<Statistic, StatisticDTOForSelect>()
         .ForMember(dest => dest.MonthYear, opt => opt.MapFrom(x => x.Month + " " + x.Year))
         .ReverseMap();
   }
}