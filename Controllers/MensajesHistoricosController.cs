using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models.DB;
using Microsoft.AspNetCore.Authorization;
using ChatAPI.Interfaces;
using static ChatAPI.Models.Clases;

namespace ChatAPI.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MensajesHistoricosController : ControllerBase
    {
        private readonly DBCHAT _db;
        private readonly IChat _iChat;

        public MensajesHistoricosController(DBCHAT context, IChat iChat)
        {
            _db = context;
            _iChat = iChat;
        }

        [HttpGet("{salaId}")]
        public async Task<ActionResult<IEnumerable<MENSAJES>>> GetMensajes(Guid salaId)
        {
            var messages = await _db.MENSAJES.Include(m => m.SALA).Where(m => m.SALA_ID == salaId && m.SALA.ESTATUS == false).ToListAsync();
            return Ok(messages);
        }

        // DELETE: api/MensajesHistoricos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensajesHistoricos(Guid id)
        {
            try
            {
                var mensaje = await _db.MENSAJES_HISTORICOS.FindAsync(id);
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
    }
}
