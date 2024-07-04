using Application.Metadatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Items
{
    public class ItemDTOForSelectList
    {
        public int ItemId { get; set; }
        //no 1
        public List<string> Authors { get; set; }
        // 57
        public string Title { get; set; }
        //12
        public DateTime DateOfIssue { get; set; }
        //36
        public string Publisher { get; set; }
        //51 
        public List<string> SubjectKeywords { get; set; }
        //15
        public string? Abstract { get; set; }
        //34
        public string? IdentifierUri { get; set; }
        //14
        public string? Description { get; set; }

        public List<string>? Types { get; set; }

     


    }
}
