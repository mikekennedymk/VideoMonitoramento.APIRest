using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoMonitoramento.APIRest.Infraestrutura.Interface;
using VideoMonitoramento.APIRest.Models;
using VideoMonitoramento.APIRest.ViewModel;

namespace VideoMonitoramento.APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ServidoresController : ControllerBase
    {
        private IServidorRepository _servidorRepository;

        public ServidoresController(IServidorRepository servidorRepository)
        {
            _servidorRepository = servidorRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Servidor>>> GetServidores()
        {
            try
            {
                var servidores = await _servidorRepository.GetServidores();
                if (servidores.Count() == 0)
                    return NotFound($"Não existem servidores cadastrados no momento");

                return Ok(servidores);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar servidor");
               
            }
        }
        [HttpGet("ServidorPorNome")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Servidor>>> GetServidorPorNome([FromQuery] string nome)
        {
            try
            {
                var servidores = await _servidorRepository.GetServidoresPorNome(nome);

                if(servidores.Count()==0)
                    return NotFound($"Não existem servidores com o nome: {nome}");

                return Ok(servidores);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar servidor");

            }
        }
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Servidor>> GetServidor(Guid id)
        {
            try
            {
                var servidor = await _servidorRepository.GetServidor(id);

                if (servidor == null)
                    return NotFound($"Não existe servidor com o ID: {id}");

                return Ok(servidor);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar servidor");
            }
        }

        [HttpGet("Disponibilidade/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Servidor>> GetStatusDisponibilidade(Guid id)
        {
            try
            {
                var servidor = await _servidorRepository.GetServidor(id);

                if (servidor == null)
                    return NotFound($"Não existe servidor com o ID: {id}");

                return Ok($"O servidor {servidor.Nome} está {servidor.Status}");

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar servidor");
            }
        }

        [HttpGet("{enderecoIP}/{portaIP:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Servidor>> GetServidoresPorEnderecoEPortaIP(string enderecoIP, int portaIP)
        {
            try
            {
                var servidor = await _servidorRepository.GetServidoresPorEnderecoEPortaIP(enderecoIP, portaIP);

                if (servidor == null)
                    return NotFound("Servidor não encontrado.");

                return Ok($"Servidor {servidor.Nome} encontrado pelo ip {enderecoIP} e porta {portaIP}.");

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao retornar servidor");
            }
        }

        [HttpPost("Servidor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(ServidorViewModel servidorViewModel)
        {
            try
            {
                var servidor = new Servidor
                {
                    Nome = servidorViewModel.Nome,
                    EnderecoIP = servidorViewModel.EnderecoIP,
                    PortaIP = servidorViewModel.PortaIP,
                    Status = servidorViewModel.Status,
                };

                await _servidorRepository.CreateServidor(servidor);

                return Ok($"Servidor com o nome: {servidor.Nome} foi criado com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar servidor");
            }
        }

        [HttpPut("Editar/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid id, [FromBody] ServidorViewModel servidorViewModel)
        {
            try
            {
                var servidor = new Servidor
                {
                    ID = servidorViewModel.ID,
                    Nome = servidorViewModel.Nome,
                    EnderecoIP = servidorViewModel.EnderecoIP,
                    PortaIP = servidorViewModel.PortaIP,
                    Status = servidorViewModel.Status,
                };

                if (servidor.ID == id)
                {
                    await _servidorRepository.UpdateServidor(servidor);
                    return Ok($"Servidor com o nome = {servidor.Nome} foi atualizado com sucesso");
                }
                else
                {
                    return NotFound($"Servidor {servidor.Nome} não encontrado");

                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar servidor");
            }
        }
        [HttpPut("Desligar/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DesligaServidor(Guid id)
        {
            try
            {
                var servidor = await _servidorRepository.GetServidor(id);

                if (servidor != null)
                {
                    servidor.Status = "Desligado";
                    await _servidorRepository.UpdateServidor(servidor);
                    return Ok($"Servidor {servidor.Nome} foi Desligado com sucesso.");
                }
                else
                {
                    return NotFound($"Servidor não encontrado");

                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar servidor");
            }
        }

        [HttpPut("Ligar/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LigaServidor(Guid id)
        {
            try
            {
                var servidor = await _servidorRepository.GetServidor(id);

                if (servidor != null)
                {
                    servidor.Status = "Ligado";
                    await _servidorRepository.UpdateServidor(servidor);
                    return Ok($"Servidor {servidor.Nome} foi Ligado com sucesso.");
                }
                else
                {
                    return NotFound($"Servidor não encontrado");

                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar servidor");
            }
        }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var servidor = await _servidorRepository.GetServidor(id);

                if (servidor != null)
                {
                    await _servidorRepository.DeleteServidor(servidor);
                    return Ok($"Servidor {servidor.Nome} foi excluído lógicamente com sucesso");
                }
                else
                {
                    return NotFound($"Servidor não encontrado");

                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover servidor");
            }
        }
    }
}
