using FrasesDoAnoApi.Controllers.Modelos;
using FrasesDoAnoApi.Dados.Configuracao;
using FrasesDoAnoApi.Dados.Modelos;
using FrasesDoAnoApi.Utils;
using System.Threading.Tasks.Dataflow;

namespace FrasesDoAnoApi.Dominio
{
    /// <summary>
    /// 
    /// </summary>
    public class VotacaoDominio
    {
        /// <summary>
        /// Contexto para acesso ao banco de dados
        /// </summary>
        private readonly DbContextSql _dbContext;
        private readonly int _idUsuarioLogado;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="dbContextSql">Contexto</param>
        public VotacaoDominio(DbContextSql dbContextSql, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContextSql;
            HttpHelper httpHelper = new HttpHelper(httpContextAccessor);
            _idUsuarioLogado = httpHelper.ObterUsuarioLogado();
        }


        /// <summary>
        /// Método para votar na farase
        /// </summary>
        /// <param name="votacao">recebe fk do: frasedoano e fk_usuario.</param>
        public void VotarNaFrase(VotarRequest votacao)

        { 
            var verificarFrase = _dbContext.Tb_frasedoano
                .FirstOrDefault(f => 
                                f.Pk_id
                                .Equals(votacao.IdFrase));

            var verificarUsuario = _dbContext.Tb_usuario
                .FirstOrDefault(f =>
                                f.Pk_id
                                .Equals(_idUsuarioLogado));

            if (verificarUsuario is null)
            {
                throw new Exception("Usuário não encontrado");
            }
            if (verificarFrase is null)
            {
                throw new Exception("Frase não encontrada");
            }
            
            var frase = new Tb_votacao()
            {
                Fk_frasedoano = votacao.IdFrase,
                Fk_usuario = _idUsuarioLogado,
                Dh_inclusao= DateTime.Now
            };
            _dbContext.Tb_votacao.Add(frase);
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Método para deletar um voto
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void DeletarVotacao(int id)
        {
            var voto = _dbContext.Tb_votacao.Find(id);
            if (voto is null)
            {
                throw new Exception($"Cód. não encontrado. Código: {id}");
            }
            _dbContext.Remove(voto);
            _dbContext.SaveChanges();
        }
    }
    
}