namespace FrasesDoAnoApi.Controllers.Modelos
{
    /// <summary>
    /// Classe para request
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; } = "";
        /// <summary>
        /// Login do usuário
        /// </summary>
        public string Login { get; set; } = "";
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Senha { get; set; } = "";
    }
}
