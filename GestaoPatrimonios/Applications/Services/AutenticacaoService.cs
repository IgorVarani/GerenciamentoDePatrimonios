using GestaoPatrimonios.Applications.Autenticacao;
using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.AutenticacaoDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _tokenJWT;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt tokenJWT)
        {
            _repository = repository;
            _tokenJWT = tokenJWT;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            var hashDigitado = CriptografiaUsuario.CriptografarSenha(senhaDigitada);

            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorNIFComTipoUsuario(loginDto.NIF);

            if (usuario == null)
            {
                throw new DomainException("NIF ou senha inválidos.");
            }

            if (usuario.Ativo == false)
            {
                throw new DomainException("Usuário inativo.");
            }

            if (!VerificarSenha(loginDto.Senha, usuario.Senha))
            {
                throw new DomainException("NIF ou senha inválidos.");
            }

            string token = _tokenJWT.GerarToken(usuario);

            TokenDto novoToken = new TokenDto()
            {
                Token = token,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                TipoUsuario = usuario.TipoUsuario.NomeTipo
            };

            return novoToken;
        }

        public void TrocarPrimeiraSenha(Guid usuarioId, TrocarPrimeiraSenhaDto dto)
        {
            Validar.ValidarSenha(dto.SenhaAtual);
            Validar.ValidarSenha(dto.NovaSenha);

            Usuario usuario = _repository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            if (!VerificarSenha(dto.SenhaAtual, usuario.Senha))
            {
                throw new DomainException("Senha atual inválida.");
            }

            if (dto.SenhaAtual == dto.NovaSenha)
            {
                throw new DomainException("A nova senha deve ser diferente da senha atual.");
            }

            usuario.Senha = CriptografiaUsuario.CriptografarSenha(dto.NovaSenha);
            usuario.PrimeiroAcesso = false;

            _repository.AtualizarSenha(usuario);
            _repository.AtualizarPrimeiroAcesso(usuario);
        }
    }
}
