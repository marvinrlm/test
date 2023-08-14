using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Controllers;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Views
{
    public partial class addEmpleado : Form
    {
        private int? Id;
        public addEmpleado(int? id)
        {
            InitializeComponent();
            this.Id = id;
            if (this.Id != null)
            {
                CargarTiposIdentificaciones();

                LoadDataEdit();
            }
        }

        private void LoadDataEdit()
        {
            EmpleadosController oEmpleadoController = new EmpleadosController();
            Empleados oEmpleado = oEmpleadoController.CargarEmpleadoFromId((int)Id);
           // txtId.Text = string.Parse(oEmpleado.Id);
            txtNombreCompleto.Text = oEmpleado.NombreCompleto;
            cboTipoIdentificacion.SelectedValue = oEmpleado.IdTipoIdentificacion.ToString();
            //cboTipoIdentificacion.SelectedText  = oEmpleado.TipoIdentificacion.ToString();
            txtNumeroIdentificacion.Text = oEmpleado.NumeroIdentificacion;
            dtpFechaIngreso.Value = oEmpleado.FechaIngreso;
            txtSalarioBaseMensual.Text = oEmpleado.SalarioBaseMensual.ToString();
            txtDireccion.Text = oEmpleado.Direccion; 



            
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addEmpleado_Load(object sender, EventArgs e)
        {
            if (this.Id == null)
            {
                this.CargarTiposIdentificaciones();
            }
        }

        private void CargarTiposIdentificaciones()
        {
            TiposIdentificacionesController tiposIdentificacionesController = new TiposIdentificacionesController();

            cboTipoIdentificacion.DataSource = tiposIdentificacionesController.CargarComboBoxTipoIdentificacion();
            cboTipoIdentificacion.DisplayMember = "TipoIdentificacion";
            cboTipoIdentificacion.ValueMember = "Id";

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            EmpleadosController oEmpleadoController = new EmpleadosController();
            try
            {
                if (Id == null)
                {
                    oEmpleadoController.Add(txtNombreCompleto.Text, (int)cboTipoIdentificacion.SelectedValue, txtNumeroIdentificacion.Text,
                        dtpFechaIngreso.Value, decimal.Parse(txtSalarioBaseMensual.Text), txtDireccion.Text);
                } else
                {
                    oEmpleadoController.Edit((int)Id,txtNombreCompleto.Text, (int)cboTipoIdentificacion.SelectedValue, txtNumeroIdentificacion.Text,
                        dtpFechaIngreso.Value, decimal.Parse(txtSalarioBaseMensual.Text), txtDireccion.Text);
                }



                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Inable to complete the process: " + ex.Message);
            }
           

        }

    }
}
