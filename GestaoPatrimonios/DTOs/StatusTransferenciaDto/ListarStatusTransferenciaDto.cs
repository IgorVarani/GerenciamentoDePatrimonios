namespace GestaoPatrimonios.DTOs.StatusTransferenciaDto
{
    public class ListarStatusTransferenciaDto
    {
        public Guid StatusTransferenciaID { get; set; }
        public string NomeStatus { get; set; } = string.Empty;
    }
}
