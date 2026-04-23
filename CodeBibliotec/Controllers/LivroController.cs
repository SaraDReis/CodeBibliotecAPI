using CodeBibliotec.Domains;
using CodeBibliotec.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//a controller lista os endpoints, ou seja, swagger lista as controllers, os endpoints

//dentro da nossa controler vamos conectar a camada mais profunda a de serviços
namespace CodeBibliotec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        //aqui nos injetamos a camada de serviço
        private readonly ILivroService _livroService;

        //com o construtor feito a nossa camada já consegue conversar com a 
        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }
    //operação de leitura(do banco)
        [HttpGet]
        //todas(ou quase) as controllers usam essa <IActionResult>
        //ListarTodosLivros: nome do metodo que vai aparecer no swagger
        
        public async Task<IActionResult> ListarTodosLivros()
        {
            //primeiro endpoint da API
            //try catch: tenta fazer algo se der erro cai no catch
            try
            {
                //isso só funciona pq a camada de serviço foi injetada aqui
                //retorna lista de livros response dto e deixa em livros
                //await usado para dar resultado apenas quando o banco tiver comunicado, sem ele o banco não tem tempo de exibir o resultado
                var livros = await _livroService.ObterTodosLivrosAsync();
                //retorna ao usuario Ok(ok:é um metodo) status code: 200, ou seja, sucesso
                return Ok(livros);
                //caso não der certo cai no catch

            }catch (Exception ex) //o erro é um objeto chamado exception
            {
                //(500, new { mensagem = "Erro ao listar livros" }): cria objeto com propriedade mensagem
                return StatusCode(500, new { mensagem = "Erro ao listar livros", erro = ex.Message }); //ex: Exception
                    //de acordo com o erro, ele passa uma mensagem ao usuario
            }
        }

        //outro metodo
        //o de busca especiifca por id(por isso no get temos o {id})
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterLivroPoriD(int id)
        {
            try
            {
                var livro = await _livroService.ObterLivroPorIdAsync(id);
                if (livro == null)
                    //no IF quando só há UMA linha(como aqui), você pode excluir as chaves, mas só se tiver UMA linha
                    //aparentemnete esses returns são coisas diferentes, por isso não se anulam
                
                    return NotFound(new { mensagem = "Livro não encontrado" });

                return Ok(livro);
                

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao obter o livro", erro = ex.Message });

            }
        }



    }
}
