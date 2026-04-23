using System.ComponentModel.DataAnnotations;

namespace CodeBibliotec.ViewModels
{
    public class LivroViewModel
    {        //forma resumida de representar as domains:
        //Required:referente ao "ComponentModel.DataAnnotations;" no topo do nosso arquivo

        //*se não colocar titulo dá erro *se execeder quantidade de caracteres dá erro
        //eles são somente para titulo, um required fica em cima doq quer aplicar
        [Required(ErrorMessage = "O titulo é obrigatório")]
        [StringLength(150, ErrorMessage = "O titulo não pode execeder 150 caracteres")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "O autor é obrigatório")]
        [StringLength(100, ErrorMessage = "O autor não pode execeder 150 caracteres")]
        public string Autor { get; set; }



        //Range: entre
        //para permitir livros somente entre esses anos de publicação
        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(0, 2026, ErrorMessage = "O ano deve ser válido")]
        public int anoPublicacao { get; set; }


        [StringLength(20, ErrorMessage = "O status não pode execeder 20 caracteres")]
        public string Status { get; set; }


        //?: pode ser nulo
        //List<int>?: lista de int's
        public List<int>? CategoriaIds { get; set; } = new List<int>();
    }
}
