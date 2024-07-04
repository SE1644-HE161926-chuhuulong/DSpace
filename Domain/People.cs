using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class People
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PeopleId { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string UserId { get; set; }
    public People PeopleParent { get; set; }
    public User User { get; set; } = null!;
    public List<People> ListPeopleCreated { get; set; }
    public List<Item> Items { get; set; }
    public List<Collection> Collection { get; set; }
    public List<Community> Community { get; set; }
    public List<GroupPeople> GroupPeoples { get; set; }
}