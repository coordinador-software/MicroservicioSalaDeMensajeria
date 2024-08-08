using ChatAPI.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.DTO
{
    public class MENSAJES_DTO
    {
        public Guid MENSAJE_ID { get; set; }
        public Guid SALA_ID { get; set; }
        public Guid USUARIO_SEG_ID { get; set; }
        public string MENSAJE { get; set; } = null!;
        public DateTime FECHA_REGISTRO { get; set; }
        public string TIPO_ARCHIVO { get; set; } = null!;
        public bool ELIMINADO { get; set; }
        public virtual SALAS SALA { get; set; } = null!;
    }
}
