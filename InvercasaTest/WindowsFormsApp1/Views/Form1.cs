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
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Views;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefrescarFormulario();
        }

        private void RefrescarFormulario()

        {
           EmpleadosController oEmpleadoController = new EmpleadosController();

            dgvEmpleados.DataSource = oEmpleadoController.CargarEmpleadosFromView();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)

        {
            int? Id = null;
            addEmpleado addEmpleado = new addEmpleado(Id);
            addEmpleado.ShowDialog();
            this.RefrescarFormulario();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //EditEmpleado editEmpleado = new EditEmpleado();
            //editEmpleado.ShowDialog();
            //this.RefrescarFormulario();
            int? Id = GetId();
            if (Id != null)
            {
                addEmpleado editEmpleado = new addEmpleado(Id);
                editEmpleado.ShowDialog();
                this.RefrescarFormulario();

            }

        }

        #region HELPER
            private int? GetId()
            {
                try
                {
                    return (int)dgvEmpleados.Rows[dgvEmpleados.CurrentRow.Index].Cells[0].Value;
                }
                catch {
                return null;
                }
             
             }

        #endregion

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? Id = GetId();
            if (Id != null)
            {

                EmpleadosController deleteEmpleado = new EmpleadosController();
                deleteEmpleado.Delete((int)Id);
                this.RefrescarFormulario();

            }
        }
    }
}
