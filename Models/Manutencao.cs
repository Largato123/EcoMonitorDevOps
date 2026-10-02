namespace MonitoramentoEnergeticoAPI.Models
{
    public class Manutencao
    {
        public int Id { get; set; }
        public int EquipamentoId { get; set; }
        public string Status { get; set; }
        public DateTime DataManutencao { get; set; }
    }
}
