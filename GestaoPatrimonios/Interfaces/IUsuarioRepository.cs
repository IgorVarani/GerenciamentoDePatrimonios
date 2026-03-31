using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
    }
}
