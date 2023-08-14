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
    public partial class FrmFuncionEscalar : Form
    {
        public FrmFuncionEscalar()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            RegistrosVacacionesController oRegVac = new RegistrosVacacionesController();
            txtResultado.Text = oRegVac.Calcular1(dtpFechaIni.Value, dtpFechaFin.Value);
        }

    }
}
