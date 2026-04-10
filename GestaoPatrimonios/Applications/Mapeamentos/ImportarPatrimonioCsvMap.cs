using CsvHelper.Configuration;
using GestaoPatrimonios.DTOs.PatrimonioDto;

namespace GestaoPatrimonios.Applications.Mapeamentos
{
    // ClassMap --> é um "tradutor de colunas", definindo como ler o CSV.
    public class ImportarPatrimonioCsvMap : ClassMap<ImportarPatrimonioCsvDto>
    {
        // Definindo os mapeamentos:
        public ImportarPatrimonioCsvMap()
        {
            // Map --> escolhe a propriedade da DTO.
            // Name --> diz qual o nome da coluna do CSV para essa propriedade.
            Map(m => m.NumeroPatrimonio).Name("N° invent.");
            Map(m => m.Denominacao).Name("Denominação do imobilizado");
            Map(m => m.DataIncorporacao).Name("Dt.incorp.");
            Map(m => m.ValorAquisicao).Name("ValAquis.");
        }
    }
}
