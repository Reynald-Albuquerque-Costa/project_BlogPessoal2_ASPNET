using BlogAPI.Src.Models;
using BlogAPI.Src.Repositories;
using BlogAPI.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IAuthentication _services;

        #endregion


        #region Constructors
        public UserController(IUser repository, IAuthentication services)
        {
            _repository = repository;
            _services = services;

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

            if (user == null) return NotFound(new { Mensagem = "User not found" });

            return Ok(user);
        }

        /// <summary>
        /// Criar novo Usuario 
        /// </summary> 
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> NovoUsuarioAsync([FromBody] User user)
        {
            try
            {
                await _services.CriarUsuarioSemDuplicarAsync(user);
                return Created($"api/Usuarios/email/{user.Email}", user);

            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }

        /// <summary>
        ///  Logar
        /// </summary> 
        [HttpPost("log")]
        [AllowAnonymous]
        public async Task<ActionResult> LogarAsync([FromBody] User user)
        {
            var auxiliar = await _repository.PegarUsuarioPeloEmailAsync(user.Email);
            if (auxiliar == null) return Unauthorized(new
            {
                Mensagem = "E-mail invalido"
            });
            if (auxiliar.Password != _services.CodificarSenha(user.Password))
                return Unauthorized(new { Mensagem = "Senha invalida" });
            var token = "Bearer " + _services.GerarToken(auxiliar);
            return Ok(new { Usuario = auxiliar, Token = token });
        }


        #endregion
    }
}
