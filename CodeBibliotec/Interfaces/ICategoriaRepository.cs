using CodeBibliotec.Domains;

namespace CodeBibliotec.Interfaces
{
    public interface ICategoriaRepository
    {

        Task<Categorium> CadastrarCategoriaAsync(Categorium categoria);

        Task<Categorium> ObterCategoriaPorIdAsync(int id);

        Task<List<Categorium>> ObterTodasAsCategoriasAsync();
    }
}
