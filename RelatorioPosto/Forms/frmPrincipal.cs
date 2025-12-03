using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RelatorioPosto.Forms
{
    public partial class frmPrincipal : Form
    {
        private RelatorioPosto.frmLogin loginForm;
        private string colaboradorCodigo;
        private List<Models.VendaItemDisplay> vendasDisplay;
        private Dictionary<string, List<Models.VendaItem>> vendasDetalhadasCache;

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
            vendasDisplay = new List<Models.VendaItemDisplay>();
            vendasDetalhadasCache = new Dictionary<string, List<Models.VendaItem>>();
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
            ConfigurarDataGridView();
            CarregarDados();
        }

        private void ConfigurarDataGridView()
        {
            dtgvProdutos.AutoGenerateColumns = false;
            dtgvProdutos.Columns.Clear();

            // Coluna do botão de expansão
            DataGridViewTextBoxColumn colExpansao = new DataGridViewTextBoxColumn();
            colExpansao.DataPropertyName = "BotaoExpansao";
            colExpansao.HeaderText = "";
            colExpansao.Name = "Expansao";
            colExpansao.Width = 30;
            colExpansao.ReadOnly = true;
            colExpansao.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colExpansao.DefaultCellStyle.Font = new Font(dtgvProdutos.Font.FontFamily, 12, FontStyle.Bold);
            dtgvProdutos.Columns.Add(colExpansao);

            // Coluna Produto
            DataGridViewTextBoxColumn colProduto = new DataGridViewTextBoxColumn();
            colProduto.DataPropertyName = "DescricaoFormatada";
            colProduto.HeaderText = "Produto";
            colProduto.Name = "Produto";
            colProduto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProduto.FillWeight = 40;
            dtgvProdutos.Columns.Add(colProduto);

            // Coluna Abastecimentos
            DataGridViewTextBoxColumn colAbastecimentos = new DataGridViewTextBoxColumn();
            colAbastecimentos.DataPropertyName = "NumAbastecimentos";
            colAbastecimentos.HeaderText = "Abastecimentos";
            colAbastecimentos.Name = "Abastecimentos";
            colAbastecimentos.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAbastecimentos.FillWeight = 20;
            colAbastecimentos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAbastecimentos.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvProdutos.Columns.Add(colAbastecimentos);

            // Coluna Litros
            DataGridViewTextBoxColumn colLitros = new DataGridViewTextBoxColumn();
            colLitros.DataPropertyName = "Quantidade";
            colLitros.HeaderText = "Litros";
            colLitros.Name = "Litros";
            colLitros.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colLitros.FillWeight = 20;
            colLitros.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colLitros.DefaultCellStyle.Format = "N3";
            colLitros.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvProdutos.Columns.Add(colLitros);

            // Coluna Total Vendido
            DataGridViewTextBoxColumn colTotalVendido = new DataGridViewTextBoxColumn();
            colTotalVendido.DataPropertyName = "ValorTotal";
            colTotalVendido.HeaderText = "Total Vendido";
            colTotalVendido.Name = "TotalVendido";
            colTotalVendido.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTotalVendido.FillWeight = 20;
            colTotalVendido.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotalVendido.DefaultCellStyle.Format = "C2";
            colTotalVendido.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvProdutos.Columns.Add(colTotalVendido);

            // Configurações gerais do DataGridView
            dtgvProdutos.AllowUserToAddRows = false;
            dtgvProdutos.AllowUserToDeleteRows = false;
            dtgvProdutos.AllowUserToResizeRows = false;
            dtgvProdutos.AllowUserToResizeColumns = false;
            dtgvProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dtgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvProdutos.MultiSelect = false;
            dtgvProdutos.RowHeadersVisible = false;
            dtgvProdutos.BackgroundColor = System.Drawing.Color.White;
            dtgvProdutos.BorderStyle = BorderStyle.Fixed3D;

            // Adicionar eventos
            dtgvProdutos.CellClick += dtgvProdutos_CellClick;
            dtgvProdutos.CellDoubleClick += dtgvProdutos_CellDoubleClick;
            dtgvProdutos.CellFormatting += dtgvProdutos_CellFormatting;
        }

        private void dtgvProdutos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < vendasDisplay.Count)
            {
                var item = vendasDisplay[e.RowIndex];

                // Aplicar estilo diferente para linhas de detalhe
                if (item.IsDetailRow)
                {
                    if (item.TipoProduto == "total_dia")
                    {
                        // Estilo para TOTAL DO DIA
                        e.CellStyle.BackColor = Color.LightBlue;
                        e.CellStyle.Font = new Font(dtgvProdutos.Font, FontStyle.Bold);
                        e.CellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        // Estilo para linhas de produtos individuais
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void dtgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0) // Clique na coluna do botão de expansão
            {
                AlternarExpansao(e.RowIndex);
            }
        }

        private void dtgvProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0) // Duplo clique em qualquer coluna exceto a de expansão
            {
                var item = vendasDisplay[e.RowIndex];
                if (!item.IsDetailRow)
                {
                    AlternarExpansao(e.RowIndex);
                }
            }
        }

        private void AlternarExpansao(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= vendasDisplay.Count)
                return;

            var item = vendasDisplay[rowIndex];

            // Não expandir linhas de detalhe
            if (item.IsDetailRow)
                return;

            if (item.IsExpanded)
            {
                // Recolher - remover linhas de detalhe
                RecolherProduto(rowIndex);
            }
            else
            {
                // Expandir - adicionar linhas de detalhe
                ExpandirProduto(rowIndex);
            }

            item.IsExpanded = !item.IsExpanded;
            AtualizarGrid();
        }

        private void ExpandirProduto(int rowIndex)
        {
            var produto = vendasDisplay[rowIndex];

            // Buscar detalhes se não estiverem em cache
            if (!vendasDetalhadasCache.ContainsKey(produto.ProdutoCodigo))
            {
                var sqlDataAccess = new DataAcess.SqlDataAcess();
                var detalhes = sqlDataAccess.GetVendasDetalhadasPorProduto(
                    colaboradorCodigo,
                    produto.ProdutoCodigo,
                    dtpDataInicio.Value,
                    dtpDataFim.Value
                );
                vendasDetalhadasCache[produto.ProdutoCodigo] = detalhes;
            }

            var vendasDetalhadas = vendasDetalhadasCache[produto.ProdutoCodigo];

            // Agrupar por data e adicionar linhas
            var vendasPorData = vendasDetalhadas
                .GroupBy(v => v.DataVenda.Date)
                .OrderBy(g => g.Key);

            int insertIndex = rowIndex + 1;

            foreach (var grupo in vendasPorData)
            {
                // Adicionar linha de TOTAL DO DIA
                var totalDia = new Models.VendaItem
                {
                    ProdutoCodigo = produto.ProdutoCodigo,
                    ProdutoDescricao = "TOTAL DO DIA",
                    NumAbastecimentos = grupo.Sum(v => v.NumAbastecimentos),
                    Quantidade = grupo.Sum(v => v.Quantidade),
                    ValorTotal = grupo.Sum(v => v.ValorTotal),
                    DataVenda = grupo.Key,
                    TipoProduto = "total_dia"
                };
                var linhaTotalDia = new Models.VendaItemDisplay(totalDia, true, grupo.Key);
                vendasDisplay.Insert(insertIndex, linhaTotalDia);
                insertIndex++;

                // Adicionar linhas de produtos do dia
                foreach (var venda in grupo.OrderBy(v => v.ProdutoDescricao))
                {
                    var detalhe = new Models.VendaItemDisplay(venda, true, grupo.Key);
                    vendasDisplay.Insert(insertIndex, detalhe);
                    insertIndex++;
                }
            }
        }

        private void RecolherProduto(int rowIndex)
        {
            var produto = vendasDisplay[rowIndex];

            // Remover todas as linhas de detalhe após este produto
            int i = rowIndex + 1;
            while (i < vendasDisplay.Count && vendasDisplay[i].IsDetailRow)
            {
                vendasDisplay.RemoveAt(i);
            }
        }

        private void AtualizarGrid()
        {
            // Salvar a seleção atual
            int selectedIndex = dtgvProdutos.CurrentRow?.Index ?? -1;

            // Atualizar o DataSource
            dtgvProdutos.DataSource = null;
            dtgvProdutos.DataSource = vendasDisplay;

            // Restaurar a seleção
            if (selectedIndex >= 0 && selectedIndex < dtgvProdutos.Rows.Count)
            {
                dtgvProdutos.Rows[selectedIndex].Selected = true;
                dtgvProdutos.CurrentCell = dtgvProdutos.Rows[selectedIndex].Cells[1];
            }
        }

        private void CarregarDados()
        {
            var sqlDataAcess = new RelatorioPosto.DataAcess.SqlDataAcess();
            var vendas = sqlDataAcess.GetVendasPorColaboradorEPeriodo(colaboradorCodigo, dtpDataInicio.Value, dtpDataFim.Value);

            // Ordenar por código do produto (AA_ITE)
            var vendasOrdenadas = vendas.OrderBy(v => v.ProdutoCodigo).ToList();

            // Limpar cache e lista de display
            vendasDetalhadasCache.Clear();
            vendasDisplay.Clear();

            // Criar itens de display para cada produto
            foreach (var venda in vendasOrdenadas)
            {
                vendasDisplay.Add(new Models.VendaItemDisplay(venda));
            }

            dtgvProdutos.DataSource = vendasDisplay;
        }

        private void btnFiltrarData_Click(object sender, EventArgs e)
        {
            vendasDetalhadasCache.Clear();
            vendasDisplay.Clear();
            CarregarDados();
        }
    }
}
