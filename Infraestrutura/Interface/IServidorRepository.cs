using VideoMonitoramento.APIRest.Models;

namespace VideoMonitoramento.APIRest.Infraestrutura.Interface
{
    public interface IServidorRepository
    {
        Task<IEnumerable<Servidor>> GetServidores();
        Task<Servidor> GetServidor(Guid id);
        Task<IEnumerable<Servidor>> GetServidoresPorNome(string nome);
        Task<Servidor> GetServidoresPorEnderecoEPortaIP(string enderecoIP, int portaIP);
        Task CreateServidor(Servidor servidor);
        Task UpdateServidor(Servidor servidor);
        Task DeleteServidor(Servidor servidor);


    }
}
