using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoAlteracaoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoAlteracao> Listar()
        {
            return _context.TipoAlteracao
                .OrderBy(tipo => tipo.NomeTipo).ToList();
        }

        public TipoAlteracao BuscarPorId(Guid tipoAlteracaoId)
        {
            return _context.TipoAlteracao
                .FirstOrDefault(t => t.TipoAlteracaoID == tipoAlteracaoId)!;
        }

        public TipoAlteracao BuscarPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao
                .FirstOrDefault(t => t.NomeTipo.ToLower() == nomeTipo.ToLower())!;
        }

        public void Adicionar(TipoAlteracao tipoAlteracao)
        {
            _context.TipoAlteracao.Add(tipoAlteracao);
            _context.SaveChanges();
        }

        public void Atualizar(TipoAlteracao tipoAlteracao)
        {
            if (tipoAlteracao == null)
            {
                return;
            }

            TipoAlteracao tipoBanco = _context.TipoAlteracao.Find(tipoAlteracao.TipoAlteracaoID)!;

            if (tipoBanco == null)
            {
                return;
            }

            tipoBanco.NomeTipo = tipoAlteracao.NomeTipo;
            tipoBanco.TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID;

            _context.SaveChanges();
        }
    }
}
