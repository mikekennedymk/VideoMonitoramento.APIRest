using Microsoft.EntityFrameworkCore;
using VideoMonitoramento.APIRest.Context;
using VideoMonitoramento.APIRest.Infraestrutura.Interface;
using VideoMonitoramento.APIRest.Models;

namespace VideoMonitoramento.APIRest.Services
{

    public class ServidorRepository : IServidorRepository
    {
        private readonly AppDbContext _context;

        public ServidorRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Servidor>> GetServidores()
        {
            try
            {
                return await _context.Servidores.Where(r => r.RemovidoEm == null).OrderBy(c => c.CriadoEm).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Ocorreu um erro ao buscar os servidores.", ex);
            }
        }

        public async Task<Servidor> GetServidor(Guid id)
        {
            try
            {
                var servidor = await _context.Servidores.Where(i => i.ID == id && i.RemovidoEm == null).FirstOrDefaultAsync(); 

                return servidor;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao buscar o servidor.", ex);
            }
        }

        public async Task<IEnumerable<Servidor>> GetServidoresPorNome(string nome)
        {
            try
            {
                IEnumerable<Servidor> servidores;

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    servidores = await _context.Servidores.Where(n => n.Nome.Contains(nome) && n.RemovidoEm == null).OrderBy(c => c.CriadoEm).ToListAsync();
                }
                else
                {
                    servidores = await GetServidores();
                }

                return servidores;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao buscar os servidores por nome.", ex);
            }
        }

        public async Task CreateServidor(Servidor servidor)
        {
            try
            {
                servidor.RemovidoEm = null;
                _context.Servidores.Add(servidor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao criar o servidor.", ex);
            }
        }

        public async Task UpdateServidor(Servidor servidor)
        {
            try
            {
                _context.Entry(servidor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o servidor.", ex);
            }
        }

        public async Task DeleteServidor(Servidor servidor)
        {
            try
            {
                servidor.RemovidoEm = DateTime.Now;
                _context.Entry(servidor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o servidor.", ex);
            }
        }
       
    }
}