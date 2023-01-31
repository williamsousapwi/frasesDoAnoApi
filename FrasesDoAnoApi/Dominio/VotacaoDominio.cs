using FrasesDoAnoApi.Controllers.Modelos;
using FrasesDoAnoApi.Dados.Configuracao;
using FrasesDoAnoApi.Dados.Modelos;
using FrasesDoAnoApi.Utils;
using Microsoft.EntityFrameworkCore;
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
        /// <param name="dbContextSql"></param>
        /// <param name="httpContextAccessor"></param>
        public VotacaoDominio(DbContextSql dbContextSql, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContextSql;
            HttpHelper httpHelper = new HttpHelper(httpContextAccessor);
            _idUsuarioLogado = httpHelper.ObterUsuarioLogado();
        }


        /// <summary>
        /// Método para votar na frase
        /// </summary>
        /// <param name="votacao">recebe fk do: frasedoano e fk_usuario.</param>
        public void VotarNaFrase(VotarRequest votacao)
        {
            ValidarVoto(votacao.IdFrase);

            var verificarFrase = _dbContext.Tb_frasedoano
                .Find(votacao.IdFrase);

            var verificarUsuario = _dbContext.Tb_usuario
                .Find(_idUsuarioLogado);

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
                Dh_inclusao = DateTime.Now
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
        private void ValidarVoto(int votoNovo)
        {
            var voto = _dbContext.Tb_votacao.FirstOrDefault(x => x.Fk_frasedoano.Equals(votoNovo) && x.Fk_usuario.Equals(_idUsuarioLogado));

            if (voto is not null)
                throw new Exception("Voto já cadastrado.");
        }
        /// <summary>
        /// Método para fazer a obtenção da contagem dos votos.
        /// </summary>
        /// <returns> Json com informações</returns>
        public List<VotarResponse> ObterVotosFrase()
        {
            var query = (
                         from fra in _dbContext.Tb_frasedoano
                         join vot in _dbContext.Tb_votacao on fra.Pk_id equals vot.Fk_frasedoano into _vot
                         from vot in _vot.DefaultIfEmpty()
                         join usu in _dbContext.Tb_usuario on fra.Fk_owner equals usu.Pk_id into _usu
                         from usu in _usu.DefaultIfEmpty()
                         group new { fra, vot, usu } by new { fra.Pk_id } into _fra
                         select new VotarResponse()
                         {
                             Frase = _fra.Max(m => m.fra.Ds_frase),
                             Observacao = _fra.Max(m => m.fra.Ds_observacao),
                             Criador = _fra.Max(m => m.usu.Ds_nome),
                             QtdVotos = _fra.Count(c => c.vot != null),
                             Id = _fra.Key.Pk_id
                         }).ToList();
            return query;
        }
    }
}