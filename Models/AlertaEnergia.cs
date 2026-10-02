namespace MonitoramentoEnergeticoAPI.Models
{
    public class AlertaEnergia
    {
        public int Id { get; set; }

        public string Mensagem { get; set; } = string.Empty;

        public DateTime DataAlerta { get; set; }

        public int? EquipamentoId { get; set; }
    }
}
