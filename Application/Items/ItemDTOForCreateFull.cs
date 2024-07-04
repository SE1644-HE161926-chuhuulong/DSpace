using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Metadatas;

namespace Application.Items
{
   public class ItemDTOForCreateFull
   {
      public int CollectionId { get; set; }
      public List<MetadataValueDTOForCreate>? ContributorAuthors { get; set; }
      public List<MetadataValueDTOForCreate>? ContributorAdvisors { get; set; }
      public List<MetadataValueDTOForCreate>? ContributorEditors { get; set; }
      public List<MetadataValueDTOForCreate>? ContributorIllustrators { get; set; }
      public List<MetadataValueDTOForCreate>? ContributorOthers { get; set; }
      public List<MetadataValueDTOForCreate>? CoverageSpatials { get; set; }
      public List<MetadataValueDTOForCreate>? CoverageTemporals { get; set; }
      public List<DateTime>? DateAccessioneds { get; set; }
      public List<DateTime>? DateAvailables { get; set; }
      public List<DateTime>? DateCopyrights { get; set; }
      public List<DateTime>? DateCreateds { get; set; }
      public List<DateTime>? DateIssueds { get; set; }
      public List<DateTime>? DateSubmitteds { get; set; }
      public List<MetadataValueDTOForCreate>? Descriptions { get; set; }
      public List<MetadataValueDTOForCreate>? DescriptionAbtracts { get; set; }
      public List<MetadataValueDTOForCreate>? DescriptionProvenances { get; set; }
      public List<MetadataValueDTOForCreate>? DescriptionSponsorships { get; set; }
      public List<MetadataValueDTOForCreate>? DescriptionStatementOfResponsibilitys { get; set; }
      public List<MetadataValueDTOForCreate>? DescriptionTableOfContents { get; set; }
      public List<MetadataValueDTOForCreate>? DescriptionUris { get; set; }
      public List<MetadataValueDTOForCreate>? Formats { get; set; }
      public List<MetadataValueDTOForCreate>? FormatExtends { get; set; }
      public List<MetadataValueDTOForCreate>? FormatMediums { get; set; }
      public List<MetadataValueDTOForCreate>? FormatCreations { get; set; }
      public List<MetadataValueDTOForCreate>? FormatMimeTypes { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierCitations { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierGovdocs { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierIsbns { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierIsnns { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierSicis { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierIsmns { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierOthers { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierLccns { get; set; }
      public List<MetadataValueDTOForCreate>? IdentifierUris { get; set; }
      public List<MetadataValueDTOForCreate>? LanguageIsos { get; set; }
      public List<MetadataValueDTOForCreate>? Publishers { get; set; }
      public List<MetadataValueDTOForCreate>? RelationIsformatOfs { get; set; }
      public List<MetadataValueDTOForCreate>? RelationIsPartOfs { get; set; }
      public List<MetadataValueDTOForCreate>? RelationIsPartOfSeries { get; set; }
      public List<MetadataValueDTOForCreate>? RelationHasParts { get; set; }
      public List<MetadataValueDTOForCreate>? RelationIsVersionOfs { get; set; }
      public List<MetadataValueDTOForCreate>? RelationHasVersions { get; set; }
      public List<MetadataValueDTOForCreate>? RelationIsBasedOns { get; set; }
      public List<MetadataValueDTOForCreate>? RelationIsReferencedBys { get; set; }
      public List<MetadataValueDTOForCreate>? RelationRequires { get; set; }
      public List<MetadataValueDTOForCreate>? RelationReplaces { get; set; }
      public List<MetadataValueDTOForCreate>? RelationIsPlacedBys { get; set; }
      public List<MetadataValueDTOForCreate>? RelationUris { get; set; }
      public List<MetadataValueDTOForCreate>? RightsUris { get; set; }
      public List<MetadataValueDTOForCreate>? SourceUris { get; set; }
      public List<MetadataValueDTOForCreate>? SubjectClassifications { get; set; }
      public List<MetadataValueDTOForCreate>? SubjectDccs { get; set; }
      public List<MetadataValueDTOForCreate>? SubjectLlcs { get; set; }
      public List<MetadataValueDTOForCreate>? SubjectLcshs { get; set; }
      public List<MetadataValueDTOForCreate>? SubjectMeshs { get; set; }
      public List<MetadataValueDTOForCreate>? SubjectOthers { get; set; }
      public List<MetadataValueDTOForCreate>? Titles { get; set; }
      public List<MetadataValueDTOForCreate>? TitleAlternatives { get; set; }
      public List<MetadataValueDTOForCreate>? Types { get; set; }
   }
}
