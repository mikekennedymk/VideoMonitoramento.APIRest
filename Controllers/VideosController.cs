using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoMonitoramento.APIRest.Infraestrutura.Interface;
using VideoMonitoramento.APIRest.Models;
using VideoMonitoramento.APIRest.Services;
using VideoMonitoramento.APIRest.ViewModel;

namespace VideoMonitoramento.APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class VideosController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IServidorRepository _servidorRepository;

        public VideosController(IVideoRepository videoRepository, IServidorRepository servidorRepository)
        {
            _videoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
            _servidorRepository = servidorRepository ?? throw new ArgumentNullException(nameof(servidorRepository));

        }

        [HttpGet("{servidorID:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos(Guid servidorID)
        {
            try
            {
                var videos = await _videoRepository.GetVideos(servidorID);
                if (!videos.Any())
                    return NotFound("Não existem vídeos cadastrados.");

                return Ok(videos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar vídeos.");
            }
        }

        [HttpGet("{servidorID:Guid}/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Video>> GetVideo(Guid servidorID, Guid id)
        {
            try
            {
                var video = await _videoRepository.GetVideo(servidorID, id);

                if (video == null)
                    return NotFound($"Não existe vídeo com o ID: {id}");

                return Ok(video);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar vídeo.");
            }
        }

        [HttpPost("{servidorID:Guid}/Video")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromForm]VideoViewModel videoViewModel, Guid servidorID)
        {
            try
            {
                var servidor = await _servidorRepository.GetServidor(servidorID);
                if (servidor == null)
                    return NotFound($"Servidor com ID {servidorID} não encontrado.");


                string pastaVideos = @"D:\Video-Monitoramento\VideoMonitoramento.APIRest\Storage\";

                string nomeVideo = videoViewModel.Video.FileName;
                if (!Directory.Exists(pastaVideos))
                {
                    Directory.CreateDirectory(pastaVideos);
                }

                string nomeArquivo = $"{servidor.Nome}-{nomeVideo}"; 


                string caminhoCompleto = Path.Combine(pastaVideos, nomeArquivo);

                using Stream fileStream = new FileStream(caminhoCompleto, FileMode.Create);

                videoViewModel.Video.CopyTo(fileStream);

                var video = new Video(videoViewModel.Descricao, nomeVideo, servidorID, servidor, caminhoCompleto);

                await _videoRepository.CreateVideo(video);

                return Ok($"Vídeo com o ID: {video.ID} foi criado com sucesso e vinculado ao Servidor: {servidor.Nome}");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar vídeo.");
            }
        }

        [HttpPut("Editar/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid id, Video video)
        {
            try
            {
                if (video.ID == id)
                {
                    await _videoRepository.UpdateVideo(video);
                    return Ok($"Vídeo com ID {id} foi atualizado com sucesso.");
                }
                else
                {
                    return NotFound($"Vídeo com ID {id} não encontrado.");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar vídeo.");
            }
        }

        [HttpDelete("{servidorID:Guid}/{videoID:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid servidorID, Guid videoID)
        {
            try
            {
                var video = await _videoRepository.GetVideo(servidorID, videoID);

                if (video == null)
                    return NotFound($"Não existe vídeo com o ID: {videoID}");

                await _videoRepository.DeleteVideo(video);
                return Ok($"Vídeo com ID {videoID} foi excluído com sucesso.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover vídeo.");
            }
        }

        [HttpPost("{servidorID:Guid}/Videos/{videoID:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DownloadVideo(Guid servidorID, Guid videoID)
        {
            var servidor = await _servidorRepository.GetServidor(servidorID);

            if(servidor == null)
                return NotFound($"Não existe Servidor com esse id: {servidorID}");

            var video = await _videoRepository.GetVideo(servidorID, videoID);

            if (video == null)
                return NotFound($"Não existe Video com esse id: {videoID} no Servidor {servidor.Nome}");

            var dataBytes = System.IO.File.ReadAllBytes(video.Caminho);

            return File(dataBytes, "video/mp4", video.Nome);

        }

    }
}
