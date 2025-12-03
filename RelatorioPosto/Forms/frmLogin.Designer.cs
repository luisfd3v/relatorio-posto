namespace RelatorioPosto
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblRelatorioVendas = new System.Windows.Forms.Label();
            this.gpbxIdColaborador = new System.Windows.Forms.GroupBox();
            this.txbxSenhaColaborador = new System.Windows.Forms.TextBox();
            this.lblSenhaColaborador = new System.Windows.Forms.Label();
            this.cbxCodigoColaborador = new System.Windows.Forms.ComboBox();
            this.lblCodigoColaborador = new System.Windows.Forms.Label();
            this.btnConfirmarLogin = new System.Windows.Forms.Button();
            this.btnCancelarLogin = new System.Windows.Forms.Button();
            this.gpbxIdColaborador.SuspendLayout();
            this.SuspendLayout();

 
            this.lblRelatorioVendas.AutoSize = true;
            this.lblRelatorioVendas.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelatorioVendas.Location = new System.Drawing.Point(47, 9);
            this.lblRelatorioVendas.Name = "lblRelatorioVendas";
            this.lblRelatorioVendas.Size = new System.Drawing.Size(278, 31);
            this.lblRelatorioVendas.TabIndex = 0;
            this.lblRelatorioVendas.Text = "Relatório de Vendas";
            this.lblRelatorioVendas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


            this.gpbxIdColaborador.Controls.Add(this.txbxSenhaColaborador);
            this.gpbxIdColaborador.Controls.Add(this.lblSenhaColaborador);
            this.gpbxIdColaborador.Controls.Add(this.cbxCodigoColaborador);
            this.gpbxIdColaborador.Controls.Add(this.lblCodigoColaborador);
            this.gpbxIdColaborador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbxIdColaborador.Location = new System.Drawing.Point(12, 64);
            this.gpbxIdColaborador.Name = "gpbxIdColaborador";
            this.gpbxIdColaborador.Size = new System.Drawing.Size(360, 147);
            this.gpbxIdColaborador.TabIndex = 1;
            this.gpbxIdColaborador.TabStop = false;
            this.gpbxIdColaborador.Text = "Identificação do Colaborador";


            this.txbxSenhaColaborador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txbxSenhaColaborador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbxSenhaColaborador.Location = new System.Drawing.Point(17, 108);
            this.txbxSenhaColaborador.Name = "txbxSenhaColaborador";
            this.txbxSenhaColaborador.Size = new System.Drawing.Size(329, 21);
            this.txbxSenhaColaborador.TabIndex = 2;


            this.lblSenhaColaborador.AutoSize = true;
            this.lblSenhaColaborador.Location = new System.Drawing.Point(14, 87);
            this.lblSenhaColaborador.Name = "lblSenhaColaborador";
            this.lblSenhaColaborador.Size = new System.Drawing.Size(46, 15);
            this.lblSenhaColaborador.TabIndex = 2;
            this.lblSenhaColaborador.Text = "Senha:";


            this.cbxCodigoColaborador.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCodigoColaborador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCodigoColaborador.FormattingEnabled = true;
            this.cbxCodigoColaborador.Location = new System.Drawing.Point(17, 49);
            this.cbxCodigoColaborador.Name = "cbxCodigoColaborador";
            this.cbxCodigoColaborador.Size = new System.Drawing.Size(329, 23);
            this.cbxCodigoColaborador.TabIndex = 1;
            this.cbxCodigoColaborador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxCodigoColaborador_KeyDown);


            this.lblCodigoColaborador.AutoSize = true;
            this.lblCodigoColaborador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoColaborador.Location = new System.Drawing.Point(14, 31);
            this.lblCodigoColaborador.Name = "lblCodigoColaborador";
            this.lblCodigoColaborador.Size = new System.Drawing.Size(137, 15);
            this.lblCodigoColaborador.TabIndex = 0;
            this.lblCodigoColaborador.Text = "Código do Colaborador:";


            this.btnConfirmarLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarLogin.Location = new System.Drawing.Point(303, 226);
            this.btnConfirmarLogin.Name = "btnConfirmarLogin";
            this.btnConfirmarLogin.Size = new System.Drawing.Size(69, 23);
            this.btnConfirmarLogin.TabIndex = 2;
            this.btnConfirmarLogin.Text = "Confirmar";
            this.btnConfirmarLogin.UseVisualStyleBackColor = true;
            this.btnConfirmarLogin.Click += new System.EventHandler(this.btnConfirmarLogin_Click);


            this.btnCancelarLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarLogin.Location = new System.Drawing.Point(228, 226);
            this.btnCancelarLogin.Name = "btnCancelarLogin";
            this.btnCancelarLogin.Size = new System.Drawing.Size(69, 23);
            this.btnCancelarLogin.TabIndex = 3;
            this.btnCancelarLogin.Text = "Cancelar";
            this.btnCancelarLogin.UseVisualStyleBackColor = true;


            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.btnCancelarLogin);
            this.Controls.Add(this.btnConfirmarLogin);
            this.Controls.Add(this.gpbxIdColaborador);
            this.Controls.Add(this.lblRelatorioVendas);
            this.Name = "frmLogin";
            this.Text = "Seleção de Colaborador";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.gpbxIdColaborador.ResumeLayout(false);
            this.gpbxIdColaborador.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRelatorioVendas;
        private System.Windows.Forms.GroupBox gpbxIdColaborador;
        private System.Windows.Forms.ComboBox cbxCodigoColaborador;
        private System.Windows.Forms.Label lblCodigoColaborador;
        private System.Windows.Forms.Label lblSenhaColaborador;
        private System.Windows.Forms.TextBox txbxSenhaColaborador;
        private System.Windows.Forms.Button btnConfirmarLogin;
        private System.Windows.Forms.Button btnCancelarLogin;
    }
}

