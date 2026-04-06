using GestaoPatrimonios.Domains;

namespace GestaoPatrimonios.DTOs.PatrimonioDto
{
    public class CriarPatrimonioDto
    {
        public string Denominacao { get; set; } = string.Empty;
        public string NumeroPatrimonio { get; set; } = string.Empty;
        public decimal? Valor { get; set; }
        public string? Imagem { get; set; } = string.Empty;

        public Guid LocalizacaoID { get; set; }
        public Guid TipoPatrimonioID { get; set; }
        public Guid StatusPatrimonioID { get; set; }
    }
}
