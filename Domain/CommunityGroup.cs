using Shared.Enums;

namespace Domain;

public class CommunityGroup
{
    public int Id { get; set; }
    public int? CommunityId { get; set; }
    public int? GroupId { get; set; }
    public bool canReview { get; set; }
    public bool canSubmit { get; set; }
    public bool canEdit { get; set; }
    public Group Group { get; set; }
    public Community Community { get; set; }
}