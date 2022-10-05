using BlogAPI.Src.Models;
using BlogAPI.Src.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controllers
{
    [ApiController]
    [Route("api/Themes")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {
        #region Attributes

        private readonly ITheme _repository;

        #endregion


        #region Constructors
        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods

        [HttpGet]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var list = await _repository.PegarTodosTemasAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("id/{idTheme}")]
        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idTheme)
        {
            try
            {
                return Ok(await _repository.PegarTemaPeloIdAsync(idTheme));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> NovoTemaAsync([FromBody] Theme theme)
        {
            await _repository.NovoTemaAsync(theme);

            return Created($"api/Temas", theme);
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarTema([FromBody] Theme theme)
        {
            try
            {
                await _repository.AtualizarTemaAsync(theme);
                return Ok(theme);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpDelete("delete/{idTheme}")]
        public async Task<ActionResult> DeletarTema([FromRoute] int idTheme)
        {
            try
            {
                await _repository.DeletarTemaAsync(idTheme);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        #endregion

    }
}
