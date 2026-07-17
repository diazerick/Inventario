using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using WebProductos.Datos;
using WebProductos.Models;

namespace WebProductos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Declarar una lista vacia
            List<E_Producto> productos = new List<E_Producto>();
            try
            {
                //Crear un objeto de la capa de Datos
                D_Producto datos = new D_Producto();
                //Obtenemos la lista de productos de la capa de datos
                productos = datos.ObtenerTodos();
                //Pasamos a la vista index los productos como modelo
                return View("Index", productos);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index", productos);
            }
        }

        public IActionResult IrAgregar()
        {
            return View("VistaAgregar");
        }

        public IActionResult Agregar(E_Producto producto)
        {
            //Validaciones, creamos un string en donde guardamos el mensaje si algo esta mal
            string validaciones = string.Empty;
            if(producto.Descripcion.Count() < 4)
            {
                validaciones += "La <b>descripción</b> debe ser de almenos 4 caracteres<br>";
            }
            if(producto.Precio <= 0)
            {
                validaciones += "El <b>precio</b> debe ser un numero positivo<br>";
            }
            if(producto.FechaIngreso > DateTime.Now)
            {
                validaciones += "La <b>Fecha Ingreso</b> no es valida<br>";
            }
            //Si validaciones es una cadena vacia,entonces los datos son validos
            if(string.IsNullOrEmpty(validaciones))
            {
                //Si entramos entonces son validos y agregamos
                //Crear un objeto la capa de datos
                D_Producto datos = new D_Producto();
                //Ejecuamos el metodo Agregar de la capa de datos
                datos.AgregarProducto(producto);
                TempData["mensaje"] = $"El producto {producto.Descripcion} se registro correctamente";
                //Redireccionamos a la accion Index
                return RedirectToAction("Index");
            }
            else
            {
                //Los datos no son validos entonces no agregamos,mostramos el mensaje de validaciones
                TempData["validaciones"] = validaciones;
                //mostramos las validaciones en la vista de agregar
                return View("VistaAgregar");
            }            
        }

        public IActionResult IrEditar(int idProducto)
        {
            D_Producto datos = new D_Producto();
            //A partir del idProducto obtenemos los demas datos del producto
            E_Producto producto = datos.ObtenerProductoPorId(idProducto);
            //Vamos al formulario de editar pasando el producto como modelo
            return View("VistaEditar", producto);
        }

        public IActionResult Editar(E_Producto producto)
        {
            //Creamos un objeto de la capa de datos
            D_Producto datos = new D_Producto();
            //Ejecutamos el metodo para actualizar
            datos.Editar(producto);
            TempData["mensaje"] = $"El producto con ID:{producto.IdProducto} se modificó correctamente";
            //Redirigimos a la accion Index
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int idProducto)
        {
            //Creamos un objeto de la capa de datos
            D_Producto datos = new D_Producto();
            datos.Eliminar(idProducto);
            TempData["mensaje"] = $"El producto con ID:{idProducto} se eliminó correctamente";
            //Redirigimos a la accion Index
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
