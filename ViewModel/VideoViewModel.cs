using VideoMonitoramento.APIRest.Models;

namespace VideoMonitoramento.APIRest.ViewModel
{
    public class VideoViewModel
    {
        public Guid ID { get; set; }
        public string Descricao { get; set; }
        public Guid ServidorID { get; set; }
        //public Servidor Servidor { get; set; }
        public IFormFile Video { get; set; }

    }
}
