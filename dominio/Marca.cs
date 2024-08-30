using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Marca
    {
        private int id;
        private string descripcion;

        public int Id
        {
            set { id = value; }
            get { return id; }
        }

        public string Descripcion
        {
            set { descripcion = value; }
            get { return descripcion; }
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
