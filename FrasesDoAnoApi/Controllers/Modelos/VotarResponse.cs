namespace FrasesDoAnoApi.Controllers.Modelos
{
    public class VotarResponse
    {
        public int Id { get; set; }
        public string Frase { get; set; } = "";
        public string Observacao { get; set; } = "";
        public string Criador { get; set; } = "";
        public int QtdVotos { get; set; }
    }
}
