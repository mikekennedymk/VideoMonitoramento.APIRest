using VideoMonitoramento.APIRest.Models;

namespace VideoMonitoramento.APIRest.ViewModel
{
    public class VideoViewModel
    {
        public string Descricao { get; set; }
        public IFormFile Video { get; set; }

    }
}
