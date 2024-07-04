using Shared.Enums;

namespace Domain;

public class CollectionGroup
{
    public int Id { get; set; }
    public int? CollectionId { get; set; }
    public int? GroupId { get; set; }
    public bool canReview { get; set; }
    public bool canSubmit { get; set; }
    public bool canEdit { get; set; }
    public Group Group { get; set; }
    public Collection Collection { get; set; }
}