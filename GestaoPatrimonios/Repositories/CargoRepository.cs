using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public CargoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Cargo> Listar()
        {
            return _context.Cargo
                .OrderBy(tipo => tipo.NomeCargo).ToList();
        }

        public Cargo BuscarPorId(Guid cargoId)
        {
            return _context.Cargo
                .FirstOrDefault(t => t.CargoID == cargoId)!;
        }

        public Cargo BuscarPorNome(string cargo)
        {
            return _context.Cargo
                .FirstOrDefault(t => t.NomeCargo.ToLower() == cargo.ToLower())!;
        }

        public void Adicionar(Cargo cargo)
        {
            _context.Cargo.Add(cargo);
            _context.SaveChanges();
        }

        public void Atualizar(Cargo cargo)
        {
            if (cargo == null)
            {
                return;
            }

            Cargo cargoBanco = _context.Cargo.Find(cargo.CargoID)!;

            if (cargoBanco == null)
            {
                return;
            }

            cargoBanco.NomeCargo = cargo.NomeCargo;
            cargoBanco.CargoID = cargo.CargoID;

            _context.SaveChanges();
        }
    }
}
