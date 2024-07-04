namespace Application.Responses
{
   public class ResponseDTO
   {
      public bool IsSuccess { get; set; }
      public string? Message { get; set; }
      public object? ObjectResponse { get; set; }
   }
}
