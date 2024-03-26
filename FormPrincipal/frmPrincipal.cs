using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace FormPrincipal
{
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listArt;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();

            

            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Categoría");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Precio");
            cboCampo.Items.Add("Código");
            cboCriterio.Enabled = false;
        }

        private void cargar()
        {
            try
            {
                NegocioArticulos negocio = new NegocioArticulos();
                listArt = negocio.listar();
                dgvArticulos.DataSource = negocio.listar();
                ocultarColumnas();
                ocultarBotones();
                cargarImagen(listArt[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Precio"].Visible = false;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;

        }

        private void ocultarBotones()
        {
            if(dgvArticulos.Rows.Count == 0)
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            else
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
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
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo art = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(art.ImagenUrl);
                ocultarColumnas();
            }
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltro;
            string filtro = txtFiltroRapido.Text;

            try
            {
                if (filtro.Length >= 1)
                    listaFiltro = listArt.FindAll(art => art.Nombre.ToLower().Contains(filtro.ToLower()) || art.Codigo.ToLower().Contains(filtro.ToLower()) || art.Categoria.Descripcion.ToLower().Contains(filtro.ToLower()) || art.Marca.Descripcion.ToLower().Contains(filtro.ToLower()));
                else
                    listaFiltro = listArt;

                dgvArticulos.DataSource = listaFiltro;
                ocultarColumnas();
                ocultarBotones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            NegocioElementos negocio = new NegocioElementos();
            string campo = cboCampo.SelectedItem.ToString();

            if(campo != "Nombre" && campo != "Código")
            {

                if (campo == "Precio")
                {
                    btnBuscar.Enabled = true;
                    cboCriterio.Enabled = true;
                    cboCriterio.DataSource = null;
                    cboCriterio.Items.Add("Mayor a");
                    cboCriterio.Items.Add("Menor a");
                    cboCriterio.Items.Add("Igual a");
                    txtFiltroAvanzado.Enabled = true;
                }

                if(campo == "Categoría")
                {
                    btnBuscar.Enabled = false;
                    txtFiltroAvanzado.Enabled = false;
                    cboCriterio.Enabled = true;
                    cboCriterio.DataSource = negocio.listarCategorias();
                    cboCriterio.ValueMember = "Id";
                    cboCriterio.DisplayMember = "Descripcion";
                }

                if (campo == "Marca")
                {
                    btnBuscar.Enabled = false;
                    txtFiltroAvanzado.Enabled = false;
                    cboCriterio.Enabled = true;
                    cboCriterio.DataSource = negocio.listarMarcas();
                    cboCriterio.ValueMember = "Id";
                    cboCriterio.DisplayMember = "Descripcion";
                }

            }
            else
            {
                cboCriterio.Enabled = false;
                btnBuscar.Enabled = true;
                txtFiltroAvanzado.Enabled = true;
            }
        }

        private void cboCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            NegocioArticulos negocio = new NegocioArticulos();
            NegocioElementos negocioE = new NegocioElementos();

            string campo;
            string criterio;

           if(cboCampo.SelectedItem == "Categoría")
            {
                campo = cboCampo.SelectedItem.ToString();
                criterio = cboCriterio.SelectedItem.ToString();

                dgvArticulos.DataSource = negocio.filtrarCriterios(campo, criterio);
                ocultarBotones();
            }

           if(cboCampo.SelectedItem == "Marca")
            {
                campo = cboCampo.SelectedItem.ToString();
                criterio = cboCriterio.SelectedItem.ToString();

                dgvArticulos.DataSource = negocio.filtrarCriterios(campo, criterio);
                ocultarBotones();
            }

            
        }

        private bool soloNumeros(string cadena)
        {
            foreach(char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una opción en la barra de 'Buscar por'", "CAMPO VACIO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            if (cboCriterio.Enabled)
            {
                if (cboCriterio.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione un criterio para el campo", "CRITERIO VACIO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                }

                if (!(soloNumeros(txtFiltroAvanzado.Text)) && txtFiltroAvanzado.Text != "")
                {
                    MessageBox.Show("Sólo se permiten caracteres numéricos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }

                if (txtFiltroAvanzado.Text == "")
                    return true;

                else
                    return false;
            }
            else
                return false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            NegocioArticulos negocio = new NegocioArticulos();

            try
            {
                if (validarFiltro())
                    return;

                if(cboCriterio.Enabled)
                {
                    string campo = cboCampo.SelectedItem.ToString();
                    string criterio = cboCriterio.SelectedItem.ToString();
                    string filtro = txtFiltroAvanzado.Text;
                    dgvArticulos.DataSource = negocio.filtrarPrecio(campo, criterio, filtro);
                }
                else
                {
                    string campo = cboCampo.SelectedItem.ToString();
                    string filtro = txtFiltroAvanzado.Text;
                    dgvArticulos.DataSource = negocio.filtrar(campo, filtro);
                }

                ocultarBotones();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAlta alta = new frmAlta();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo art = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAlta alta = new frmAlta(art);
            alta.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
            cargar();
        }

        private void eliminar()
        {
            NegocioArticulos negocio = new NegocioArticulos();
            Articulo art = new Articulo();

            try
            {
                DialogResult respuesta = MessageBox.Show("El artículo seleccionado será eliminado completamente de la base de datos. ¿Está seguro que desea eliminarlo?", "ELIMINAR ARTICULO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(respuesta == DialogResult.Yes)
                {
                    art = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminarArticulo(art.Id);
                    MessageBox.Show("El artículo ha sido eliminado exitosamente.", "ELIMINAR ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtFiltroAvanzado_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroAvanzado.Text == "")
                cargar();
        }

        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Articulo art = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmDetalle detalle = new frmDetalle(art);
            detalle.ShowDialog();

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
