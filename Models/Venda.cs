using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace EmpreendedoresApp.Models
{
    public class Venda
    {
        public Venda()
        {
            Itens = new ObservableCollection<ItemVenda>();
        }

        // Representa uma venda realizada

        public int Id { get; set; }

        // Data da venda
        public DateTime DataVenda { get; set; } = DateTime.Now;
        
        public ICollection<ItemVenda> Itens { get; set; }

        // Desconto % aplicado na venda
        public decimal DescontoPercentual { get; set; } = 0;
        
        public decimal Subtotal => Itens.Sum(item => item.Subtotal);

        public decimal ValorDesconto => Subtotal * (DescontoPercentual) / 100;
        
        public decimal Total => Subtotal - ValorDesconto;
        
    }
}
