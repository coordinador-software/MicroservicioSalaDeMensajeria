using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.DB
{
    public class PARTICIPANTES
    {
        [Key]
        public Guid PARTICIPANTE_ID { get; set; }
        [Required]
        public Guid SALA_ID { get; set; }
        [Required]
        public Guid USUARIO_SEG_ID { get; set; }
        public DateTime FECHA_REGISTRO { get; set; }
        public virtual SALAS SALA { get; set; } = null!;
    }
}
