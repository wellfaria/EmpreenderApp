using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpreendedoresApp.Models;
using EmpreendedoresApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EmpreendedoresApp.Services
{
    public class RelatorioFinanceiroService
    {
        public RelatorioFinanceiroDTO Gerar(DateTime inicio, DateTime fim)
        {
            using var context = new AppDbContext();

            var vendas = context.Vendas
                .Include(v => v.Itens)
                .Where(v => v.DataVenda.Date >= inicio.Date && v.DataVenda.Date <= fim.Date)
                .ToList();
            

            var faturamentoBruto = vendas.Sum(v => v.Subtotal);
            var totalDescontos = vendas.Sum(v => v.ValorDesconto);

            return new RelatorioFinanceiroDTO 
            {
                DataInicio = inicio,
                DataFim = fim,
                TotalVendas = vendas.Count,
                FaturamentoBruto = faturamentoBruto,
                TotalDescontos = totalDescontos,
                FaturamentoLiquido = faturamentoBruto - totalDescontos

            };
        }
    }
}
