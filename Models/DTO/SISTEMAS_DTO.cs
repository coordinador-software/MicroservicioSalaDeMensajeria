using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.DTO
{
    public class SISTEMAS_DTO
    {
        [Required]
        public string NOMBRE_SISTEMA { get; set; } = null!;
        [Required]
        public string API_KEY { get; set; } = null!;
    }
}
