namespace MonitoramentoEnergeticoAPI.Models
{
    public class ConsumoEnergia
    {
        public int Id { get; set; }
        public int EquipamentoId { get; set; }
        public decimal ConsumoKwh { get; set; }
        public DateTime DataLeitura { get; set; }
    }
}
