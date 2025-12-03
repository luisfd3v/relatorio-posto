using RelatorioPosto.DataAcess;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using RelatorioPosto.Forms;

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

            txbxSenhaColaborador.PasswordChar = '*';

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
                cbxCodigoColaborador.SelectedIndex = -1;
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

        private void btnConfirmarLogin_Click(object sender, EventArgs e)
        {
            if (cbxCodigoColaborador.SelectedValue != null)
            {
                int codigo = Convert.ToInt32(cbxCodigoColaborador.SelectedValue);
                string senhaInput = txbxSenhaColaborador.Text.Trim();
                if (string.IsNullOrEmpty(senhaInput))
                {
                    MessageBox.Show("Digite a senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string senhaHash = GetMd5Hash(senhaInput);

                string query = $"SELECT AP_VEN FROM AVENDEGE WHERE CODIGO_VEN = {codigo}";
                DataTable dt = sqlDataAcess.ExecuteQuery(query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string senhaBanco = dt.Rows[0]["AP_VEN"].ToString();
                    if (senhaHash.Equals(senhaBanco, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Login bem-sucedido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataRowView selectedRow = (DataRowView)cbxCodigoColaborador.SelectedItem;
                        string nomeColaborador = selectedRow["NomeCompleto"].ToString();
                        RelatorioPosto.Forms.frmPrincipal principal = new RelatorioPosto.Forms.frmPrincipal(this, nomeColaborador);
                        principal.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Senha incorreta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Colaborador não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecione um colaborador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public void ResetFields()
        {
            cbxCodigoColaborador.SelectedIndex = -1;
            txbxSenhaColaborador.Text = "";
        }
    }
}
