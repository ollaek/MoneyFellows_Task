using MF_Task.Service.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MF_Task.Service.Queries
{
    public class LoginQuery : BaseQuery<BaseResponseDTO<string>>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
