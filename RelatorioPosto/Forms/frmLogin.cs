using RelatorioPosto.DataAcess;
using System;
using System.Data;
using System.Windows.Forms;

namespace RelatorioPosto
{
    public partial class frmLogin : Form
    {
        private SqlDataAcess sqlDataAcess;

        public frmLogin()
        {
            InitializeComponent();
            sqlDataAcess = new SqlDataAcess();
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            sqlDataAcess.TestConnection();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string query = "SELECT CODIGO_VEN, NOME_VEN, AP_VEN FROM AVENDEGE WHERE AQ_VEN = 'S' ORDER BY CODIGO_VEN";
            DataTable dt = sqlDataAcess.ExecuteQuery(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("NomeCompleto", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    int codigo = Convert.ToInt32(row["CODIGO_VEN"]);
                    row["NomeCompleto"] = $"{codigo} - {row["NOME_VEN"]}";
                }

                cbxCodigoColaborador.DataSource = dt;
                cbxCodigoColaborador.DisplayMember = "NomeCompleto";
                cbxCodigoColaborador.ValueMember = "CODIGO_VEN";
                cbxCodigoColaborador.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                MessageBox.Show("Nenhum colaborador encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbxCodigoColaborador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbxCodigoColaborador.SelectedItem != null && cbxCodigoColaborador.DroppedDown == false)
                {
                    txbxSenhaColaborador.Focus();
                    return;
                }

                string codigoDigitado = cbxCodigoColaborador.Text.Trim();
                if (!string.IsNullOrEmpty(codigoDigitado) && int.TryParse(codigoDigitado, out int codigoInput))
                {
                    foreach (DataRowView item in cbxCodigoColaborador.Items)
                    {
                        if (Convert.ToInt32(item["CODIGO_VEN"]) == codigoInput)
                        {
                            cbxCodigoColaborador.SelectedItem = item;
                            txbxSenhaColaborador.Focus();
                            return;
                        }
                    }
                    MessageBox.Show($"Colaborador com código '{codigoDigitado}' não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxCodigoColaborador.Text = "";
                }
                else if (!string.IsNullOrEmpty(codigoDigitado))
                {
                    MessageBox.Show("Código deve ser numérico.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxCodigoColaborador.Text = "";
                }
            }
        }
    }
}
