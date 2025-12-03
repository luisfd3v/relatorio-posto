using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RelatorioPosto.DataAcess
{
    public class SqlDataAcess
    {
        private const string ConnectionStringName = "BancoVTi";
        private readonly string _connectionString;

        public SqlDataAcess()
        {
            ConnectionStringSettings connectionSetting = ConfigurationManager.ConnectionStrings[ConnectionStringName];

            if (connectionSetting == null || string.IsNullOrEmpty(connectionSetting.ConnectionString))
            {
                throw new ConfigurationErrorsException($"Erro: A string de conexão '{ConnectionStringName}' não foi encontrada ou está vazia no App.config.");
            }

            _connectionString = connectionSetting.ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MessageBox.Show("Conexão com o Banco de Dados bem-sucedida!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show($"Erro Crítico de Configuração: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Falha na Conexão SQL: Verifique suas credenciais e o servidor.\n\nDetalhes do Erro: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro Inesperado durante o Teste de Conexão: {ex.Message}", "Erro Desconhecido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar consulta: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
