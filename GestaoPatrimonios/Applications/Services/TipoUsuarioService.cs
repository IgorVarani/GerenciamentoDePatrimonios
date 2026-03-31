using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.Bairro;
using GestaoPatrimonios.DTOs.TipoUsuarioDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _repository;

        public TipoUsuarioService(ITipoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoUsuarioDto> Listar()
        {
            List<TipoUsuario> tipos = _repository.Listar();

            List<ListarTipoUsuarioDto> tiposDto = tipos.Select(tipo => new ListarTipoUsuarioDto
            {
                TipoUsuarioID = tipo.TipoUsuarioID,
                NomeTipo = tipo.NomeTipo,
            }).ToList();

            return tiposDto;
        }

        public ListarTipoUsuarioDto BuscarPorId(Guid tipoId)
        {
            TipoUsuario tipoUsuario = _repository.BuscarPorId(tipoId);

            if (tipoUsuario == null)
            {
                throw new DomainException("Bairro não encontrado.");
            }

            return new ListarTipoUsuarioDto
            {
                TipoUsuarioID = tipoUsuario.TipoUsuarioID,
                NomeTipo = tipoUsuario.NomeTipo
            };
        }

        public void Adicionar(CriarTipoUsuarioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoUsuario tipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Já existe um tipo com esse nome.");
            }

            TipoUsuario tipo = new TipoUsuario
            {
                NomeTipo = dto.NomeTipo,
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid tipoId, CriarTipoUsuarioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoUsuario tipoBanco = _repository.BuscarPorId(tipoId);
            
            if (tipoBanco == null)
            {
                throw new DomainException("Tipo não encontrado.");
            }

            TipoUsuario usuarioExistente = _repository.BuscarPorNome(dto.NomeTipo);

            tipoBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
