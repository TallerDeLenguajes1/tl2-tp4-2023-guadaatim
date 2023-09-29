namespace EspacioCadeteria;
using EspacioCadete;
using EspacioPedido;
using EspacioCliente;
using System.ComponentModel;
using System.Data.Common;

public class Cadeteria
{
    private string? nombre;
    private string? telefono;
    private List<Cadete>? listadoCadetes;
    private List<Pedido> listadoPedidos;

    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        listadoCadetes = new List<Cadete>();
        listadoPedidos = new List<Pedido>();
    }

    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }

    //metodos

    public void AgregarCadetes(List<Cadete> listadoCadetes)
    {
        this.listadoCadetes = listadoCadetes;
    }

    public void DarDeAltaPedido(int num, string observacion, string nombre, string direccion, int telefono, string datosReferenciadeDireccion, int idCadete)
    {
        Cliente clienteNuevo = new Cliente(nombre, direccion, telefono, datosReferenciadeDireccion);
        Pedido pedidoNuevo = new Pedido(num, observacion, clienteNuevo);

        AgregarPedido(pedidoNuevo);
        AsignarCadeteAPedido(pedidoNuevo.Numero, idCadete);
    }

    public void AgregarPedido(Pedido pedidoNuevo)
    {
        listadoPedidos.Add(pedidoNuevo);
    }

    public void AsignarCadeteAPedido(int idPedido, int idCadete)
    {
        Cadete? cadeteElegido = listadoCadetes.Find(c => c.Id == idCadete);

        foreach (var pedido in listadoPedidos)
        {
            if (pedido.Numero == idPedido)
            {
                pedido.Cadete = cadeteElegido;
            }
        }
    }

        public Pedido BuscarPedido(int idPedido)
    {
        Pedido pedidoBuscado = new Pedido();
        
        foreach (var p in listadoPedidos)
        {
            if (p.Numero == idPedido)
            {
                pedidoBuscado = p;
                break;
            }
        }

        return pedidoBuscado;
    }

    public void EliminarPedido(int idPedido)
    {
        foreach (var pedido in listadoPedidos)
        {
            if (pedido.Numero == idPedido)
            {
                listadoPedidos.Remove(pedido);
                break;
            }
        }
    }

    public float JornalACobrar(int idCadete)
    {
        float total = 0;

        foreach (var pedido in listadoPedidos)
        {
            if (pedido.Cadete.Id == idCadete)
            {
                if (pedido.Estado == Estado.Entregado)
                {
                    total += 500;
                }
            }
        }
        return total;
    }

    public int CantidadEnviosPorCadete(int idCadete)
    {
        int total = listadoPedidos.Count(p => p.Cadete.Id == idCadete && p.Estado == Estado.Entregado);
        return total;
    }

    public void Informe(List<Cadete> listadoCadetes, int idCadete)
    {
        Console.WriteLine("-------INFORME-------");
        Console.WriteLine("Monto ganado: " + JornalACobrar(idCadete));
        Console.WriteLine("Cantidad de envios: " + CantidadEnviosPorCadete(idCadete));
    }
}