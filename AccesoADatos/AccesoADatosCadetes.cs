using EspacioCadete;
using System.Text.Json;
namespace EspacioAccesoADatosCadetes;

public class AccesoADatosCadetes
{
    public List<Cadete> Obtener()
    {
        string? jsonString = File.ReadAllText("Cadetes.json");
        List<Cadete>? listadoCadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonString);
        return listadoCadetes;
    }

    public bool Guardar(List<Cadete> cadetes)
    {
        string? listadoCadetesJson = JsonSerializer.Serialize(cadetes);
        File.WriteAllText("Cadetes.json", listadoCadetesJson);

        if (listadoCadetesJson != null)
        {
            return true;
        } else
        {
            return false;
        }
    }
}