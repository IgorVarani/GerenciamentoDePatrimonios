using GestaoPatrimonios.Contexts;
using GestaoPatrimonios.Domains;
using GestaoPatrimonios.Interfaces;

namespace GestaoPatrimonios.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public EnderecoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(endereco => endereco.Logradouro).ToList();
        }

        public Endereco BuscarPorId(Guid enderecoId)
        {
            return _context.Endereco.Find(enderecoId)!;
        }

        public Endereco? BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId)
        {
            return _context.Endereco.FirstOrDefault(endereco =>
                endereco.Logradouro.ToLower() == logradouro.ToLower() &&
                endereco.Numero == numero &&
                endereco.BairroID == bairroId);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
            {
                return;
            }

            Endereco enderecoBanco = _context.Endereco.Find(endereco.EnderecoID)!;

            if (enderecoBanco == null)
            {
                return;
            }

            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Complemento = endereco.Complemento;
            enderecoBanco.CEP = endereco.CEP;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.BairroID = endereco.BairroID;

            _context.SaveChanges();
        }

        public bool BairroExiste(Guid bairroId)
        {
            return _context.Bairro.Any(b => b.BairroID == bairroId);
        }
    }
}
