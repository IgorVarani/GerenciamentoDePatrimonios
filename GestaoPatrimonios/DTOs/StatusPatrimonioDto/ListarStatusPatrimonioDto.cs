namespace GestaoPatrimonios.DTOs.StatusPatrimonioDto
{
    public class ListarStatusPatrimonioDto
    {
        public Guid StatusPatrimonioID { get; set; }
        public string NomeStatus { get; set; } = string.Empty;
    }
}
