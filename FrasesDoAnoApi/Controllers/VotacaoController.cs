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

    public class VotacaoController : ControllerBase
    {
        private readonly VotacaoDominio _votacaoDominio;

        /// <summary>
        /// Faz a conexão com o domínio
        /// </summary>
        /// <param name="votacaoDominio"></param>
        public VotacaoController(VotacaoDominio votacaoDominio)
        {
            _votacaoDominio = votacaoDominio;
        }

        /// <summary>
        /// Para votar na frase.
        /// </summary>
        /// <param name="votar">Recebe o fk do frases do ano e o fk do usuário</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VotarFrase([FromBody] VotarRequest votar)
        {
            try
            {
                _votacaoDominio.VotarNaFrase(votar);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest($"{ex.Message}"); 
            }
        }
        /// <summary>
        /// Deletar o voto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>retorna cod. 200 se deletar.</returns>
        [HttpDelete("{id}")]
        public ActionResult DeletarVoto(int id)
        {
            try
            {
                _votacaoDominio.DeletarVotacao(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar a frase. Detalhes: {ex.Message}");
            }
        }
    }
}