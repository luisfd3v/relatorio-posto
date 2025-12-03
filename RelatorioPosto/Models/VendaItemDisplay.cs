using System;

namespace RelatorioPosto.Models
{
    public class VendaItemDisplay
    {
        public bool IsExpanded { get; set; }
        public bool IsDetailRow { get; set; }
        public string ProdutoCodigo { get; set; }
        public string ProdutoDescricao { get; set; }
        public int NumAbastecimentos { get; set; }
        public double Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public DateTime? DataVenda { get; set; }
        public string TipoProduto { get; set; }
        public VendaItem ProdutoPai { get; set; }

        public string BotaoExpansao
        {
            get
            {
                if (IsDetailRow)
                    return string.Empty;
                return IsExpanded ? "-" : "+";
            }
        }

        public string DescricaoFormatada
        {
            get
            {
                if (IsDetailRow)
                {
                    if (TipoProduto == "total_dia" && DataVenda.HasValue)
                        return $"{DataVenda.Value:dd/MM/yyyy} - TOTAL DO DIA";
                    else if (DataVenda.HasValue)
                        return $"        {ProdutoDescricao}";
                }
                return ProdutoDescricao;
            }
        }

        public VendaItemDisplay(VendaItem item, bool isDetail = false, DateTime? data = null)
        {
            IsExpanded = false;
            IsDetailRow = isDetail;
            ProdutoCodigo = item.ProdutoCodigo;
            ProdutoDescricao = item.ProdutoDescricao;
            NumAbastecimentos = item.NumAbastecimentos;
            Quantidade = item.Quantidade;
            ValorTotal = item.ValorTotal;
            DataVenda = data ?? item.DataVenda;
            TipoProduto = item.TipoProduto;
            if (isDetail)
                ProdutoPai = null;
        }
    }
}
