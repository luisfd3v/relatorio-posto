using System;
using System.Windows.Forms;

namespace RelatorioPosto.Forms
{
    public partial class frmPrincipal : Form
    {
        private RelatorioPosto.frmLogin loginForm;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        public frmPrincipal(RelatorioPosto.frmLogin login, string nomeColaborador)
        {
            InitializeComponent();
            loginForm = login;
            lblColaboradorSelecionado2.Text = nomeColaborador;
        }

        private void btnTrocarColaborador_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que quer trocar o colaborador?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (loginForm != null)
                {
                    loginForm.ResetFields();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    RelatorioPosto.frmLogin login = new RelatorioPosto.frmLogin();
                    login.Show();
                    this.Close();
                }
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (loginForm == null || !loginForm.Visible)
            {
                Application.Exit();
            }
        }
    }
}
