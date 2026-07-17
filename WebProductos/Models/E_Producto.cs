namespace WebProductos.Models
{
    public class E_Producto
    {
        //Propiedades simples
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Disponible { get; set; }
        public int PrecioDescuento { get; set; }

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

        public DateTime FechaCaducidad
        {
            get
            {
                return FechaIngreso.AddMonths(2);
            }
        }
    }
}
