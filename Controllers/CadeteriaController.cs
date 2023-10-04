using System.Runtime.CompilerServices;
using EspacioCadete;
using EspacioCadeteria;
using EspacioInforme;
using EspacioPedido;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaController;

[ApiController]
[Route("[controller]")]

public class CadeteriaController : ControllerBase
{
    private static Cadeteria? cadeteria;
    private readonly ILogger<CadeteriaController> logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        this.logger = logger;
        cadeteria = Cadeteria.GetInstance();
    }

    [HttpGet("GetCadeteria")]
    public ActionResult<string> GetCadeteria()
    {
        return cadeteria.Nombre;
    }

    [HttpGet("GetPedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        List<Pedido> pedidos = cadeteria.GetPedidos();
        return Ok(pedidos);
    }

    [HttpGet("GetCadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        List<Cadete> cadetes = cadeteria.GetCadetes();
        return Ok(cadetes);
    }

    [HttpGet("GetInforme")]
    public ActionResult<string> GetInforme()
    {
        Informe informe = new Informe();
        return Ok(informe);
    }

    [HttpPost("AgregarPedido")]
    public ActionResult<Pedido> AgregarPedidos(Pedido pedidoNuevo)
    {
        cadeteria.AgregarPedido(pedidoNuevo);
        return Ok(pedidoNuevo);
    }

    [HttpPut("AsignarCadeteAPedido")]
    public ActionResult<Pedido> AsignarPedido(int idPedido, int idCadete)
    {
        cadeteria.AsignarCadeteAPedido(idPedido, idCadete);
        Pedido pedidoAsignado = cadeteria.BuscarPedido(idPedido);
        return Ok(pedidoAsignado);
    }

    [HttpPut("CambiarEstado")]
    public ActionResult<Pedido> CambiarEstadoPedido(int idPedido)
    {
        Pedido pedidoBuscado = cadeteria.BuscarPedido(idPedido);
        pedidoBuscado.CambiarEstado();
        return Ok(pedidoBuscado);
    }

    [HttpPut("AsignarCadeteAPedido")]
    public ActionResult<Pedido> CambiarCadetePedido(int idPedido, int idCadete)
    {
        cadeteria.AsignarCadeteAPedido(idPedido, idCadete);
        Pedido pedido = cadeteria.BuscarPedido(idPedido);
        return Ok(pedido);
    }

}