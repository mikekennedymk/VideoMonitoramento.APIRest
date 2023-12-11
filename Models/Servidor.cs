using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoMonitoramento.APIRest.Models
{
    [Table("Servidores")]
    public class Servidor
    {
        // Propriedades da classe Servidor
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "O nome do servidor é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do servidor deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O endereço IP do servidor é obrigatório.")]
        [RegularExpression(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$", ErrorMessage = "Formato de endereço IP inválido.")]
        public string EnderecoIP { get; set; }

        [Required(ErrorMessage = "A porta do servidor é obrigatória.")]
        [Range(0, 65535, ErrorMessage = "A porta deve estar entre 0 e 65535.")]
        public int PortaIP { get; set; }

        public string Status { get; set; }
        public DateTime? RemovidoEm { get; set; }

        [Required(ErrorMessage = "Data de Criação obrigatória.")]
        public DateTime CriadoEm { get; set; }

        public ICollection<Video> Videos { get; set; } = new List<Video>();


        // Construtor da classe Servidor
        public Servidor()
        {
            ID = Guid.NewGuid();
            CriadoEm = DateTime.Now;

        }
        public Servidor(string nome, string enderecoIP,int portaIP, string status)
        {
            ID = Guid.NewGuid();
            Nome = nome;
            EnderecoIP = enderecoIP;
            PortaIP = portaIP;
            Status = status;
            CriadoEm = DateTime.Now;
        }


    }

}
