using EspacioCadeteria;

namespace EspacioInforme;

public class Informe
{
    private float montoGanado;
    private int cantidadEnviosCadaCadete;
    private int cantidadEnviosPromedioPorCadete;

    public float MontoGanado { get => montoGanado; set => montoGanado = value; }
    public int CantidadEnviosCadaCadete { get => cantidadEnviosCadaCadete; set => cantidadEnviosCadaCadete = value; }
    public int CantidadEnviosPromedioPorCadete { get => cantidadEnviosPromedioPorCadete; set => cantidadEnviosPromedioPorCadete = value; }

    
    // public float calcularMontoGanado()
    // {

    // }

    // public int calcularCantidadEnviosPorCadete(int idCadete)
    // {
    //     var cadeteria = new Cadeteria();
    //     return cadeteria.CantidadEnviosPorCadete(idCadete);
    // }

    // public int calcularCantidadEnviosPromedioPorCadete(int idCadete)
    // {
    //     var cadeteria = new Cadeteria();
    //     int cantidad = 0;


    // }

}