using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatAPI.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace ChatAPI.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly DBCHAT _db;

        public ParticipantesController(DBCHAT context)
        {
            _db = context;
        }

        // GET: api/Participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PARTICIPANTES>>> GetParticipantes()
        {
            return await _db.PARTICIPANTES.ToListAsync();
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PARTICIPANTES>> GetParticipantes(Guid id)
        {
            var participantes = await _db.PARTICIPANTES.FindAsync(id);

            if (participantes == null)
            {
                return NotFound();
            }

            return participantes;
        }

        // PUT: api/Participantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipantes(Guid id, PARTICIPANTES participantes)
        {
            if (id != participantes.PARTICIPANTE_ID)
            {
                return BadRequest();
            }

            _db.Entry(participantes).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantesExists(id))
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

        // POST: api/Participantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PARTICIPANTES>> PostParticipantes(PARTICIPANTES participantes)
        {
            _db.PARTICIPANTES.Add(participantes);
            await _db.SaveChangesAsync();

            return CreatedAtAction("Participante", new { id = participantes.PARTICIPANTE_ID }, participantes);
        }

        // DELETE: api/Participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipantes(Guid id)
        {
            var participantes = await _db.PARTICIPANTES.FindAsync(id);
            if (participantes == null)
            {
                return NotFound();
            }

            participantes.ELIMINADO = true;
            await _db.SaveChangesAsync();

            return Ok("Eliminado con exito");
        }

        private bool ParticipantesExists(Guid id)
        {
            return _db.PARTICIPANTES.Any(e => e.PARTICIPANTE_ID == id);
        }
    }
}
