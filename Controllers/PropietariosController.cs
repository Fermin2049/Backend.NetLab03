using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using TpFinalLaboratorio.Net.Data;
using TpFinalLaboratorio.Net.Models;

namespace TpFinalLaboratorio.Net.Controllers
{
    [Authorize(Roles = "Propietario")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropietariosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _config;

        public PropietariosController(MyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginView loginView)
        {
            var propietario = await _context.Propietarios.FirstOrDefaultAsync(x =>
                x.Email == loginView.Usuario
            );
            if (
                propietario == null
                || loginView.Clave == null
                || !VerifyPassword(loginView.Clave, propietario.Password)
            )
            {
                return BadRequest("Nombre de usuario o clave incorrecta");
            }

            var token = GenerateJwtToken(propietario);
            return Ok(new { token });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<Propietario>> GetMyDetails()
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

            return propietario;
        }

        [HttpPut("me")]
        public async Task<IActionResult> PutMyDetails(
            [FromForm] Propietario propietario,
            [FromForm] IFormFile? fotoPerfil,
            [FromForm] string? currentPassword,
            [FromForm] string? newPassword
        )
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (email == null)
            {
                return Unauthorized();
            }

            var existingPropietario = await _context
                .Propietarios.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email);
            if (existingPropietario == null)
            {
                return NotFound();
            }

            if (fotoPerfil != null && fotoPerfil.Length > 0)
            {
                var fileExtension = Path.GetExtension(fotoPerfil.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine("wwwroot/images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fotoPerfil.CopyToAsync(stream);
                }

                propietario.FotoPerfil = $"images/{uniqueFileName}";
            }
            else
            {
                propietario.FotoPerfil = existingPropietario.FotoPerfil;
            }

            // Verificar si se proporciona una nueva contraseña
            if (!string.IsNullOrEmpty(newPassword))
            {
                // Verificar la contraseña actual
                if (!VerifyPassword(currentPassword, existingPropietario.Password))
                {
                    return BadRequest("La contraseña actual es incorrecta.");
                }

                // Actualizar la contraseña
                propietario.Password = HashPassword(newPassword);
            }
            else
            {
                propietario.Password = existingPropietario.Password;
            }

            _context.Entry(propietario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropietarioExists(existingPropietario.IdPropietario))
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

        [HttpPost("solicitar-restablecimiento")]
        public async Task<IActionResult> SolicitarRestablecimiento([FromForm] string email)
        {
            var propietario = await _context.Propietarios.FirstOrDefaultAsync(p =>
                p.Email == email
            );
            if (propietario == null)
            {
                return BadRequest("Correo electrónico no encontrado.");
            }

            // Generar token de restablecimiento
            var token = Guid.NewGuid().ToString();

            // Guardar el token y su fecha de expiración
            propietario.ResetToken = token;
            propietario.ResetTokenExpiry = DateTime.UtcNow.AddHours(1); // Token válido por 1 hora
            await _context.SaveChangesAsync();

            // Enviar correo electrónico con el enlace de restablecimiento
            var resetLink =
                $"{Request.Scheme}://{Request.Host}/api/Propietarios/me/restablecer-contrasena?token={token}";
            await EnviarCorreoAsync(
                email,
                "Restablecimiento de contraseña",
                $"Haga clic en el siguiente enlace para restablecer su contraseña: {resetLink}"
            );

            return Ok(
                "Se ha enviado un enlace de restablecimiento de contraseña a su correo electrónico."
            );
        }

        [HttpPost("me/restablecer-contrasena")]
        public async Task<IActionResult> RestablecerContraseña(
            [FromBody] RestablecerContrasenaRequest request
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
                return NotFound("Propietario no encontrado.");
            }

            // Verificar el token de restablecimiento
            if (!VerificarTokenDeRestablecimiento(propietario, request.Token))
            {
                return BadRequest("Token de restablecimiento inválido o expirado.");
            }

            // Actualizar la contraseña
            propietario.Password = HashPassword(request.NuevaContrasena);
            await _context.SaveChangesAsync();

            return Ok("Contraseña restablecida con éxito.");
        }

