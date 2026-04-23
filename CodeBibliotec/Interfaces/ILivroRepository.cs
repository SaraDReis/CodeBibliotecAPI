using CodeBibliotec.Domains;
//o que tem que ser feito(NÃO como)
namespace CodeBibliotec.Interfaces
{
    //Especifica para a camada de repositorio(Camadas de estoque:Repositories: se comunica com quem acessa o banco
    //*é responsável pelo acesso de informações
    //*é como uma dispensa)

    public interface ILivroRepository
    {
        //operações CRUD
        //Cadastro:

        //CadastrarLivroAsyc: função/metodo
        //Livro: vindo da domain, Livro: parametro, cada um é algo diferente, apenas foram nomeados da mesma maneira
        //Task(aqui):Retorna algo que é asyncrono, recebendo um objeto livro e devolvendo depois de cadastrar
        Task<Livro> CadastrarLivroAsyc(Livro Livro);

        //obter livro por id
        //passa id do tipo int(codigo)
        //acha o livro correspondente ao codigo
        //devolve ele 
            Task<Livro> ObterLivroPorIdAsync(int id);

        //<List<Livro>> : lista de livros
        //ele é algo que só executa
        //devolve tudo(lista de livros)
        Task<List<Livro>> ObterTodosLivrosAsync();


        //atualiza livro
        //ele retorna um boleano se deu ou não certo a atualização
        //*operação de escrita no banco, voce localiza o item existente e substituir as informações
        Task<bool> AtualizarLivrosAsync(int id, Livro livro);


        //apaga livro usando id
        //retorna boleana se deu ou não certo
        //procura e faz a operação no banco
        Task<bool> DeletarLivroAsync(int id);
    }
}
