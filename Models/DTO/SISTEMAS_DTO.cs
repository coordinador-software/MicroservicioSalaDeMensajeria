using ChatAPI.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.DTO
{
    public class SISTEMAS_DTO
    {
        public Guid SISTEMA_ID { get; set; }
        [Required]
        public string NOMBRE_SISTEMA { get; set; } = null!;
        [Required]
        public string API_KEY { get; set; } = null!;
        public DateTime FECHA_REGISTRO { get; set; }
        public bool ELIMINAR_SALAS { get; set; }
        public bool ELIMINAR_MENSAJES { get; set; }
        public bool ELIMINAR_ARCHIVOS { get; set; }
        public bool ELIMINADO { get; set; }
        public ICollection<SALAS>? SALAS { get; set; }
    }
}
