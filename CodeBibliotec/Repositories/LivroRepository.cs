using CodeBibliotec.Context;
using CodeBibliotec.Domains;
using CodeBibliotec.Interfaces;
using Microsoft.EntityFrameworkCore;

//essa é um camada de repostorio, você precisa saber quais camadas se relacionam diretamente com ele
//no nosso caso, a de contexto
//inserir dependencia, visto que dependendemos da camada de contexto
//metodo construtor: "é necessario para linkar as camadas"
namespace CodeBibliotec.Repositories
{

    //na classe herdada implemente a interface(ctlr + . em cima de "ILivroRepository"
    //implemente a interface que ira aparecer todos os metodos *as tasks, mas ainda não funcionaram, não estaram implementados de fato)
    public class LivroRepository : ILivroRepository
    {
        //classe privada
        //readonly: somente para leitura
        //BibliotecContext: esse é o tipo dela
        //_context: usa _ pois é privado, é uma convenção
        //_context: é praticamente a representação do nosso banco de dados(veja oq está na classe de contexto e vc entendera)
        //vc ira conseguir se comunicar com a camada de contetxo apartir desse objeto
        //usamos a de contexto pois é a mais interna(ignorandoa  domain)

        //_context + ctlr + . + criar metodo contrutor
        private readonly BibliotecContext _context;

        //esse é o metodo construtor(ele tem o mesmo nome do de cima"BibliotecContext")
        //*metodo construtor para injetar a camada de contexto no repository
        public LivroRepository(BibliotecContext context)
        {
            _context = context;
        }

        //metodos implementados
        //para implementar voce tera que criar um por um
        public Task<bool> AtualizarLivrosAsync(int id, Livro livro)
        {
            throw new NotImplementedException();
        }






        public async Task<Livro> CadastrarLivroAsyc(Livro Livro)
        {
            if (Livro.IdCategoria != null && Livro.IdCategoria.Any()) //o meu id categoria é diferente de nulo? e/&& se os IDs existem
                                                                      //.Any()
            {
                var categoriaIds = Livro.IdCategoria.Select(c => c.Id).ToList();
                Livro.IdCategoria = await _context.Categoria.Where(c => categoriaIds.Contains(c.Id)).ToListAsync();
                //Where(): pega categorias que correspondem a tal regra
                //Contains: somente as que contem no banco
                //ToListAsync(): listar apos a regra
            }

            _context.Livros.Add(Livro); //adicionar a tabela de livros(apenas se if der certo)
                await _context.SaveChangesAsync(); //salva para que fique no banco

                return Livro; //esse metodo retorna um livro
        }







        public Task<bool> DeletarLivroAsync(int id)
        {
            throw new NotImplementedException();
        }







        public async Task<Livro> ObterLivroPorIdAsync(int id)
        {
            //acessando livro atraves de context
            //include funciona como um join
            return await _context.Livros.Include(l => l.IdCategoria).FirstOrDefaultAsync(l => l.Id == id);
            //indo no contexto de livro, vendo as categorias correspondentes e pega o primeiro livro que corresponde ao filtro(l.Id == id)

        }







        public async Task<List<Livro>> ObterTodosLivrosAsync()
        {
            //preciso acessar os livros no banco de dados e retonar,
            //já temos a injeção de contexto: _context(atraves dese objeto)

            //context.Livros: entidade livro vindo de context que conecta ao bando de dados
            //.include: inclui a coleçao de categorias(l => l.IdCategoria) que esta relacionada ao livro
            //.ToListAsync(); para inserir em uma lista de forma asincrona

            //*ao usar o await se adiciona o async na classe: public async
            //toda vez que há comunicação com banco de dados se usa um metodo asincrono
            //*as coisas não são instantaneas, não sabemos quanto demora as coisas,
            //se não usar async é capaz de gerar erro, executa NULO se não der tempo de trazer a resposta!
            return await _context.Livros
                .Include(l => l.IdCategoria)
                .ToListAsync();
        }



    }
}
