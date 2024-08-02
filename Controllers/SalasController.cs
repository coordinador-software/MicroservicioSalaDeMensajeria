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
    public class SalasController : ControllerBase
    {
        private readonly DBCHAT _db;

        public SalasController(DBCHAT context)
        {
            _db = context;
        }

        // GET: api/Salas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SALAS>>> GetSalas()
        {
            return await _db.SALAS.ToListAsync();
        }

        // GET: api/Salas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SALAS>> GetSalas(Guid id)
        {
            var sALAS = await _db.SALAS.FindAsync(id);

            if (sALAS == null)
            {
                return NotFound();
            }

            return sALAS;
        }

        // PUT: api/Salas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalas(Guid id, SALAS sALAS)
        {
            if (id != sALAS.SALA_ID)
            {
                return BadRequest();
            }

            _db.Entry(sALAS).State = EntityState.Modified;

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
        public async Task<ActionResult<SALAS>> PostSalas(SALAS sALAS)
        {
            _db.SALAS.Add(sALAS);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Sala", new { id = sALAS.SALA_ID }, sALAS);
        }

        // DELETE: api/Salas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalas(Guid id)
        {
            var sALAS = await _db.SALAS.FindAsync(id);
            if (sALAS == null)
            {
                return NotFound();
            }

            _db.SALAS.Remove(sALAS);
            await _db.SaveChangesAsync();

            return Ok("Eliminado con exito");
        }

        private bool SalasExists(Guid id)
        {
            return _db.SALAS.Any(e => e.SALA_ID == id);
        }
    }
}
