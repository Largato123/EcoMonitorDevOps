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
    public class SetorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SetorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/setor?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var query = _context.Setores.AsQueryable();

            var total = await query.CountAsync();

            var setores = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                Total = total,
                Page = page,
                PageSize = pageSize,
                Data = setores
            });
        }

        // GET: api/setor/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var setor = await _context.Setores.FindAsync(id);

            if (setor == null)
                return NotFound();

            return Ok(setor);
        }

        // POST: api/setor
        [HttpPost]
        public async Task<IActionResult> Create(Setor setor)
        {
            _context.Setores.Add(setor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = setor.Id }, setor);
        }

        // PUT: api/setor/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Setor setor)
        {
            var existing = await _context.Setores.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.Nome = setor.Nome;
            existing.LimiteConsumo = setor.LimiteConsumo;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/setor/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var setor = await _context.Setores.FindAsync(id);

            if (setor == null)
                return NotFound();

            _context.Setores.Remove(setor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
