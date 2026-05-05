using CodeBibliotec.Context;
using CodeBibliotec.Domains;
using CodeBibliotec.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeBibliotec.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly BibliotecContext _context;

        public CategoriaRepository(BibliotecContext context)
        {
            _context = context;
        }

        public Task<Categorium> CadastrarCategoriaAsync(Categorium categoria)
        {
            throw new NotImplementedException();
        }




        public async Task<Categorium> ObterCategoriaPorIdAsync(int id)
        {
           return await _context.Categoria.FirstOrDefaultAsync(c => c.Id == id);

            //o "include" não é incluido no obter categoria pois categoria é independente, o include relaciona tabelas, como um join,
            //vsito que não precisamos relacionar a um livro quando buscamos por uma categoria.
        }




        public async Task<List<Categorium>> ObterTodasAsCategoriasAsync()
        {
            return await _context.Categoria.ToListAsync();

        }

        Task<Categorium> ICategoriaRepository.ObterTodasAsCategoriasAsync()
        {
            throw new NotImplementedException();
        }
    }
}
