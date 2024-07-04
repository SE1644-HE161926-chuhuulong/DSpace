using System.Text.RegularExpressions;

namespace DSpace.ValidateData
{
   public class ValidateData
   {
      public bool ValidateString(string data)
      {
         if (data == null)
         {
            return false;
         }
         if (!Regex.IsMatch(data, @"^[a-zA-Z0-9_\s]+$"))
         {
            return false;
         }
         return true;
      }
   }
}