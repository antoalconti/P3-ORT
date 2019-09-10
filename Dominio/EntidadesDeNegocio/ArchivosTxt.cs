using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio.EntidadesDeNegocio
{
    public static class ArchivosTxt
    {
        

        private static string ArchivoBarrios = AppDomain.CurrentDomain.BaseDirectory + "Archivos/Barrios.txt";

        private static string ArchivoViviendas = AppDomain.CurrentDomain.BaseDirectory + "Archivos/Viviendas.txt";

        private static string ArchivoParametros = AppDomain.CurrentDomain.BaseDirectory + "Archivos/Parametros.txt";

        private static Repositorios.RepositorioBarrio repoB = new Repositorios.RepositorioBarrio();
        private static Repositorios.RepositorioVivienda repoV = new Repositorios.RepositorioVivienda();
        private static Repositorios.RepositorioParametro repoP = new Repositorios.RepositorioParametro();

        public static bool LeerBarrioYAgregar()
        {
            bool r = false;
            StreamReader sr = null;
            using (sr = new StreamReader(ArchivoBarrios))
            {
                string linea = sr.ReadLine();
                while (linea != null)
                {
                    Barrio unB = ObtenerDesdeStringBarrio(linea, "#");
                    if (repoB.AddBarrio(unB))
                        r = true;
                    linea = sr.ReadLine();
                }
            }
            return r;
        }

        private static Barrio ObtenerDesdeStringBarrio(string dato, string delimitador)
        {
            string[] vecDatos = dato.Split(delimitador.ToCharArray());
            return new Barrio
            {
                Nombre = vecDatos[0],
                Catastro = vecDatos[1],
                Descripcion = vecDatos[2]
            };
        }

        public static bool LeerViviendaYAgregar()
        {
            StreamReader sr = null;
            using (sr = new StreamReader(ArchivoViviendas))
            {
                string linea = sr.ReadLine();
                while (linea != null)
                {
                    Vivienda unV = ObtenerDesdeStringVivienda(linea, "#");
                    repoV.AddVivienda(unV);
                    linea = sr.ReadLine();
                }
                return true;
            }
            return false;
        }

        private static Vivienda ObtenerDesdeStringVivienda(string dato, string delimitador)
        {
            string[] vecDatos = dato.Split(delimitador.ToCharArray());
            String tipo = vecDatos[10];
            Barrio bar = repoB.FindByNameBarrio(vecDatos[3]);
            if (tipo == "Usada")
            {
                return new Usada
                {
                    ID = int.Parse(vecDatos[0]),
                    Calle = vecDatos[1],
                    NumPuerta = int.Parse(vecDatos[2]),
                    Barrio = bar,
                    Descripcion = bar.Descripcion,
                    CantBanios = int.Parse(vecDatos[5]),
                    CantDorm = int.Parse(vecDatos[6]),
                    Metraje = int.Parse(vecDatos[7]),
                    Anio = int.Parse(vecDatos[8]),
                    PrecioFinal = float.Parse(vecDatos[9].ToString(), CultureInfo.InvariantCulture),
                    Tipo = tipo,
                    Estado = "Recibida",
                    Contribucion = int.Parse(vecDatos[11]),
                };
            }
            else
            {
                return new Nueva
                {
                    ID = int.Parse(vecDatos[0]),
                    Calle = vecDatos[1],
                    NumPuerta = int.Parse(vecDatos[2]),
                    Barrio = bar,
                    CantBanios = int.Parse(vecDatos[5]),
                    CantDorm = int.Parse(vecDatos[6]),
                    Metraje = int.Parse(vecDatos[7]),
                    Anio = int.Parse(vecDatos[8]),
                    PrecioFinal = float.Parse(vecDatos[9].ToString(), CultureInfo.InvariantCulture),
                    Tipo = tipo,
                    Estado = "Recibida",
                    Descripcion = bar.Descripcion,
                };
            }
        }

        public static bool LeerParametroYAgregar()
        {
            bool r = false;
            StreamReader sr = null;
            using (sr = new StreamReader(ArchivoParametros))
            {
                string linea = sr.ReadLine();
                while (linea != null)
                {
                    List<Parametros> listaPara = ObtenerDesdeStringParametro(linea, "#", "=");
                    foreach (Parametros elem in listaPara)
                    {
                        if (repoP.AddParametro(elem))
                            r = true;
                        linea = sr.ReadLine();
                    }
                    linea = sr.ReadLine();
                }
            }
            return r;
        }

        //aca fue diferente a el resto porque las lines que devuelve el stream reader es una sola, estonces tuvimos que dividirla
        //por los caracteres delimitados dos veces para poder quenerar los objetos parametros que estan cargados en el archivo previamente
        private static List<Parametros> ObtenerDesdeStringParametro(string dato, string delimitador, string delimitador2)
        {
            List<Parametros> lista = new List<Parametros>();
            string[] vecDatos = dato.Split(delimitador.ToCharArray());
            string[] vecDatos2 = new String[2];
            for (int i = 0; i < vecDatos.Length - 1; i++)
            {
                vecDatos2 = vecDatos[i].Split(delimitador2.ToCharArray());
                Parametros p = new Parametros
                {
                    Nombre = vecDatos2[0],
                    Valor = double.Parse((vecDatos2[1]).ToString(), CultureInfo.InvariantCulture),
                };
                lista.Add(p);
            }
            return lista;
        }

    }
}
