using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        public List<SolicitacaoTransferencia> Listar();
        public SolicitacaoTransferencia BuscarPorId(Guid id);

        bool ExisteSolicitacaoPendente (Guid patrimonioId);
        bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId);
        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);
        void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);
        bool LocalizacaoExiste(Guid localizacaoId);
        Patrimonio BuscarPatrimonioPorId(Guid patrimonioId);
        StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);
        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
        void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia);
        void AtualizarPatrimonio(Patrimonio patrimonio);
        void AdicionarLog(LogPatrimonio logPatrimonio);
    }
}
