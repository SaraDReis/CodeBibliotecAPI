using CodeBibliotec.Domains;
using CodeBibliotec.Interfaces;
using CodeBibliotec.ViewModels;
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

            }
            catch (Exception ex) //o erro é um objeto chamado exception
            {
                //(500, new { mensagem = "Erro ao listar livros" }): cria objeto com propriedade mensagem
                return StatusCode(500, new { mensagem = "Erro ao listar livros", erro = ex.Message }); //ex: Exception
                                                                                                       //de acordo com o erro, ele passa uma mensagem ao usuario
            }
        }

        //outro metodo
        //o de busca especiifca por id(por isso no get temos o {id})
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterLivroPorId(int id)
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



        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarLivro(LivroViewModel livroViewModel)
        {
            if (!ModelState.IsValid) //valida a parte do corpo da requsição/se model state NÃO(!) for valido
            {
                return BadRequest(ModelState);
            }

            try
            {
                var livro = await _livroService.CadastrarLivrosAsync(livroViewModel);

                return CreatedAtAction(nameof(ObterLivroPorId), new { id = livro.Id }, livro); //retorna o livro criado, com o id e o status code 201 (Created)

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message }); //se der erro de argumento, retorna bad request com a mensagem do erro

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao cadastrar o livro", erro = ex.Message });

            }
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> AtualizarLivro(int id, LivroViewModel livroViewModel)
        {
            if (!ModelState.IsValid) //verificar se o corpo da função é valido
                return BadRequest(ModelState);

            try
            {
                //sempre que você tem um método que retorna algo, você pode criar uma variável para receber o retorno, mesmo que você não vá usar depois.
                var resultado = await _livroService.AtualizarLivrosAsync(id, livroViewModel); //*lembrando que o atualizarLivrosAsync retorna um booleano

                if (!resultado)  //se o resultado não for valido(não houver um resultado)
                    return NotFound(new {mensagem = "Livro não encontrado"});

                return Ok(new { mensagem = "Livro atualizado com sucesso" });
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message }); //lembrando que ex. é um objeto da classe argument exception que tem a propriedade "Message"

            }catch(Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao atualizar livro", erro = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarLivro(int id)
        {
            try
            {
                var resultado = await _livroService.DeletarLivroAsync(id);
                if (!resultado) //se o resultado for falso(visto que esperamos que retorne um verdadeiro)
                                //**não pode colocar resultado = null, pois o DeletarLivroAsync retorna um boleano
                                //**você pode colcoar resultado != true, pois diferente de verdaderio é igual a false, um valor válido para uma boleana
                    return NotFound(new { mensagem = "Livro não foi encontrado" }); //se não houver reultado, aparece isto

                return Ok(new { mensagem = "Livro deletado com sucesso" }); //se der certo

            }catch(Exception ex) //qualquer exeção
            {
                return StatusCode(500, new { mensagem = "Erro ao deletar livro", erro = ex.Message });
            }





        }




    }
}
