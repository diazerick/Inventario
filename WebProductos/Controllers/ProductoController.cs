using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProductos.Datos;
using WebProductos.Models;

namespace WebProductos.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            //Creamos una lista vacia
            List<E_Producto> productos = new List<E_Producto>();
            try
            {
                //Creamos un objeto de la Capa Datos para usar sus metodos
                D_Producto datos = new D_Producto();
                //Obtener la lista de productos
                productos = datos.ObtenerProductos();
                //Pasamos los productos como modelo a la visa
                return View("Consulta", productos);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Consulta", productos);
            }
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View("VistaAgregar");
        }

        [HttpPost]
        public ActionResult Agregar(E_Producto producto)
        {
            //Creamos un objeto de la Capa Datos para usar sus metodos
            D_Producto datos = new D_Producto();

            //Validaciones
            string validaciones = string.Empty;

            if (producto.Descripcion.Count() < 4)
                validaciones += "La <b>Descripción</b> debe ser de almenos 5 caracteres <br>";

            if (producto.Precio <= 0)
                validaciones += "El <b>Precio</b> debe ser mayor a 0 <br>";

            if (producto.FechaIngreso > DateTime.Today)
                validaciones += "La <b>Fecha Ingreso</b> no puede ser posterior a hoy <br>";

            if (validaciones != string.Empty)
            {
                //NO paso validaciones, no agregamos, mostramos mensaje con las validaciones
                TempData["validaciones"] = validaciones;
                return View("VistaAgregar");
            }
            else
            {
                //Paso las validaciones entonces agregamos
                datos.AgregarProducto(producto);
                TempData["mensaje"] = $"El producto {producto.Descripcion} se registro correctamente";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Editar(int idProducto)
        {
            //Creamos un objeto de la capa de datos
            D_Producto datos = new D_Producto();
            //Obtener el producto
            E_Producto producto = datos.ObtenerProductoPorId(idProducto);
            //Pasamos el producto a la vista como modelo
            return View("VistaEditar", producto);
        }

        [HttpPost]
        public ActionResult Editar(E_Producto producto)
        {
            //Creamos un objeto de la capa de datos
            D_Producto datos = new D_Producto();

            datos.EditarProducto(producto);

            TempData["mensaje"] = $"El producto con ID {producto.IdProducto} se modificó correctamente";

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int idProducto)
        {
            //Creamos un objeto de la capa de datos
            D_Producto datos = new D_Producto();

            datos.EliminarProducto(idProducto);

            TempData["mensaje"] = $"El producto con ID {idProducto} se eliminó correctamente";

            return RedirectToAction("Index");
        }
    }
}