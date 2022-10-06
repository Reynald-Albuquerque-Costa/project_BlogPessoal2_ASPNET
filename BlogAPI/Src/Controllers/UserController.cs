using BlogAPI.Src.Models;
using BlogAPI.Src.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attributes

        private readonly IUser _repository;

        #endregion


        #region Constructors
        public UserController(IUser repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Pegar usuario pelo Email
        /// </summary>
        [HttpGet("email/{emailUser}")]
        public async Task<ActionResult> PegarUsuarioPeloEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.PegarUsuarioPeloEmailAsync(emailUser);

            if (user == null) return NotFound(new{Mensagem = "User not found" });

            return Ok(user);
        }

        /// <summary>
        /// Criar novo Usuario 
        /// </summary> 
        [HttpPost]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] User user)
        {
            await _repository.NovoUsuarioAsync(user);

            return Created($"api/Usuarios/{user.Email}", user);
        }

        #endregion
    }
}
