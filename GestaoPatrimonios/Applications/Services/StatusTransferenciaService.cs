using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.Bairro;
using GestaoPatrimonios.DTOs.StatusTransferenciaDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class StatusTransferenciaService
    {
        private readonly IStatusTransferenciaRepository _repository;

        public StatusTransferenciaService(IStatusTransferenciaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusTransferenciaDto> Listar()
        {
            List<StatusTransferencia> statuss = _repository.Listar();

            List<ListarStatusTransferenciaDto> statussDto = statuss.Select(statuss => new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = statuss.StatusTransferenciaID,
                NomeStatus = statuss.NomeStatus,
            }).ToList();

            return statussDto;
        }

        public ListarStatusTransferenciaDto BuscarPorId(Guid statusId)
        {
            StatusTransferencia status = _repository.BuscarPorId(statusId);

            if (status == null)
            {
                throw new DomainException("Status não encontrado.");
            }

            return new ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = status.StatusTransferenciaID,
                NomeStatus = status.NomeStatus,
            };
        }

        public void Adicionar(CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusTransferencia statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um status com esse nome.");
            }

            StatusTransferencia status = new StatusTransferencia
            {
                NomeStatus = dto.NomeStatus,
            };

            _repository.Adicionar(status);
        }

        public void Atualizar(Guid statusId, CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusTransferencia statusBanco = _repository.BuscarPorId(statusId);

            if (statusBanco == null)
            {
                throw new DomainException("Status não encontrado.");
            }

            statusBanco.NomeStatus = dto.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
}
