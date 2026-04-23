namespace CodeBibliotec.ViewModels
{
    public class LivroResponseDto
    {
        public int Id { get; set; }
        //coloca interrogação?
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnoPublicacao { get; set; }
        public string Status { get; set; }


        //trazer os nomes das categorias associadas a um livro:
        public List<CategoriaNomeDto> idCategoria { get; set; } = new List<CategoriaNomeDto>();
    }
}
