using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace ChatAPI.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MensajesHistoricosController : ControllerBase
    {
        private readonly DBCHAT _db;

        public MensajesHistoricosController(DBCHAT context)
        {
            _db = context;
        }

        // GET: api/MensajesHistoricos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MENSAJES_HISTORICOS>>> GetMensajesHistoricos()
        {
            return await _db.MENSAJES_HISTORICOS.ToListAsync();
        }

        // GET: api/MensajesHistoricos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MENSAJES_HISTORICOS>> GetMensajesHistoricos(Guid id)
        {
            var mENSAJES_HISTORICOS = await _db.MENSAJES_HISTORICOS.FindAsync(id);

            if (mENSAJES_HISTORICOS == null)
            {
                return NotFound();
            }

            return mENSAJES_HISTORICOS;
        }

        // PUT: api/MensajesHistoricos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMensajesHistoricos(Guid id, MENSAJES_HISTORICOS mENSAJES_HISTORICOS)
        {
            if (id != mENSAJES_HISTORICOS.MENSAJE_ID)
            {
                return BadRequest();
            }

            _db.Entry(mENSAJES_HISTORICOS).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensajesHistoricosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Actualizado con exito");
        }

        // POST: api/MensajesHistoricos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MENSAJES_HISTORICOS>> PostMensajesHistoricos(MENSAJES_HISTORICOS mENSAJES_HISTORICOS)
        {
            _db.MENSAJES_HISTORICOS.Add(mENSAJES_HISTORICOS);
            await _db.SaveChangesAsync();

            return CreatedAtAction("MensajeHistorico", new { id = mENSAJES_HISTORICOS.MENSAJE_ID }, mENSAJES_HISTORICOS);
        }

        // DELETE: api/MensajesHistoricos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensajesHistoricos(Guid id)
        {
            var mENSAJES_HISTORICOS = await _db.MENSAJES_HISTORICOS.FindAsync(id);
            if (mENSAJES_HISTORICOS == null)
            {
                return NotFound();
            }

            _db.MENSAJES_HISTORICOS.Remove(mENSAJES_HISTORICOS);
            await _db.SaveChangesAsync();

            return Ok("Eliminado con exito");
        }

        private bool MensajesHistoricosExists(Guid id)
        {
            return _db.MENSAJES_HISTORICOS.Any(e => e.MENSAJE_ID == id);
        }
    }
}
