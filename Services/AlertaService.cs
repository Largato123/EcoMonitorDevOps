using MonitoramentoEnergeticoAPI.Data;
using MonitoramentoEnergeticoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MonitoramentoEnergeticoAPI.Services
{
    public class AlertaService
    {
        private readonly AppDbContext _context;

        public AlertaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task VerificarConsumo(int equipamentoId)
        {
            var equipamento = await _context.Equipamentos.FindAsync(equipamentoId);
            if (equipamento == null) return;

            var setor = await _context.Setores.FindAsync(equipamento.SetorId);
            if (setor == null) return;

            var consumoTotal = await _context.ConsumosEnergia
                .Where(x => x.EquipamentoId == equipamentoId)
                .SumAsync(x => x.ConsumoKwh);

            if (consumoTotal > setor.LimiteConsumo)
            {
                var alerta = new AlertaEnergia
                {
                    Mensagem = $"ALERTA: consumo do setor {setor.Nome} ultrapassou o limite!",
                    DataAlerta = DateTime.Now,
                    EquipamentoId = equipamentoId
                };

                _context.AlertasEnergia.Add(alerta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
