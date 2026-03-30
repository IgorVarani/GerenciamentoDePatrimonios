using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface ICidadeRepository
    {
        List<Cidade> Listar();
        Cidade BuscarPorId(Guid cidadeId);
        Cidade BuscarPorNomeEEstado(string nomeCidade, string nomeEstado);
        void Adicionar(Cidade cidade);
        void Atualizar(Cidade cidade);
    }
}
