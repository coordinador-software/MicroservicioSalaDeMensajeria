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
    public class SistemasController : ControllerBase
    {
        private readonly DBCHAT _db;

        public SistemasController(DBCHAT context)
        {
            _db = context;
        }

        // GET: api/Sistemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SISTEMAS>>> GetSistemas()
        {
            return await _db.SISTEMAS.ToListAsync();
        }

        // GET: api/Sistemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SISTEMAS>> GetSistemas(Guid id)
        {
            var sISTEMAS = await _db.SISTEMAS.FindAsync(id);

            if (sISTEMAS == null)
            {
                return NotFound();
            }

            return sISTEMAS;
        }

        // PUT: api/Sistemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSistemas(Guid id, SISTEMAS sISTEMAS)
        {
            if (id != sISTEMAS.SISTEMA_ID)
            {
                return BadRequest();
            }

            _db.Entry(sISTEMAS).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SistemasExists(id))
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

        // POST: api/Sistemas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SISTEMAS>> PostSistemas(SISTEMAS sISTEMAS)
        {
            _db.SISTEMAS.Add(sISTEMAS);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Sistema", new { id = sISTEMAS.SISTEMA_ID }, sISTEMAS);
        }

        // DELETE: api/Sistemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSistemas(Guid id)
        {
            var sISTEMAS = await _db.SISTEMAS.FindAsync(id);
            if (sISTEMAS == null)
            {
                return NotFound();
            }

            _db.SISTEMAS.Remove(sISTEMAS);
            await _db.SaveChangesAsync();

            return Ok("Eliminado con exito");
        }

        private bool SistemasExists(Guid id)
        {
            return _db.SISTEMAS.Any(e => e.SISTEMA_ID == id);
        }

        [HttpGet("HolaMundo")]
        public ActionResult<string> GetHolaMundo()
        {
            return "HolaMundo";
        }
    }
}
