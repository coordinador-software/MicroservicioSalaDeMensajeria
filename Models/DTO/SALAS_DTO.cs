using ChatAPI.Models.DB;

namespace ChatAPI.Models.DTO
{
    public class SALAS_DTO
    {
        public Guid SALA_ID { get; set; }
        public Guid SISTEMA_ID { get; set; }
        public string NOMBRE_SALA { get; set; } = null!;
        public string? DESCRIPCION_SALA { get; set; }
        public bool ESTATUS { get; set; }
        public DateTime FECHA_REGISTRO { get; set; } = DateTime.Now;
        public DateTime? FECHA_MODIFICACION { get; set; }
        public string USUARIO_REGISTRO { get; set; } = null!;
        public string? USUARIO_MODIFICACION { get; set; }
        public bool ELIMINADO { get; set; }
        public SISTEMAS? SISTEMA { get; set; }
        public ICollection<PARTICIPANTES>? PARTICIPANTES { get; set; }
        public ICollection<MENSAJES>? MENSAJES { get; set; }
        public ICollection<MENSAJES_HISTORICOS>? MENSAJES_HISTORICOS { get; set; }
    }
}
