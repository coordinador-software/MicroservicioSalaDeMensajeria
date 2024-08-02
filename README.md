
# ChatService

## Descripción
ChatService es una API desarrollada en ASP.NET 8 que permite la gestión de salas de chat, mensajes y usuarios con soporte para SQL Server y comunicación en tiempo real usando SignalR. El servicio está diseñado para ser desplegado en AWS con autoscaling y balanceo de carga.

## Requisitos
- .NET 8 SDK
- SQL Server
- AWS CLI y cuenta de AWS

## Configuración del Proyecto

1. **Crear el Proyecto:**
    ```bash
    dotnet new webapi -n ChatService
    cd ChatService
    ```

2. **Agregar Paquetes Necesarios:**
    ```bash
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.AspNetCore.SignalR
    ```

3. **Configurar la Base de Datos SQL Server:**
    ```csharp
    public class ChatContext : DbContext
    {
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<System> Systems { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("YourConnectionStringHere");
        }
    }
    ```

## Modelos de Datos
Definir los modelos en `Models`:
- `ChatRoom`
- `Message`
- `System`
- `Participant`

## Controladores
Ejemplo de controlador para `Message`:
```csharp
[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly ChatContext _context;

    public MessagesController(ChatContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Message>> PostMessage(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMessage), new { id = message.MensajeId }, message);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetMessage(Guid id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message == null)
        {
            return NotFound();
        }
        return Ok(message);
    }
}
```

## Despliegue en AWS
1. **Crear Instancia EC2:**
    - Configura una instancia EC2 con Ubuntu 24.01.
    - Permite el tráfico en los puertos 80 y 443.

2. **Configurar Autoscaling y Load Balancer:**
    - Configura un grupo de Auto Scaling.
    - Configura un Load Balancer para distribuir el tráfico.

3. **Despliegue:**
    - Implementa el microservicio en la instancia EC2.
    - Asegura el inicio automático del servicio.

## Milestones
1. Configuración Inicial del Proyecto.
2. Definición de Modelos y Controladores.
3. Integración de SignalR.
4. Despliegue en AWS.
5. Pruebas y Validación.

## Pruebas y Monitoreo
1. **Pruebas de Carga:** Verificar manejo de hasta 2400 usuarios simultáneos.
2. **Evaluación de Funcionalidades:** Confirmar gestión de salas, mensajes y multimedia.
3. **Monitoreo y Optimización:** Ajustar el rendimiento y escalabilidad.


## License

<p xmlns:cc="http://creativecommons.org/ns#" xmlns:dct="http://purl.org/dc/terms/"><a property="dct:title" rel="cc:attributionURL" href="http://www.prosur.com.mx/">MicroservicioSalasDeMensajeria</a> by <a rel="cc:attributionURL dct:creator" property="cc:attributionName" href="http://www.prosur.com.mx/">Prosur</a> is licensed under <a href="https://creativecommons.org/licenses/by-nc-nd/4.0/?ref=chooser-v1" target="_blank" rel="license noopener noreferrer" style="display:inline-block;">Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International<img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/cc.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/by.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/nc.svg?ref=chooser-v1" alt=""><img style="height:22px!important;margin-left:3px;vertical-align:text-bottom;" src="https://mirrors.creativecommons.org/presskit/icons/nd.svg?ref=chooser-v1" alt=""></a></p>
