using VideoMonitoramento.APIRest.Models;

namespace VideoMonitoramento.APIRest.Infraestrutura.Interface
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetVideos(Guid servidorID);
        Task<Video> GetVideo(Guid servidorID, Guid id );
        Task CreateVideo(Video video);
        Task UpdateVideo(Video video);
        Task DeleteVideo(Video video);
        IEnumerable<Video> GetVideosAntigos(int dias);
        IEnumerable<Video> GetVideosExcluidos(int dias);

    }
}
