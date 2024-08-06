namespace ChatAPI.Models
{
    public class Classes
    {
        public partial class REQUEST_TOKEN
        {
            public DateTime EXPIRES_AT { get; set; }
            public string? TOKEN { get; set; }
        }
    }
}
