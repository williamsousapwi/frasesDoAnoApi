using FrasesDoAnoApi.Controllers.Modelos;
using FrasesDoAnoApi.Dados.Configuracao;
using FrasesDoAnoApi.Dados.Modelos;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="dbContextSql">Contexto</param>
        public FrasesDoAnoDominio(DbContextSql dbContextSql)
        {
            _dbContext = dbContextSql;
        }

        /// <summary>
        /// Pesquisa por frase
        /// </summary>
        /// <param name="frase">Recebe uma frase do tipo string</param>
        public List<FraseResponse> ConsultarFrase(string frase)
        {
            var frasePorNome = _dbContext.Tb_frasedoano
                .Where(w => 
                       w.Ds_frase.Contains(frase.Trim()))
                       .ToList()
                .Select(s => new FraseResponse()
                {
                    Id = s.Pk_id,
                    Frase= s.Ds_frase,
                    Observacao= s.Ds_observacao,
                    Inclusao = s.Dh_inclusao,
                    Alteracao = s.Dh_alteracao,
                    Inativo = s.Tg_inativo
                });
           
            return frasePorNome.ToList();   
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
                throw new Exception("Cód. não encontrado.");
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
                throw new Exception("Cód. não encontrado.");
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
