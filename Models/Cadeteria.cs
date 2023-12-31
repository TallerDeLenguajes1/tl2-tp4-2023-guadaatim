namespace EspacioCadeteria;
using EspacioCadete;
using EspacioPedido;
using EspacioCliente;
using System.ComponentModel;
using System.Data.Common;
using EspacioAccesoADatos;

public class Cadeteria
{
    private string? nombre;
    private string? telefono;
    private List<Cadete>? listadoCadetes;
    private List<Pedido>? listadoPedidos;

    //singleton
    private static Cadeteria? instance;

    public static Cadeteria GetInstance()
    {
        if (instance == null)
        {
            Random random = new Random();
            var HelperJSON = new AccesoJSON();
            List<Cadeteria> listaCadeterias = HelperJSON.LeerArchivoCadeteria("Cadeterias.json");
            instance = listaCadeterias[random.Next(0, listaCadeterias.Count() + 1)];
            instance.AgregarCadetes(HelperJSON.LeerArchivoCadetes("Cadetes.json"));
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

    public List<Pedido> GetPedidos()
    {
        return this.listadoPedidos;
    }

    public List<Cadete> GetCadetes()
    {
        return this.listadoCadetes;
    }

    public bool AgregarCadetes(List<Cadete> listadoCadetes)
    {
        this.listadoCadetes = listadoCadetes;

        if (this.listadoCadetes != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool DarDeAltaPedido(int num, string observacion, string nombre, string direccion, int telefono, string datosReferenciadeDireccion, int idCadete)
    {
        Cliente clienteNuevo = new Cliente(nombre, direccion, telefono, datosReferenciadeDireccion);
        Pedido pedidoNuevo = new Pedido(num, observacion, clienteNuevo);

        bool control = AgregarPedido(pedidoNuevo);
        AsignarCadeteAPedido(pedidoNuevo.Numero, idCadete);

        if (control)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool AgregarPedido(Pedido pedidoNuevo)
    {
        listadoPedidos.Add(pedidoNuevo);

        if((listadoPedidos.Find(p => p.Numero == pedidoNuevo.Numero) != null))
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool AsignarCadeteAPedido(int idPedido, int idCadete)
    {
        Cadete? cadeteElegido = listadoCadetes.Find(c => c.Id == idCadete);        
        Pedido? pedidoAsignar = listadoPedidos.Find(p => p.Numero == idPedido);
        
        pedidoAsignar.Cadete = cadeteElegido;

        if (pedidoAsignar.Cadete != null)
        {
            return true;
        } else
        {
            return false;
        }

    }

    public Pedido BuscarPedido(int idPedido)
    {
        Pedido? pedidoBuscado = new Pedido();

        pedidoBuscado = listadoPedidos.Find(p => p.Numero == idPedido);

        return pedidoBuscado;
    }

    public bool EliminarPedido(int idPedido)
    {
        
        Pedido? pedidoEliminar = listadoPedidos.Find(p => p.Numero == idPedido);
        listadoPedidos.Remove(pedidoEliminar);

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