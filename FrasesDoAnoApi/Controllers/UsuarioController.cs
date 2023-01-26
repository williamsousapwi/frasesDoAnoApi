using FrasesDoAnoApi.Controllers.Modelos;
using FrasesDoAnoApi.Dados.Modelos;
using FrasesDoAnoApi.Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FrasesDoAnoApi.Controllers
{
    /// <summary>
    ///  Class para o domínio
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]

    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDominio _usuarioDominio;

        /// <summary>
        /// Faz a conexão com o domínio
        /// </summary>
        /// <param name="usuarioDominio"></param>
        public UsuarioController (UsuarioDominio usuarioDominio)
        {
            _usuarioDominio = usuarioDominio;
        }
        /// <summary>
        /// Cadastrar usuario
        /// </summary>
        /// <param name="cadastro">Recebe nome,login e senha</param>
        /// <returns>retorna 200 ao cadastrar</returns>
        [HttpPost("Cadastro")]
        public ActionResult CadastrarUsuario([FromBody] UserRequest cadastro)
        {
            try
            {
                _usuarioDominio.AdicionarUsuario(cadastro);
                return Ok();
            }
            catch (Exception ex)

            {

                return BadRequest($"{ex.Message}");
            }
        }
        /// <summary>
        /// Efetua o login e verifica se já tem cadastrado.
        /// </summary>
        /// <param name="login">Recebe o login e a senha cadastrada</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public ActionResult UsuarioLogin(UserRequest login)
        {
            try
            {
                _usuarioDominio.LoginUsuario(login);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }

}