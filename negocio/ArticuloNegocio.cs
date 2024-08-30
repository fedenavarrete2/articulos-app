using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using negocio;
using dominio;
using System.Diagnostics.Eventing.Reader;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion, C.Descripcion, A.ImagenUrl, A.Precio,  M.Id IdMarca, C.Id IdCategoria FROM ARTICULOS A JOIN MARCAS M ON A.IdMarca = M.Id JOIN CATEGORIAS C On C.Id = A.IdCategoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = Convert.ToInt32(datos.Lector[0]);
                    articulo.Codigo = Convert.ToString(datos.Lector[1]);
                    articulo.Nombre = Convert.ToString(datos.Lector[2]);
                    articulo.Descripcion = Convert.ToString(datos.Lector[3]);
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = Convert.ToInt32(datos.Lector["IdMarca"]);
                    articulo.Marca.Descripcion = Convert.ToString(datos.Lector[4]);
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = Convert.ToInt32(datos.Lector["IdCategoria"]);
                    articulo.Categoria.Descripcion = Convert.ToString(datos.Lector[5]);
                    articulo.UrlImagen = Convert.ToString(datos.Lector[6]);
                    articulo.Precio = Convert.ToDecimal(datos.Lector[7]); 

                    lista.Add(articulo);

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @imagenUrl, @precio)");
                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@imagenUrl", nuevo.UrlImagen);
                datos.setearParametro("@precio", nuevo.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void modificar(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update articulos set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagenUrl, Precio = @precio where id = @id");
                datos.setearParametro("@codigo", modificar.Codigo);
                datos.setearParametro("@nombre", modificar.Nombre);
                datos.setearParametro("@descripcion", modificar.Descripcion);
                datos.setearParametro("@idMarca", modificar.Marca.Id);
                datos.setearParametro("@idCategoria", modificar.Categoria.Id);
                datos.setearParametro("@imagenUrl", modificar.UrlImagen);
                datos.setearParametro("@precio", modificar.Precio);
                datos.setearParametro("id", modificar.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                datos.cerrarConexion();
            }

        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE ID = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion, C.Descripcion, A.ImagenUrl, A.Precio,  M.Id IdMarca, C.Id IdCategoria FROM ARTICULOS A JOIN MARCAS M ON A.IdMarca = M.Id JOIN CATEGORIAS C On C.Id = A.IdCategoria WHERE ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a:":
                            consulta += "Precio > " + filtro;
                            break;
                        case "Menor a:":
                            consulta += "Precio < " + filtro;
                            break;
                        case "Igual a:":
                            consulta += "Precio = " + filtro;
                            break;
                        default:
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Empieza con:":
                            consulta += "Nombre Like '" + filtro + "%' ";
                            break;
                        case "Termina con:":
                            consulta += "Nombre Like '%" + filtro + "' ";
                            break;
                        case "Contiene:":
                            consulta += "Nombre Like '%" + filtro + "%' ";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Celulares":
                            consulta += "C.Descripcion = 'Celulares'";
                            break;
                        case "Televisores":
                            consulta += "C.Descripcion = 'Televisores'";
                            break;
                        case "Media":
                            consulta += "C.Descripcion = 'Media'";
                            break;
                        case "Audio":
                            consulta += "C.Descripcion = 'Audio'";
                            break;
                        default:
                            break;
                    }

                }
  
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = Convert.ToInt32(datos.Lector[0]);
                    articulo.Codigo = Convert.ToString(datos.Lector[1]);
                    articulo.Nombre = Convert.ToString(datos.Lector[2]);
                    articulo.Descripcion = Convert.ToString(datos.Lector[3]);
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = Convert.ToInt32(datos.Lector["IdMarca"]);
                    articulo.Marca.Descripcion = Convert.ToString(datos.Lector[4]);
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = Convert.ToInt32(datos.Lector["IdCategoria"]);
                    articulo.Categoria.Descripcion = Convert.ToString(datos.Lector[5]);
                    articulo.UrlImagen = Convert.ToString(datos.Lector[6]);
                    articulo.Precio = Convert.ToDecimal(datos.Lector[7]);

                    lista.Add(articulo);

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }

}
