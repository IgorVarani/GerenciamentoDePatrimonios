using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public PatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Patrimonio> Listar()
        {
            return _context.Patrimonio
                .OrderBy(patrimonio => patrimonio.Denominacao)
                .ToList();
        }

        public Patrimonio BuscarPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId)!;
        }

        public Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId)
        {
            return _context.Patrimonio.FirstOrDefault(p =>
                p.NumeroPatrimonio.ToLower() == numeroPatrimonio.ToLower() &&
                p.PatrimonioID != patrimonioId)!;
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(l => l.LocalizacaoID == localizacaoId);
        }

        /*
        public bool TipoPatrimonioExiste(Guid tipoPatrimonioId)
        {
            return _context.TipoPatrimonio.Any(t => t.TipoPatrimonioID == tipoPatrimonioId);
        }
        */

        public bool StatusPatrimonioExiste(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Any(s => s.StatusPatrimonioID == statusPatrimonioId);
        }

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID)!;

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.PatrimonioID = patrimonio.PatrimonioID;
            patrimonioBanco.LocalizacaoID = patrimonio.LocalizacaoID;
            patrimonioBanco.TipoPatrimonioID = patrimonio.TipoPatrimonioID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio? patrimonioBanco = _context.Patrimonio.FirstOrDefault
                (varAux => varAux.PatrimonioID == patrimonio.PatrimonioID && varAux.StatusPatrimonioID == patrimonio.StatusPatrimonioID);

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.PatrimonioID = patrimonio.PatrimonioID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;
        }
    }
}
