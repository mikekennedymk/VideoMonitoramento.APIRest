using Microsoft.EntityFrameworkCore;
using VideoMonitoramento.APIRest.Models;

namespace VideoMonitoramento.APIRest.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet <Servidor> Servidores { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeamento da entidade Servidor
            modelBuilder.Entity<Servidor>(entity =>
            {
                entity.ToTable("Servidores"); // Nome da tabela
                entity.HasKey(e => e.ID); // Chave primária

                // Mapeamento das propriedades
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EnderecoIP)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PortaIP)
                    .IsRequired();

                // Relacionamento 1:N Servidor -> Videos
                entity.HasMany(s => s.Videos)
                    .WithOne(v => v.Servidor)
                    .HasForeignKey(v => v.ServidorID)
                    .OnDelete(DeleteBehavior.Restrict); // Define o comportamento de exclusão
            });

            // Mapeamento da entidade Video
            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("Videos"); // Nome da tabela
                entity.HasKey(e => e.ID); // Chave primária

                // Mapeamento das propriedades
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(500);

                // Relacionamento N:1 Videos -> Servidor
                entity.HasOne(v => v.Servidor)
                    .WithMany(s => s.Videos)
                    .HasForeignKey(v => v.ServidorID)
                    .OnDelete(DeleteBehavior.Cascade); // Define o comportamento de exclusão
            });
        }

    }
}
