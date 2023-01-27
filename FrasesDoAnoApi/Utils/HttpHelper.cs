using FrasesDoAnoApi.Controllers.Modelos;
using FrasesDoAnoApi.Dados.Modelos;
using FrasesDoAnoApi.Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http.Features;

namespace FrasesDoAnoApi.Utils
{
    /// <summary>
    /// Classe de auxílio.
    /// </summary>
    public class HttpHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
  
        }
        /// <summary>
        /// Id do usuário logado, recebido pelo header.
        /// </summary>
        /// <returns></returns>
        public int ObterUsuarioLogado()
        {
            int idUsuarioLogado;
            var receberId = _httpContextAccessor.HttpContext.Request.Headers["IdUsuarioLogado"].FirstOrDefault();
            int.TryParse(receberId, out idUsuarioLogado);
            if (idUsuarioLogado == 0)
            {
                throw new Exception("Id do usuário logado inválido.");
            }

            return idUsuarioLogado;

        }
    }
}
    