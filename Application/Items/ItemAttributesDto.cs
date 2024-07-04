namespace Application.Items;

public class ItemAttributesDto
{
    //dc.contributor.author
    public string Author { get; set; }
    //dc.title
    public string Title { get; set; }
    //dc.title.alternative
    public string OtherTitle { get; set; }
    //dc.date.issued
    public string DateOfIssue { get; set; }
    //dc.publisher
    public string Publisher { get; set; }
    //dc.identifier.citation
    public string Citation { get; set; }
    //dc.relation.ispartofseries
    public string SeriesNo { get; set; }
    //dc.identifier
    public string Identifiers { get; set; }
    //dc.type
    public string Type { get; set; }
    //dc.language.iso
    public string Language { get; set; }
    //dc.subject
    public string SubjectKeywords { get; set; }
    //dc.description.abstract
    public string Abstract { get; set; }
    //dc.description.sponsorship
    public string Sponsors { get; set; }
    //dc.description
    public string Description { get; set; }

   //author: '',
   //     title: '',
   //     otherTitle: '',
   //     dateOfIssue: '',
   //     publisher: '',
   //     citation: '',
   //     seriesNo: '',
   //     identifiers: '',
   //     type: '',
   //     language: '',
   //     subjectKeywords: '',
   //     abstract: '',
   //     sponsors: '',
   //     description: ''
}