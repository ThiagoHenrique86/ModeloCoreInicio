using Prodesp.Core.Backend.Domain.Models;
using Prodesp.Core.Backend.Domain.Services;
using Prodesp.Domain.Repositories.Interfaces;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Services.Implementations;
public class LoginService :
        Service<Login, ILoginRepository>, ILoginService
{
    private readonly ILoginRepository _LoginRepository;
    private readonly ISessaoRepository _SessaoRepository;
    private readonly IUsuarioRepository _UsuarioRepository;

    public LoginService(ILoginRepository LoginRepository,
                        ISessaoRepository SessaoRepository,
                        IUsuarioRepository UsuarioRepository
        ) : base(LoginRepository)
    {
        this._LoginRepository = LoginRepository;
        this._SessaoRepository = SessaoRepository;
        this._UsuarioRepository = UsuarioRepository;
    }
    public async Task<Login?> GetLoginAsync(string login)
    {
        var validacoes = new Core.Backend.Domain.Validations.ValidationResult();

        if (base._repository == null)
            throw new ApplicationException("O repositório não foi carregado corretamente");

        if (string.IsNullOrEmpty(login))
            throw new ApplicationException("O login não foi informado");


        //var _login = base._repository.GetLogin(login);
        var _login = await base._repository.GetLoginAsync(login);
        //.FirstOrDefault();



        return _login;
    }

    public async Task<Login?> AddQuantidadeTentativaLogin(Login login)
    {
        if (login.Usuario == null)
            throw new ApplicationException("Erro ao localizar o usuário");

        var PoliticaSenha = "5";
        int ConfigTentativaLogin = 10;
        int.TryParse(PoliticaSenha, out ConfigTentativaLogin);

        var numeroTentativas = ++login.NumeroTentativaLogin;
        var listaLogins = await this._LoginRepository.FindAsync(x => x.IdUsuario == login.IdUsuario);

        listaLogins.ToList().ForEach(async l =>
        {
            l.NumeroTentativaLogin = numeroTentativas;

            if (l.NumeroTentativaLogin >= ConfigTentativaLogin)
            {
                l.NumeroTentativaLogin = numeroTentativas;
                l.FlagBloqueado = 1;
                login = (l.IdLogin == login.IdLogin ? l : login);
            }

            await this._repository.UpdateAsync(l);
        });

        return login;
    }

    public async Task<bool> AtualizarLogin(Login login)
    {
        if (login == null)
            throw new ApplicationException("Não foi informado o login a ser atualizado");

        login.DataAlteracao = DateTime.Now;
        await base._repository.UpdateAsync(login);

        return true;
    }

    public async Task<Sessao?> GerarSessao(string ip, Login login, string userAgent, int TempoExpiracao, string TokenJWT)
    {
        var sessaoAtiva = new Shared.Entities.Sessao
        {
            IdAccessToken = Guid.NewGuid().ToString(),
            CodigoAccessToken = TokenJWT,
            QtdDuracaoAccessToken = TempoExpiracao,
            DataValidadeAccessToken = DateTime.Now.AddHours(TempoExpiracao),
            IdUsuario = login.IdUsuario,
            IdLogin = login.IdLogin,
            NumeroIp = ip,
            NomeHost = userAgent,
            DataInclusao = DateTime.Now,
            FlagRefreshAccessToken = "S"
        };

        await _SessaoRepository.InsertAsync(sessaoAtiva);
        return sessaoAtiva;
    }

    public async Task<PageResult<Login>?> GetAll()
    {
        if (base._repository == null)
            return null;

        var filter = await base._repository.SelectAllAsync();
        var hp = new Gsnet.Framework.HelperPage(1, int.MaxValue, filter.Count());
        return new PageResult<Shared.Entities.Login>
        {
            Records = filter.ToList(),
            Page = hp
        };
    }

    public async Task<Login?> GetLogin(string cdlogin)
    {
        if (base._repository == null)
            return null;

        return await base._repository.GetLoginAsync(cdlogin);
    }

    public async Task<Login?> GetLoginByToken(string token)
    {
        return await base._repository.GetLoginByToken(token);
    }

    public async Task<Login?> Login(string login)
    {
        var validacoes = new Core.Backend.Domain.Validations.ValidationResult();

        if (base._repository == null)
            throw new ApplicationException("O repositório não foi carregado corretamente");

        if (string.IsNullOrEmpty(login))
            throw new ApplicationException("O login não foi informado");


        //var _login = base._repository.GetLogin(login);
        var _login = await base._repository.GetLoginAsync(login);
            //.FirstOrDefault();



        return _login;
    }

    public async Task<bool> ZerarTentativasLogin(Login login)
    {
        if (login == null)
            throw new ApplicationException("Foi solicitado zerar a quantidade de tentativas mas não foi informado o login");

        var listaLogins = await this._LoginRepository.FindAsync(x => x.IdUsuario == login.IdUsuario);

        listaLogins.ToList().ForEach(async l =>
        {
            l.NumeroTentativaLogin = 0;
            await this._repository.UpdateAsync(l);
        });

        return true;
    }
}