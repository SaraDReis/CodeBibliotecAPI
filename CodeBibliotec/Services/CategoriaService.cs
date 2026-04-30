using CodeBibliotec.Domains;
using CodeBibliotec.Interfaces;
using CodeBibliotec.ViewModels;

namespace CodeBibliotec.Services
{
    public class CategoriaService : ICategoriaService
    {
      private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }








        public Task<CategoriaResponseDto> CadastrarCategoriaAsync(CategoriaViewModel categoriaViewModel)
        {
            throw new NotImplementedException();
        }






        public async Task<CategoriaResponseDto> ObterCategoriaPorIdAsync(int id)
        {
         var categoria = await _categoriaRepository.ObterCategoriaPorIdAsync(id);
            return MapToCategoriaResponseDto(categoria);
        }






        public async Task<List<CategoriaResponseDto>> ObterTodasAsCategoriasAsync()
        {
            
            var categorias = await _categoriaRepository.ObterTodasAsCategoriasAsync();

            return categorias.Select(MapToCategoriaResponseDto).ToList();


        }









        private CategoriaResponseDto MapToCategoriaResponseDto(Categorium categoria)
        {
            if (categoria == null)
            {
                return null!;
            }
            return new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

    }
}
