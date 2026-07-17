namespace WebProductos.Models
{
    public class E_Producto
    {
        //Propiedades simples
        public int IdGerente { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int PrecioDescuento { get; set; }
        public string Gerente { get; set; }
        public int IdProducto { get; set; }
        public bool Disponible { get; set; }
        public int IdSucursal { get; set; }
       
        public DateTime FechaCaducidad
        {
            get
            {
                return FechaIngreso.AddMonths(2);
            }
        }

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
