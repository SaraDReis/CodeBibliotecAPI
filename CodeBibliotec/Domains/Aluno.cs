using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CodeBibliotec.Domains;

[Table("Aluno")]
[Index("Matricula", Name = "UQ__Aluno__30962D15C110488E", IsUnique = true)]
public partial class Aluno
{
    [Key]
    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column("matricula")]
    [StringLength(20)]
    [Unicode(false)]
    public string Matricula { get; set; } = null!;

    [InverseProperty("IdAlunoNavigation")]

    //aluno possui/faz avaliações
    //é um atributo dentro de uma coleçao:
    //ICollection<Avaliacao>
    //eu posso colcoar varias coisas dentro de uma coleção, um aluno pode fazer varias avaliações, *o aluno pode possuir uma coleção de favoritos
    public virtual ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();

    [InverseProperty("IdAlunoNavigation")]
    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Aluno")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdAlunoNavigation")]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
