using EmpreendedoresApp.Data;
using EmpreendedoresApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EmpreendedoresApp.ViewModels
{
    public class VendasSaidasViewModel
    {
        private readonly AppDbContext _context;

        public Venda VendaAtual { get; private set; }

        public VendasSaidasViewModel()
        {
            _context = new AppDbContext();
            VendaAtual = new Venda();
        }

      

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (produto == null)
                throw new InvalidOperationException("Produto não selecionado");

            if (quantidade <= 0)
                throw new InvalidOperationException("Quantidade inválida");

            var produtoDb = _context.Produtos.Find(produto.Id);

            if (produtoDb == null)
                throw new InvalidOperationException("Produto não encontrado no banco de dados");

            var item = new ItemVenda
            {
                Produto = produtoDb,
                Quantidade = quantidade,
                PrecoUnitario = produtoDb.Preco
            };

            item.Venda = VendaAtual;
            VendaAtual.Itens.Add(item);
        }

        public void FinalizarVenda()
        {
            if (!VendaAtual.Itens.Any())
                throw new InvalidOperationException("Nenhum item adicionado à venda");
            
            if (VendaAtual.Id != 0)
                throw new InvalidOperationException("Venda já finalizada");

            _context.Vendas.Add(VendaAtual);
            _context.SaveChanges();
        }  

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
