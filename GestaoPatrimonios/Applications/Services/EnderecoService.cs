using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.AreaDto;
using GestaoPatrimonios.DTOs.CidadeDto;
using GestaoPatrimonios.DTOs.EnderecoDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();

            List<ListarEnderecoDto> enderecosDto = enderecos.Select(endereco => new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento!,
                CEP = endereco.CEP!,
                BairroID = endereco.BairroID,
            }).ToList();

            return enderecosDto;
        }

        public ListarEnderecoDto BuscarPorId(Guid enderecoId)
        {
            Endereco endereco = _repository.BuscarPorId(enderecoId);

            if (endereco == null)
            {
                throw new DomainException("Bairro não encontrado.");
            }

            return new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento!,
                CEP = endereco.CEP!,
                BairroID = endereco.BairroID,
            };
        }

        public void Adicionar(CriarEnderecoDto dto)
        {
            Validar.ValidarNome(dto.Logradouro);

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(
                dto.Logradouro,
                dto.Numero,
                dto.BairroID)!;

            if (enderecoExistente != null)
            {
                throw new DomainException("Já existe um endereço assim registrado.");
            }

            if (!_repository.BairroExiste(dto.BairroID))
            {
                throw new DomainException("Bairro informado não existe.");
            }

            Endereco endereco = new Endereco
            {
                Logradouro = dto.Logradouro,
                Complemento = dto.Complemento,
                CEP = dto.CEP,
                BairroID = dto.BairroID,
            };

            _repository.Adicionar(endereco);
        }

        public void Atualizar(Guid enderecoId, CriarEnderecoDto dto)
        {
            Validar.ValidarNome(dto.Logradouro);

            Endereco enderecoBanco = _repository.BuscarPorId(enderecoId);

            if (enderecoBanco == null)
            {
                throw new DomainException("Endereco não encontrado.");
            }

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(
                dto.Logradouro,
                dto.Numero,
                dto.BairroID)!;

            if (enderecoExistente != null && enderecoExistente.EnderecoID != enderecoId)
            {
                throw new DomainException("Já existe um endereço assim registrado.");
            }

            if (!_repository.BairroExiste(dto.BairroID))
            {
                throw new DomainException("Bairro informado não existe.");
            }

            enderecoBanco.Logradouro = dto.Logradouro;
            enderecoBanco.Complemento = dto.Complemento;
            enderecoBanco.CEP = dto.CEP;
            enderecoBanco.BairroID = dto.BairroID;

            _repository.Atualizar(enderecoBanco);
        }
    }
}
