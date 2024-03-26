using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;

namespace Negocio
{
    public class NegocioElementos
    {
        public List<Categorias> listarCategorias()
        {
            List<Categorias> lista = new List<Categorias>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearQuery("select Id, Descripcion from CATEGORIAS");
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    Categorias categoria = new Categorias();
                    categoria.Id = (int)datos.Lector["Id"];
                    categoria.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(categoria);
                }
                
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }

        public List<Marcas> listarMarcas()
        {
            List<Marcas> lista = new List<Marcas>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearQuery("select Id, Descripcion from MARCAS");
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    Marcas marca = new Marcas();
                    marca.Id = (int)datos.Lector["Id"];
                    marca.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(marca);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }


    }
}
