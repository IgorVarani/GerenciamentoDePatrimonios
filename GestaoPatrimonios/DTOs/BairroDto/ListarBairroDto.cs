namespace GestaoPatrimonios.DTOs.Bairro
{
    public class ListarBairroDto
    {
        public Guid BairroID { get; set; }
        public string NomeBairro { get; set; } = string.Empty;
        public Guid CidadeID { get; set; }
    }
}
