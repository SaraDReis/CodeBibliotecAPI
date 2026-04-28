using CodeBibliotec.Domains;
using CodeBibliotec.Interfaces;
using CodeBibliotec.ViewModels;

namespace CodeBibliotec.Services
{
    //herança da interface "ILivroService"
    //implemente a interface (ctlr + . + ILivroService + iimplementar interface)
    //
    public class LivroService : ILivroService
    {
        //vamos criar um construtor para acessar a camada mais interna, nesse caso a de repositorio
        private readonly ILivroRepository _livroRepository;

        //construtor criado a partir de _livroRepository:
        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }



        public async Task<bool> AtualizarLivrosAsync(int id, LivroViewModel livroViewModel)
        {
            var livro = new Livro
            {
                Id = id,
                Titulo = livroViewModel.Titulo,
                Autor = livroViewModel.Autor,
                AnoPublicacao = livroViewModel.anoPublicacao,

                Status = string.IsNullOrWhiteSpace(livroViewModel.Status)
                  //if ternário
                  ? "Disponível"
                  : livroViewModel.Status.Trim()
            };
            if(livroViewModel.CategoriaIds != null)
            {
                livro.IdCategoria = livroViewModel.CategoriaIds
                    .Select(id => new Categorium { Id = id, Nome = string.Empty }).ToList(); //Nome = string.Empty: nome é uma palavra e está vazio
            }
            return await _livroRepository.AtualizarLivrosAsync(id, livro);
        }

        public async Task<LivroResponseDto> CadastrarLivrosAsync(LivroViewModel livroViewModel)
        {
            var livro = new Livro
            {
                Titulo = livroViewModel.Titulo,
                Autor = livroViewModel.Autor,
                AnoPublicacao = livroViewModel.anoPublicacao,

                Status = string.IsNullOrWhiteSpace(livroViewModel.Status)

              //if ternário
              ? "Disponível"
              : livroViewModel.Status.Trim() //Trim: remove espaços em branco antes e depois da string
            };

            //verificar se ID's existem
            if (livroViewModel.CategoriaIds != null && livroViewModel.CategoriaIds.Any()) //se as categorias ID's é diferente de nulo e se elas tem algo
            {
                livro.IdCategoria = livroViewModel.CategoriaIds
                    .Select(id => new Categorium { Id = id, Nome = string.Empty }).ToList();

            }

            var response = await _livroRepository.CadastrarLivroAsyc(livro);

            return MapToLivroResponseDto(response);
        }

        public Task<bool> DeletarLivroAsync(int id)
        {
            throw new NotImplementedException();
        }





        public async Task<LivroResponseDto> ObterLivroPorIdAsync(int id)
        {
            //oq tem aqui só funciona se tiver a parte desse metodo em repository
            var livro = await _livroRepository.ObterLivroPorIdAsync(id);
            return MapToLivroResponseDto(livro);
        }






        public async Task<List<LivroResponseDto>> ObterTodosLivrosAsync()
        {
           
        var livros = await _livroRepository.ObterTodosLivrosAsync();
            //o metodo do repositorio mapeia os livros e retorna-os
            return livros.Select(MapToLivroResponseDto).ToList();
            //map: mapeia os livros
        }




        //função auxiliar
        //dentro dela estamos mapeando informações e retornando da forma que o usuario ira entender
        //ex: id = nome
        //o user n sabe qual id representa tal coisa
        private LivroResponseDto MapToLivroResponseDto(Livro livro)
        {
            //Livro(maiusculo): das domains(é o mais completo, tem todas as informações e relações vindas do banco de dados)
            //LivroResponseDto: livro resumido
            if (livro == null) //verificando se o livro é nulo
            {
                return null!; //se é nulo, retonar nulo
            }
            return new LivroResponseDto
            {
                //colocar os parametros do livroresponse para pegar as propriedades
                //livro(minusculo): objeto livro
                Id = livro.Id, //propriedade Id, vindo de livro
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicacao = livro.AnoPublicacao,
                Status = livro.Status,

                //para cada id categoria se usa o nome, não o id:
                //Select(c => new CategoriaNomeDto { Nome = c.Nome }): para cada id retorne o nome
                //.ToList: para cada um adicione em uma lista
                idCategoria = livro.IdCategoria.Select(c => new CategoriaNomeDto { Nome = c.Nome }).ToList()
            };

        }

    }
}
