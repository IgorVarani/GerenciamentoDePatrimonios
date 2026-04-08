using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorId(Guid patrimonioId);
        Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null);

        bool LocalizacaoExiste(Guid localizacaoId);
        bool StatusPatrimonioExiste(Guid statusPatrimonioId);
        //bool TipoPatrimonioExiste(Guid tipoPatrimonioId);

        void Adicionar(Patrimonio patrimonio);
        void Atualizar(Patrimonio patrimonio);
        void AtualizarStatus(Patrimonio patrimonio);
    }
}
