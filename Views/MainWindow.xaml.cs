using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmpreendedoresApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnProdutos_Click(object sender, RoutedEventArgs e)
        {
            ProdutosView janelaProdutos = new ProdutosView();
            janelaProdutos.Show();
            
        }
        
        private void BtnVendasSaida_Click(object sender, RoutedEventArgs e)
        {
            VendasSaidasView janelaVendasSaidas = new VendasSaidasView();
            janelaVendasSaidas.Show();
        }

        private void BtnRelatorioFinanceiro_Click(object sender, RoutedEventArgs e)
        {
            RelatorioFinanceiroView janelaRelatorioFinanceiro = new RelatorioFinanceiroView();
            janelaRelatorioFinanceiro.Show();
        }
    }
}