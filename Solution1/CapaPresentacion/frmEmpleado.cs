using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
	public partial class frmEmpleado : Form
	{
		CN_Validacion cnValidacion = new CN_Validacion();
		public frmEmpleado()
		{
			InitializeComponent();
		}

		private void btnAceptar_Click(object sender, EventArgs e)
		{
			string texto = "";
			if(txtRazon.Text.Length == 0)
			{
				texto += "Debe ingresar la razón social del Empleado\n";
			}
			if (dtpFecha.Value > DateTime.Today)
			{
				texto += "La fecha de nacimiento no debe ser mayor a la Actual\n";
			}
			if (txtEdad.Text.Length == 0)
			{
				texto += "Debe ingresar la edad del Empleado\n";
			}
			if (txtSalario.Text.Length == 0)
			{
				texto += "Debe ingresar el salario del Empleado\n";
			}

			if(texto.Length == 0)
			{
				CN_Empleado cnEmpleado = new CN_Empleado();
				cnEmpleado.razonsocial = txtRazon.Text;
				cnEmpleado.fechanacimiento = dtpFecha.Text;
				cnEmpleado.edad = txtEdad.Text;
				cnEmpleado.salario = txtSalario.Text;

				if(Program.semaforo == 0)
				{
					string mensaje = cnEmpleado.Insertar();

					if (mensaje == null)
					{
						MessageBox.Show("Empleado Registrado Correctamente", "REGISTRADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else
				{
					cnEmpleado.idempleado = Program.idempleado;

					string mensaje = cnEmpleado.Modificar();

					if (mensaje == null)
					{
						MessageBox.Show("Empleado Modificado Correctamente", "MODIFICADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				btnCancelar_Click(sender, e);
				mostrarDatos("");
			}
			else
			{
				MessageBox.Show(texto, "NO REGISTRADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void txtRazon_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = cnValidacion.validarLetra(e.KeyChar);
			if(e.KeyChar == 13)
			{ dtpFecha.Focus(); }
		}

		private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{ txtEdad.Focus(); }
		}

		private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = cnValidacion.validarNumero(e.KeyChar);
			if (e.KeyChar == 13)
			{ txtSalario.Focus(); }
		}

		private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = cnValidacion.validarDecimal(e.KeyChar, txtSalario.Text);
			if (e.KeyChar == 13)
			{ btnAceptar.Focus(); }
		}

		private void btnCancelar_Click(object sender, EventArgs e)
		{
			Program.semaforo = 0;
			Program.idempleado = "";
			txtRazon.Clear();
			dtpFecha.Value = DateTime.Today;
			txtEdad.Clear();
			txtSalario.Clear();
			txtRazon.Focus();
			btnAceptar.Text = "Aceptar";
		}

		private void txtTexto_TextChanged(object sender, EventArgs e)
		{
			mostrarDatos(txtTexto.Text);
		}

		private void mostrarDatos(string texto)
		{
			CN_Empleado cnEmpleado = new CN_Empleado();
			var ds = new DataSet();
			cnEmpleado.razonsocial = texto;
			ds = cnEmpleado.Consultar();
			dgvDatos.Rows.Clear();
			foreach (DataRow fila in ds.Tables[0].Rows)
			{
				dgvDatos.Rows.Add(null, null, fila[0], fila[1], fila[2], fila[3], fila[4]);
			}
		}

		private void frmEmpleado_Load(object sender, EventArgs e)
		{
			mostrarDatos("");
		}

		private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				DataGridViewRow fila = dgvDatos.Rows[dgvDatos.CurrentRow.Index];
				Program.idempleado = fila.Cells[2].Value.ToString();
				if (e.ColumnIndex == 0)
				{
					Program.semaforo = 1;
					txtRazon.Text = fila.Cells[3].Value.ToString();
					dtpFecha.Text = fila.Cells[4].Value.ToString();
					txtEdad.Text = fila.Cells[5].Value.ToString();
					txtSalario.Text = fila.Cells[6].Value.ToString();
					btnAceptar.Text = "Modificar";
				}
				else if (e.ColumnIndex == 1)
				{
					DialogResult res = MessageBox.Show("¿Está Seguro?", "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (res == DialogResult.Yes)
					{
						CN_Empleado cnEmpleado = new CN_Empleado();
						cnEmpleado.idempleado = Program.idempleado;
						string mensaje = cnEmpleado.Eliminar();
						if (mensaje == null)
						{
							MessageBox.Show("Empleado Eliminado", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
							btnCancelar_Click(sender, e);
							mostrarDatos("");
						}
						else
						{
							MessageBox.Show(mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
