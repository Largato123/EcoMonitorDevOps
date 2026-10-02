namespace MonitoramentoEnergeticoAPI.Models
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }

        public int SetorId { get; set; }
    }
}
