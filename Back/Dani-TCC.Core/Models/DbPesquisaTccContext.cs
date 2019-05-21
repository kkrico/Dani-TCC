using Microsoft.EntityFrameworkCore;

namespace Dani_TCC.Core.Models
{
    public partial class DbPesquisaTccContext : DbContext
    {
        public DbPesquisaTccContext()
        {
        }

        public DbPesquisaTccContext(DbContextOptions<DbPesquisaTccContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<Pesquisa> Pesquisa { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Questao> Questao { get; set; }
        public virtual DbSet<Resposta> Resposta { get; set; }
        public virtual DbSet<Valoresresposta> Valoresresposta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=DB_PESQUISA_TCC");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Foto>(entity =>
            {
                entity.HasKey(e => e.Idfoto);

                entity.ToTable("FOTO", "DB_PESQUISA_TCC");

                entity.Property(e => e.Idfoto)
                    .HasColumnName("IDFOTO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Eleito)
                    .HasColumnName("ELEITO")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Hashfoto)
                    .IsRequired()
                    .HasColumnName("HASHFOTO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idetnia)
                    .HasColumnName("IDETNIA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idgenero)
                    .HasColumnName("IDGENERO")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Pesquisa>(entity =>
            {
                entity.HasKey(e => e.Idpesquisa);

                entity.ToTable("PESQUISA", "DB_PESQUISA_TCC");

                entity.HasIndex(e => e.Idpessoa)
                    .HasName("FK_RELATIONSHIP_1");

                entity.Property(e => e.Idpesquisa)
                    .HasColumnName("IDPESQUISA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Horafimpreenchimento).HasColumnName("HORAFIMPREENCHIMENTO");

                entity.Property(e => e.Horainiciopreenchimento).HasColumnName("HORAINICIOPREENCHIMENTO");

                entity.Property(e => e.Idpessoa)
                    .HasColumnName("IDPESSOA")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdpessoaNavigation)
                    .WithMany(p => p.Pesquisa)
                    .HasForeignKey(d => d.Idpessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_1");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.Idpessoa);

                entity.ToTable("PESSOA", "DB_PESQUISA_TCC");

                entity.Property(e => e.Idpessoa)
                    .HasColumnName("IDPESSOA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Emailpessoa)
                    .HasColumnName("EMAILPESSOA")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Idetnia)
                    .HasColumnName("IDETNIA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idfaixaetaria)
                    .HasColumnName("IDFAIXAETARIA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idgenero)
                    .HasColumnName("IDGENERO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idrendafamilia)
                    .HasColumnName("IDRENDAFAMILIA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idsexualidade)
                    .HasColumnName("IDSEXUALIDADE")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Questao>(entity =>
            {
                entity.HasKey(e => e.Idquestao);

                entity.ToTable("QUESTAO", "DB_PESQUISA_TCC");

                entity.Property(e => e.Idquestao)
                    .HasColumnName("IDQUESTAO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descricaoquestao)
                    .HasColumnName("DESCRICAOQUESTAO")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Resposta>(entity =>
            {
                entity.HasKey(e => e.Idresposta);

                entity.ToTable("RESPOSTA", "DB_PESQUISA_TCC");

                entity.HasIndex(e => e.Idpesquisa)
                    .HasName("FK_RELATIONSHIP_2");

                entity.HasIndex(e => e.Idquestao)
                    .HasName("FK_RELATIONSHIP_3");

                entity.Property(e => e.Idresposta)
                    .HasColumnName("IDRESPOSTA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idpesquisa)
                    .HasColumnName("IDPESQUISA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idquestao)
                    .HasColumnName("IDQUESTAO")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdpesquisaNavigation)
                    .WithMany(p => p.Resposta)
                    .HasForeignKey(d => d.Idpesquisa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_2");

                entity.HasOne(d => d.IdquestaoNavigation)
                    .WithMany(p => p.Resposta)
                    .HasForeignKey(d => d.Idquestao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_3");
            });

            modelBuilder.Entity<Valoresresposta>(entity =>
            {
                entity.HasKey(e => e.Idvalorresposta);

                entity.ToTable("VALORESRESPOSTA", "DB_PESQUISA_TCC");

                entity.HasIndex(e => e.Idfoto)
                    .HasName("FK_RELATIONSHIP_5");

                entity.HasIndex(e => e.Idresposta)
                    .HasName("FK_RELATIONSHIP_4");

                entity.Property(e => e.Idvalorresposta)
                    .HasColumnName("IDVALORRESPOSTA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Foiselecionada)
                    .HasColumnName("FOISELECIONADA")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Idfoto)
                    .HasColumnName("IDFOTO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idresposta)
                    .HasColumnName("IDRESPOSTA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Temposelecao).HasColumnName("TEMPOSELECAO");

                entity.HasOne(d => d.IdfotoNavigation)
                    .WithMany(p => p.Valoresresposta)
                    .HasForeignKey(d => d.Idfoto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_5");

                entity.HasOne(d => d.IdrespostaNavigation)
                    .WithMany(p => p.Valoresresposta)
                    .HasForeignKey(d => d.Idresposta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_4");
            });
        }
    }
}
