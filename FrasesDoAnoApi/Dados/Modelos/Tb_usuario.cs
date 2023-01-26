using System.ComponentModel.DataAnnotations;

namespace FrasesDoAnoApi.Dados.Modelos
{
    /// <summary>
    /// Tabela principal
    /// </summary>
    public class Tb_usuario
    {
        /// <summary>
        /// Pk_id chave unique
        /// </summary>
        [Key]

        public int Pk_id { get; set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Ds_nome { get; set; } = "";
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Ds_senha { get; set; } = "";
        /// <summary>
        /// Login do usuário
        /// </summary>
        public string Ds_login { get; set; } = "";
        /// <summary>
        /// Data de inclusão do cadastro
        /// </summary>
        public DateTime Dh_inclusao { get; set; }
        /// <summary>
        /// Data de alteração do cadastro
        /// </summary>
        public DateTime? Dh_alteracao { get; set; }
    }
}
