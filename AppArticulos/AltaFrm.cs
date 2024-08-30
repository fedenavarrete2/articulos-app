using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppArticulos
{
    public partial class AltaFrm : Form
    {
        private Articulo articulo = null;
        public AltaFrm()
        {
            InitializeComponent();
            Text = "Cargar Articulo";
        }
        public AltaFrm(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }
        private void AltaFrm_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtUrlImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    txtPrecio.Text = articulo.Precio.ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
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
        private bool validadCampo()
        {
            if(txtCodigo.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un codigo");
                txtCodigo.Focus();
                return true;
            }

            return false;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
            if (articulo == null)
                        articulo = new Articulo();

                        articulo.Codigo = Convert.ToString(txtCodigo.Text);
                        articulo.Nombre = Convert.ToString(txtNombre.Text);
                        articulo.Descripcion = Convert.ToString(txtDescripcion.Text);
                        articulo.Marca = (Marca)cboMarca.SelectedItem;
                        articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                        articulo.UrlImagen = Convert.ToString(txtUrlImagen.Text);
                        articulo.Precio = Convert.ToDecimal(txtPrecio.Text);

                        if (articulo.Id != 0)
                        {
                            negocio.modificar(articulo);
                            MessageBox.Show("Modificado correctamente.");
                        }
                        else
                        {
                            negocio.agregar(articulo);
                            MessageBox.Show("Agregado correctamente.");
                        }

                        Close();        

            }
            catch (FormatException ex)
            {
                {
                    MessageBox.Show("Debe cargar el precio del producto.");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
