using System.ComponentModel.DataAnnotations;

namespace FrasesDoAnoApi.Dados.Modelos
{
    /// <summary>
    /// Tabela principal
    /// </summary>
    public class Tb_votacao
    {
        /// <summary>
        /// Pk_id chave unique
        /// </summary>
        [Key]

        public int Pk_id { get; set; }

        /// <summary>
        /// Data de inclusão do cadastro
        /// </summary>
        public DateTime Dh_inclusao { get; set; }

        /// <summary>
        /// Chave da tabela do usuário
        /// </summary>
        public int Fk_usuario { get; set; }
        /// <summary>
        /// Chave apra tabela FraseDoAno
        /// </summary>
        public int Fk_frasedoano { get; set; } 
     
    }
}
