using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProductos.Models
{
    public class E_Producto
    {
        //Propiedades simples
        public string Gerente { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public int IdProducto { get; set; }
        public DateTime FechaIngreso { get; set; }

        public decimal PrecioDescuento { get; set; }

        public bool Disponible { get; set; }
        //Propiedades de solo lectura
        public string DisponibleHtml
        {
            get
            {
                if (Disponible == true)
                    return "<span class='badge text-bg-success'>Si</span>";
                else
                    return "<span class='badge text-bg-danger'>No</span>";
            }
        }
        public DateTime FechaCaducidad
        {
            get
            {
                return FechaIngreso.AddMonths(2);
            }
        }
    }
}