using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.Bairro;
using GestaoPatrimonios.DTOs.TipoPatrimonioDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class TipoPatrimonioService
    {
        private readonly ITipoPatrimonioRepository _repository;

        public TipoPatrimonioService(ITipoPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoPatrimonioDto> Listar()
        {
            List<TipoPatrimonio> tipos = _repository.Listar();

            List<ListarTipoPatrimonioDto> tiposDto = tipos.Select(tipo => new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipo.TipoPatrimonioID,
                NomeTipo = tipo.NomeTipo,
            }).ToList();

            return tiposDto;
        }

        public ListarTipoPatrimonioDto BuscarPorId(Guid tipoId)
        {
            TipoPatrimonio tipo = _repository.BuscarPorId(tipoId);

            if (tipo == null)
            {
                throw new DomainException("Tipo não encontrado.");
            }

            return new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipo.TipoPatrimonioID,
                NomeTipo = tipo.NomeTipo,
            };
        }

        public void Adicionar(CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Já existe um tipo com esse nome.");
            }

            TipoPatrimonio tipo = new TipoPatrimonio
            {
                NomeTipo = dto.NomeTipo,
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid tipoId, CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoBanco = _repository.BuscarPorId(tipoId);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo não encontrado.");
            }

            tipoBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
