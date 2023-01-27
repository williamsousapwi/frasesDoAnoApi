using FrasesDoAnoApi.Controllers.Modelos;
using FrasesDoAnoApi.Dados.Configuracao;
using FrasesDoAnoApi.Dados.Modelos;
using FrasesDoAnoApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;

namespace FrasesDoAnoApi.Dominio
{
    /// <summary>
    /// Classe que faz as consultas no banco
    /// </summary>
    public class FrasesDoAnoDominio
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
        public FrasesDoAnoDominio(DbContextSql dbContextSql, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContextSql;
            HttpHelper httpHelper = new HttpHelper(httpContextAccessor);
            _idUsuarioLogado = httpHelper.ObterUsuarioLogado();


        }

        /// <summary>
        /// Pesquisa por frase
        /// </summary>
        /// <param name="frase">Recebe uma frase do tipo string</param>
        public List<FraseResponse> ConsultarFrase(string frase)
        {
            var query = from frases in _dbContext.Tb_frasedoano
                        join usuarios in _dbContext.Tb_usuario on frases.Fk_owner equals usuarios.Pk_id into _usuarios
                        from usuarios in _usuarios.DefaultIfEmpty()

                        join votos in _dbContext.Tb_votacao on new { IdFrase = frases.Pk_id, IdUsuario = _idUsuarioLogado }  equals new { IdFrase = votos.Fk_frasedoano ,IdUsuario = votos.Fk_usuario} into _votos
                        from votos in _votos.DefaultIfEmpty()

                        select new FraseResponse()
                        {
                            Id              = frases.Pk_id,
                            idUsuario       = frases.Fk_owner ?? 0,
                            idVotacao       = votos.Pk_id,
                            Frase           = frases.Ds_frase,
                            Observacao      = frases.Ds_observacao,
                            Inclusao        = frases.Dh_inclusao,
                            Alteracao       = frases.Dh_alteracao,
                            Inativo         = frases.Tg_inativo,
                            Criador         = usuarios != null ? usuarios.Ds_nome : ""
                        };
            if (!string.IsNullOrWhiteSpace(frase))
            query = query.Where(w => w.Frase.Contains(frase));

            return query.ToList();
        }
        /// <summary>
        /// Pesquisa por id.
        /// </summary>
        /// <param name="id"> Id informado na url para consulta.</param>
        /// <returns></returns>
        public FraseResponse ConsultarPorId(int id)
        {
            var frasePorId = _dbContext.Tb_frasedoano
                .Where(w =>
                       w.Pk_id.Equals(id))
                .Select(s => new FraseResponse()
                {
                    Id = s.Pk_id,
                    Frase = s.Ds_frase,
                    Observacao = s.Ds_observacao,
                    Inclusao = s.Dh_inclusao,
                    Alteracao = s.Dh_alteracao,
                    Inativo = s.Tg_inativo
                }).FirstOrDefault();

            if (frasePorId is null)
            {
                throw new Exception("Frase não encontrada, tente editar uma frase existente.");
            }
            return frasePorId;
        }
        /// <summary>
        /// Deleta com base no id.
        /// </summary>
        /// <param name="id"> id informado na url para encontrar e deletar a frase.</param>
        public void DeletarFrase(int id)
        {
            var frases = _dbContext.Tb_frasedoano.Find(id);
            if (frases is null)
            {
                throw new Exception($"Cód. não encontrado. Código: {id}");
            }
            _dbContext.Remove(frases);
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Adiciona frase, passando a frase e observação.
        /// </summary>
        /// <param name=" cadastroFrase"> Conjunto de informações para cadastro da frase.</param>
        public void AdicionarFrase(FraseRequest cadastroFrase)
        {
            if (string.IsNullOrWhiteSpace(cadastroFrase.Frase))
            {
                throw new Exception("A frase é obrigatória.");
            }
            var resposta = new Tb_frasedoano()
            {
                Ds_frase = cadastroFrase.Frase,
                Ds_observacao = cadastroFrase.Observacao,
                Dh_inclusao = DateTime.Now,
                Tg_inativo = false
            };
          
            _dbContext.Tb_frasedoano.Add(resposta);
            _dbContext.SaveChanges(); 
        }
        /// <summary>
        /// Altera a frase, pegando id existente.
        /// </summary>
        /// <param name="id">Id para indicar a frase a ser alterada.</param>
        /// <param name="alterarFrase">Conjunto de informaçoes para alterar a frase.</param>
        public void AlterarFrase(int id, FraseRequest alterarFrase)
        {

            var dadosExistentes = _dbContext.Tb_frasedoano.Find(id);
            if (dadosExistentes is null)
            {
                throw new Exception("Frase não encontrada, tente editar uma frase existente.");
            }

            if (string.IsNullOrWhiteSpace(dadosExistentes.Ds_frase))
            {
                throw new Exception("A frase é obrigatória.");
            }

            dadosExistentes.Ds_frase = alterarFrase.Frase;
            dadosExistentes.Ds_observacao = alterarFrase.Observacao;
            dadosExistentes.Dh_alteracao = DateTime.Now;
            dadosExistentes.Tg_inativo = false;
   
            _dbContext.SaveChanges();   
        }

       
        
        


    }
}
