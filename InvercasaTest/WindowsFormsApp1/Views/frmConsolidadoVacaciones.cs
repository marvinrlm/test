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
    public partial class frmConsolidadoVacaciones : Form
    {
        public frmConsolidadoVacaciones()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConsolidadoVacaciones_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void Refrescar()
        {
            VacacionesConsolidadasController oVacConsol = new VacacionesConsolidadasController();
            dgvConsolidadoVacaciones.DataSource = oVacConsol.CargarVacacionesConsolidadasFromView();
        }
    }
}
