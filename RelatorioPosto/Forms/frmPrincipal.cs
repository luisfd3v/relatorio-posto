using System;
using System.Windows.Forms;

namespace RelatorioPosto.Forms
{
    public partial class frmPrincipal : Form
    {
        private RelatorioPosto.frmLogin loginForm;
        private string colaboradorCodigo;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        public frmPrincipal(RelatorioPosto.frmLogin login, string nomeColaborador, string codigoColaborador)
        {
            InitializeComponent();
            loginForm = login;
            colaboradorCodigo = codigoColaborador;
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

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dtpDataInicio.Value = new DateTime(now.Year, now.Month, 1);
            dtpDataFim.Value = now;
            CarregarDados();
        }

        private void CarregarDados()
        {
            var sqlDataAcess = new RelatorioPosto.DataAcess.SqlDataAcess();
            var vendas = sqlDataAcess.GetVendasPorColaboradorEPeriodo(colaboradorCodigo, dtpDataInicio.Value, dtpDataFim.Value);
            dtgvProdutos.DataSource = vendas;
        }

        private void btnFiltrarData_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }
    }
}
