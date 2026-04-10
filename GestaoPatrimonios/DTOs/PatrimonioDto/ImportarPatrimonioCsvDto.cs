namespace GestaoPatrimonios.DTOs.PatrimonioDto
{
    public class ImportarPatrimonioCsvDto
    {
        public string NumeroPatrimonio { get; set; } = string.Empty;
        public string Denominacao { get; set; } = string.Empty;
        public string? DataIncorporacao { get; set; } = string.Empty;
        public string? ValorAquisicao { get; set; } = string.Empty;
    }
}
