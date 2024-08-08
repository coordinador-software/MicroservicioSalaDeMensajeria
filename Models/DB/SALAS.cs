using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.DB
{
    public class SALAS
    {
        public SALAS()
        {
            PARTICIPANTES = new HashSet<PARTICIPANTES>();
            MENSAJES = new HashSet<MENSAJES>();
            MENSAJES_HISTORICOS = new HashSet<MENSAJES_HISTORICOS>();
        }
        [Key]
        public Guid SALA_ID { get; set; }
        [Required]
        public Guid SISTEMA_ID { get; set; }
        [Required]
        public string NOMBRE_SALA { get; set; } = null!;
        public string? DESCRIPCION_SALA { get; set; }
        public bool ESTATUS { get; set; }
        public DateTime FECHA_REGISTRO { get; set; } = DateTime.Now;
        public DateTime? FECHA_MODIFICACION { get; set; }
        [Required]
        public string USUARIO_REGISTRO { get; set; } = null!;
        public string? USUARIO_MODIFICACION { get; set; }
        public bool ELIMINADO { get; set; }
        public virtual SISTEMAS SISTEMA { get; set; } = null!;
        public virtual ICollection<PARTICIPANTES> PARTICIPANTES { get; set; }
        public virtual ICollection<MENSAJES> MENSAJES { get; set; }
        public virtual ICollection<MENSAJES_HISTORICOS> MENSAJES_HISTORICOS { get; set; }
    }
}
