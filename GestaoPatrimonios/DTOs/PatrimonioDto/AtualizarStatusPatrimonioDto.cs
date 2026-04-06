namespace GestaoPatrimonios.DTOs.PatrimonioDto
{
    public class AtualizarStatusPatrimonioDto
    {
        public Guid PatrimonioID { get; set; } = Guid.Empty;
        public Guid StatusPatrimonioID { get; set; } = Guid.Empty;

        public string Denominacao { get; set; } = string.Empty;
        public string NumeroPatrimonio { get; set; } = string.Empty;
    }
}
