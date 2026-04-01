using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.Bairro;
using GestaoPatrimonios.DTOs.CargoDto;
using GestaoPatrimonios.DTOs.TipoPatrimonioDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCargoDto> Listar()
        {
            List<Cargo> tipos = _repository.Listar();

            List<ListarCargoDto> tiposDto = tipos.Select(tipo => new ListarCargoDto
            {
                CargoID = tipo.CargoID,
                NomeCargo = tipo.NomeCargo,
            }).ToList();

            return tiposDto;
        }

        public ListarCargoDto BuscarPorId(Guid cargoId)
        {
            Cargo cargo = _repository.BuscarPorId(cargoId);

            if (cargo == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            return new ListarCargoDto
            {
                CargoID = cargo.CargoID,
                NomeCargo = cargo.NomeCargo,
            };
        }

        public void Adicionar(CriarCargoDto dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoExistente = _repository.BuscarPorNome(dto.NomeCargo);

            if (cargoExistente != null)
            {
                throw new DomainException("Já existe um cargo com esse nome.");
            }

            Cargo cargo = new Cargo
            {
                NomeCargo = dto.NomeCargo,
            };

            _repository.Adicionar(cargo);
        }

        public void Atualizar(Guid cargoId, CriarCargoDto dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoBanco = _repository.BuscarPorId(cargoId);

            if (cargoBanco == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            cargoBanco.NomeCargo = dto.NomeCargo;

            _repository.Atualizar(cargoBanco);
        }
    }
}
