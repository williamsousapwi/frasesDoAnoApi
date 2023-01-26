namespace FrasesDoAnoApi.Controllers.Modelos
{
    /// <summary>
    /// Classe de response 
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Nome da pessoa
        /// </summary>
        public string Nome { get; set; } = "";
        /// <summary>
        /// Nome do tipo string para logar
        /// </summary>
        public string Login { get; set; } = "";
        /// <summary>
        /// Senha para login
        /// </summary>
        public string Senha { get; set; } = "";
    }
}
