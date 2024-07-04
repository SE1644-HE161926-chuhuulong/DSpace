using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    [Column("first_name")]
    public string FirstName { get; set; }
    [Column("last_name")]
    public string LastName { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("activated")]
    public bool isActive { get; set; }
    public People? People { get; set; }
    public List<Subscribe> Subscribes { get; set; }
    [JsonIgnore]
    public virtual ICollection<UserRole> UserRoles { get; set; }
}