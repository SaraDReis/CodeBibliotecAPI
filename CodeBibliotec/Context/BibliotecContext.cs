using System;
using System.Collections.Generic;
using CodeBibliotec.Domains;
using Microsoft.EntityFrameworkCore;

namespace CodeBibliotec.Context;

//Apartir do scaffold nos criamos essa context, dentro dela nos temos todos os itens do nosso banco de dados
public partial class BibliotecContext : DbContext // ":" HERANÇA
{
    public BibliotecContext()
    {
    }

    public BibliotecContext(DbContextOptions<BibliotecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<Avaliacao> Avaliacaos { get; set; }

    //ibliotecarium foi uma tentativa do programa criar o nome bibliotecaria no plural, *não mexa no nome para causar problemas de mapeamento
    public virtual DbSet<Bibliotecarium> Bibliotecaria { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Livro> Livros { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //tabela criada via codigo
        modelBuilder.Entity<Aluno>(entity =>
        {
            //entidade criada via codigo:
            //entidade.possuichave(entidade é usuario).possuinome(referenteAEstaPrimaryKey)
            entity.HasKey(e => e.IdUsuario).HasName("PK__Aluno__645723A6370C3F2E");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Aluno).HasConstraintName("FK__Aluno__idUsuario__60A75C0F");
        });

        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Avaliaca__3213E83F8A3491D3");
            //withMany:pode ter varias(*questão de cardinalidade "1 to many/ (1,N)")
            entity.HasOne(d => d.IdAlunoNavigation).WithMany(p => p.Avaliacaos).HasConstraintName("FK__Avaliacao__idAlu__778AC167");

            entity.HasOne(d => d.IdLivroNavigation).WithMany(p => p.Avaliacaos).HasConstraintName("FK__Avaliacao__idLiv__787EE5A0");
        });

        modelBuilder.Entity<Bibliotecarium>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Bibliote__645723A62C367D97");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Bibliotecarium).HasConstraintName("FK__Bibliotec__idUsu__6477ECF3");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3213E83F134D086A");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorito__3213E83F98F7A68B");

            entity.HasOne(d => d.IdAlunoNavigation).WithMany(p => p.Favoritos).HasConstraintName("FK__Favorito__idAlun__7C4F7684");

            entity.HasOne(d => d.IdLivroNavigation).WithMany(p => p.Favoritos).HasConstraintName("FK__Favorito__idLivr__7D439ABD");
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Livro__3213E83FA463A7C8");

            entity.HasMany(d => d.IdCategoria).WithMany(p => p.IdLivros)
                .UsingEntity<Dictionary<string, object>>(
                    "LivroCategorium",
                    r => r.HasOne<Categorium>().WithMany()
                        .HasForeignKey("IdCategoria")
                        .HasConstraintName("FK__LivroCate__idCat__6E01572D"),
                    l => l.HasOne<Livro>().WithMany()
                        .HasForeignKey("IdLivro")
                        .HasConstraintName("FK__LivroCate__idLiv__6D0D32F4"),
                    j =>
                    {
                        j.HasKey("IdLivro", "IdCategoria").HasName("PK__LivroCat__BB669497D8CAA3C9");
                        j.ToTable("LivroCategoria");
                        j.IndexerProperty<int>("IdLivro").HasColumnName("idLivro");
                        j.IndexerProperty<int>("IdCategoria").HasColumnName("idCategoria");
                    });
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserva__3213E83FEB7ACE7D");

            entity.HasOne(d => d.IdAlunoNavigation).WithMany(p => p.Reservas).HasConstraintName("FK__Reserva__idAluno__71D1E811");

            entity.HasOne(d => d.IdLivroNavigation).WithMany(p => p.Reservas).HasConstraintName("FK__Reserva__idLivro__72C60C4A");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83F043BDBAB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
