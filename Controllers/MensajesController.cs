using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models.DB;
using Microsoft.AspNetCore.SignalR;
using ChatAPI.Helpers;
using ChatAPI.Interfaces;
using static ChatAPI.Models.Clases;

namespace ChatAPI.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        private readonly DBCHAT _db;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IChat _iChat;

        public MensajesController(DBCHAT context, IHubContext<ChatHub> hubContext, IChat iChat)
        {
            _db = context;
            _hubContext = hubContext;
            _iChat = iChat;
        }

        [HttpGet("{salaId}")]
        public async Task<ActionResult<IEnumerable<MENSAJES>>> GetMensajes(Guid salaId)
        {
            var messages = await _db.MENSAJES.Where(m => m.SALA_ID == salaId).ToListAsync();
            return Ok(messages);
        }

        //// GET: api/Mensajes/5
        [HttpGet("GetMensaje/{id}")]
        public async Task<ActionResult<MENSAJES>> GetMensaje(Guid id)
        {
            var mensaje = await _db.MENSAJES.FindAsync(id);

            if (mensaje == null)
            {
                return NotFound();
            }

            return Ok(mensaje);
        }

        // POST: api/Mensajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MENSAJES>> PostMensajes(MENSAJES mensaje)
        {
            _db.MENSAJES.Add(mensaje);

            try
            {
                await _db.SaveChangesAsync();
                await _hubContext.Clients.Group(mensaje.SALA_ID.ToString()).SendAsync("ReceiveMessage", mensaje.SALA_ID, mensaje.USUARIO_SEG_ID, mensaje.MENSAJE);
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: Falla al guardar el mensaje");
            }

            return Ok(mensaje);
        }

        // POST: api/Mensajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<MENSAJES>> PostArchivo(IFormFile archivo, Guid salaId)
        //{
        //    if (archivo == null || archivo.Length == 0)
        //    {
        //        return BadRequest("No se ha proporcionado un archivo o el archivo está vacío.");
        //    }



        //    MENSAJES mensaje {


        //    };
        //    _db.MENSAJES.Add(mensaje);
        //   // JsonSerializer.Deserialize<WeatherForecast>(json);
        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //        await _hubContext.Clients.Group(mensaje.SALA_ID.ToString()).SendAsync("ReceiveMessage", mensaje.SALA_ID, mensaje.USUARIO_SEG_ID, mensaje.MENSAJE);
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error: Falla al guardar el mensaje");
        //    }

        //    return CreatedAtAction(nameof(GetMensajes), new { id = mensaje.MENSAJE_ID }, mensaje);
        //}

        // PUT: api/Mensajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMensajes(Guid id, MENSAJES mensajes)
        //{
        //    if (id != mensajes.MENSAJE_ID)
        //    {
        //        return BadRequest();
        //    }

        //    _db.Entry(mensajes).State = EntityState.Modified;

        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MensajesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok("Actualizado con exito");
        //}

        // DELETE: api/Mensajes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensajes(Guid id)
        {
            try
            {
                var mensaje = await _db.MENSAJES.FindAsync(id);
                if (mensaje == null)
                {
                    return NotFound();
                }
                bool eliminarMensaje = await _iChat.verificarPermisoEliminacion(mensaje.TIPO_ARCHIVO == "TEXTO" ? TipoEliminacion.Mensaje : TipoEliminacion.Archivo, id);
                if (eliminarMensaje == false)
                {
                    throw new Exception("ERROR: NO SE CUENTA CON EL PERMISO PARA ELIMINAR EL MENSAJE");
                }
                mensaje.ELIMINADO = true;
                await _db.SaveChangesAsync();

                return Ok("Eliminado con exito");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //private bool MensajesExists(Guid id)
        //{
        //    return _db.MENSAJES.Any(e => e.MENSAJE_ID == id);
        //}
    }
}