        private async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Nombre del remitente", _config["SMTP_User"]));
            mensaje.To.Add(new MailboxAddress("Nombre del destinatario", destinatario));
            mensaje.Subject = asunto;
            mensaje.Body = new TextPart("plain") { Text = cuerpo };

            using var cliente = new SmtpClient();

            if (int.TryParse(_config["SMTP_Port"], out int smtpPort))
            {
                await cliente.ConnectAsync(
                    _config["SMTP_Host"],
                    smtpPort,
                    SecureSocketOptions.StartTls
                );
            }
            else
            {
                throw new ArgumentException("Invalid SMTP port configuration");
            }

            await cliente.AuthenticateAsync(_config["SMTP_User"], _config["SMTP_Pass"]);
            await cliente.SendAsync(mensaje);
            await cliente.DisconnectAsync(true);
        }

        [HttpGet("{id}/restablecer-contrasena")]
        public IActionResult MostrarFormularioRestablecimiento(int id, [FromQuery] string token)
        {
            // Verificar si el token es válido y no ha expirado
            var propietario = _context.Propietarios.FirstOrDefault(p =>
                p.IdPropietario == id
                && p.ResetToken == token
                && p.ResetTokenExpiry > DateTime.UtcNow
            );
            if (propietario == null)
            {
                return BadRequest("Token de restablecimiento inválido o expirado.");
            }

            return Ok(
                new
                {
                    Message = "Formulario de restablecimiento de contraseña",
                    Token = token,
                    Propietario = new
                    {
                        Id = propietario.IdPropietario,
                        Email = propietario.Email,
                        Nombre = propietario.Nombre,
                        Apellido = propietario.Apellido,
                    },
                }
            );
        }

        private bool PropietarioExists(int id)
        {
            return _context.Propietarios.Any(e => e.IdPropietario == id);
        }

        private bool VerificarTokenDeRestablecimiento(Propietario propietario, string token)
        {
            // Verificar si el token y su fecha de expiración existen
            if (propietario.ResetToken == null || propietario.ResetTokenExpiry == null)
            {
                return false;
            }

            // Comprobar si el token coincide y si no ha expirado
            return propietario.ResetToken == token
                && propietario.ResetTokenExpiry > DateTime.UtcNow;
        }

        private string HashPassword(string password)
        {
            var saltConfig = _config["Salt"];
            if (string.IsNullOrEmpty(saltConfig))
            {
                throw new ArgumentNullException("Salt configuration is missing.");
            }

            byte[] salt = Encoding.ASCII.GetBytes(saltConfig);
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                )
            );
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return HashPassword(enteredPassword) == storedHash;
        }

        private string GenerateJwtToken(Propietario propietario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    _config["TokenAuthentication:SecretKey"]
                        ?? throw new ArgumentNullException("TokenAuthentication:SecretKey")
                )
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, propietario.Email),
                new Claim("FullName", propietario.Nombre + " " + propietario.Apellido),
                new Claim(ClaimTypes.Role, "Propietario"),
                new Claim("Dni", propietario.Dni),
                new Claim("Telefono", propietario.Telefono),
                new Claim("FotoPerfil", Url.Content($"~/{propietario.FotoPerfil}")),
                new Claim(
                    "IdPropietario",
                    propietario.IdPropietario.ToString()
                ) // Agregar IdPropietario
                ,
            };

            var token = new JwtSecurityToken(
                issuer: _config["TokenAuthentication:Issuer"],
                audience: _config["TokenAuthentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class RestablecerContrasenaRequest
    {
        public string Token { get; set; } = string.Empty;
        public string NuevaContrasena { get; set; } = string.Empty;
    }

    public class LoginView
    {
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
    }
}
