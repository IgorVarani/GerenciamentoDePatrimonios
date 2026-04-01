using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusTransferenciaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusTransferencia> Listar()
        {
            return _context.StatusTransferencia
                .OrderBy(tipo => tipo.NomeStatus).ToList();
        }

        public StatusTransferencia BuscarPorId(Guid statusId)
        {
            return _context.StatusTransferencia
                .FirstOrDefault(s => s.StatusTransferenciaID == statusId)!;
        }

        public StatusTransferencia BuscarPorNome(string status)
        {
            return _context.StatusTransferencia
                .FirstOrDefault(s => s.NomeStatus.ToLower() == status.ToLower())!;
        }

        public void Adicionar(StatusTransferencia status)
        {
            _context.StatusTransferencia.Add(status);
            _context.SaveChanges();
        }

        public void Atualizar(StatusTransferencia status)
        {
            if (status == null)
            {
                return;
            }

            StatusTransferencia statusBanco = _context.StatusTransferencia.Find(status.StatusTransferenciaID)!;

            if (statusBanco == null)
            {
                return;
            }

            statusBanco.NomeStatus = status.NomeStatus;
            statusBanco.StatusTransferenciaID = status.StatusTransferenciaID;

            _context.SaveChanges();
        }
    }
}
