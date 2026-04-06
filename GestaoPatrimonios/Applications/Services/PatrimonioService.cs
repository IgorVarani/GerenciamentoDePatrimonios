using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.PatrimonioDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Applications.Services
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;

        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarPatrimonioDto> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            if (patrimonios == null)
            {
                throw new DomainException("Patrimônios não existem.");
            }

            List<ListarPatrimonioDto> listarDTO = patrimonios.Select(varAux => new ListarPatrimonioDto
            {
                StatusPatrimonioID = varAux.StatusPatrimonioID,
                TipoPatrimonioID = varAux.TipoPatrimonioID,
                PatrimonioID = varAux.PatrimonioID,
                Valor = varAux.Valor,
                Denominacao = varAux.Denominacao,
                Imagem = varAux.Imagem,
                LocalizacaoID = varAux.LocalizacaoID,
                NumeroPatrimonio = varAux.NumeroPatrimonio
            }).ToList();

            return listarDTO;
        }

        public ListarPatrimonioDto BuscarPorId(Guid id)
        {
            Patrimonio? patrimonio = _repository.BuscarPorId(id);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não existe.");
            }

            ListarPatrimonioDto listarDTO = new ListarPatrimonioDto
            {
                StatusPatrimonioID = patrimonio.StatusPatrimonioID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                PatrimonioID = patrimonio.PatrimonioID,
                Valor = patrimonio.Valor,
                Denominacao = patrimonio.Denominacao,
                Imagem = patrimonio.Imagem,
                LocalizacaoID = patrimonio.LocalizacaoID,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio
            };

            return listarDTO;
        }

        public ListarPatrimonioDto BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(numeroPatrimonio, patrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não existe.");
            }

            ListarPatrimonioDto listarDTO = new ListarPatrimonioDto
            {
                StatusPatrimonioID = patrimonio.StatusPatrimonioID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                PatrimonioID = patrimonio.PatrimonioID,
                Valor = patrimonio.Valor,
                Denominacao = patrimonio.Denominacao,
                Imagem = patrimonio.Imagem,
                LocalizacaoID = patrimonio.LocalizacaoID,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio
            };

            return listarDTO;
        }

        public void Adicionar(ListarPatrimonioDto criarDTO)
        {
            Validar.ValidarNome(criarDTO.Denominacao);
            Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(criarDTO.NumeroPatrimonio, criarDTO.PatrimonioID);

            if (patrimonio != null)
            {
                throw new DomainException("Já existe um patrimônio com esse nome e número.");
            }

            Patrimonio patrimonioBanco = new Patrimonio
            {
                StatusPatrimonioID = criarDTO.StatusPatrimonioID,
                TipoPatrimonioID = criarDTO.TipoPatrimonioID,
                PatrimonioID = criarDTO.PatrimonioID,
                Valor = criarDTO.Valor,
                Denominacao = criarDTO.Denominacao,
                Imagem = criarDTO.Imagem,
                LocalizacaoID = criarDTO.LocalizacaoID,
                NumeroPatrimonio = criarDTO.NumeroPatrimonio
            };

            _repository.Adicionar(patrimonioBanco);
        }

        public void Atualizar(Guid id, CriarPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.Denominacao);
            Patrimonio patrimonioBanco = _repository.BuscarPorId(id);
            Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(dto.NumeroPatrimonio, id);

            patrimonioBanco.StatusPatrimonioID = dto.StatusPatrimonioID;
            patrimonioBanco.TipoPatrimonioID = dto.TipoPatrimonioID;
            patrimonioBanco.LocalizacaoID = dto.LocalizacaoID;
            patrimonioBanco.Valor = dto.Valor;
            patrimonioBanco.Denominacao = dto.Denominacao;
            patrimonioBanco.Imagem = dto.Imagem;
            patrimonioBanco.NumeroPatrimonio = dto.NumeroPatrimonio;

            _repository.Atualizar(patrimonioBanco);
        }

        public void AtualizarStatus(Guid id, AtualizarStatusPatrimonioDto dto)
        {
            Patrimonio patrimonioBanco = _repository.BuscarPorId(id);
            Patrimonio patrimonio = _repository.BuscarPorNumeroPatrimonio(dto.NumeroPatrimonio, id);


            patrimonioBanco.StatusPatrimonioID = dto.StatusPatrimonioID;

            _repository.Atualizar(patrimonioBanco);
        }
    }
}
