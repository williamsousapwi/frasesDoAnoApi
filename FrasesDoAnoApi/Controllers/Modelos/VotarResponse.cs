namespace FrasesDoAnoApi.Controllers.Modelos
{
    public class VotarResponse
    {
        public int Pk_id { get; set; }
        public string Frase { get; set; } = "";
        public string Observacao { get; set; } = "";
        public string Autor { get; set; } = "";
        public int QtdVotos { get; set; }
    }
}
