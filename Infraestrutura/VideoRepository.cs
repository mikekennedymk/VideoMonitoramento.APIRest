using Microsoft.EntityFrameworkCore;
using VideoMonitoramento.APIRest.Context;
using VideoMonitoramento.APIRest.Infraestrutura.Interface;
using VideoMonitoramento.APIRest.Models;

namespace VideoMonitoramento.APIRest.Services
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;

        public VideoRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Video>> GetVideos(Guid servidorID)
        {
            try
            {
                return await _context.Videos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao buscar os vídeos.", ex);
            }
        }

        public async Task<Video> GetVideo(Guid servidorID, Guid id)
        {
            try
            {
                var video = await _context.Videos.Where(v => v.ID == id && v.ServidorID == servidorID && v.RemovidoEm == null).FirstOrDefaultAsync();
                return video;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao buscar o vídeo.", ex);
            }
        }

        public async Task CreateVideo(Video video)
        {
            try
            {
                _context.Videos.Add(video);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao criar o vídeo.", ex);
            }
        }

        public async Task UpdateVideo(Video video)
        {
            try
            {
                _context.Entry(video).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o vídeo.", ex);
            }
        }

        public async Task DeleteVideo(Video video)
        {
            try
            {
                video.RemovidoEm = DateTime.Now;
                _context.Entry(video).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao deletar o vídeo.", ex);
            }
        }

        public async Task DeleteVideoAntigos(int dias)
        {
            try
            {

                DateTime dataLimite = DateTime.Today.AddDays(-dias);

                var videosParaRemover = _context.Videos.Where(v => v.CriadoEm <= dataLimite && v.RemovidoEm == null).ToList();

                foreach (var video in videosParaRemover)
                {
                    video.RemovidoEm = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao remover os vídeos", ex);
            }
        }


        public IEnumerable<Video> GetVideosAntigos(int dias)
        {
            DateTime dataLimite = DateTime.Today.AddDays(-dias);

            return _context.Videos.Where(v => v.CriadoEm <= dataLimite && v.RemovidoEm == null).ToList();
        }

        public IEnumerable<Video> GetVideosExcluidos(int dias)
        {
            DateTime dataLimite = DateTime.Today.AddDays(-dias);

            return _context.Videos.Where(v => v.CriadoEm <= dataLimite && v.RemovidoEm != null).ToList();
        }
    }
}
