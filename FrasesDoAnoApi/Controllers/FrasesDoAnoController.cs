using FrasesDoAnoApi.Controllers.Modelos;
using FrasesDoAnoApi.Dados.Modelos;
using FrasesDoAnoApi.Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FrasesDoAnoApi.Controllers
{
    /// <summary>
    ///  Class para o dom�nio
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    
    public class FrasesDoanoController : ControllerBase
    {
        private readonly FrasesDoAnoDominio _frasesDoAnoDominio;

        /// <summary>
        /// Faz a conex�o com o dom�nio
        /// </summary>
        /// <param name="frasesDoAnoDominio"></param>
        public FrasesDoanoController (FrasesDoAnoDominio frasesDoAnoDominio)
        {
            _frasesDoAnoDominio = frasesDoAnoDominio;
        }

        /// <summary>
        /// Consulta frase por nome
        /// </summary>
        /// <param name="frase">frase para efetuar a consulta</param>
        /// <returns> Retorna o Id, frase, observa��o, inclus�o, alteracao e se est� ativo</returns>
        [HttpGet]
       
        public ActionResult<List<FraseResponse>> ConsultarFrase([FromQuery] string? frase="")
        {
            try
            {              
                return _frasesDoAnoDominio.ConsultarFrase(frase);
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao exibir as frases. Detalhes: {ex.Message}"); 
            }
        }
        /// <summary>
        /// Consulta frase por id
        /// </summary>
        /// <param name="id"> c�digo para efetuar a consulta</param>
        /// <returns>Retorna o Id, frase, observa��o, inclus�o, alteracao e se est� ativo</returns>
        [HttpGet("{id}")]
        public ActionResult<FraseResponse> ConsultarFrasePorId(int id)
        {
            try
            {
                return _frasesDoAnoDominio.ConsultarPorId(id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao exibir a frase. Detalhes: {ex.Message}");
            }
        }
        /// <summary>
        /// Deleta frase por id
        /// </summary>
        /// <param name="id">c�digo para efetuar a exclus�o/delete.</param>
        [HttpDelete("{id}")]
        public ActionResult DeletarFrase(int id)
        {
            try
            {
                _frasesDoAnoDominio.DeletarFrase(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar a frase. Detalhes: {ex.Message}");
            }
        }
        /// <summary>
        /// Cadastra frase
        /// </summary>
        /// <param name="frase">Frase e observa��o a ser cadastrado</param>
        /// <returns>Se tudo der  certo, retorna 200.</returns>
        [HttpPost]
        public ActionResult CadastrarFrase([FromBody] FraseRequest frase)
        {
            try
            {
                _frasesDoAnoDominio.AdicionarFrase(frase);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        /// <summary>
        /// Altera a frase cadastrada
        /// </summary>
        /// <param name="id">identifica qual frase cadastrada a ser alterada</param>
        /// <param name="frase"> Altera a frase com base no que for informado.</param>
        /// <returns>Se tudo der certo, c�digo 200.</returns>
        [HttpPut("{id}")]
        public ActionResult AlterarFrases(int id, [FromBody] FraseRequest frase)
        {
            try
            {
                _frasesDoAnoDominio.AlterarFrase(id, frase);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao alterar as informa��es. {ex.Message}");
            }
        }
    }

}