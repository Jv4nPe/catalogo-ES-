using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace FormPrincipal
{
    public partial class frmAlta : Form
    {
        private Articulo articulo = null;
        public frmAlta()
        {
            InitializeComponent();
        }

        public frmAlta(Articulo art)
        {
            InitializeComponent();
            this.articulo = art;
            Text = "MODIFICAR ARTICULO";
        }

        private void frmAlta_Load(object sender, EventArgs e)
        {
            NegocioElementos negocio = new NegocioElementos();

            try
            {
                cboCategoria.DataSource = negocio.listarCategorias();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = negocio.listarMarcas();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.SelectedIndex = -1;
                cboMarca.SelectedIndex = -1;

                if (articulo != null)
                {
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtPrecio.Text = articulo.PrecioFormateado;
                    txtUrlImagen.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);
                    rtxtDescripcion.Text = articulo.Descripcion;

                    btnAceptar.Enabled = true;
                }
                else
                    btnAceptar.Enabled = false;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagenAlta.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagenAlta.Load("https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg");
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            NegocioArticulos negocio = new NegocioArticulos();

            bool i = false;

            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                if (validarPrecio())
                    return;

                articulo.Categoria = (Categorias)cboCategoria.SelectedItem;
                articulo.Marca = (Marcas)cboMarca.SelectedItem;
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Precio = Convert.ToDecimal(txtPrecio.Text);
                articulo.ImagenUrl = txtUrlImagen.Text;
                articulo.Descripcion = rtxtDescripcion.Text;


                if (articulo.Id == 0)
                {
                    negocio.agregarArticulo(articulo);
                    MessageBox.Show("Nuevo artículo agregado exitosamente.", "ARTICULO AGREGADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    i = true;
                }
                else
                {
                    negocio.modificarArticulo(articulo);
                    MessageBox.Show("El artículo ha sido modificado exitosamente.", "ARTICULO MODIFICADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    i = true;
                }  
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (i)
                    Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool validarPrecio()
        {
            if (!(soloNumeros(txtPrecio.Text)))
            {
                MessageBox.Show("Sólo se permiten caracteres numéricos para el precio", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        private void inhabilitarAceptar()
        {
            if (txtCodigo.Text != "" && txtNombre.Text != "" && txtPrecio.Text != "" && cboCategoria.SelectedIndex >= 0 && cboMarca.SelectedIndex >= 0)
                btnAceptar.Enabled = true;
            else
                btnAceptar.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "img|*.*";

            if(archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            inhabilitarAceptar();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            inhabilitarAceptar();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            inhabilitarAceptar();
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            inhabilitarAceptar();
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            inhabilitarAceptar();
        }

    }
}
