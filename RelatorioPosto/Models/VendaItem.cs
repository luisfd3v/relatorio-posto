using System;

namespace RelatorioPosto.Models
{
    public class VendaItem
    {
        public string ProdutoCodigo { get; set; }
        public string ProdutoDescricao { get; set; }
        public double Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public DateTime DataVenda { get; set; }
        public string TipoProduto { get; set; }
        public int NumAbastecimentos { get; set; }
    }
}