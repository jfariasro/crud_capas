using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
	public class CD_Empleado
	{
		public int idempleado { get; set; }
		public string razonsocial { get; set; }
		public DateTime fechanacimiento { get; set; }
		public int edad { get; set; }
		public float salario { get; set; }
		SqlCommand comando = new SqlCommand();
        CD_Conexion conexion = new CD_Conexion();

		public string Insertar()
		{
            try
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "insertar_empleado";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@razonsocial", razonsocial);
                comando.Parameters.AddWithValue("@fechanacimiento", fechanacimiento);
                comando.Parameters.AddWithValue("@edad", edad);
                comando.Parameters.AddWithValue("@salario", salario);
                comando.Connection = conexion.Conectar();
                comando.ExecuteReader();
                conexion.Desconectar();
                return null;
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
                return ex.Message;
            }
        }

        public string Modificar()
        {
            try
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "modificar_empleado";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idempleado", idempleado);
                comando.Parameters.AddWithValue("@razonsocial", razonsocial);
                comando.Parameters.AddWithValue("@fechanacimiento", fechanacimiento);
                comando.Parameters.AddWithValue("@edad", edad);
                comando.Parameters.AddWithValue("@salario", salario);
                comando.Connection = conexion.Conectar();
                comando.ExecuteReader();
                conexion.Desconectar();
                return null;
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
                return ex.Message;
            }
        }

        public string Eliminar()
        {
            try
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "eliminar_empleado";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@idempleado", idempleado);
                comando.Connection = conexion.Conectar();
                comando.ExecuteReader();
                conexion.Desconectar();
                return null;
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
                return ex.Message;
            }
        }

        public DataSet Consultar()
        {
            try
            {
                var datos = new DataSet();
                var adaptador = new SqlDataAdapter();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "consultar_empleado";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@razonsocial", razonsocial);
                comando.Connection = conexion.Conectar();
                comando.ExecuteReader();
                conexion.Desconectar();
                adaptador.SelectCommand = comando;
                adaptador.Fill(datos);
                return datos;
            }
            catch (Exception ex)
            {
                conexion.Desconectar();
                return null;
            }
        }
    }
}
