namespace ChatAPI.Models
{
    public class Clases
    {
        public partial class REQUEST_TOKEN
        {
            public DateTime EXPIRES_AT { get; set; }
            public string? TOKEN { get; set; }
        }

        public enum TipoEliminacion
        {
            Archivo,
            Mensaje,
            Sala
        }
    }
}
