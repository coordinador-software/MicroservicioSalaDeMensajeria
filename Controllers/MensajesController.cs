using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models.DB;

namespace ChatAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        private readonly DBCHAT _db;

        public MensajesController(DBCHAT context)
        {
            _db = context;
        }

        // GET: api/Mensajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MENSAJES>>> GetMensajes()
        {
            return await _db.MENSAJES.ToListAsync();
        }

        // GET: api/Mensajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MENSAJES>> GetMensajes(Guid id)
        {
            var mENSAJES = await _db.MENSAJES.FindAsync(id);

            if (mENSAJES == null)
            {
                return NotFound();
            }

            return mENSAJES;
        }

        // PUT: api/Mensajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMensajes(Guid id, MENSAJES mENSAJES)
        {
            if (id != mENSAJES.MENSAJE_ID)
            {
                return BadRequest();
            }

            _db.Entry(mENSAJES).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensajesExists(id))
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

        // POST: api/Mensajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MENSAJES>> PostMensajes(MENSAJES mENSAJES)
        {
            _db.MENSAJES.Add(mENSAJES);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Mensaje", new { id = mENSAJES.MENSAJE_ID }, mENSAJES);
        }

        // DELETE: api/Mensajes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensajes(Guid id)
        {
            var mENSAJES = await _db.MENSAJES.FindAsync(id);
            if (mENSAJES == null)
            {
                return NotFound();
            }

            _db.MENSAJES.Remove(mENSAJES);
            await _db.SaveChangesAsync();

            return Ok("Eliminado con exito");
        }

        private bool MensajesExists(Guid id)
        {
            return _db.MENSAJES.Any(e => e.MENSAJE_ID == id);
        }
    }
}
