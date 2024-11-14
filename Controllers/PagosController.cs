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
    public class PagosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PagosController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
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
                .Pagos.Include(p => p.Contrato)
                .ThenInclude(c => c.Inmueble)
                .Where(p =>
                    p.Contrato != null
                    && p.Contrato.Inmueble != null
                    && p.Contrato.Inmueble.IdPropietario == propietario.IdPropietario
                )
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
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

            var pago = await _context
                .Pagos.Include(p => p.Contrato)
                .ThenInclude(c => c.Inmueble)
                .FirstOrDefaultAsync(p =>
                    p.IdPago == id
                    && p.Contrato != null
                    && p.Contrato.Inmueble != null
                    && p.Contrato.Inmueble.IdPropietario == propietario.IdPropietario
                );

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
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
                    c.IdContrato == pago.IdContrato
                    && c.Inmueble != null
                    && c.Inmueble.IdPropietario == propietario.IdPropietario
                );

            if (contrato == null)
            {
                return BadRequest("El contrato no pertenece al propietario logueado.");
            }

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPago), new { id = pago.IdPago }, pago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.IdPago)
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

            var existingPago = await _context
                .Pagos.Include(p => p.Contrato)
                .ThenInclude(c => c.Inmueble)
                .FirstOrDefaultAsync(p =>
                    p.IdPago == id
                    && p.Contrato != null
                    && p.Contrato.Inmueble != null
                    && p.Contrato.Inmueble.IdPropietario == propietario.IdPropietario
                );

            if (existingPago == null)
            {
                return NotFound();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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
        public async Task<IActionResult> DeletePago(int id)
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

            var pago = await _context
                .Pagos.Include(p => p.Contrato)
                .ThenInclude(c => c.Inmueble)
                .FirstOrDefaultAsync(p =>
                    p.IdPago == id
                    && p.Contrato != null
                    && p.Contrato.Inmueble != null
                    && p.Contrato.Inmueble.IdPropietario == propietario.IdPropietario
                );

            if (pago == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.IdPago == id);
        }

        // Nuevo método para obtener pagos por propietario
        [HttpGet("ByPropietario")]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagosByPropietario()
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

            var pagos = await _context
                .Pagos.Include(p => p.Contrato)
                .ThenInclude(c => c.Inmueble)
                .Where(p =>
                    p.Contrato != null
                    && p.Contrato.Inmueble != null
                    && p.Contrato.Inmueble.IdPropietario == propietario.IdPropietario
                )
                .ToListAsync();

            if (pagos == null || pagos.Count == 0)
            {
                return NotFound();
            }

            return pagos;
        }
    }
}
