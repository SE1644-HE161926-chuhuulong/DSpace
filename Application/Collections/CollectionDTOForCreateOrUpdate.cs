using System.ComponentModel.DataAnnotations;

namespace Application.Collections
{
   public class CollectionDTOForCreateOrUpdate
   {
      public string LogoUrl { get; set; }
      [RegularExpression(@"^[a-zA-Z0-9''-'\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂ ưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]{1,40}$",
        ErrorMessage = "Characters are not allowed.")]
      public string CollectionName { get; set; }
      [RegularExpression(@"^[a-zA-Z0-9''-'\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂ ưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]{1,1000}$",
        ErrorMessage = "Characters are not allowed.")]
      public string ShortDescription { get; set; }
      public int CommunityId { get; set; }
      public bool isActive { get; set; }
      public string License { get; set; }
      public int EntityTypeId { get; set; }
   }
}
