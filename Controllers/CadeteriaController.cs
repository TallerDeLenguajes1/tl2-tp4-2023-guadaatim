using System.Runtime.CompilerServices;
using EspacioCadete;
using EspacioCadeteria;
using EspacioPedido;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaController;

[ApiController]
[Route("[controller]")]

public class CadeteriaController : ControllerBase
{
    private Cadeteria? cadeteria;

    [HttpGet]
    public ActionResult<List<Pedido>> GetPedidos()
    {

    }

    [HttpGet]
    public ActionResult<List<Cadete>> GetCadetes()
    {

    }

    [HttpGet]
    public ActionResult<string> GetInforme()
    {

    }

    [HttpPost]
    public ActionResult AgregarPedidos()
    {

    }

    [HttpPut]
    public ActionResult AsignarPedido()
    {

    }

    [HttpPut]
    public ActionResult CambiarEstadoPedido()
    {

    }

    [HttpPut]
    public ActionResult CambiarCadetePedido()
    {
        
    }

}