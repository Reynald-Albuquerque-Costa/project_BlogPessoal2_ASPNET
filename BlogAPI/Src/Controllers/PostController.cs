using BlogAPI.Src.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BlogAPI.Src.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlogAPI.Src.Controllers 
{
    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Attributes

        private readonly IPost _repository;

        #endregion


        #region Constructors
        public PostController(IPost repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods

        /// <summary> 
        /// Pegar todos as postagem
        /// </summary> 
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodasPostagensAsync()
        {
            var list = await _repository.PegarTodasPostagensAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        /// <summary> 
        /// Pegar postagem pelo ID
        /// </summary> 
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> PegarPostagemPeloIdAsync([FromRoute] int idPost)
        {
            try
            {
                return Ok(await _repository.PegarPostagemPeloIdAsync(idPost));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary> 
        /// Criar nova postagem
        /// </summary> 
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovaPostagemAsync([FromBody] Post post)
        {
            try
            {
                await _repository.NovaPostagemAsync(post);
                return Created($"api/Posts", post);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary> 
        /// Atualizar postagem
        /// </summary> 
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AtualizarPostagemAsync([FromBody] Post post)
        {
            try
            {
                await _repository.AtualizarPostagemAsync(post);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary> 
        /// Deletar postagem
        /// </summary> 
        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public async Task<ActionResult> DeletarPostagem([FromRoute] int idPost)
        {
            try
            {
                await _repository.DeletarPostagemAsync(idPost);
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
