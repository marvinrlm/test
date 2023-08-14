namespace WindowsFormsApp1.Views
{
    partial class frmRegistroVacaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CboEmpleado = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRegistroVacaciones = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroVacaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // CboEmpleado
            // 
            this.CboEmpleado.FormattingEnabled = true;
            this.CboEmpleado.Location = new System.Drawing.Point(348, 41);
            this.CboEmpleado.Name = "CboEmpleado";
            this.CboEmpleado.Size = new System.Drawing.Size(415, 24);
            this.CboEmpleado.TabIndex = 0;
            this.CboEmpleado.SelectedIndexChanged += new System.EventHandler(this.CboEmpleado_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre del Empleado:";
            // 
            // dgvRegistroVacaciones
            // 
            this.dgvRegistroVacaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistroVacaciones.Location = new System.Drawing.Point(12, 146);
            this.dgvRegistroVacaciones.Name = "dgvRegistroVacaciones";
            this.dgvRegistroVacaciones.ReadOnly = true;
            this.dgvRegistroVacaciones.RowHeadersWidth = 51;
            this.dgvRegistroVacaciones.RowTemplate.Height = 24;
            this.dgvRegistroVacaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRegistroVacaciones.Size = new System.Drawing.Size(899, 627);
            this.dgvRegistroVacaciones.TabIndex = 6;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(804, 96);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(107, 32);
            this.btnCerrar.TabIndex = 7;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(13, 96);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(107, 32);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Agregar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(153, 96);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(107, 32);
            this.btnEditar.TabIndex = 9;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // frmRegistroVacaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 785);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvRegistroVacaciones);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CboEmpleado);
            this.Name = "frmRegistroVacaciones";
            this.Text = "frmRegistroVacaciones";
            this.Load += new System.EventHandler(this.frmRegistroVacaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistroVacaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CboEmpleado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvRegistroVacaciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEditar;
    }
}