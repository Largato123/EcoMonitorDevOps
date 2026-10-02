using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoEnergeticoAPI.Data;

namespace MonitoramentoEnergeticoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlertaEnergiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertaEnergiaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var query = _context.AlertasEnergia
                .OrderByDescending(x => x.DataAlerta);

            var total = await _context.AlertasEnergia.CountAsync();

            var alertas = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                Total = total,
                Page = page,
                PageSize = pageSize,
                Data = alertas
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alerta = await _context.AlertasEnergia.FindAsync(id);

            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var alerta = await _context.AlertasEnergia.FindAsync(id);

            if (alerta == null)
                return NotFound();

            _context.AlertasEnergia.Remove(alerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
