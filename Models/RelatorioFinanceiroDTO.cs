using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpreendedoresApp.Models
{
    public class RelatorioFinanceiroDTO
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int TotalVendas { get; set; }
        public decimal FaturamentoBruto { get; set; }
        public decimal TotalDescontos { get; set; }
        public decimal FaturamentoLiquido { get; set; }
    }
}
