using Microsoft.Data.SqlClient;
using WebProductos.Models;

namespace WebProductos.Datos
{
    public class D_Producto
    {
        private string CadenaConexion = "server=localhost;database=generacion43;user=sa;password=devo123;TrustServerCertificate=true";

        public List<E_Producto> ObtenerTodos()
        {
            //Creamos la lista de productos vacia
            List<E_Producto> lista = new List<E_Producto>();
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos";
                SqlCommand comando = new SqlCommand(query, conexion);
                //Creamos un objeto de SqlDataReader y ahi guardamos el resultado de ejecutar el query
                SqlDataReader reader = comando.ExecuteReader();
                //Recorremos las filas de reader con un ciclo while
                while (reader.Read())
                {
                    //Creamos un producto
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
            catch(Exception ex)
            {
                //Lanzamos nuevamente el error para que el controlador se entere del error
                throw ex;
            }
            finally
            {
                //Cerramos la conexion
                conexion.Close();
            }                      
            //Regresamos la lista de productos
            return lista;
        }

        public void AgregarProducto(E_Producto producto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            conexion.Open();
            //Creamos el query utilizando parametros: @nombreParametro
            string query = "INSERT INTO Productos(descripcion,precio,fechaIngreso,disponible) " +
                                       "VALUES(@descripcion,@precio,@fechaIngreso,@disponible)";
            SqlCommand comando = new SqlCommand(query, conexion);
            //Asigamos valores a los parametros del query
            comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
            comando.Parameters.AddWithValue("@disponible", producto.Disponible);
            //Ejecutamos el query
            comando.ExecuteNonQuery();
        }

        public E_Producto ObtenerProductoPorId(int idProducto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            conexion.Open();
            string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos " +
                            "WHERE idProducto=@id";
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@id", idProducto);
            SqlDataReader reader = comando.ExecuteReader();
            //Creamos un producto
            E_Producto producto = new E_Producto();
            //Leemos los datos del reader y los guardamos en el producto
            if (reader.Read())
            {
                producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                producto.Descripcion = Convert.ToString(reader["descripcion"]);
                producto.Precio = Convert.ToDecimal(reader["precio"]);
                producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                producto.Disponible = Convert.ToBoolean(reader["disponible"]);
            }
            conexion.Close();
            return producto;
        }

        public void Editar(E_Producto producto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                //sanitizar los caracteres de los query--se crea el query con parametros @Nparametro
                string query = "UPDATE productos " +
                            "SET Descripcion=@descripcion,precio=@precio,fechaIngreso=@fechaIngreso,Disponible=@disponible " +
                             "WHERE IdProducto = @IdProducto";
                SqlCommand comando = new SqlCommand(query, conexion);
                //Asignamos valores a los parametros del query
                comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
                comando.Parameters.AddWithValue("@disponible", producto.Disponible);

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

        public void Eliminar(int idProducto)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                //sanitizar los caracteres de los query--se crea el query con parametros @Nparametro
                string query = "DELETE  FROM productos " +
                               " WHERE IdProducto=@IdProducto";
                SqlCommand comando = new SqlCommand(query, conexion);
                //Asignamos valores a los parametros del query
                comando.Parameters.AddWithValue("@IdProducto", idProducto);
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
