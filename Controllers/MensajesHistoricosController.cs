using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models.DB;
using Microsoft.AspNetCore.Authorization;
using ChatAPI.Interfaces;
using static ChatAPI.Models.Clases;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<ActionResult<IEnumerable<MENSAJES_HISTORICOS>>> GetMensajes(Guid salaId)
        {
            SALAS? sala = await _db.SALAS.FindAsync(salaId);
            if (sala == null)
            {
                return NotFound("ERROR: SALA_ID INEXISTENTE");
            }
            var messages = await _db.MENSAJES_HISTORICOS.Where(m => m.SALA_ID == salaId).ToListAsync();
            if (!messages.IsNullOrEmpty() && sala.ESTATUS == true) //LA SALA DE CONVERSACIÓN DEBE ESTAR CERRADA
            {
                return NotFound("ERROR: PROBLEMA DE INTEGRIDAD, LA SALA DEBE ESTAR CERRADA PARA OBTENER LOS MENSAJES DEL HISTORICO");
            }
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
