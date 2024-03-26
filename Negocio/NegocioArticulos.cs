using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Datos;
using Dominio;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace Negocio
{
    public class NegocioArticulos
    {
        public List<Articulo> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();

            try
            {
                datos.setearQuery("select A.Id, A.Nombre, A.IdMarca, A.IdCategoria, M.Descripcion as Marca, C.Descripcion as Categoria, A.Descripcion, A.Codigo, A.ImagenUrl, A.Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id");
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo();
                    art.Id = (int)datos.Lector["Id"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Marca = new Marcas();
                    art.Marca.Descripcion = (string)datos.Lector["Marca"];
                    art.Marca.Id = (int)datos.Lector["IdMarca"];
                    art.Categoria = new Categorias();
                    art.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    art.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    art.Descripcion = (string)datos.Lector["Descripcion"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        art.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    art.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public List<Articulo> filtrar(string campo, string filtro)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();

            try
            {
                string query = "select A.Id, A.Nombre, M.Descripcion as Marca, C.Descripcion as Categoria, A.Descripcion, A.Codigo, A.ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id and ";

                if (campo == "Nombre")
                    query += "Nombre like '%" + filtro + "%'";

                if (campo == "Código")
                    query += "Codigo like '%" + filtro + "%'";

                datos.setearQuery(query);
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo();
                    art.Id = (int)datos.Lector["Id"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Marca = new Marcas();
                    art.Marca.Descripcion = (string)datos.Lector["Marca"];
                    art.Marca.Id = (int)datos.Lector["IdMarca"];
                    art.Categoria = new Categorias();
                    art.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    art.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    art.Descripcion = (string)datos.Lector["Descripcion"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        art.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    art.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public List<Articulo> filtrarPrecio(string campo, string criterio, string filtro)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();

            try
            {
                string query = "select A.Id, A.Nombre, M.Descripcion as Marca, C.Descripcion as Categoria, A.Descripcion, A.Codigo, A.ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id and ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Menor a":
                            query += "Precio <" + filtro;
                            break;

                        case "Mayor a":
                            query += "Precio >" + filtro;
                            break;

                        default:
                            query += "Precio =" + filtro;
                            break;

                    }
                }

                datos.setearQuery(query);
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo();
                    art.Id = (int)datos.Lector["Id"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Marca = new Marcas();
                    art.Marca.Descripcion = (string)datos.Lector["Marca"];
                    art.Marca.Id = (int)datos.Lector["IdMarca"];
                    art.Categoria = new Categorias();
                    art.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    art.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    art.Descripcion = (string)datos.Lector["Descripcion"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        art.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    art.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public List<Articulo> filtrarCriterios(string campo, string criterio)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();

            try
            {
                string query = "select A.Id, A.Nombre, M.Descripcion as Marca, C.Descripcion as Categoria, A.Descripcion, A.Codigo, A.ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id and ";

                if (campo == "Categoría")
                    query += "C.Descripcion like '" + criterio + "'";

                if (campo == "Marca")
                    query += "M.Descripcion like '" + criterio + "'";

                datos.setearQuery(query);
                datos.ejecutarLector();

                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo();
                    art.Id = (int)datos.Lector["Id"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Marca = new Marcas();
                    art.Marca.Descripcion = (string)datos.Lector["Marca"];
                    art.Marca.Id = (int)datos.Lector["IdMarca"];
                    art.Categoria = new Categorias();
                    art.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    art.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    art.Descripcion = (string)datos.Lector["Descripcion"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        art.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    art.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }

        public void agregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearQuery("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @Marca, @Categoria, @UrlImagen, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@Marca", nuevo.Marca.Id);
                datos.setearParametro("@Categoria", nuevo.Categoria.Id);
                datos.setearParametro("@UrlImagen", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void modificarArticulo(Articulo modificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearQuery("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @Marca, IdCategoria = @Categoria, ImagenUrl = @UrlImagen, Precio = @Precio where Id = @Id");
                datos.setearParametro("@Codigo", modificado.Codigo);
                datos.setearParametro("@Nombre", modificado.Nombre);
                datos.setearParametro("@Descripcion", modificado.Descripcion);
                datos.setearParametro("@Marca", modificado.Marca.Id);
                datos.setearParametro("@Categoria", modificado.Categoria.Id);
                datos.setearParametro("@UrlImagen", modificado.ImagenUrl);
                datos.setearParametro("@Precio", modificado.Precio);
                datos.setearParametro("@Id", modificado.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }

        public void eliminarArticulo(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearQuery("delete from ARTICULOS where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
