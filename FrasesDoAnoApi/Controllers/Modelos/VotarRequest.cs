namespace FrasesDoAnoApi.Controllers.Modelos
{
    /// <summary>
    /// Classe para request da votação
    /// </summary>
    public class VotarRequest
    {
        /// <summary>
        /// fk do usuário.
       /// </summary>
        public int Id_usuario { get; set; } 
        /// <summary>
        /// id do fk_frasedoano
        /// </summary>
        public int Id_frasedoano { get; set; } 

    }
}
