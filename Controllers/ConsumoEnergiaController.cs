using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoEnergeticoAPI.Data;
using MonitoramentoEnergeticoAPI.Models;

namespace MonitoramentoEnergeticoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConsumoEnergiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsumoEnergiaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var query = _context.ConsumosEnergia.AsQueryable();

            var total = await query.CountAsync();

            var consumos = await query
                .OrderByDescending(x => x.DataLeitura)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                Total = total,
                Page = page,
                PageSize = pageSize,
                Data = consumos
            });
        }

        [HttpGet("equipamento/{equipamentoId}")]
        public async Task<IActionResult> GetByEquipamento(int equipamentoId)
        {
            var consumos = await _context.ConsumosEnergia
                .Where(x => x.EquipamentoId == equipamentoId)
                .ToListAsync();

            return Ok(consumos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConsumoEnergia consumo)
        {
            consumo.DataLeitura = DateTime.Now;

            _context.ConsumosEnergia.Add(consumo);
            await _context.SaveChangesAsync();

            return Ok(consumo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var consumo = await _context.ConsumosEnergia.FindAsync(id);

            if (consumo == null)
                return NotFound();

            _context.ConsumosEnergia.Remove(consumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}