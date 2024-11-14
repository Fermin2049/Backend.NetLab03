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
    public class InmueblesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public InmueblesController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inmueble>>> GetInmuebles()
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
                .Inmuebles.Where(i => i.IdPropietario == propietario.IdPropietario)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inmueble>> GetInmueble(int id)
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
                i.IdInmueble == id && i.IdPropietario == propietario.IdPropietario
            );

            if (inmueble == null)
            {
                return NotFound();
            }

            return inmueble;
        }

        [HttpPost]
        public async Task<ActionResult<Inmueble>> PostInmueble(
            [FromForm] Inmueble inmueble,
            [FromForm] IFormFile? imagen
        )
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

            if (imagen != null && imagen.Length > 0)
            {
                var fileExtension = Path.GetExtension(imagen.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine("wwwroot/images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                inmueble.Imagen = $"images/{uniqueFileName}";
            }

            inmueble.IdPropietario = propietario.IdPropietario;

            _context.Inmuebles.Add(inmueble);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInmueble), new { id = inmueble.IdInmueble }, inmueble);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInmueble(
            int id,
            [FromForm] Inmueble inmueble,
            [FromForm] IFormFile? imagen
        )
        {
            if (id != inmueble.IdInmueble)
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

            var existingInmueble = await _context
                .Inmuebles.AsNoTracking()
                .FirstOrDefaultAsync(i =>
                    i.IdInmueble == id && i.IdPropietario == propietario.IdPropietario
                );
            if (existingInmueble == null)
            {
                return NotFound();
            }

            if (imagen != null && imagen.Length > 0)
            {
                var fileExtension = Path.GetExtension(imagen.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine("wwwroot/images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                inmueble.Imagen = $"images/{uniqueFileName}";
            }
            else
            {
                inmueble.Imagen = existingInmueble.Imagen;
            }

            inmueble.IdPropietario = propietario.IdPropietario;

            _context.Entry(inmueble).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InmuebleExists(id))
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
        public async Task<IActionResult> DeleteInmueble(int id)
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
                i.IdInmueble == id && i.IdPropietario == propietario.IdPropietario
            );
            if (inmueble == null)
            {
                return NotFound();
            }

            _context.Inmuebles.Remove(inmueble);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Nuevo método para obtener inmuebles por propietario
        [HttpGet("ByPropietario")]
        public async Task<ActionResult<IEnumerable<Inmueble>>> GetInmueblesByPropietario()
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

            var inmuebles = await _context
                .Inmuebles.Where(i => i.IdPropietario == propietario.IdPropietario)
                .ToListAsync();

            if (inmuebles == null || inmuebles.Count == 0)
            {
                return NotFound();
            }

            return inmuebles;
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> UpdateEstadoInmueble(int id, [FromBody] string nuevoEstado)
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
                i.IdInmueble == id && i.IdPropietario == propietario.IdPropietario
            );
            if (inmueble == null)
            {
                return NotFound();
            }

            inmueble.Estado = nuevoEstado;

            _context.Entry(inmueble).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InmuebleExists(id))
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

        // Nuevo método para obtener contratos por inmueble
        [HttpGet("ByInmueble/{inmuebleId}")]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContractsByInmuebleId(
            int inmuebleId
        )
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

            var contracts = await _context
                .Contratos.Include(c => c.Inmueble)
                .Where(c =>
                    c.IdInmueble == inmuebleId
                    && c.Inmueble.IdPropietario == propietario.IdPropietario
                )
                .ToListAsync();

            if (contracts == null || contracts.Count == 0)
            {
                return NotFound();
            }

            return contracts;
        }

        private bool InmuebleExists(int id)
        {
            return _context.Inmuebles.Any(e => e.IdInmueble == id);
        }
    }
}
