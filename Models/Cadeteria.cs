namespace EspacioCadeteria;
using EspacioCadete;
using EspacioPedido;
using EspacioCliente;
using EspacioAccesoADatosCadeteria;
using EspacioAccesoADatosCadetes;
using EspacioAccesoADatosPedidos;
using System.ComponentModel;
using System.Data.Common;

public class Cadeteria
{
    private string? nombre;
    private string? telefono;
    private List<Cadete>? listadoCadetes;
    private List<Pedido>? listadoPedidos;
    private AccesoADatosCadetes? accesoCadetes;
    private AccesoADatosPedidos? accesoPedidos;
    private static Cadeteria? instance;

    public static Cadeteria GetInstance()
    {
        if (instance == null)
        {
            AccesoADatosCadeteria HelperCadeteria = new();
            instance = HelperCadeteria.Obtener();
            instance.accesoCadetes = new AccesoADatosCadetes();
            instance.accesoPedidos = new AccesoADatosPedidos();
            instance.AgregarCadetes();
            instance.accesoPedidos.Obtener();

        }
        return instance;
    }
    
    public Cadeteria()
    {
    }

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
    public bool AgregarCadetes()
    {
        this.listadoCadetes = accesoCadetes.Obtener();

        if (this.listadoCadetes != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public List<Pedido> GetPedidos()
    {
        return accesoPedidos.Obtener();
    }

    public List<Cadete> GetCadetes()
    {
        return this.listadoCadetes;
    }

    public bool DarDeAltaPedido(int num, string observacion, string nombre, string direccion, int telefono, string datosReferenciadeDireccion, int idCadete)
    {
        Cliente clienteNuevo = new Cliente(nombre, direccion, telefono, datosReferenciadeDireccion);
        Pedido pedidoNuevo = new Pedido(num, observacion, clienteNuevo);

        
        //AgregarPedido(pedidoNuevo);

        return true;
    }

    public bool AgregarPedido(Pedido pedidoNuevo, int idCadete)
    {
        listadoPedidos = accesoPedidos.Obtener();
        pedidoNuevo = AsignarCadeteAPedido(pedidoNuevo, idCadete);
        listadoPedidos.Add(pedidoNuevo);
        pedidoNuevo.Numero = listadoPedidos.Count();
        
        if (accesoPedidos.Guardar(listadoPedidos))
        {
            return true;    
        } else
        {
            return false;
        }
    }

//controlar
    public Pedido AsignarCadeteAPedido(Pedido pedidoAsignar, int idCadete)
    {
        listadoCadetes = accesoCadetes.Obtener();
        Cadete? cadeteElegido = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);   
        
        pedidoAsignar.Cadete = cadeteElegido;

        return pedidoAsignar;
    }

    public Pedido BuscarPedido(int idPedido)
    {
        listadoPedidos = accesoPedidos.Obtener();
        Pedido? pedidoBuscado = listadoPedidos.Find(p => p.Numero == idPedido);

        return pedidoBuscado;
    }

//controlar
    public bool CambiarEstadoPedido(int idPedido)
    {
        listadoPedidos = accesoPedidos.Obtener();
        Pedido? pedidoCambiar = listadoPedidos.FirstOrDefault(p => p.Numero == idPedido);
        pedidoCambiar.CambiarEstado();
        listadoPedidos.Add(pedidoCambiar);
        accesoPedidos.Guardar(listadoPedidos);
        return true;
    }
//controlar
    public bool EliminarPedido(int idPedido)
    {
        Pedido? pedidoEliminar = listadoPedidos.Find(p => p.Numero == idPedido);
        listadoPedidos.Remove(pedidoEliminar);
        accesoPedidos.Guardar(listadoPedidos);

        if ((listadoPedidos.Find(p => p.Numero == pedidoEliminar.Numero) != null))
        {
            return false;
        } else
        {
            return true;
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
}