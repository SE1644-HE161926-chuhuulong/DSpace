using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Metadatas
{
    public class MetadataValueDTOForSelect
    {
        public int MetadataValueId { get; set; }
        public string? TextValue { get; set; }
        public string? TextLang { get; set; }

        public string MetadataFieldName { get; set; }
        public int MetadataFieldId { get; set; }

        public int ItemId { get; set; }

    }
}
