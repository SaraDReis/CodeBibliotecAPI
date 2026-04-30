using CodeBibliotec.Interfaces;
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

        [HttpGet("{id}")]
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
    }
}
