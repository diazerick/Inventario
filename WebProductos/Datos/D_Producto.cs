using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebProductos.Models;

namespace WebProductos.Datos
{
    public class D_Producto
    {
        private string CadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        public List<E_Producto> ObtenerProductos()
        {
            //Creamos una lista de productos vacia
            List<E_Producto> lista = new List<E_Producto>();
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos";
                SqlCommand comando = new SqlCommand(query, conexion);
                //Creamos un SqlDataReader para guardar los resultados de ejecutar el query
                SqlDataReader reader = comando.ExecuteReader();
                //Recorremos todas las filas del reader
                while (reader.Read())
                {
                    //Crear un producto
                    E_Producto producto = new E_Producto();
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = Convert.ToString(reader["descripcion"]);
                    producto.Precio = Convert.ToDecimal(reader["precio"]);
                    producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                    producto.Disponible = Convert.ToBoolean(reader["disponible"]);
                    //Agregamos el producto a la lista
                    lista.Add(producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Cerramos la conexion
                conexion.Close();
            }
            //Regresamos la lista
            return lista;
        }

        public void AgregarProducto(E_Producto producto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            conexion.Open();
            string query = "INSERT INTO Productos(descripcion,precio,fechaIngreso,disponible) " +
                                          "VALUES(@parametro1,@parametro2,@parametro3,@parametro4)";
            SqlCommand comando = new SqlCommand(query, conexion);
            //Le asignamos valores a los parametros del query
            comando.Parameters.AddWithValue("@parametro1", producto.Descripcion);
            comando.Parameters.AddWithValue("@parametro2", producto.Precio);
            comando.Parameters.AddWithValue("@parametro3", producto.FechaIngreso);
            comando.Parameters.AddWithValue("@parametro4", producto.Disponible);
          
            comando.ExecuteNonQuery();

            conexion.Close();
        }

        public E_Producto ObtenerProductoPorId(int idProducto)
        {
            E_Producto producto = new E_Producto();
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            conexion.Open();
            string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos " +
                            "WHERE idProducto = @idProducto";
            SqlCommand comando = new SqlCommand(query, conexion);
            //Creamos un SqlDataReader para guardar los resultados de ejecutar el query

            comando.Parameters.AddWithValue("@idProducto", idProducto);

            SqlDataReader reader = comando.ExecuteReader();
            //Recorremos todas las filas del reader
            if (reader.Read())
            {
                producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                producto.Descripcion = Convert.ToString(reader["descripcion"]);
                producto.Precio = Convert.ToDecimal(reader["precio"]);
                producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                producto.Disponible = Convert.ToBoolean(reader["disponible"]);
            }
            //Cerramos la conexion
            conexion.Close();
            //Regresamos el producto
            return producto;
        }

        public void EditarProducto(E_Producto producto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                string query = "UPDATE Productos SET descripcion=@descripcion, precio=@precio," +
                    "fechaIngreso=@fechaIngreso, disponible=@disponible WHERE idProducto=@idProducto";
                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
                comando.Parameters.AddWithValue("@disponible", producto.Disponible);
                comando.Parameters.AddWithValue("@idProducto", producto.IdProducto);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }                       
        }

        public void EliminarProducto(int idProducto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                string query = "DELETE Productos WHERE idProducto=@idProducto";
                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@idProducto", idProducto);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }                       
        }
    }
}