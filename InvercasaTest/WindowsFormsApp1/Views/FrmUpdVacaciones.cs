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
    public partial class FrmUpdVacaciones : Form
    {
        private int _id;
        private DateTime _date;
        public FrmUpdVacaciones(int id, DateTime date)
        {
            InitializeComponent();
            _id = id;
            _date = date;
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("id: " + _id + "Fecha: " + _date);
            RegistrosVacacionesController oRegVac = new RegistrosVacacionesController();
            //oRegVac.Edit(_id, _date, (int)cboEstadoVacacion.SelectedValue);
            oRegVac.EditarVacacionesTrans(_id, _date,(int)cboEstadoVacacion.SelectedValue,_date.Year, _date.Month);
            this.Close();
        }

        private void FrmUpdVacaciones_Load(object sender, EventArgs e)
        {
            CargarEstadoVacacion();
        }
        private void CargarEstadoVacacion()
        {
            RegistrosVacacionesController oRegVac = new RegistrosVacacionesController();
            cboEstadoVacacion.DataSource = oRegVac.CargarEstadoVacaciones();
            cboEstadoVacacion.DisplayMember = "EstadoVacacion";
            cboEstadoVacacion.ValueMember = "Id";
            cboEstadoVacacion.SelectedIndex = -1;
        }


    }
}
