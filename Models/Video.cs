using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoMonitoramento.APIRest.Models
{
    [Table("Videos")]

    public class Video
    {
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "A descrição do vídeo é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O nome do vídeo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O caminho do vídeo é obrigatório.")]
        public string Caminho { get; set; }

        public DateTime? RemovidoEm { get; set; }

        [Required(ErrorMessage = "Data de Criação obrigatória.")]
        public DateTime CriadoEm { get; set; }

        [Required(ErrorMessage = "O ID do servidor é obrigatório.")]
        public Guid ServidorID { get; set; } // Chave estrangeira para o servidor representando relação n:1 (N vídeos para 1 servidor )

        [ForeignKey("ServidorID")]
        public Servidor Servidor { get; set; } // Propriedade de navegação para o servidor

        //Construtores

        public Video()
        {
            ID = Guid.NewGuid(); // Gera um novo GUID para o vídeo ao criar uma instância
        }
        public Video(string descricao, string nome, Guid idServidor, Servidor servidor, string caminhoVideo)
        {
            ID = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            ServidorID = idServidor;
            Servidor = servidor;
            Caminho = caminhoVideo;
            CriadoEm = DateTime.Now;

        }
    }
}
