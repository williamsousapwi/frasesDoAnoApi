namespace FrasesDoAnoApi.Controllers.Modelos
{
    /// <summary>
    /// Resposta para o usuário
    /// </summary>
    public class FraseResponse
    {
        /// <summary>
        /// Id/código, para identificar o cadastro.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// informa a frase cadastrada ou que irá ser cadastrada.
        /// </summary>
        public string Frase { get; set; } = "";
        /// <summary>
        /// informa a observacao cadastrada ou que irá ser cadastrada.
        /// </summary>
        public string Observacao { get; set; } = "";
        /// <summary>
        /// Informa data de inclusão
        /// </summary>
        public DateTime Inclusao { get; set; }
        /// <summary>
        ///  Informa data de alteração
        /// </summary>
        public DateTime ?Alteracao { get; set; }
        /// <summary>
        /// Campo para informar se está inativo ou não.
        /// </summary>
        public bool Inativo { get; set; }

    }
}
