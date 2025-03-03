﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace Dani_TCC.Core.Models
{
    public partial class DB_PESQUISA_TCCContext : DbContext
    {
        private static readonly LoggerFactory MyLoggerFactory = 
            new LoggerFactory(new[] { 
                new DebugLoggerProvider() 
            });
        
        public DB_PESQUISA_TCCContext(DbContextOptions<DB_PESQUISA_TCCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Survey> Survey { get; set; }
        public virtual DbSet<Valueanswer> Valueanswer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.Idanswer);

                entity.ToTable("ANSWER", "pesquisapsicologia");

                entity.HasIndex(e => e.Idquestion)
                    .HasName("FK_RELATIONSHIP_3");

                entity.HasIndex(e => e.Idsurvey)
                    .HasName("FK_RELATIONSHIP_2");

                entity.Property(e => e.Idanswer)
                    .HasColumnName("IDANSWER")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idquestion)
                    .HasColumnName("IDQUESTION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idsurvey)
                    .HasColumnName("IDSURVEY")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdquestionNavigation)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.Idquestion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_3");

                entity.HasOne(d => d.IdsurveyNavigation)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.Idsurvey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_2");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Idperson);

                entity.ToTable("PERSON", "pesquisapsicologia");

                entity.Property(e => e.Idperson)
                    .HasColumnName("IDPERSON")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idagegroup)
                    .HasColumnName("IDAGEGROUP")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idethnicity)
                    .HasColumnName("IDETHNICITY")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idfamilyincome)
                    .HasColumnName("IDFAMILYINCOME")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idgender)
                    .HasColumnName("IDGENDER")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idsexuality)
                    .HasColumnName("IDSEXUALITY")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.Idphoto);

                entity.ToTable("PHOTO", "pesquisapsicologia");

                entity.Property(e => e.Idphoto)
                    .HasColumnName("IDPHOTO")
                    .HasColumnType("int(11)");


                entity.Property(e => e.FileContents)
                    .HasColumnName("FILECONTENTS")
                    .HasColumnType("blob");

                entity.Property(e => e.Elected)
                    .HasColumnName("ELECTED")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Idethnicity)
                    .HasColumnName("IDETHNICITY")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idgender)
                    .HasColumnName("IDGENDER")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Photohash)
                    .IsRequired()
                    .HasColumnName("PHOTOHASH")
                    .HasMaxLength(5000)
                    .IsUnicode(false);
                
                entity.Property(e => e.PhotoName)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Idquestion);

                entity.ToTable("QUESTION", "pesquisapsicologia");

                entity.Property(e => e.Idquestion)
                    .HasColumnName("IDQUESTION")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Questiondescription)
                    .HasColumnName("QUESTIONDESCRIPTION")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(e => e.Idsurvey);

                entity.ToTable("SURVEY", "pesquisapsicologia");

                entity.HasIndex(e => e.Idperson)
                    .HasName("FK_RELATIONSHIP_1");

                entity.Property(e => e.Idsurvey)
                    .HasColumnName("IDSURVEY")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Finalfilldate).HasColumnName("FINALFILLDATE");

                entity.Property(e => e.Idperson)
                    .HasColumnName("IDPERSON")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Initialfilldate).HasColumnName("INITIALFILLDATE");

                entity.HasOne(d => d.IdpersonNavigation)
                    .WithMany(p => p.Survey)
                    .HasForeignKey(d => d.Idperson)
                    .HasConstraintName("FK_RELATIONSHIP_1");
            });

            modelBuilder.Entity<Valueanswer>(entity =>
            {
                entity.HasKey(e => e.Idvalueanswer);

                entity.ToTable("VALUEANSWER", "pesquisapsicologia");

                entity.HasIndex(e => e.Idanswer)
                    .HasName("FK_RELATIONSHIP_4");

                entity.HasIndex(e => e.Idphoto)
                    .HasName("FK_RELATIONSHIP_5");

                entity.Property(e => e.Idvalueanswer)
                    .HasColumnName("IDVALUEANSWER")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Haschoosen)
                    .HasColumnName("HASCHOOSEN")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Idanswer)
                    .HasColumnName("IDANSWER")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idphoto)
                    .HasColumnName("IDPHOTO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Selectiontime).HasColumnName("SELECTIONTIME");

                entity.HasOne(d => d.IdanswerNavigation)
                    .WithMany(p => p.Valueanswer)
                    .HasForeignKey(d => d.Idanswer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_4");

                entity.HasOne(d => d.IdphotoNavigation)
                    .WithMany(p => p.Valueanswer)
                    .HasForeignKey(d => d.Idphoto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RELATIONSHIP_5");
            });
        }
    }
}