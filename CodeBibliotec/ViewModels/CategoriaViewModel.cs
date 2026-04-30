using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace CodeBibliotec.ViewModels
{
    public class CategoriaViewModel
    {
        //o nome não pode ser nulo, tem que ser preenchido, por isso o required
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da categoria não pode conter mais de 50 caracteres.")]
        public string Nome { get; set; }

       

        //lista de livros, é o inverso do livro, onde o livro tem uma lista de categorias, aqui a categoria tem uma lista de livros
    }
}
