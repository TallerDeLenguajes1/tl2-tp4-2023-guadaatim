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

    public Cadeteria(AccesoADatosCadeteria accesoADatosCadeteria, AccesoADatosCadetes accesoADatosCadetes, AccesoADatosPedidos accesoADatosPedidos)
    {
        Cadeteria cadAux = accesoADatosCadeteria.Obtener();
        this.nombre = cadAux.nombre;
        this.telefono = cadAux.telefono;
        accesoCadetes = new AccesoADatosCadetes();
        accesoPedidos = new AccesoADatosPedidos();
        //this.AgregarCadetes();
        listadoCadetes = accesoCadetes.Obtener();
        listadoPedidos = accesoPedidos.Obtener();
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

    public string GetNombreCadeteria()
    {
        return this.nombre;
    }

    public List<Pedido> GetPedidos()
    {
        return this.listadoPedidos;
    }

    public List<Cadete> GetCadetes()
    {
        return this.listadoCadetes;
    }

    public Cadete GetCadete(int idCadete)
    {
        Cadete? cadeteBuscado = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);

        if (cadeteBuscado != null)
        {
            return cadeteBuscado;
        } else
        {
            return null;
        }
    }
    
    public bool AgregarPedido(Pedido pedidoNuevo, int idCadete)
    {
        //listadoPedidos = accesoPedidos.Obtener();
        pedidoNuevo = AsignarCadeteAPedido(pedidoNuevo, idCadete);
        listadoPedidos.Add(pedidoNuevo);
        pedidoNuevo.Numero = listadoPedidos.Count();
        bool control = accesoPedidos.Guardar(listadoPedidos); 
        
        if (control)
        {
            return true;    
        } else
        {
            return false;
        }
    }

    public bool AgregarCadete(Cadete cadeteNuevo)
    {
        listadoCadetes.Add(cadeteNuevo);
        cadeteNuevo.Id = listadoCadetes.Count();
        bool control = accesoCadetes.Guardar(listadoCadetes);

        if (control)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public Pedido AsignarCadeteAPedido(Pedido pedidoAsignar, int idCadete)
    {
        //AgregarCadetes();
        Cadete? cadeteElegido = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);   
        
        pedidoAsignar.Cadete = cadeteElegido;
        accesoPedidos.Guardar(listadoPedidos);

        return pedidoAsignar;
    }

    public Pedido BuscarPedido(int idPedido)
    {
        //listadoPedidos = accesoPedidos.Obtener();
        Pedido? pedidoBuscado = listadoPedidos.Find(p => p.Numero == idPedido);

        return pedidoBuscado;
    }

    public bool CambiarEstadoPedido(int idPedido)
    {
        //listadoPedidos = accesoPedidos.Obtener();
        Pedido? pedidoCambiar = listadoPedidos.FirstOrDefault(p => p.Numero == idPedido);
        bool control = pedidoCambiar.CambiarEstado();
        accesoPedidos.Guardar(listadoPedidos);
        
        if(control)
        {
            return true;
        } else
        {
            return false;
        }
    }

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

    // public float JornalACobrar(int idCadete)
    // {
    //     float total = 0;

    //     foreach (var pedido in listadoPedidos)
    //     {
    //         if (pedido.Cadete.Id == idCadete)
    //         {
    //             if (pedido.Estado == Estado.Entregado)
    //             {
    //                 total += 500;
    //             }
    //         }
    //     }
    //     return total;
    // }

    // public int CantidadEnviosPorCadete(int idCadete)
    // {
    //     int total = listadoPedidos.Count(p => p.Cadete.Id == idCadete && p.Estado == Estado.Entregado);
    //     return total;
    // }
}