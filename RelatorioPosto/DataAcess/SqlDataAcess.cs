using System;
using System.Collections.Generic;
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
                    MessageBox.Show("Conexão com o Banco de Datos bem-sucedida!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public List<Models.VendaItem> GetVendasPorColaboradorEPeriodo(string codigoVendedor, DateTime dataInicio, DateTime dataFim)
        {
            var vendas = new List<Models.VendaItem>();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            p.AA_ITE as CodigoProduto,
                            p.AB_ITE as DescricaoProduto,
                            SUM(al.AF_LPO) AS quantidade_total,
                            SUM(al.AL_LPO - al.AH_LPO) AS valor_liquido,
                            ac.AG_CUP as DataEmissao,
                            p.BZ_ITE as TipoProduto,
                            COUNT(DISTINCT ac.AA_CUP) AS num_abastecimentos
                        FROM 
                            ALANCAPO al
                        INNER JOIN 
                            ACUPOMPO ac ON al.AA_LPO = ac.AA_CUP 
                                AND al.AM_LPO = ac.AK_CUP 
                                AND al.AN_LPO = ac.AN_CUP
                        INNER JOIN 
                            CE_PRODUTO p ON CAST(al.AC_LPO AS INT) = CAST(p.AA_ITE AS INT)
                        WHERE 
                            ac.AF_CUP = @codigoVendedor
                            AND ac.AG_CUP >= @dataInicio
                            AND ac.AG_CUP < @dataFim
                            AND al.AB_LPO = '01'
                            AND ac.AH_CUP BETWEEN '3' AND '8'
                        GROUP BY 
                            p.AA_ITE, p.AB_ITE, ac.AG_CUP, p.BZ_ITE
                        ORDER BY 
                            p.AA_ITE, ac.AG_CUP
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigoVendedor", int.Parse(codigoVendedor));
                        cmd.Parameters.AddWithValue("@dataInicio", dataInicio.Date);
                        cmd.Parameters.AddWithValue("@dataFim", dataFim.AddDays(1).Date);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var produtosPorData = new Dictionary<(string, string), Models.VendaItem>();
                            var outrosTotal = (quantidade: 0.0, valorTotal: 0.0, numAbastecimentos: 0);

                            while (reader.Read())
                            {
                                string produtoCodigo = reader["CodigoProduto"].ToString();
                                string produtoDescricao = reader["DescricaoProduto"].ToString();
                                double quantidadeTotal = reader["quantidade_total"] != DBNull.Value ? Convert.ToDouble(reader["quantidade_total"]) : 0.0;
                                double valorLiquido = reader["valor_liquido"] != DBNull.Value ? Convert.ToDouble(reader["valor_liquido"]) : 0.0;
                                DateTime dataVenda = Convert.ToDateTime(reader["DataEmissao"]);
                                string bzIte = reader["TipoProduto"].ToString().Trim();
                                int numAbastecimentos = reader["num_abastecimentos"] != DBNull.Value ? Convert.ToInt32(reader["num_abastecimentos"]) : 1;

                                if (bzIte == "4" || bzIte == "04")
                                {
                                    var key = (produtoCodigo, produtoDescricao);
                                    if (!produtosPorData.ContainsKey(key))
                                    {
                                        produtosPorData[key] = new Models.VendaItem
                                        {
                                            ProdutoCodigo = produtoCodigo,
                                            ProdutoDescricao = produtoDescricao,
                                            Quantidade = 0.0,
                                            ValorTotal = 0.0,
                                            DataVenda = DateTime.Now.Date, // não usado
                                            TipoProduto = "combustivel",
                                            NumAbastecimentos = 0
                                        };
                                    }
                                    produtosPorData[key].Quantidade += quantidadeTotal;
                                    produtosPorData[key].ValorTotal += valorLiquido;
                                    produtosPorData[key].NumAbastecimentos += numAbastecimentos;
                                }
                                else
                                {
                                    outrosTotal.quantidade += quantidadeTotal;
                                    outrosTotal.valorTotal += valorLiquido;
                                    outrosTotal.numAbastecimentos += numAbastecimentos;
                                }
                            }

                            foreach (var item in produtosPorData.Values)
                            {
                                vendas.Add(item);
                            }

                            if (outrosTotal.valorTotal > 0)
                            {
                                vendas.Add(new Models.VendaItem
                                {
                                    ProdutoCodigo = "999",
                                    ProdutoDescricao = "OUTROS",
                                    Quantidade = outrosTotal.quantidade,
                                    ValorTotal = outrosTotal.valorTotal,
                                    DataVenda = DateTime.Now.Date,
                                    TipoProduto = "outros",
                                    NumAbastecimentos = outrosTotal.numAbastecimentos
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar vendas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return vendas;
        }

        public List<Models.VendaItem> GetVendasDetalhadasPorProduto(string codigoVendedor, string codigoProduto, DateTime dataInicio, DateTime dataFim)
        {
            var vendas = new List<Models.VendaItem>();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            p.AA_ITE as CodigoProduto,
                            p.AB_ITE as DescricaoProduto,
                            SUM(al.AF_LPO) AS quantidade_total,
                            SUM(al.AL_LPO - al.AH_LPO) AS valor_liquido,
                            ac.AG_CUP as DataEmissao,
                            p.BZ_ITE as TipoProduto,
                            COUNT(DISTINCT ac.AA_CUP) AS num_abastecimentos
                        FROM 
                            ALANCAPO al
                        INNER JOIN 
                            ACUPOMPO ac ON al.AA_LPO = ac.AA_CUP 
                                AND al.AM_LPO = ac.AK_CUP 
                                AND al.AN_LPO = ac.AN_CUP
                        INNER JOIN 
                            CE_PRODUTO p ON CAST(al.AC_LPO AS INT) = CAST(p.AA_ITE AS INT)
                        WHERE 
                            ac.AF_CUP = @codigoVendedor
                            AND p.AA_ITE = @codigoProduto
                            AND ac.AG_CUP >= @dataInicio
                            AND ac.AG_CUP < @dataFim
                            AND al.AB_LPO = '01'
                            AND ac.AH_CUP BETWEEN '3' AND '8'
                        GROUP BY 
                            p.AA_ITE, p.AB_ITE, ac.AG_CUP, p.BZ_ITE
                        ORDER BY 
                            ac.AG_CUP, p.AB_ITE
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigoVendedor", int.Parse(codigoVendedor));
                        cmd.Parameters.AddWithValue("@codigoProduto", codigoProduto);
                        cmd.Parameters.AddWithValue("@dataInicio", dataInicio.Date);
                        cmd.Parameters.AddWithValue("@dataFim", dataFim.AddDays(1).Date);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string produtoCodigo = reader["CodigoProduto"].ToString();
                                string produtoDescricao = reader["DescricaoProduto"].ToString();
                                double quantidadeTotal = reader["quantidade_total"] != DBNull.Value ? Convert.ToDouble(reader["quantidade_total"]) : 0.0;
                                double valorLiquido = reader["valor_liquido"] != DBNull.Value ? Convert.ToDouble(reader["valor_liquido"]) : 0.0;
                                DateTime dataVenda = Convert.ToDateTime(reader["DataEmissao"]);
                                string bzIte = reader["TipoProduto"].ToString().Trim();
                                int numAbastecimentos = reader["num_abastecimentos"] != DBNull.Value ? Convert.ToInt32(reader["num_abastecimentos"]) : 1;

                                vendas.Add(new Models.VendaItem
                                {
                                    ProdutoCodigo = produtoCodigo,
                                    ProdutoDescricao = produtoDescricao,
                                    Quantidade = quantidadeTotal,
                                    ValorTotal = valorLiquido,
                                    DataVenda = dataVenda,
                                    TipoProduto = bzIte == "4" || bzIte == "04" ? "combustivel" : "outros",
                                    NumAbastecimentos = numAbastecimentos
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar vendas detalhadas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return vendas;
        }
    }
}
