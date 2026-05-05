using CodeBibliotec.Domains;
using CodeBibliotec.Interfaces;
using CodeBibliotec.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeBibliotec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        
        [HttpGet("{id}", Name = "ObterCategoriaPorId")]
        public async Task<IActionResult> ObterCategoriaPorIdAsync(int id)
        {
            try
            {
                var categoria = await _categoriaService.ObterCategoriaPorIdAsync(id);
                if (categoria == null)
                    return NotFound(new { mensagem = "Categoria não encontrada" });

                return Ok(categoria);


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao obter categoria por id", erro = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodosAsCategorias()
        {
            try
            {
                var categorias = await _categoriaService.ObterTodasAsCategoriasAsync();
                return Ok(categorias);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar categorias", erro = ex.Message });
            }
        }




        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarCategoria(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) //valida corpo da requisição
            {
                return BadRequest(ModelState);
            }
            try
            {
                var categoria = await _categoriaService.CadastrarCategoriaAsync(categoriaViewModel);
                return CreatedAtAction(nameof(ObterCategoriaPorIdAsync), new { id = categoria.Id }, categoria);

            } catch (ArgumentException ex)
            {
                return BadRequest(new {mensagem = ex.Message});

            } catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao cadastrar categoria", erro = ex.Message });



            }
        }
        


    }
}
