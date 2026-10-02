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
    public class EquipamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EquipamentoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/equipamento?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var query = _context.Equipamentos.AsQueryable();

            var total = await query.CountAsync();

            var equipamentos = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                Total = total,
                Page = page,
                PageSize = pageSize,
                Data = equipamentos
            });
        }

        // GET: api/equipamento/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);

            if (equipamento == null)
                return NotFound();

            return Ok(equipamento);
        }

        // POST: api/equipamento
        [HttpPost]
        public async Task<IActionResult> Create(Equipamento equipamento)
        {
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return Ok(equipamento);
        }

        // PUT: api/equipamento/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Equipamento equipamento)
        {
            var existing = await _context.Equipamentos.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.Nome = equipamento.Nome;
            existing.Status = equipamento.Status;
            existing.SetorId = equipamento.SetorId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/equipamento/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);

            if (equipamento == null)
                return NotFound();

            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
