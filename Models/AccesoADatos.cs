using EspacioCadete;
using EspacioCadeteria;
using System.ComponentModel;
using System.Text.Json;

namespace EspacioAccesoADatos;

public abstract class AccesoADatos
{
    public virtual List<Cadete> LeerArchivoCadetes(string nombre)
    {
        return null;
    }

    public virtual List<Cadeteria> LeerArchivoCadeteria(string nombre)
    {
        return null;
    }
}

public class AccesoCSV : AccesoADatos
{
    public override List<Cadete> LeerArchivoCadetes(string nombre)
    {
        List<Cadete> listadoCadetes = new List<Cadete>();

        StreamReader strCadetes = new StreamReader(nombre);
        string? linea;
        int i = 0;

        while ((linea = strCadetes.ReadLine()) != null)
        {
            string[] fila = linea.Split(",").ToArray();

            if (i > 0)
            {
                Cadete cadeteAgregar = new Cadete(Convert.ToInt32(fila[0]), fila[1], fila[2], fila[3]);
                listadoCadetes.Add(cadeteAgregar);
            }

            i++;
        }

        return listadoCadetes;
    }

    public override List<Cadeteria> LeerArchivoCadeteria(string nombre)
    {
        List<Cadeteria> listadoCadeterias = new List<Cadeteria>();

        StreamReader strCadeterias = new StreamReader(nombre);
        string? linea;
        int i = 0;

        while ((linea = strCadeterias.ReadLine()) != null)
        {
            string[] fila = linea.Split(",").ToArray();

            if (i > 0)
            {
                Cadeteria cadeteriaAgregar = new Cadeteria(fila[1], "111");
                listadoCadeterias.Add(cadeteriaAgregar);
            }

            i++;
        }
        
        return listadoCadeterias;
    }
}

public class AccesoJSON : AccesoADatos
{
    public override List<Cadete> LeerArchivoCadetes(string nombre)
    {
        if (File.Exists(nombre))
        {
            string? jsonstring = File.ReadAllText(nombre);
            List<Cadete>? listadoCadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonstring);
            return listadoCadetes;
        } else
        {
            return null;
        }
    }

    public override List<Cadeteria> LeerArchivoCadeteria(string nombre)
    {
        if (File.Exists(nombre))
        {
            string? jsonstring = File.ReadAllText(nombre);
            List<Cadeteria>? listadoCadeterias = JsonSerializer.Deserialize<List<Cadeteria>>(jsonstring);
            return listadoCadeterias;
        } else
        {
            return null;
        }
    }
}