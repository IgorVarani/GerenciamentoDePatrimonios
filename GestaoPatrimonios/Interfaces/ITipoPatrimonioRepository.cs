using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface ITipoPatrimonioRepository
    {
        List<TipoPatrimonio> Listar();
        TipoPatrimonio BuscarPorId(Guid tipoPatrimonioId);
        TipoPatrimonio BuscarPorNome(string nomeTipo);
        void Adicionar(TipoPatrimonio tipoPatrimonio);
        void Atualizar(TipoPatrimonio tipoPatrimonio);
    }
}
