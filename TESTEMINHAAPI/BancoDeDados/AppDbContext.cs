using Microsoft.EntityFrameworkCore;
using TESTEMINHAAPI.Models;

//Arquivo resposavel por criar a conexão com o banco de dados, e criar as tabelas a partir dos modelos criados.
//
//Ele é necessário para o funcionamento do Entity Framework Core, que é a biblioteca utilizada para acessar o banco de dados.
//
//Ele é utilizado para criar a conexão com o banco de dados, e para criar as tabelas a partir dos modelos criados.
//
//Ele é necessário para o funcionamento do Entity Framework Core, que é a biblioteca utilizada para acessar o banco de dados.
namespace TESTEMINHAAPI.BancoDeDados
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Treinamentos> Treinamentos { get; set; }
        public DbSet<Modulos> Modulos { get; set; }

        public DbSet<Aulas> Aulas { get; set; }
        public DbSet<Questoes> Questoes { get; set; }
        public DbSet<Midias> Midias { get; set; }
        public DbSet<Alternativas> Alternativas { get; set; }
        public DbSet<Vagas> Vagas { get; set; }
        public DbSet<Candidaturas> Candidaturas { get; set; }
        public DbSet<Certificados> Certificados { get; set; }
        public DbSet<Notas> Notas { get; set; }
        public DbSet<Progress> Progress { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<UseProva> UseProva { get; set; }
        public DbSet<UseTreinamentos> UseTreinamentos { get; set; }

        public DbSet<UsuarioTreinamento> UsuarioTreinamentos { get; set; }
    }
}
