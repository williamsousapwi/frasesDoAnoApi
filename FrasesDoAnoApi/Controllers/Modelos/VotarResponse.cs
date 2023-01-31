namespace FrasesDoAnoApi.Controllers.Modelos
{
    /// <summary>
    /// Classe de response
    /// </summary>
    public class VotarResponse
    {
        /// <summary>
        /// Campo pk_id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Campo ds_frase
        /// </summary>
        public string Frase { get; set; } = "";
        /// <summary>
        /// Campo ds_observacao
        /// </summary>
        public string Observacao { get; set; } = "";
        /// <summary>
        /// Campo ds_nome da tb_usuario
        /// </summary>
        public string Criador { get; set; } = "";
        /// <summary>
        /// QtdVotos, count do pk_id dos votos
        /// </summary>
        public int QtdVotos { get; set; }
    }
}
