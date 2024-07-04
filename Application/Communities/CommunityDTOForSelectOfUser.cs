using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Communities
{
    public class CommunityDTOForSelectOfUser
    {
        public int CommunityId { get; set; }
        public string LogoUrl { get; set; }
        public string CommunityName { get; set; }
        public string ShortDescription { get; set; }
        public int? ParentCommunityId { get; set; }
    }
}