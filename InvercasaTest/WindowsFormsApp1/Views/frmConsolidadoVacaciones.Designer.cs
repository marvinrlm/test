namespace WindowsFormsApp1.Views
{
    partial class frmConsolidadoVacaciones
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
            this.dgvConsolidadoVacaciones = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsolidadoVacaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConsolidadoVacaciones
            // 
            this.dgvConsolidadoVacaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsolidadoVacaciones.Location = new System.Drawing.Point(12, 84);
            this.dgvConsolidadoVacaciones.Name = "dgvConsolidadoVacaciones";
            this.dgvConsolidadoVacaciones.ReadOnly = true;
            this.dgvConsolidadoVacaciones.RowHeadersWidth = 51;
            this.dgvConsolidadoVacaciones.RowTemplate.Height = 24;
            this.dgvConsolidadoVacaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConsolidadoVacaciones.Size = new System.Drawing.Size(1243, 638);
            this.dgvConsolidadoVacaciones.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(1111, 33);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(143, 38);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // frmConsolidadoVacaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 734);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvConsolidadoVacaciones);
            this.Name = "frmConsolidadoVacaciones";
            this.Text = "frmConsolidadoVacaciones";
            this.Load += new System.EventHandler(this.frmConsolidadoVacaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsolidadoVacaciones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConsolidadoVacaciones;
        private System.Windows.Forms.Button btnCerrar;
    }
}