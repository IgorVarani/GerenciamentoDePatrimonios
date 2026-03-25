using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface IAreaRepository
    {
        List<Area> Listar();

        Area BuscarPorId(Guid areaId);
        Area BuscarPorNome(string nomeArea);

        public void Adicionar(Area area);
        public void Atualizar(Area area);
    }
}
