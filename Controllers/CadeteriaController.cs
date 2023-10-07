using System.Runtime.CompilerServices;
using EspacioCadete;
using EspacioCadeteria;
using EspacioInforme;
using EspacioPedido;
using EspacioAccesoADatosCadeteria;
using EspacioAccesoADatosCadetes;
using EspacioAccesoADatosPedidos;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaController;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria? cadeteria;
    private readonly ILogger<CadeteriaController> logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        this.logger = logger;
        AccesoADatosCadeteria HelperCadeteria = new AccesoADatosCadeteria();
        AccesoADatosCadetes accesoADatosCadetes = new AccesoADatosCadetes();
        AccesoADatosPedidos accesoADatosPedidos = new AccesoADatosPedidos();
        cadeteria = new Cadeteria(HelperCadeteria, accesoADatosCadetes, accesoADatosPedidos);
    }
    
    [HttpGet("GetCadeteria")]
    public ActionResult<string> GetCadeteria()
    {
        return Ok(cadeteria.GetNombreCadeteria());
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

    [HttpGet("GetCadete")]
    public ActionResult<Cadete> GetCadete(int idCadete)
    {
        Cadete cadeteBuscado = cadeteria.GetCadete(idCadete);

        if (cadeteBuscado != null)
        {
            return Ok(cadeteBuscado);
        } else
        {
            return NotFound();
        }
    }

    [HttpGet("GetPedido")]
    public ActionResult<Pedido> GetPedido(int idPedido)
    {
        Pedido pedidoBuscado = cadeteria.BuscarPedido(idPedido);

        if (pedidoBuscado != null)
        {
            return Ok(pedidoBuscado);
        } else
        {
            return NotFound();
        }
    }

    [HttpGet("GetInforme")]
    public ActionResult<string> GetInforme()
    {
        Informe informe = new Informe();
        return Ok(informe);
    }

    [HttpPost("AgregarPedidos")]
    public ActionResult<Pedido> AddPedido(Pedido pedidoNuevo, int idCadete)
    {
        bool control = cadeteria.AgregarPedido(pedidoNuevo, idCadete);
        if (control)
        {
            return Ok(pedidoNuevo);
        } else
        {
            return BadRequest(pedidoNuevo);
        }
    }

    [HttpPost("AgregarCadete")]
    public ActionResult<Cadete> AddCadete(Cadete cadete)
    {
        bool control = cadeteria.AgregarCadete(cadete);
        if (control)
        {
            return Ok(cadete);
        } else
        {
            return BadRequest();
        }
    }

    [HttpPut("AsignarPedido")]
    public ActionResult<Pedido> AsignarPedido(int idPedido, int idCadete)
    {
        Pedido pedidoAsignado = cadeteria.BuscarPedido(idPedido);
        cadeteria.AsignarCadeteAPedido(pedidoAsignado, idCadete);
        return Ok(pedidoAsignado);
    }

    [HttpPut("CambiarEstadoPedido")]
    public ActionResult<Pedido> CambiarEstadoPedido(int idPedido)
    {
        bool control = cadeteria.CambiarEstadoPedido(idPedido);
        Pedido pedidoBuscado = cadeteria.BuscarPedido(idPedido);
        return Ok(pedidoBuscado);
    }

    [HttpPut("CambiarCadetePedido")]
    public ActionResult<Pedido> CambiarCadetePedido(int idPedido, int idCadete)
    {
        Pedido pedido = cadeteria.BuscarPedido(idPedido);
        cadeteria.AsignarCadeteAPedido(pedido, idCadete);
        return Ok(pedido);
    }
}