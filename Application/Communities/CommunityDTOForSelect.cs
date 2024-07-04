namespace Application.Communities
{
    public class CommunityDTOForSelect
    {
        public int CommunityId { get; set; }
        public string LogoUrl { get; set; }
        public string CommunityName { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public bool isActive { get; set; }
        public int? ParentCommunityId { get; set; }

    }
}
