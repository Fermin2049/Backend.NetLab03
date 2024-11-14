using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TpFinalLaboratorio.Net.Data;
using TpFinalLaboratorio.Net.Models;

namespace TpFinalLaboratorio.Net.Controllers
{
    [Authorize(Roles = "Propietario")]
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
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return Unauthorized();
            }

            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p =>
                p.Email == email
            );
            if (propietario == null)
            {
                return NotFound();
            }

            return await _context
                .Contratos.Include(c => c.Inmueble)
                .ThenInclude(i => i.Propietario)
                .Include(c => c.Inquilino)
                .Where(c => c.Inmueble.IdPropietario == propietario.IdPropietario)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contrato>> GetContrato(int id)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return Unauthorized();
            }

            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p =>
                p.Email == email
            );
            if (propietario == null)
            {
                return NotFound();
            }

            var contrato = await _context
                .Contratos.Include(c => c.Inmueble)
                .Include(c => c.Inquilino)
                .FirstOrDefaultAsync(c =>
                    c.IdContrato == id && c.Inmueble.IdPropietario == propietario.IdPropietario
                );

            if (contrato == null)
            {
                return NotFound();
            }

            return contrato;
        }

        [HttpPost]
        public async Task<ActionResult<Contrato>> PostContrato(Contrato contrato)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return Unauthorized();
            }

            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p =>
                p.Email == email
            );
            if (propietario == null)
            {
                return NotFound();
            }

            var inmueble = await _context.Inmuebles.FirstOrDefaultAsync(i =>
                i.IdInmueble == contrato.IdInmueble && i.IdPropietario == propietario.IdPropietario
            );
            if (inmueble == null)
            {
                return BadRequest("El inmueble no pertenece al propietario logueado.");
            }

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

            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return Unauthorized();
            }

            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p =>
                p.Email == email
            );
            if (propietario == null)
            {
                return NotFound();
            }

            var existingContrato = await _context
                .Contratos.Include(c => c.Inmueble)
                .FirstOrDefaultAsync(c =>
                    c.IdContrato == id && c.Inmueble.IdPropietario == propietario.IdPropietario
                );

            if (existingContrato == null)
            {
                return NotFound();
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
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return Unauthorized();
            }

            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p =>
                p.Email == email
            );
            if (propietario == null)
            {
                return NotFound();
            }

            var contrato = await _context
                .Contratos.Include(c => c.Inmueble)
                .FirstOrDefaultAsync(c =>
                    c.IdContrato == id && c.Inmueble.IdPropietario == propietario.IdPropietario
                );

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

        // Nuevo método para obtener contratos por propietario
        [HttpGet("ByPropietario")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratosByPropietario()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return Unauthorized();
            }

            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p =>
                p.Email == email
            );
            if (propietario == null)
            {
                return NotFound();
            }

            var contratos = await _context
                .Contratos.Include(c => c.Inmueble)
                .ThenInclude(i => i.Propietario)
                .Include(c => c.Inquilino)
                .Where(c => c.Inmueble.IdPropietario == propietario.IdPropietario)
                .ToListAsync();

            if (contratos == null || contratos.Count == 0)
            {
                return NotFound();
            }

            return contratos;
        }
    }
}
