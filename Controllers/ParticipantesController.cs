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
    //[Route("api/v1/[controller]")]
    //[ApiController]
    //public class ParticipantesController : ControllerBase
    //{
    //    private readonly DBCHAT _db;

    //    public ParticipantesController(DBCHAT context)
    //    {
    //        _db = context;
    //    }

    //    // GET: api/Participantes
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<PARTICIPANTES>>> GetParticipantes()
    //    {
    //        return await _db.PARTICIPANTES.ToListAsync();
    //    }

    //    // GET: api/Participantes/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<PARTICIPANTES>> GetParticipantes(Guid id)
    //    {
    //        var pARTICIPANTES = await _db.PARTICIPANTES.FindAsync(id);

    //        if (pARTICIPANTES == null)
    //        {
    //            return NotFound();
    //        }

    //        return pARTICIPANTES;
    //    }

    //    // PUT: api/Participantes/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutParticipantes(Guid id, PARTICIPANTES pARTICIPANTES)
    //    {
    //        if (id != pARTICIPANTES.PARTICIPANTE_ID)
    //        {
    //            return BadRequest();
    //        }

    //        _db.Entry(pARTICIPANTES).State = EntityState.Modified;

    //        try
    //        {
    //            await _db.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ParticipantesExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return Ok("Actualizado con exito");
    //    }

    //    // POST: api/Participantes
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<PARTICIPANTES>> PostParticipantes(PARTICIPANTES pARTICIPANTES)
    //    {
    //        _db.PARTICIPANTES.Add(pARTICIPANTES);
    //        await _db.SaveChangesAsync();

    //        return CreatedAtAction("Participante", new { id = pARTICIPANTES.PARTICIPANTE_ID }, pARTICIPANTES);
    //    }

    //    // DELETE: api/Participantes/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeletePARTICIPANTES(Guid id)
    //    {
    //        var pARTICIPANTES = await _db.PARTICIPANTES.FindAsync(id);
    //        if (pARTICIPANTES == null)
    //        {
    //            return NotFound();
    //        }

    //        _db.PARTICIPANTES.Remove(pARTICIPANTES);
    //        await _db.SaveChangesAsync();

    //        return Ok("Eliminado con exito");
    //    }

    //    private bool ParticipantesExists(Guid id)
    //    {
    //        return _db.PARTICIPANTES.Any(e => e.PARTICIPANTE_ID == id);
    //    }
    //}
}
