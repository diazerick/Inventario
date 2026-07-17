namespace WebProductos.Models
{
    public class E_Producto
    {
        //Propiedades simples
        public int IdProducto { get; set; }
        public int IdVendedor { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int PrecioDescuento { get; set; }
        public string Gerente { get; set; }
        public int IdProducto { get; set; }
        public bool Disponible { get; set; }
        public int PrecioDescuento { get; set; }
        public string NombreVendedor { get; set; }

        public int IdSucursal { get; set; }

        public string NombreSucursal { get; set; }

        //Propiedades full o de solo de lectura
        public string DisponibleTexto
        {
            get
            {
                if (Disponible == true)
                    return "Si";
                else
                    return "No";
            }
        }
    }
}
