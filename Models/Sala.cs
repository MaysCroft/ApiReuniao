namespace ApiReuniao.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public double PrecoHora { get; set; }
        public string PossuiProjetor { get; set; }
        public string Status { get; set; }
    }
}
