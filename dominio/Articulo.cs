using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        private int id;
        private string codigo;
        private string nombre;
        private string descripcion;
        private Marca marca;
        private Categoria categoria;
        private string urlImagen;
        private decimal precio;

        public int Id
        { get { return id; } set { id = value; } }

        public string Codigo
        { get { return codigo; } set { codigo = value; } }

        public string Nombre
        { get { return nombre; } set { nombre = value; } }

        public string Descripcion
        { get { return descripcion; } set { descripcion = value; } }

        public Marca Marca
        { get { return marca; } set { marca = value; } }

        public Categoria Categoria
        { get { return categoria; } set { categoria = value; } }

        public string UrlImagen
        { get { return urlImagen; } set { urlImagen = value; } }

        public decimal Precio
        { get { return precio; } set { precio = value; } }



    }
}
