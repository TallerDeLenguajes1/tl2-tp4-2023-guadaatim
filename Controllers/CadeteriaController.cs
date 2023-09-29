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

    [HttpGet("GetPedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        var pedidos = cadeteria.GetPedidos();
        return Ok(pedidos);
    }

    [HttpGet("GetCadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        var cadetes = cadeteria.GetCadetes();
        return Ok(cadetes);
    }

    [HttpGet]
    public ActionResult<string> GetInforme()
    {
        var informe = new Informe();
        return Ok(informe);
    }

    [HttpPost("AgregarPedido")]
    public ActionResult AgregarPedidos(Pedido pedidoNuevo)
    {
        cadeteria.AgregarPedido(pedidoNuevo);
        return Ok(pedidoNuevo);
    }

    [HttpPut("AsignarCadeteAPedido")]
    public ActionResult AsignarPedido(int idPedido, int idCadete)
    {
        cadeteria.AsignarCadeteAPedido(idPedido, idCadete);
        var pedidoAsignado = cadeteria.BuscarPedido(idPedido);
        return Ok(pedidoAsignado);
    }

    [HttpPut("CambiarEstado")]
    public ActionResult CambiarEstadoPedido(int idPedido)
    {
        var pedidoBuscado = cadeteria.BuscarPedido(idPedido);
        pedidoBuscado.CambiarEstado();
        return Ok(pedidoBuscado);
    }

    [HttpPut("AsignarCadeteAPedido")]
    public ActionResult CambiarCadetePedido(int idPedido, int idCadete)
    {
        cadeteria.AsignarCadeteAPedido(idPedido, idCadete);
        var pedido = cadeteria.BuscarPedido(idPedido);
        return Ok(pedido);
    }

}