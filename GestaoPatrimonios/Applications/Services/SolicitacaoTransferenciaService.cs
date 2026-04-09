using GestaoPatrimonios.Applications.Regras;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.DTOs.SolicitacaoTransferenciaDto;
using GestaoPatrimonios.Exceptions;
using GestaoPatrimonios.Interfaces;
using Microsoft.AspNetCore.Http.Timeouts;

namespace GestaoPatrimonios.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<ListarSolicitacaoTransferenciaDto> Listar()
        {
            List<SolicitacaoTransferencia> solicitacoes = _repository.Listar();

            List<ListarSolicitacaoTransferenciaDto> solicitacoesDto = solicitacoes.Select(solicitacao => new ListarSolicitacaoTransferenciaDto()
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalizacaoID
            }).ToList();

            return solicitacoesDto;
        }

        public ListarSolicitacaoTransferenciaDto BuscarPorId(Guid transferenciaId)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferência não encontrada.");
            }

            ListarSolicitacaoTransferenciaDto solicitacaoDto = new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalizacaoID
            };

            return solicitacaoDto;
        }

        public void Adicionar(Guid usuarioId, CriarSolicitacaoTransferenciaDto dto)
        {
            Validar.ValidarJustificativa(dto.Justificativa);

            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(dto.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.LocalizacaoExiste(dto.LocalizacaoID))
            {
                throw new DomainException("Localização não encontrada.");
            }

            if (patrimonio.LocalizacaoID == dto.LocalizacaoID)
            {
                throw new DomainException("O patrimônio já está nessa localização.");
            }

            if (_repository.ExisteSolicitacaoPendente(dto.PatrimonioID))
            {
                throw new DomainException("Já existe uma solicitação pendente para esse patrimônio");
            }

            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoID);

                if (!usuarioResponsavel) // Caso retorne "false".
                {
                    throw new DomainException("O responsável só pode solicitar transferência de patrimônio do ambiente ao qual está vinculado.");
                }
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação.");

            if (statusPendente == null)
            {
                throw new DomainException("Status de transferência pendente não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = new SolicitacaoTransferencia
            {
                DataCriacaoSolicitante = DateTime.Now,
                Justificativa = dto.Justificativa,
                StatusTransferenciaID = statusPendente.StatusTransferenciaID,
                UsuarioIDSolicitacao = usuarioId,
                UsuarioIDAprovacao = null,
                PatrimonioID = dto.PatrimonioID,
                LocalizacaoID = dto.LocalizacaoID,
            };

            _repository.Adicionar(solicitacao);
        }

        public void Responder(Guid transferenciaId, Guid usuarioId, ResponderSolicitacaoTransferenciaDto dto)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);


            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferência não encontrada.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(solicitacao.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação.");

            if (statusPendente == null)
            {
                throw new DomainException("Status pendente não encontrada.");
            }

            if (solicitacao.StatusTransferenciaID != statusPendente.StatusTransferenciaID)
            {
                throw new DomainException("Essa solicitação já foi respondida.");
            }

            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoID);

                if (!usuarioResponsavel)
                {
                    throw new DomainException("Somente o responsável do ambiente de origem pode aprovar ou rejeitar essa solicitação.");
                }

                StatusTransferencia statusResposta;

                if (dto.Aprovado)
                {
                    statusResposta = _repository.BuscarStatusTransferenciaPorNome("Aprovado");
                }
                else
                {
                    statusResposta = _repository.BuscarStatusTransferenciaPorNome("Recusado");
                }

                if (statusResposta == null)
                {
                    throw new DomainException("Status de resposta da transferência não encontrado.");
                }

                solicitacao.StatusTransferenciaID = statusResposta.StatusTransferenciaID;
                solicitacao.UsuarioIDAprovacao = usuarioId;
                solicitacao.DataResposta = DateTime.Now;

                _repository.Atualizar(solicitacao);
            }

            if (dto.Aprovado)
            {
                StatusPatrimonio statusTransferido = _repository.BuscarStatusPatrimonioPorNome("Transferido");

                if (statusTransferido == null)
                {
                    throw new DomainException("Status de patrimônio 'Transferido' não encontrado.");
                }

                TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Transferência");

                if (tipoAlteracao == null)
                {
                    throw new DomainException("Tipo alteração 'Transferência' não encontrado.");
                }

                patrimonio.LocalizacaoID = solicitacao.LocalizacaoID;
                patrimonio.StatusPatrimonioID = statusTransferido.StatusPatrimonioID;

                _repository.AtualizarPatrimonio(patrimonio);

                LogPatrimonio log = new LogPatrimonio
                {
                    DataTransferencia = DateTime.Now,
                    TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                    StatusPatrimonioID = statusTransferido.StatusPatrimonioID,
                    PatrimonioID = patrimonio.PatrimonioID,
                    UsuarioID = usuarioId,
                    LocalizacaoID = patrimonio.LocalizacaoID,
                };

                _repository.AdicionarLog(log);
            }
        }
    }
}
