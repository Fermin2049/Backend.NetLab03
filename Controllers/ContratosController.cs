using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TpFinalLaboratorio.Net.Data;
using TpFinalLaboratorio.Net.Models;

namespace TpFinalLaboratorio.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ContratosController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratos()
        {
            return await _context
                .Contratos.Include(c => c.Inmueble)
                .ThenInclude(i => i.Propietario) // Incluye el propietario del inmueble
                .Include(c => c.Inquilino)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contrato>> GetContrato(int id)
        {
            var contrato = await _context
                .Contratos.Include(c => c.Inmueble)
                .Include(c => c.Inquilino)
                .FirstOrDefaultAsync(c => c.IdContrato == id);

            if (contrato == null)
            {
                return NotFound();
            }

            return contrato;
        }

        [HttpPost]
        public async Task<ActionResult<Contrato>> PostContrato(Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContrato), new { id = contrato.IdContrato }, contrato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContrato(int id, Contrato contrato)
        {
            if (id != contrato.IdContrato)
            {
                return BadRequest();
            }

            _context.Entry(contrato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratoExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }

            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContratoExists(int id)
        {
            return _context.Contratos.Any(e => e.IdContrato == id);
        }

        [HttpGet("ByPropietario/{propietarioId}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratosByPropietarioId(
            int propietarioId
        )
        {
            var contratos = await _context
                .Contratos.Include(c => c.Inmueble)
                .ThenInclude(i => i.Propietario != null ? i.Propietario : null)
                .Include(c => c.Inquilino)
                .Where(c => c.Inmueble != null && c.Inmueble.IdPropietario == propietarioId)
                .ToListAsync();

            if (contratos == null || contratos.Count == 0)
            {
                return NotFound();
            }

            return contratos;
        }
    }
}
