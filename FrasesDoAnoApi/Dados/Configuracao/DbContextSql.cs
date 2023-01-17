using FrasesDoAnoApi.Dados.Modelos;
using Microsoft.EntityFrameworkCore;

namespace FrasesDoAnoApi.Dados.Configuracao
{
    /// <summary>
    /// Classe de acesso ao banco (DbContext)
    /// </summary> 
    public class DbContextSql : DbContext
    {
        /// <summary>
        /// String de conexão com o banco de dados
        /// </summary>
        public string StringConexaoBd { get; private set; }
        /// <summary>
        /// Referênciando a tb_frasedoano
        /// </summary>
        public DbSet<Tb_frasedoano> Tb_frasedoano { get; set; }
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public DbContextSql(string stringConexao)
        {
            if (string.IsNullOrWhiteSpace(stringConexao))
            {
                throw new ArgumentException("String de conexão para o banco de dados não foi configurada.");
            }

            StringConexaoBd = stringConexao;
        }

        /// <summary>
        /// Configuração para usar string de conexão
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StringConexaoBd);
        }
    }
}
