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
        public DbSet<Prova> Provas { get; set; }
        public DbSet<Alternativas> Alternativas { get; set; }
        public DbSet<Vagas> Vagas { get; set; }
        public DbSet<Candidaturas> Candidaturas { get; set; }
        public DbSet<Certificados> Certificados { get; set; }
        public DbSet<Notas> Notas { get; set; }
        public DbSet<Progress> Progress { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<UseProva> UseProva { get; set; }
        public DbSet<UseTreinamentos> UseTreinamentos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        public DbSet<Licencas> Licencas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Garantir que todas as propriedades de chave primária inteiras nomeadas "id" ou "Id"
            // sejam geradas pelo banco ao inserir (auto-increment / ValueGeneratedOnAdd).
            // Isso evita depender somente de atributos nas classes e padroniza o comportamento.
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var idProp = entityType.FindProperty("id") ?? entityType.FindProperty("Id");
                if (idProp != null && idProp.ClrType == typeof(int))
                {
                    idProp.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                }
            }

            // Configura explicitamente a relação entre Licencas e Usuario usando a FK usuario_id
            modelBuilder.Entity<Licencas>(entity =>
            {
                entity.ToTable("Licencas");

                entity.HasKey(e => e.id);

                entity.Property(e => e.token).HasColumnName("token").HasMaxLength(200).IsRequired();
                entity.Property(e => e.criado_em).HasColumnName("criado_em");
                entity.Property(e => e.validade_em).HasColumnName("validade_em");
                entity.Property(e => e.ativo).HasColumnName("ativo");
                entity.Property(e => e.preco).HasColumnName("preco");

                entity.HasOne(e => e.usuario)
                      .WithMany(u => u.licencas)
                      .HasForeignKey(e => e.usuario_id)
                      .HasConstraintName("FK_Licencas_Usuarios_usuario_id")
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
