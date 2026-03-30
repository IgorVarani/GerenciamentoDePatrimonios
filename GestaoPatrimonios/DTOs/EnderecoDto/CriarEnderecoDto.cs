namespace GestaoPatrimonios.DTOs.EnderecoDto
{
    public class CriarEnderecoDto
    {
        public string Logradouro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public int Numero { get; set; }
        public Guid BairroID { get; set; }
    }
}
