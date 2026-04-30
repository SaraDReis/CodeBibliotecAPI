namespace CodeBibliotec.ViewModels
{
    public class CategoriaResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<String> IdLivros { get; set; } = new List<string>();
    }
}
