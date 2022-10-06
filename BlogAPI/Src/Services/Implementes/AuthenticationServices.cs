using BlogAPI.Src.Models;
using BlogAPI.Src.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Src.Services.Implementes
{
    public class AuthenticationServices : IAuthentication
    {
        #region Atributos

        private IUser _repository;
        public IConfiguration Configuracao { get; }

        #endregion


        #region Construtores
        public AuthenticationServices(IUser repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuracao = configuration;
        }

        #endregion


        #region Métodos




        #endregion
        public string CodificarSenha(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public async Task CriarUsuarioSemDuplicarAsync(User user)
        {
            var auxiliar = await _repository.PegarUsuarioPeloEmailAsync(user.Email);

            if (auxiliar != null) throw new Exception("This email is already being used");

            user.Password = CodificarSenha(user.Password);

            await _repository.NovoUsuarioAsync(user);
        }

        public string GerarToken(User user)
        {
            var tokenManipulador = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Configuracao["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.Role, user.Type.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
            )
            };
            var token = tokenManipulador.CreateToken(tokenDescricao);
            return tokenManipulador.WriteToken(token);
        }
    }
}
