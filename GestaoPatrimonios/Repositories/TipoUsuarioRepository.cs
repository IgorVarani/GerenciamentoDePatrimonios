using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoUsuarioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuario
                .OrderBy(tUsuario => tUsuario.NomeTipo).ToList();
        }

        public TipoUsuario BuscarPorId(Guid tUsuarioId)
        {
            return _context.TipoUsuario.Find(tUsuarioId)!;
        }

        public TipoUsuario BuscarPorNome(string nomeTipo)
        {
            return _context.TipoUsuario
                .FirstOrDefault(t => t.NomeTipo.ToLower() == nomeTipo.ToLower())!;
        }

        public void Adicionar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuario.Add(tipoUsuario);
            _context.SaveChanges();
        }

        public void Atualizar(TipoUsuario tipoUsuario)
        {
            if (tipoUsuario == null)
            {
                return;
            }

            TipoUsuario tipoUsuarioBanco = _context.TipoUsuario.Find(tipoUsuario.TipoUsuarioID)!;

            if (tipoUsuarioBanco == null)
            {
                return;
            }

            tipoUsuarioBanco.NomeTipo = tipoUsuario.NomeTipo;
            tipoUsuarioBanco.TipoUsuarioID = tipoUsuario.TipoUsuarioID;

            _context.SaveChanges();
        }
    }
}