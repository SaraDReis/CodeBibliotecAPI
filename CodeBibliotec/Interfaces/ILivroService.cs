using CodeBibliotec.Domains;
using CodeBibliotec.ViewModels;

namespace CodeBibliotec.Interfaces
{
    //referente a camada service
    public interface ILivroService
    {
        //definindo metodos 
        //<Livro> vindo de domains(se precisar importar use ctrl + .)

        //LivroViewModel:livro resumido
        //livroViewModel: aqui vira um livro completo
        Task<LivroResponseDto> CadastrarLivrosAsync(LivroViewModel livroViewModel);



        Task<LivroResponseDto> ObterLivroPorIdAsync(int id);

        Task<List<LivroResponseDto>> ObterTodosLivrosAsync();

        Task<bool> AtualizarLivrosAsync(int id, LivroViewModel livroViewModel);

        Task<bool> DeletarLivroAsync(int id);
    }
}
