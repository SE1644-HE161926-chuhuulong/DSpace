using Application.Communities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Collections
{
    public class CollectionDTOForSelectOfUser
    {
        public int CollectionId { get; set; }
        public string LogoUrl { get; set; }
        public string CollectionName { get; set; }
        public string ShortDescription { get; set; }
        public int CommunityId { get; set; }
      
        public string License { get; set; }
        public int EntityTypeId { get; set; }

         public bool IsSubcribed { get; set; }

        public CommunityDTOForSelectOfUser CommunityDTOForSelectOfUser { get; set; }
    }
}