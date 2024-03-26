using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        private decimal precio;

        public int Id { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string ImagenUrl { get; set; }
        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public Marcas Marca { get; set; }

        [DisplayName("Categoría")]
        public Categorias Categoria { get; set; }

        [DisplayName("Precio")]
        public string PrecioFormateado { get { return precio.ToString("C", CultureInfo.CreateSpecificCulture("es-AR")); } }
        public string Descripcion { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }
    }
}
