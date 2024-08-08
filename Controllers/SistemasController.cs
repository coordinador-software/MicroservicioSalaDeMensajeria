using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models.DB;
using RHAPI.Interfaces;
using static ChatAPI.Models.Clases;
using ChatAPI.Models.DTO;
using AutoMapper;

namespace ChatAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SistemasController : ControllerBase
    {
        private readonly DBCHAT _db;
        private IMapper _iMapper;

        public SistemasController(DBCHAT context, IMapper iMapper)
        {
            _db = context;
            _iMapper = iMapper;
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
            var sistemas = await _db.SISTEMAS.FindAsync(id);

            if (sistemas == null)
            {
                return NotFound();
            }

            return sistemas;
        }

        // PUT: api/Sistemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSistemas(Guid id, SISTEMAS sistemas)
        {
            if (id != sistemas.SISTEMA_ID)
            {
                return BadRequest();
            }

            _db.Entry(sistemas).State = EntityState.Modified;

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
        public async Task<ActionResult<SISTEMAS>> PostSistemas(SISTEMAS sistema)
        {
            _db.SISTEMAS.Add(sistema);
            await _db.SaveChangesAsync();

            return Ok(sistema);
        }

        // DELETE: api/Sistemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSistemas(Guid id)
        {
            var sistemas = await _db.SISTEMAS.FindAsync(id);
            if (sistemas == null)
            {
                return NotFound();
            }

            sistemas.ELIMINADO = true;
            await _db.SaveChangesAsync();

            return Ok("Eliminado con exito");
        }

        private bool SistemasExists(Guid id)
        {
            return _db.SISTEMAS.Any(e => e.SISTEMA_ID == id);
        }
    }
}
