namespace RelatorioPosto.Forms
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.gpbxFiltroColaborador = new System.Windows.Forms.GroupBox();
            this.btnFiltrarData = new System.Windows.Forms.Button();
            this.btnTrocarColaborador = new System.Windows.Forms.Button();
            this.dtpDataFim = new System.Windows.Forms.DateTimePicker();
            this.dtpDataInicio = new System.Windows.Forms.DateTimePicker();
            this.lblDataFim = new System.Windows.Forms.Label();
            this.lblDataInicio = new System.Windows.Forms.Label();
            this.lblColaboradorSelecionado2 = new System.Windows.Forms.Label();
            this.lblColaboradorSelecionado = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gpbxFiltroColaborador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbxFiltroColaborador
            // 
            this.gpbxFiltroColaborador.Controls.Add(this.btnFiltrarData);
            this.gpbxFiltroColaborador.Controls.Add(this.btnTrocarColaborador);
            this.gpbxFiltroColaborador.Controls.Add(this.dtpDataFim);
            this.gpbxFiltroColaborador.Controls.Add(this.dtpDataInicio);
            this.gpbxFiltroColaborador.Controls.Add(this.lblDataFim);
            this.gpbxFiltroColaborador.Controls.Add(this.lblDataInicio);
            this.gpbxFiltroColaborador.Controls.Add(this.lblColaboradorSelecionado2);
            this.gpbxFiltroColaborador.Controls.Add(this.lblColaboradorSelecionado);
            this.gpbxFiltroColaborador.Location = new System.Drawing.Point(7, 5);
            this.gpbxFiltroColaborador.Name = "gpbxFiltroColaborador";
            this.gpbxFiltroColaborador.Size = new System.Drawing.Size(770, 105);
            this.gpbxFiltroColaborador.TabIndex = 0;
            this.gpbxFiltroColaborador.TabStop = false;
            // 
            // btnFiltrarData
            // 
            this.btnFiltrarData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarData.Location = new System.Drawing.Point(537, 46);
            this.btnFiltrarData.Name = "btnFiltrarData";
            this.btnFiltrarData.Size = new System.Drawing.Size(78, 46);
            this.btnFiltrarData.TabIndex = 6;
            this.btnFiltrarData.Text = "Filtrar";
            this.btnFiltrarData.UseVisualStyleBackColor = true;
            // 
            // btnTrocarColaborador
            // 
            this.btnTrocarColaborador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrocarColaborador.Location = new System.Drawing.Point(633, 12);
            this.btnTrocarColaborador.Name = "btnTrocarColaborador";
            this.btnTrocarColaborador.Size = new System.Drawing.Size(121, 23);
            this.btnTrocarColaborador.TabIndex = 5;
            this.btnTrocarColaborador.Text = "Trocar Colaborador";
            this.btnTrocarColaborador.UseVisualStyleBackColor = true;
            this.btnTrocarColaborador.Click += new System.EventHandler(this.btnTrocarColaborador_Click);
            // 
            // dtpDataFim
            // 
            this.dtpDataFim.Location = new System.Drawing.Point(80, 72);
            this.dtpDataFim.Name = "dtpDataFim";
            this.dtpDataFim.Size = new System.Drawing.Size(440, 20);
            this.dtpDataFim.TabIndex = 4;
            // 
            // dtpDataInicio
            // 
            this.dtpDataInicio.Location = new System.Drawing.Point(80, 46);
            this.dtpDataInicio.Name = "dtpDataInicio";
            this.dtpDataInicio.Size = new System.Drawing.Size(440, 20);
            this.dtpDataInicio.TabIndex = 1;
            // 
            // lblDataFim
            // 
            this.lblDataFim.AutoSize = true;
            this.lblDataFim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataFim.Location = new System.Drawing.Point(6, 75);
            this.lblDataFim.Name = "lblDataFim";
            this.lblDataFim.Size = new System.Drawing.Size(60, 15);
            this.lblDataFim.TabIndex = 3;
            this.lblDataFim.Text = "Data Fim:";
            // 
            // lblDataInicio
            // 
            this.lblDataInicio.AutoSize = true;
            this.lblDataInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataInicio.Location = new System.Drawing.Point(6, 47);
            this.lblDataInicio.Name = "lblDataInicio";
            this.lblDataInicio.Size = new System.Drawing.Size(68, 15);
            this.lblDataInicio.TabIndex = 2;
            this.lblDataInicio.Text = "Data Início:";
            // 
            // lblColaboradorSelecionado2
            // 
            this.lblColaboradorSelecionado2.AutoSize = true;
            this.lblColaboradorSelecionado2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColaboradorSelecionado2.Location = new System.Drawing.Point(155, 16);
            this.lblColaboradorSelecionado2.Name = "lblColaboradorSelecionado2";
            this.lblColaboradorSelecionado2.Size = new System.Drawing.Size(90, 17);
            this.lblColaboradorSelecionado2.TabIndex = 1;
            this.lblColaboradorSelecionado2.Text = "1 - WCLICK";
            // 
            // lblColaboradorSelecionado
            // 
            this.lblColaboradorSelecionado.AutoSize = true;
            this.lblColaboradorSelecionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColaboradorSelecionado.Location = new System.Drawing.Point(6, 16);
            this.lblColaboradorSelecionado.Name = "lblColaboradorSelecionado";
            this.lblColaboradorSelecionado.Size = new System.Drawing.Size(150, 15);
            this.lblColaboradorSelecionado.TabIndex = 0;
            this.lblColaboradorSelecionado.Text = "Colaborador Selecionado:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 116);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(770, 440);
            this.dataGridView1.TabIndex = 1;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.gpbxFiltroColaborador);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.Text = "Relatório de Vendas";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrincipal_FormClosed);
            this.gpbxFiltroColaborador.ResumeLayout(false);
            this.gpbxFiltroColaborador.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbxFiltroColaborador;
        private System.Windows.Forms.Label lblColaboradorSelecionado;
        private System.Windows.Forms.Label lblColaboradorSelecionado2;
        private System.Windows.Forms.Label lblDataFim;
        private System.Windows.Forms.Label lblDataInicio;
        private System.Windows.Forms.DateTimePicker dtpDataFim;
        private System.Windows.Forms.DateTimePicker dtpDataInicio;
        private System.Windows.Forms.Button btnTrocarColaborador;
        private System.Windows.Forms.Button btnFiltrarData;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}