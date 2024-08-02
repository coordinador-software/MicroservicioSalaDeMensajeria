using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.DB
{
    public class MENSAJES
    {
        [Key]
        public Guid MENSAJE_ID { get; set; }
        [Required]
        public Guid SALA_ID { get; set; }
        [Required]
        public Guid USUARIO_SEG_ID { get; set; }
        [Required]
        public string MENSAJE { get; set; } = null!;
        public DateTime FECHA_REGISTRO { get; set; }
        [Required]
        public string TIPO_ARCHIVO { get; set; } = null!;
        public virtual SALAS SALA { get; set; } = null!;
    }
}
