using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prodesp.Application.AppServices;
using Prodesp.Application.CrossCutting.TO.Request.Login;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Domain.Shared.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Prodesp.Tests.UnitTests
{
    [TestClass]
    public class LoginAppServiceTest
    {
       
        public LoginAppService Criar_LoginAppService(int bloqueado = 0, int ativo = 1)
        {
            var tokenHelperAppService = new Mock<ITokenHelperAppService>();
            var configurationService = new Mock<IConfiguration>();
            var loginService =  new Mock<ILoginService>();

            var LoginAppService = new LoginAppService(loginService.Object, tokenHelperAppService.Object,  configurationService.Object);

            var taskSessao = Task.FromResult<Sessao?>(new Sessao() { CodigoAccessToken = "123456", IdLogin = "123456" });
            var taskLogin = Task.FromResult<Login?>(new Login() { FlagAtivo = ativo, FlagBloqueado = bloqueado, CodigoLogin = "login", CodigoSenha = "CqnJgvCeqOHFGrOPSws0AQ==", Usuario = new Domain.Shared.Entities.Usuario() { Ativo = ativo, CodigoSaltKey = "0AA0160E" } });
            var taskZerar = Task.FromResult<bool>(true);
            tokenHelperAppService.Setup(s => s.GerarToken(It.IsAny<Login>(), It.IsAny<string>(), It.IsAny<string>())).Returns( taskSessao);
            loginService.Setup(l => l.Login(It.IsAny<string>())).Returns(taskLogin) ;
            loginService.Setup(l => l.GetLoginAsync(It.IsAny<string>())).Returns(taskLogin);
            loginService.Setup(s => s.ZerarTentativasLogin(It.IsAny<Login>())).Returns(taskZerar);
            return LoginAppService;
        }

        public LoginRequest GerarLogin(string login, string senha) {

            var loginRequest = new LoginRequest();

            loginRequest.Data = new LoginRequestData();
            loginRequest.Data.Login = login;
            loginRequest.Data.Senha = senha;
            
            return loginRequest;
        } 
        

        [TestMethod]
        public void Metodo_Logar_ComSucesso()
        {
            // Arrange
            var loginAppService = this.Criar_LoginAppService();

            // Act
            var result =  loginAppService.Logar(this.GerarLogin("login","123456"), "IP", "USER_AGENT");

            // Assert
            Assert.IsNotNull(result.Result.Data.SessaoAtiva);

        }

        [TestMethod]
        public void Metodo_Logar_Mensagem_UsuarioOuSenhaIncorretos()
        {
            var loginAppService = this.Criar_LoginAppService();

            // Act
            var result = loginAppService.Logar(this.GerarLogin("login", "passwordErrado"), "IP", "USER_AGENT");

            // Assert
            Assert.AreEqual("Login ou senha inválido", result.Result.Errors.Select(a => a.Message).FirstOrDefault());
        }

        [TestMethod]
        public void Metodo_Logar_Mensagem_Bloqueado()
        {
            var loginAppService = this.Criar_LoginAppService(1);

            // Act
            var result = loginAppService.Logar(this.GerarLogin("login", "123456"), "IP", "USER_AGENT");

            // Assert
            Assert.AreEqual("Login bloqueado", result.Result.Errors.Select(a => a.Message).FirstOrDefault());
        }

        [TestMethod]
        public void Metodo_Logar_Mensagem_UsuarioInativo()
        {
            var loginAppService = this.Criar_LoginAppService(0,0);

            // Act
            var result = loginAppService.Logar(this.GerarLogin("login", "123456"), "IP", "USER_AGENT");

            // Assert
            Assert.AreEqual("Login inativo", result.Result.Errors.Select(a => a.Message).FirstOrDefault());
        }   
        
    }
}