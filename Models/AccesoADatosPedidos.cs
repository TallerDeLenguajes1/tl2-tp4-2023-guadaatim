using System.Text.Json;
using EspacioPedido;
namespace EspacioAccesoADatosPedidos;

public class AccesoADatosPedidos
{
    public List<Pedido> Obtener()
    {
        string? jsonString = File.ReadAllText("Pedidos.json");
        List<Pedido> listadoPedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonString);
        return listadoPedidos;
    }

    public bool Guardar(List<Pedido> pedidos)
    {
        string listadoPedidosJson = JsonSerializer.Serialize(pedidos);
        File.WriteAllText("Pedidos.json", listadoPedidosJson);
        
        if (listadoPedidosJson != null)
        {
            return true;
        } else
        {
            return false;
        }
    }
}