using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
	public class CN_Validacion
	{
		CD_Validacion cdValidacion = new CD_Validacion();

		public Boolean validarLetra(int e)
		{
			return cdValidacion.validarLetra(e);
		}

		public Boolean validarNumero(int e)
		{
			return cdValidacion.validarNumero(e);
		}

		public Boolean validarDecimal(int e, string cadena)
		{
			return cdValidacion.validarDecimal(e, cadena);
		}

	}
}
