using System.ComponentModel.DataAnnotations;

namespace FrasesDoAnoApi.Dados.Modelos
{
    /// <summary>
    /// Tabela principal
    /// </summary>
    public class Tb_frasedoano
    {
        /// <summary>
        /// Pk_id chave unique
        /// </summary>
        [Key]
  
        public int Pk_id { get; set; }
        /// <summary>
        /// Frase
        /// </summary>
        public string Ds_frase { get; set; } = "";
        /// <summary>
        /// Comentários adicionais
        /// </summary>
        public string Ds_observacao { get; set; } = "";
        /// <summary>
        /// Se está inatido 1 = true/ 0 = false
        /// </summary>
        public bool Tg_inativo { get; set; }
        /// <summary>
        /// Data de inclusão do cadastro
        /// </summary>
        public DateTime Dh_inclusao { get; set; }
        /// <summary>
        /// Data de alteração do cadastro
        /// </summary>
        public DateTime ?Dh_alteracao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Fk_owner { get; set; }
    }
}
