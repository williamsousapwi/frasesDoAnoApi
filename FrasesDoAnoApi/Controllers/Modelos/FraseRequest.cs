namespace FrasesDoAnoApi.Controllers.Modelos
{
    /// <summary>
    /// Classe para request da frase
    /// </summary>
    public class FraseRequest
    {
        /// <summary>
        /// Frase informada
        /// </summary>
        public string Frase { get; set; } = "";
        /// <summary>
        /// Observacao informada
        /// </summary>
        public string Observacao { get; set; } = ""; 
        /// <summary>
        /// Id do usuário
        /// </summary>
        public int IdUsuario { get; set; }
    }
}
