using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models.DB;
using Microsoft.AspNetCore.Authorization;
using ChatAPI.Interfaces;
using static ChatAPI.Models.Clases;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Expressions;
using AutoMapper;
using ChatAPI.Models.DTO;

namespace ChatAPI.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly DBCHAT _db;
        private readonly IChat _iChat;

        public SalasController(DBCHAT context, IChat iChat)
        {
            _db = context;
            _iChat = iChat;
        }

        // GET: api/Salas
        [HttpGet("{sistemaId}")]
        public async Task<ActionResult<IEnumerable<SALAS>>> GetSalas(Guid sistemaId)
        {
            List<SALAS> salas = await _db.SALAS.Where(s => s.SISTEMA_ID == sistemaId).ToListAsync();
            return Ok(salas);
        }

        // GET: api/Salas/5
        [HttpGet("GetSala/{id}")]
        public async Task<ActionResult<SALAS>> GetSala(Guid id)
        {
            SALAS? sala = await _db.SALAS.FirstOrDefaultAsync(s => s.SALA_ID == id);
            //SALAS? sala = await _db.SALAS.Include(s => s.PARTICIPANTES).Include(s => s.MENSAJES).Include(s => s.MENSAJES_HISTORICOS).Include(s => s.SISTEMA).FirstOrDefaultAsync(s => s.SALA_ID == id);
            if (sala == null)
            {
                return NotFound();
            }
            return Ok(sala);
        }

        // PUT: api/Salas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalas(Guid id, SALAS salas)
        {
            if (id != salas.SALA_ID)
            {
                return BadRequest();
            }
            _db.Entry(salas).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Salas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SALAS>> PostSalas(SALAS sala)
        {
            
            if (!_db.SISTEMAS.Any(s => s.SISTEMA_ID == sala.SISTEMA_ID)) {
                return NotFound("ERROR: SISTEMA_ID INEXISTENTE");
            }
            _db.SALAS.Add(sala);
            await _db.SaveChangesAsync();

            return Ok(sala);
        }

        // SOLO SI ELIMINAR_SALAS ESTA ACTIVO
        // DELETE: api/Salas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalas(Guid id)
        {
            try
            {

                SALAS? sala = await _db.SALAS.FindAsync(id);
                if (sala == null)
                {
                    return NotFound();
                }
                bool eliminarSala = await _iChat.verificarPermisoEliminacion(TipoEliminacion.Sala, id);
                if (eliminarSala == false)
                {
                    throw new Exception("ERROR: NO SE CUENTA CON EL PERMISO PARA ELIMINAR LA SALA DE CONVERSACIÓN");
                }

                if (sala.ESTATUS)
                {
                    _db.MENSAJES.Where(m => m.SALA_ID == sala.SALA_ID && m.TIPO_ARCHIVO != "TEXTO").ToList().ForEach(m => m.ELIMINADO = true);
                }
                else
                {
                    _db.MENSAJES_HISTORICOS.Where(m => m.SALA_ID == sala.SALA_ID && m.TIPO_ARCHIVO != "TEXTO").ToList().ForEach(m => m.ELIMINADO = true);
                }


                sala.ELIMINADO = true;
                await _db.SaveChangesAsync();

                return Ok("Eliminado con exito");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool SalasExists(Guid id)
        {
            return _db.SALAS.Any(e => e.SALA_ID == id);
        }
    }
}
