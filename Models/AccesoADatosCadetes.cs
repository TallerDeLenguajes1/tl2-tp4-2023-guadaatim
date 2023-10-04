using EspacioCadete;
using System.Text.Json;
namespace EspacioAccesoADatosCadetes;

public class AccesoADatosCadetes
{
    public List<Cadete> Obtener()
    {
        string? jsonString = File.ReadAllText("../Cadetes.json");
        List<Cadete> listadoCadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonString);
        return listadoCadetes;
    }
}