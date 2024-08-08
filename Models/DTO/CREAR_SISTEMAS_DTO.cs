using ChatAPI.Models.DB;

namespace ChatAPI.Models.DTO
{
    public class CREAR_SISTEMAS_DTO
    {
        public Guid SISTEMA_ID { get; set; }
        public string NOMBRE_SISTEMA { get; set; } = null!;
        public string API_KEY { get; set; } = null!;
        public DateTime FECHA_REGISTRO { get; set; } = DateTime.Now;
        public bool ELIMINAR_SALAS { get; set; } = false;
        public bool ELIMINAR_MENSAJES { get; set; } = false;
        public bool ELIMINAR_ARCHIVOS { get; set; } = false;
        public bool ELIMINADO { get; set; } = false;
    }
}
