using Prodesp.Core.Backend.Domain.Models;
using Prodesp.Core.Backend.Domain.Services;
using Prodesp.Core.Backend.Domain.Validations;
using Prodesp.Domain.Repositories.Interfaces;
using Prodesp.Domain.Services.Interfaces;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Domain.Services.Implementations;

    public class UsuarioService : Service<Shared.Entities.Usuario, IUsuarioRepository>, IUsuarioService
    {
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly ISessaoRepository _sessaoRepository;
        private readonly ILoginRepository _loginRepository;
        public UsuarioService(IUsuarioRepository UsuarioRepository,
                             ISessaoRepository SessaoRepository,
                             ILoginRepository LoginRepository
                             ) : base(UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;
            _sessaoRepository = SessaoRepository;
            _loginRepository = LoginRepository;
        }

        public async Task<Usuario?> AlterarUsuario(Usuario usuario)
        {
            return await this._UsuarioRepository.AlterarUsuario(usuario);
        }

        public async Task<ValidationResult> AtualizarSenha(string accessToken, string senhaAntiga, string novaSenha)
        {
            var validacoes = new Core.Backend.Domain.Validations.ValidationResult();

            var sessao = await _sessaoRepository.GetSessaoByToken(accessToken);
            if (sessao == null)
                validacoes.Add("Token inválido. Por favor faça o login novamente!");
            else
            {
                if (sessao.DataValidadeAccessToken < DateTime.Now)
                    validacoes.Add("Token expirado. Faça o login novamente");

                else if (sessao.Login == null)
                    validacoes.Add("Não foi possível localizar o login desta sessão");

                else if (string.IsNullOrEmpty(sessao.Login.CodigoSenha))
                    validacoes.Add("Senha cadastrada está em branco");

                else if (sessao.Login.Usuario == null)
                    validacoes.Add("Não foi possível localizar o munícipe deste login");

                else
                {
                    if (string.IsNullOrEmpty(sessao.Login.Usuario.CodigoSaltKey))
                        validacoes.Add("Não foi possível localizar o SaltKey do munícipe");
                    else
                    {
                        var saltKey = sessao.Login.Usuario.CodigoSaltKey;
                        var senhaAtualLogin = sessao.Login.CodigoSenha;

                        //var criptoSenhaAntiga = LoginService.CriptoPass(saltKey, senhaAntiga);
                        if (senhaAntiga != senhaAtualLogin)
                            validacoes.Add("Senha atual informada está incorreta");

                        //var novaSenhaCriptografada = LoginService.CriptoPass(saltKey, novaSenha);
                        if (string.IsNullOrEmpty(novaSenha))
                            validacoes.Add("Senha atual informada está incorreta");

                        if (!validacoes.Errors.Any())
                        {
                            var logins = await _loginRepository.FindAsync(x => x.IdUsuario == sessao.IdUsuario && x.FlagAtivo == 1);
                            //Where(x => x.IdUsuario == sessao.IdUsuario && x.FlagAtivo == 1).toListAsync();
                            logins.ToList().ForEach(async login =>
                            {
                                login.CodigoSenha = novaSenha;
                                login.DataAlteracao = DateTime.Now;
                                login.NumeroTentativaLogin = 0;
                                login.FlagBloqueado = 0;

                                await _loginRepository.UpdateAsync(login);
                            });
                        }
                    }
                }
            }

            return validacoes;
        }

        public async Task<PageResult<Usuario>?> GetAll()
        {
            if (base._repository == null)
                return null;

            //var filter = base._repository.SelectAll().ToList();
            var filter = await _repository.FindAsync(x => x.Ativo == 1);
            var hp = new Gsnet.Framework.HelperPage(1, int.MaxValue, filter.ToList().Count);
            return new PageResult<Shared.Entities.Usuario>
            {
                Records = filter.OrderBy(x => x.NomeUsuario).ToList(),
                Page = hp
            };
        }

        public async Task<Usuario?> GetUsuario(string idUsuario)
        {
            if (base._repository == null)
                return null;

            return await base._repository.GetByUsuarioID(idUsuario);
        }

        public async Task<Login?> GetUsuarioPorLogin(string cdLogin)
        {
            if (base._repository == null)
                return null;


           var queryLogin = await  _loginRepository.GetLoginAsync(cdLogin);
                                
            return queryLogin;
        }

        public async Task<Usuario?> IncluirUsuario(Usuario usuario, Login login)
        {
            return await this._UsuarioRepository.IncluirUsuario(usuario, login);
        }

        public async Task<Login?> ResetarSenha(Login login, string novaSenha)
        {
            if (base._repository == null)
                return null;

            login.CodigoSenha = novaSenha;
            login.DataAlteracao = null;
            login.NumeroTentativaLogin = 0;
            login.FlagBloqueado = 0;
            login.FlagAtivo = 1;

            await this._loginRepository.UpdateAsync(login);

            return login;
        }
    }
