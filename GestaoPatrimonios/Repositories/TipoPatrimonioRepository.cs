using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio
                .OrderBy(tipo => tipo.NomeTipo).ToList();
        }

        public TipoPatrimonio BuscarPorId(Guid tipoPatrimonioId)
        {
            return _context.TipoPatrimonio
                .FirstOrDefault(t => t.TipoPatrimonioID == tipoPatrimonioId)!;
        }

        public TipoPatrimonio BuscarPorNome(string nomeTipo)
        {
            return _context.TipoPatrimonio
                .FirstOrDefault(t => t.NomeTipo.ToLower() == nomeTipo.ToLower())!;
        }

        public void Adicionar(TipoPatrimonio tipoPatrimonio)
        {
            _context.TipoPatrimonio.Add(tipoPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipoPatrimonio)
        {
            if (tipoPatrimonio == null)
            {
                return;
            }

            TipoPatrimonio tipoBanco = _context.TipoPatrimonio.Find(tipoPatrimonio.TipoPatrimonioID)!;

            if (tipoBanco == null)
            {
                return;
            }

            tipoBanco.NomeTipo = tipoPatrimonio.NomeTipo;
            tipoBanco.TipoPatrimonioID = tipoPatrimonio.TipoPatrimonioID;

            _context.SaveChanges();
        }

    }
}
