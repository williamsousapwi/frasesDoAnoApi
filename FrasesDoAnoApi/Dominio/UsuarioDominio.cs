using FrasesDoAnoApi.Controllers.Modelos;
using FrasesDoAnoApi.Dados.Configuracao;
using FrasesDoAnoApi.Dados.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace FrasesDoAnoApi.Dominio
{
    /// <summary>
    /// 
    /// </summary>
    public class UsuarioDominio
    {
        /// <summary>
        /// Contexto para acesso ao banco de dados
        /// </summary>
        private readonly DbContextSql _dbContext;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="dbContextSql">Contexto</param>
        public UsuarioDominio(DbContextSql dbContextSql)
        {
            _dbContext = dbContextSql;
        }
        /// <summary>
        /// Valida o cadastro do usuário, trazendo erros e etc.
        /// </summary>
        /// <param name="cadastroUsuario"></param>
        /// <exception cref="Exception"></exception>
        private void ValidarCadastroUsuario(UserRequest cadastroUsuario)
        {

            if (string.IsNullOrWhiteSpace(cadastroUsuario.Login))
            {
                throw new Exception("Login é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(cadastroUsuario.Nome))
            {
                throw new Exception("Nome é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(cadastroUsuario.Senha))
            {
                throw new Exception("Senha é obrigatória.");
            }

            // A expressao regular para validar senha: Caractér especial, Letra maiúscula e minúscula, numero e no mínimo 8 caractéres.
            if (!Regex.IsMatch(cadastroUsuario.Senha, "^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\\d]){1,})(?=(.*[\\W]){1,})(?!.*\\s).{8,}$"))
                throw new Exception("Senha inválida, deve conter:\n um caractér especial,\n letra maiúscula,\n letra minuscula ,\n um numero \n e no mínimo 8 caractéres.");

            cadastroUsuario.Login.ToLower();
            ValidarLogin(cadastroUsuario.Login);
        }

        private void ValidarLogin(string loginNovo)
        {
            var user = _dbContext.Tb_usuario.FirstOrDefault(x => x.Ds_login.Equals(loginNovo.ToLower()));

            if (user is not null)
                throw new Exception("Usuário já cadastrado.");
        }
        /// <summary>
        /// Metódo para cadastar usuário, valida caso o usuário já tenha sido cadastrado.
        /// </summary>
        /// <param name="cadastroUsuario"></param>
        public void AdicionarUsuario(UserRequest cadastroUsuario)
        {
            ValidarCadastroUsuario(cadastroUsuario);

            var usuario = new Tb_usuario()
            {
                Ds_login = cadastroUsuario.Login,
                Ds_nome = cadastroUsuario.Nome,
                Ds_senha = cadastroUsuario.Senha,
                Dh_inclusao = DateTime.Now

            };
            _dbContext.Tb_usuario.Add(usuario);
            _dbContext.SaveChanges();

        }

        /// <summary>
        /// Método de login, que verifica o login e senha informados.
        /// </summary>
        /// <param name="loginUsuario"> login e senha</param>
        public void LoginUsuario(UserRequest loginUsuario)
        {
            var verificarUsuario = _dbContext.Tb_usuario
                .FirstOrDefault(w => w.Ds_login.Equals(loginUsuario.Login) && w.Ds_senha.Equals(loginUsuario.Senha));

            if (verificarUsuario is null)
            {
                throw new Exception("Usuario não encontrado ou credenciais inválidas.");
            };

        }
    }
}

