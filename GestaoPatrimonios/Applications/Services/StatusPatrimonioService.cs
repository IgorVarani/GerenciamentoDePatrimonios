using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.Bairro;
using GestaoPatrimonios.DTOs.StatusPatrimonioDto;
using GestaoPatrimonios.DTOs.StatusTransferenciaDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;

        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusPatrimonioDto> Listar()
        {
            List<StatusPatrimonio> statuss = _repository.Listar();

            List<ListarStatusPatrimonioDto> statussDto = statuss.Select(statuss => new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = statuss.StatusPatrimonioID,
                NomeStatus = statuss.NomeStatus,
            }).ToList();

            return statussDto;
        }

        public ListarStatusPatrimonioDto BuscarPorId(Guid statusId)
        {
            StatusPatrimonio status = _repository.BuscarPorId(statusId);

            if (status == null)
            {
                throw new DomainException("Status não encontrado.");
            }

            return new ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = status.StatusPatrimonioID,
                NomeStatus = status.NomeStatus,
            };
        }

        public void Adicionar(CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um status com esse nome.");
            }

            StatusPatrimonio status = new StatusPatrimonio
            {
                NomeStatus = dto.NomeStatus,
            };

            _repository.Adicionar(status);
        }

        public void Atualizar(Guid statusId, CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio statusBanco = _repository.BuscarPorId(statusId);

            if (statusBanco == null)
            {
                throw new DomainException("Status não encontrado.");
            }

            statusBanco.NomeStatus = dto.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
}
