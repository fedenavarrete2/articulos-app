using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
			AccesoDatos datos = new AccesoDatos();
			List<Categoria> lista = new List<Categoria>();
			try
			{
				datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Categoria categoria = new Categoria();

					categoria.Id = Convert.ToInt32(datos.Lector["id"]);
					categoria.Descripcion = Convert.ToString(datos.Lector["descripcion"]);

					lista.Add(categoria);
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
