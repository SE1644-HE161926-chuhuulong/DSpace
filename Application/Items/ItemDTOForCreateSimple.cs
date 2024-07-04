using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items
{
   public class ItemDTOForCreateSimple
   {
      //dc.contributor.author
      public List<string>? Author { get; set; }
      //dc.title
      public string Title { get; set; }
      //dc.title.alternative
      public List<string>? OtherTitle { get; set; }
      //dc.date.issued
      public DateTime DateOfIssue { get; set; }
      //dc.publisher
      public string? Publisher { get; set; }
      //dc.identifier.citation
      public string? Citation { get; set; }
      //dc.relation.ispartofseries
      public List<string> SeriesNo { get; set; }
      //dc.identifier
      public List<string>? IdentifiersISBN { get; set; }
      public List<string>? IdentifiersISSN { get; set; }
      public List<string>? IdentifiersSICI { get; set; }

      public List<string>? IdentifiersISMN { get; set; }

      public List<string>? IdentifiersOther { get; set; }

      public List<string>? IdentifiersLCCN { get; set; }

      public string IdentifiersURI { get; set; }
      //dc.type
      public List<string>? Type { get; set; }
      //dc.language.iso
      public string? Language { get; set; }
      //dc.subject
      public List<string>? SubjectKeywords { get; set; }
      //dc.description.abstract
      public string? Abstract { get; set; }
      //dc.description.sponsorship
      public string? Sponsors { get; set; }
      //dc.description
      public string? Description { get; set; }

      public int CollectionId { get; set; }
   }
}
