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

namespace WindowsFormsApp1.Views
{
    public partial class FrmAddVacaciones : Form
    {
       private int IdEmpleado;
        //private DateTime? Fecha=null;
        public FrmAddVacaciones(int idEmpleado)
        {
            InitializeComponent();
            IdEmpleado = idEmpleado;
           // Fecha = fecha;
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddVacaciones_Load(object sender, EventArgs e)
        {
            CargarEstadoVacacion();
            //if (Fecha != null)
            //{
            //    dtpFechaVacacion.Value = (DateTime)Fecha;
            //    dtpFechaVacacion.Enabled = false;

            //}

        }

        private void CargarEstadoVacacion()
        {
            RegistrosVacacionesController oRegVac = new RegistrosVacacionesController();
            cboEstadoVacacion.DataSource = oRegVac.CargarEstadoVacaciones();
            cboEstadoVacacion.DisplayMember = "EstadoVacacion";
            cboEstadoVacacion.ValueMember = "Id";
            cboEstadoVacacion.SelectedIndex = -1;  
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            RegistrosVacacionesController oRegVac = new RegistrosVacacionesController();

            oRegVac.RegistroVacacionesTrans(IdEmpleado, dtpFechaVacacion.Value, (int)cboEstadoVacacion.SelectedValue,
              dtpFechaVacacion.Value.Year, dtpFechaVacacion.Value.Month);

            //oRegVac.Add(IdEmpleado, dtpFechaVacacion.Value, (int)cboEstadoVacacion.SelectedValue);
            this.Close();
        }
    }
}
