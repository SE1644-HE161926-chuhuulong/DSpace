namespace Application.Peoples
{
   public class PeopleDTOForSelect
   {
      public int PeopleId { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }
      public string Email { get; set; }
      public string Address { get; set; }
      public string PhoneNumber { get; set; }
      public string? CreatedBy { get; set; }
      public DateTime? CreatedDate { get; set; }
      public string? LastModifiedBy { get; set; }
      public DateTime? LastModifiedDate { get; set; }
      public string UserId { get; set; }
      public string Role { get; set; }
      public bool IsActive { get; set; }


   }
}