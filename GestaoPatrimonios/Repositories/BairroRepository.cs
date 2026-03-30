using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public BairroRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Bairro> Listar()
        {
            return _context.Bairro
                .OrderBy(bairro => bairro.NomeBairro)
                .ToList();
        }

        public Bairro BuscarPorId(Guid bairroId)
        {
            return _context.Bairro.Find(bairroId)!;
        }

        public void Adicionar(Bairro bairro)
        {
            _context.Bairro.Add(bairro);
            _context.SaveChanges();
        }

        public void Atualizar(Bairro bairro)
        {
            if (bairro == null)
            {
                return;
            }

            Bairro bairroBanco = _context.Bairro.Find(bairro.BairroID)!;

            if (bairroBanco == null)
            {
                return;
            }

            bairroBanco.NomeBairro = bairro.NomeBairro;
            bairroBanco.CidadeID = bairro.CidadeID;

            _context.SaveChanges();
        }

        public Bairro BuscarPorNome(string nomeBairro, Guid cidadeId)
        {
            return _context.Bairro.FirstOrDefault(b =>
                b.NomeBairro.ToLower() == nomeBairro.ToLower() &&
                b.CidadeID == cidadeId)!;
        }

        public bool CidadeExiste(Guid cidadeId)
        {
            return _context.Cidade.Any(c => c.CidadeID == cidadeId);
        }
    }
}
