using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using negocio;
using dominio;

namespace AppArticulos
{
    public partial class Articulos : Form
    {

        List<Articulo> listaArticulo = new List<Articulo>();
        public Articulos()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Precio");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Categoria");
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulo = negocio.listar();
            dgvArticulos.DataSource = listaArticulo;
            pbxArticulos.Load(listaArticulo[0].UrlImagen);
            ocultarColumnas();
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    cargarImagen(seleccionado.UrlImagen);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxArticulos.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTOwRConBYl2t6L8QMOAQqa5FDmPB_bg7EnGA&s");
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada = new List<Articulo>(); ;
            string filtro = txtBuscar.Text;
            try
            {
                if (filtro.Length >= 3)
                {
                    listaFiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()));
                }
                else
                {
                    listaFiltrada = listaArticulo;
                }
                dgvArticulos.DataSource = listaFiltrada;
                ocultarColumnas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCriterio.DataSource = null;
            cboCriterio.Items.Clear();
            string opcion = cboCampo.SelectedItem.ToString();


                if (opcion == "Precio")
                {
                    cboCriterio.Items.Add("Mayor a:");
                    cboCriterio.Items.Add("Menor a:");
                    cboCriterio.Items.Add("Igual a:");
                }
                else if (opcion == "Nombre")
                {
                    cboCriterio.Items.Add("Empieza con:");
                    cboCriterio.Items.Add("Termina con:");
                    cboCriterio.Items.Add("Contiene:");

                }
                else
                {
                cboCriterio.Items.Add("Celulares");
                cboCriterio.Items.Add("Televisores");
                cboCriterio.Items.Add("Media");
                cboCriterio.Items.Add("Audio");
            }
        }

        private bool validar()
        {
            if(cboCampo.SelectedIndex == -1) 
            {
                MessageBox.Show("Debe seleccionar un campo para filtar.");
                return true;
            }
            else if(cboCriterio.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un criterio para filtrar.");
                    return true;
            }
            else if(cboCampo.SelectedItem.ToString() == "Precio")
            {
                if(txtFiltro.Text == string.Empty)
                {
                    MessageBox.Show("Debe ingresar números en la caja de texto.");
                    return true;
                }

                else if ((soloNumeros(txtFiltro.Text)))
                {
                    MessageBox.Show("Debe ingresar solo números.");
                    return true;
                }

            }
            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if(char.IsNumber(caracter))
                { return false; }
            }
            return true;
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if(validar())            
                    return;
                    string campo = cboCampo.SelectedItem.ToString();
                    string criterio = cboCriterio.SelectedItem.ToString();
                    string filtro = txtFiltro.Text;
                    dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form formulario = new AltaFrm();
            formulario.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            AltaFrm modificar = new AltaFrm(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;

            try
            {
                if (MessageBox.Show("Seguro que desa eliminar este articulo?", "Eliminar Articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que desea salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();

        }

    }
}
