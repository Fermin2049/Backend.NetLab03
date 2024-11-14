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
    public class InquilinosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public InquilinosController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inquilino>>> GetInquilinos()
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

            var inquilinos = await _context
                .Inquilinos.Include(i => i.Contratos)
                .ThenInclude(c => c.Inmueble)
                .Where(i =>
                    i.Contratos.Any(c => c.Inmueble.IdPropietario == propietario.IdPropietario)
                )
                .ToListAsync();

            return inquilinos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inquilino>> GetInquilino(int id)
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

            var inquilino = await _context
                .Inquilinos.Include(i => i.Contratos)
                .ThenInclude(c => c.Inmueble)
                .FirstOrDefaultAsync(i =>
                    i.IdInquilino == id
                    && i.Contratos.Any(c => c.Inmueble.IdPropietario == propietario.IdPropietario)
                );

            if (inquilino == null)
            {
                return NotFound();
            }

            return inquilino;
        }

        [HttpPost]
        public async Task<ActionResult<Inquilino>> PostInquilino(Inquilino inquilino)
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

            // Asignar el propietario logueado al inquilino
            inquilino.PropietarioId = propietario.IdPropietario;

            _context.Inquilinos.Add(inquilino);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetInquilino),
                new { id = inquilino.IdInquilino },
                inquilino
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInquilino(int id, Inquilino inquilino)
        {
            if (id != inquilino.IdInquilino)
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

            var existingInquilino = await _context
                .Inquilinos.Include(i => i.Contratos)
                .ThenInclude(c => c.Inmueble)
                .FirstOrDefaultAsync(i =>
                    i.IdInquilino == id
                    && i.Contratos.Any(c => c.Inmueble.IdPropietario == propietario.IdPropietario)
                );

            if (existingInquilino == null)
            {
                return NotFound();
            }

            _context.Entry(inquilino).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InquilinoExists(id))
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
        public async Task<IActionResult> DeleteInquilino(int id)
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

            var inquilino = await _context
                .Inquilinos.Include(i => i.Contratos)
                .ThenInclude(c => c.Inmueble)
                .FirstOrDefaultAsync(i =>
                    i.IdInquilino == id
                    && i.Contratos.Any(c => c.Inmueble.IdPropietario == propietario.IdPropietario)
                );

            if (inquilino == null)
            {
                return NotFound();
            }

            _context.Inquilinos.Remove(inquilino);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InquilinoExists(int id)
        {
            return _context.Inquilinos.Any(e => e.IdInquilino == id);
        }

        // Nuevo método para obtener inquilinos por propietario
        [HttpGet("ByPropietario")]
        public async Task<ActionResult<IEnumerable<Inquilino>>> GetInquilinosByPropietario()
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

            var inquilinos = await _context
                .Inquilinos.Include(i => i.Contratos)
                .ThenInclude(c => c.Inmueble)
                .Where(i =>
                    i.Contratos.Any(c => c.Inmueble.IdPropietario == propietario.IdPropietario)
                )
                .ToListAsync();

            if (inquilinos == null || inquilinos.Count == 0)
            {
                return NotFound();
            }

            return inquilinos;
        }
    }
}
