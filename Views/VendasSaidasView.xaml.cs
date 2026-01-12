using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using EmpreendedoresApp.Models;
using EmpreendedoresApp.Data;
using Microsoft.EntityFrameworkCore;
using EmpreendedoresApp.ViewModels;

namespace EmpreendedoresApp.Views
{
    /// <summary>
    /// Lógica interna para VendasSaidasView.xaml
    /// </summary>
    /// 
    public partial class VendasSaidasView : Window
    {

        private Venda _vendaAtual = new Venda();

        public VendasSaidasView()
        {
            InitializeComponent();
            DataContext = new VendasSaidasViewModel();
            Loaded += VendasSaidasView_Loaded;
            CarregarProdutos();
        }

        private void VendasSaidasView_Loaded(object sender, RoutedEventArgs e)
        {
            txtDesconto.Text="0";
            dgItensVenda.ItemsSource = _vendaAtual.Itens;
            AtualizarTotal();
        }

        private void ResetarVenda()
            {
            _vendaAtual = new Venda();
            dgItensVenda.ItemsSource = _vendaAtual.Itens;
           

            cmbProdutos.SelectedIndex = -1;
            txtQuantidade.Text = "1";
            txtPrecoUnitario.Text = "";
            txtDesconto.Text = "0";

           

            AtualizarTotal();
        }

        private void CarregarProdutos()
        {
            using var context = new AppDbContext();

            cmbProdutos.ItemsSource = context.Produtos
                .OrderBy(p => p.Nome)
                .ToList();
        }

        private void CmbProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProdutos.SelectedItem is Produto produto) 
            {
                txtPrecoUnitario.Text = produto.Preco.ToString("F2");
            }
        }

        private void BtnAdicionarItem_Click(object sender, RoutedEventArgs e)
        {
            //Produto Selecionado
            if(cmbProdutos.SelectedItem is not Produto produto)
            {
                MessageBox.Show("Selecione um produto/serviço.");
                return;
            }

            //Quantidade
            if(!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }
            //Preço Unitário
            if(!decimal.TryParse(txtPrecoUnitario.Text,
                NumberStyles.Any,
                CultureInfo.CurrentCulture,
                out decimal precoUnitario) || precoUnitario <= 0)
            {
                MessageBox.Show("Preço inválido.");
                return;
            }
        
            var item = new ItemVenda
            {
                ProdutoId = produto.Id,
                Quantidade = quantidade,
                PrecoUnitario = precoUnitario,
                

            };


            //Adicionar na venda
            _vendaAtual.Itens.Add(item);

            //Atualizar total
            AtualizarTotal();

            //Reset básico
            txtQuantidade.Text = "1";
        }

        private void BtnRemoverItem_Click(object sender, RoutedEventArgs e)
        {
           if (dgItensVenda.SelectedItem is not ItemVenda item) 
           {
                MessageBox.Show("Selecione um item para remover.");
                return;
           }

           _vendaAtual.Itens.Remove(item);
           AtualizarTotal();
        }

        private void TxtDesconto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!decimal.TryParse(txtDesconto.Text,
                NumberStyles.Any,
                CultureInfo.CurrentCulture,
                out decimal desconto))
                desconto = 0;

            if(desconto < 0) desconto = 0;
            if(desconto > 100) desconto = 100;

            _vendaAtual.DescontoPercentual = desconto;
            AtualizarTotal();
            
        }

        private void AtualizarTotal()
        {
            if (txtTotalBruto == null)
                return;
            
            txtTotalBruto.Text = $"Total: {_vendaAtual.Total:C}";
            btnFinalizarVenda.IsEnabled = _vendaAtual.Itens.Any();
        }

        private void BtnFinalizarVenda_Click(object sender, RoutedEventArgs e)
        {
            if(!btnFinalizarVenda.IsEnabled)
                return;

            if (!_vendaAtual.Itens.Any())
           {
                MessageBox.Show("Adicione ao menos um item à venda.");
                return;
           }

           btnFinalizarVenda.IsEnabled = false;

           try 
           { 
                _vendaAtual.DataVenda = DateTime.Now;

                using var context = new Data.AppDbContext();
                context.Vendas.Add(_vendaAtual);
                context.SaveChanges();
           

                MessageBox.Show("Venda finalizada com sucesso!");
                //Resetar para a próxima venda
                ResetarVenda();
           }
           catch (Exception ex)
           {
                btnFinalizarVenda.IsEnabled = true; 
                MessageBox.Show($"Erro ao finalizar a venda: {ex.Message}");

           }
        }
    }    
}
