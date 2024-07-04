using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors
{
   public class AuthorDTOForCreateUpdate
   {
      [RegularExpression(@"^[a-zA-Z''-'\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂ ưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]{1,40}$",
       ErrorMessage = "Characters are not allowed.")]
      public string FullName { get; set; }
      [RegularExpression(@"^[a-zA-Z''-'\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂ ưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ]{1,40}$",
       ErrorMessage = "Characters are not allowed.")]
      public string JobTitle { get; set; }
      [DataType(DataType.DateTime)]
      public DateTime DateAccessioned { get; set; }
      [DataType(DataType.DateTime)]
      public DateTime DateAvailable { get; set; }
      public string Uri { get; set; }
      public string Type { get; set; }
   }
}