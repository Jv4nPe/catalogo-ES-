using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormPrincipal
{
    public partial class frmDetalle : Form
    {
        private Articulo articulo;
        public frmDetalle(Articulo art)
        {
            InitializeComponent();
            this.articulo = art;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                lblCategoria.Text = articulo.Categoria.Descripcion;
                lblMarca.Text = articulo.Marca.Descripcion;
                lblCodigo.Text = articulo.Codigo;
                lblNombre.Text = articulo.Nombre;
                lblPrecio.Text = articulo.PrecioFormateado;
                lblDescripcion.Text = articulo.Descripcion;
                cargarImagen(articulo.ImagenUrl);

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
                pbxImagen.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxImagen.Load("https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg");
            }
        }
    }
}
