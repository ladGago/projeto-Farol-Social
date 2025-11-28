using Microsoft.EntityFrameworkCore;
using projeto.Models;

namespace projeto.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }
        public DbSet<ProjetoModel>Projetos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}
