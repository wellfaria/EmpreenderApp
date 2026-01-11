using EmpreendedoresApp.Models;
using SQLitePCL;
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
using EmpreendedoresApp.Data;

namespace EmpreendedoresApp.Views
{
    /// <summary>
    /// Lógica interna para ProdutosView.xaml
    /// </summary>
    public partial class ProdutosView : Window
    {
        private readonly AppDbContext _context = new AppDbContext(); // Instância do contexto do banco de dados

        private Produto ProdutoEmEdicao = null; // Produto atualmente em edição

        public ProdutosView()
        {
           

            InitializeComponent();
            CarregarProdutos(); // Carrega os produtos ao iniciar a janela
        }

        private void CarregarProdutos() // Carrega os produtos do banco de dados e exibe na DataGrid
        {
            DataGridProdutos.ItemsSource = _context.Produtos.ToList();
        }

        private void ResetarCampos()
        {
            txtNomeProduto.Text = "";
            txtPrecoProduto.Text = "";
            btnAdicionarProduto.Content = "Adicionar Produto";
            ProdutoEmEdicao = null;
        }


        private void BtnAdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            //Campo de Nome do Produto - Validação
            if(string.IsNullOrWhiteSpace(txtNomeProduto.Text))
            {
                MessageBox.Show("O nome do produto é obrigatório. Por favor, preencha o campo.");
                return;
            }

            //Campo de Preço - Validação
            if(!decimal.TryParse(txtPrecoProduto.Text, out decimal preco))
            {
                MessageBox.Show("Por favor, insira um preço válido (Apenas números).");
                return;
            }

            if(ProdutoEmEdicao != null) 
            {

                ProdutoEmEdicao.Nome =  txtNomeProduto.Text;
                ProdutoEmEdicao.Preco = preco;

                _context.Produtos.Update(ProdutoEmEdicao); // Atualiza o produto no DbSet
                _context.SaveChanges(); // Salva as mudanças no banco de dados
                

                

                CarregarProdutos(); // Recarrega a lista de produtos para refletir a atualização
                DataGridProdutos.UnselectAll();
                ResetarCampos();
                return;
            }
            else
            {
              var novoProduto = new Produto 
              {
                  Nome = txtNomeProduto.Text,
                  Preco = preco
              };
                _context.Produtos.Add(novoProduto); // Adiciona o novo produto ao DbSet
                _context.SaveChanges(); // Salva as mudanças no banco de dados
            }
            
            CarregarProdutos(); // Recarrega a lista de produtos para refletir a adição
            ResetarCampos();
        }
        
        private void BtnExcluirProduto_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridProdutos.SelectedItem is Produto produtoSelecionado)
            {
                // Remove o produto selecionado do banco de dados
                _context.Produtos.Remove(produtoSelecionado);
                _context.SaveChanges(); // Salva as mudanças no banco de dados
                CarregarProdutos(); // Recarrega a lista de produtos para refletir a exclusão
                DataGridProdutos.UnselectAll();
                ResetarCampos();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.");
            }
        }

        private void DataGridProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        

            if (DataGridProdutos.SelectedItem is Produto produto)
            {
                // Preenche os campos de texto com os dados do produto selecionado para edição
                ProdutoEmEdicao = produto; // Define o produto em edição

                txtNomeProduto.Text = produto.Nome;
                txtPrecoProduto.Text = produto.Preco.ToString();

                btnAdicionarProduto.Content = "Salvar Alterações";
            }
            else
            {
                ResetarCampos();
            }
        }
    }
}
