using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoMonitoramento.APIRest.Infraestrutura.Interface;
using VideoMonitoramento.APIRest.Models;

namespace VideoMonitoramento.APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LixeiraController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IServidorRepository _servidorRepository;

        public LixeiraController(IVideoRepository videoRepository, IServidorRepository servidorRepository)
        {
            _videoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
            _servidorRepository = servidorRepository ?? throw new ArgumentNullException(nameof(servidorRepository));

        }

        [HttpDelete("RemoverAntigos/{dias:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoverVideosAntigos(int dias)
        {
            try
            {
         
                var videosParaRemover = _videoRepository.GetVideosAntigos(dias);

                if (videosParaRemover.Count() == 0)
                    return NotFound($"Não há vídeos para serem removidos.");


                string pastaVideosExcluidos = @"D:\Video-Monitoramento\VideoMonitoramento.APIRest\Recycler\";


                if (!Directory.Exists(pastaVideosExcluidos))
                {
                    Directory.CreateDirectory(pastaVideosExcluidos);
                }


                foreach (var video in videosParaRemover)
                {
                    string caminhoArquivoMP4 = video.Caminho;

                    string novoCaminho = Path.Combine(pastaVideosExcluidos, Path.GetFileName(caminhoArquivoMP4));

                    video.RemovidoEm = DateTime.Now;
                    video.Caminho = novoCaminho;

                    await _videoRepository.UpdateVideo(video);

                   
                    if (System.IO.File.Exists(caminhoArquivoMP4))
                    {
                        System.IO.File.Move(caminhoArquivoMP4, novoCaminho);
                        // Mova o arquivo para uma pasta de vídeos excluídos ou renomeie-o para indicar que foi marcado como excluído
                    }
                }

                return Ok($"Os vídeos cadastrados há {dias} dias a partir da data de hoje foram excluídos logicamente.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover vídeos.");
            }
        }
        [HttpPut("RestaurarAntigos/{dias:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RestaurarVideos(int dias)
        {
            try
            {
                var videosParaRestaurar = _videoRepository.GetVideosExcluidos(dias);


                if (videosParaRestaurar.Count() == 0)
                    return NotFound($"Não há vídeos para serem restaurados.");


                string pastaVideos = @"D:\Video-Monitoramento\VideoMonitoramento.APIRest\Storage\";


                foreach (var video in videosParaRestaurar)
                {
                    string caminhoArquivoMP4 = video.Caminho;

                    string novoCaminho = Path.Combine(pastaVideos, Path.GetFileName(caminhoArquivoMP4));


                    video.RemovidoEm = null;
                    video.Caminho = novoCaminho;


                    await _videoRepository.UpdateVideo(video);


                    if (System.IO.File.Exists(caminhoArquivoMP4))
                    {
                        System.IO.File.Move(caminhoArquivoMP4, novoCaminho);
                    }
                }

                return Ok($"Os vídeos excluídos há {dias} dias a partir do dia de hoje foram restaurados.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao restaurar vídeos.");
            }
        }


    }
}
