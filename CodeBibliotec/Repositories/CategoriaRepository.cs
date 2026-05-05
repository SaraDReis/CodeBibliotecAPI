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

        public async Task<Categorium> CadastrarCategoriaAsync(Categorium categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();

            return categoria;
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

        
    }
}
