using CodeBibliotec.ViewModels;

namespace CodeBibliotec.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaResponseDto> CadastrarCategoriaAsync(CategoriaViewModel categoriaViewModel);

        Task<List<CategoriaResponseDto>> ObterTodasAsCategoriasAsync();

        Task<CategoriaResponseDto> ObterCategoriaPorIdAsync(int id);


    }
}
