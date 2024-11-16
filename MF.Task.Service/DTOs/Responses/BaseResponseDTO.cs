
namespace MF_Task.Service.DTOs
{
    public class BaseResponseDTO<T> 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }  

        public BaseResponseDTO(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public BaseResponseDTO(bool success, string message, List<string> errors, T data = default)
        {
            Success = success;
            Message = message;
            Errors = errors;
            Data = data;
        }
    }

}
