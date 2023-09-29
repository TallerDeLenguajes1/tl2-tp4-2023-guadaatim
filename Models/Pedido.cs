namespace EspacioPedido;
using EspacioCliente;
using EspacioCadete;

public enum Estado
{
    Pendiente,
    Entregado,
    Cancelado
}

public class Pedido
{
    private int numero;
    private string? observacion;
    private Cliente? cliente;
    private Estado estado;
    private Cadete? cadete;

    public Pedido()
    {
    }

    public Pedido(int numero, string? observacion, Cliente? cliente)
    {
        this.numero = numero;
        this.observacion = observacion;
        this.cliente = cliente;
        estado = Estado.Pendiente;
        Cadete = new Cadete();
    }

    public int Numero { get => numero;}
    public string? Observacion { get => observacion; }
    public Estado Estado { get => estado;}
    public Cadete? Cadete { get => cadete; set => cadete = value; }

    public void AgregarCliente(Cliente clienteNuevo)
    {
        cliente = clienteNuevo;
    }

    public void EliminarCliente()
    {
        cliente = null;
    }

    public string VerDireccionCliente()
    {
        return cliente.Direccion;
    }

    public string VerDatosDeCliente()
    {
        return cliente.DatosReferenciaDireccion;
    } 

    public void CambiarEstado()
    {
        if (estado == Estado.Pendiente )
        {
            estado = Estado.Entregado;
        } 
    }

   public void CancelarPedido()
    {
        estado = Estado.Cancelado;      
    }
} 
