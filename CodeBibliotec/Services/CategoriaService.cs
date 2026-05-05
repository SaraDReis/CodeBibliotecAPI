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








        public async Task<CategoriaResponseDto> CadastrarCategoriaAsync(CategoriaViewModel categoriaViewModel)
        {
            var categoria = new Categorium
            {
                Nome = categoriaViewModel.Nome
            };


            var response = await  _categoriaRepository.CadastrarCategoriaAsync(categoria);
            return MapToCategoriaResponseDto(response);
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
            //**Select só funciona se ICatagoria tiver uma List


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
                Nome = categoria.Nome,
                IdLivros = categoria.IdLivros?.Select(l => l.Titulo).ToList()



            };
        }

    }
}
