using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
	class CD_Conexion
	{
		SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-69AEPO7;Initial Catalog=empresa;Integrated Security=True");

		public SqlConnection Conectar()
		{
			if(conexion.State == ConnectionState.Closed)
			{
				conexion.Open();
			}
			return conexion;
		}

		public SqlConnection Desconectar()
		{
			if (conexion.State == ConnectionState.Open)
			{
				conexion.Close();
			}
			return conexion;
		}
	}
}
