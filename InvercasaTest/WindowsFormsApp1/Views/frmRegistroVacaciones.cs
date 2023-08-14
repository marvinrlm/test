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
   
    public partial class frmRegistroVacaciones : Form
    {
        private bool _inited = false;
        public frmRegistroVacaciones()
        {
            InitializeComponent();
        }

        private void frmRegistroVacaciones_Load(object sender, EventArgs e)
        {
            //CargarEstadoVacacion();
            CargarEmpleados();  
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void CargarEmpleados()
        {
            RegistrosVacacionesController oRegVac = new RegistrosVacacionesController();
            CboEmpleado.DataSource = oRegVac.CargarEmpleado();
            CboEmpleado.DisplayMember = "NombreCompleto";
            CboEmpleado.ValueMember = "Id";
            CboEmpleado.SelectedIndex=-1;
            _inited = true;

        }

        private void CargarRegistroVacaciones(int id)
        {
            RegistrosVacacionesController oRegVac = new RegistrosVacacionesController();
            dgvRegistroVacaciones.DataSource = oRegVac.CargarRegistroVacacionesId(id);

        }

        private void CboEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_inited == true)
            {
                //MessageBox.Show("Empleado:" + CboEmpleado.SelectedValue);
                CargarRegistroVacaciones((int)CboEmpleado.SelectedValue);
            }
          
        }

        private void btnGuardar_Click(object sender, EventArgs e)

        {
           // DateTime? Fecha = null;
            FrmAddVacaciones frm = new FrmAddVacaciones((int)CboEmpleado.SelectedValue);
            frm.ShowDialog();
            CargarRegistroVacaciones((int)CboEmpleado.SelectedValue);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            DateTime? Fecha = GetFechaVacacion();
            if (Fecha != null)
            {
                FrmUpdVacaciones frm = new FrmUpdVacaciones((int)CboEmpleado.SelectedValue,(DateTime)Fecha);
                frm.ShowDialog();
                CargarRegistroVacaciones((int)CboEmpleado.SelectedValue);

            }




        }

        #region HELPER
        private DateTime? GetFechaVacacion()
        {
            try
            {
                return (DateTime)dgvRegistroVacaciones.Rows[dgvRegistroVacaciones.CurrentRow.Index].Cells[2].Value;
            }
            catch
            {
                return null;
            }

        }

        #endregion


    }
}
