using System.ComponentModel.DataAnnotations;

namespace Application.Peoples
{
   public class PeopleDTOForCreateUpdate
   {
      [RegularExpression(@"^[a-zA-Z'\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂ ưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]{1,40}$",
       ErrorMessage = "Characters are not allowed.")]
      public string FirstName { get; set; }
      [RegularExpression(@"^[a-zA-Z'\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂ ưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]{1,40}$",
       ErrorMessage = "Characters are not allowed.")]
      public string LastName { get; set; }
      [RegularExpression(@"^[a-zA-Z0-9'\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂ ưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]{1,100}$",
       ErrorMessage = "Characters are not allowed.")]
      public string Address { get; set; }
      [RegularExpression(@"^[0-9'-']{1,13}$",
       ErrorMessage = "Characters are not allowed.")]
      public string PhoneNumber { get; set; }
      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }
      [RegularExpression(@"^[A-Z]{1,10}$",
      ErrorMessage = "Characters are not allowed.")]
      public string rolename { get; set; }
   }
}