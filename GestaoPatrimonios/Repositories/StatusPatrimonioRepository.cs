using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio
                .OrderBy(tipo => tipo.NomeStatus).ToList();
        }

        public StatusPatrimonio BuscarPorId(Guid statusId)
        {
            return _context.StatusPatrimonio
                .FirstOrDefault(s => s.StatusPatrimonioID == statusId)!;
        }

        public StatusPatrimonio BuscarPorNome(string status)
        {
            return _context.StatusPatrimonio
                .FirstOrDefault(s => s.NomeStatus.ToLower() == status.ToLower())!;
        }

        public void Adicionar(StatusPatrimonio status)
        {
            _context.StatusPatrimonio.Add(status);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio status)
        {
            if (status == null)
            {
                return;
            }

            StatusPatrimonio statusBanco = _context.StatusPatrimonio.Find(status.StatusPatrimonioID)!;

            if (statusBanco == null)
            {
                return;
            }

            statusBanco.NomeStatus = status.NomeStatus;
            statusBanco.StatusPatrimonioID = status.StatusPatrimonioID;

            _context.SaveChanges();
        }
    }
}
