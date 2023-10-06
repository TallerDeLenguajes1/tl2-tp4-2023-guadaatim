using EspacioCadeteria;
using System.Text.Json;
namespace EspacioAccesoADatosCadeteria;


public class AccesoADatosCadeteria
{
    public Cadeteria Obtener()
    {
        Random random = new Random();
        string? jsonString = File.ReadAllText("Cadeterias.json");
        List<Cadeteria>? listadoCadeterias = JsonSerializer.Deserialize<List<Cadeteria>>(jsonString);
        return listadoCadeterias[random.Next(0, 5)];
    }
}