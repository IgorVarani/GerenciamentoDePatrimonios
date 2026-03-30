namespace GestaoPatrimonios.DTOs.EnderecoDto
{
    public class ListarEnderecoDto
    {
        public Guid EnderecoID { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public int Numero { get; set; }
        public Guid BairroID { get; set; }
    }
}
