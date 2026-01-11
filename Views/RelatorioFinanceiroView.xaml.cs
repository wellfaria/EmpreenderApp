using System.IO;
using System.Windows;
using EmpreendedoresApp.Services;


namespace EmpreendedoresApp.Views
{
    /// <summary>
    /// Lógica interna para RelatorioFinanceiroView.xaml
    /// </summary>
    public partial class RelatorioFinanceiroView : Window
    {
        public RelatorioFinanceiroView()
        {
            InitializeComponent();

            dpInicio.SelectedDate = DateTime.Today.AddDays(-30);
            dpFim.SelectedDate = DateTime.Today;
        }

        private void BtnGerarRelatorio_Click(object sender, RoutedEventArgs e)
        {
            if (dpInicio.SelectedDate == null || dpFim.SelectedDate == null) 
            {
                MessageBox.Show("Selecione as datas de início e fim.");
                return;
            }

            var inicio = dpInicio.SelectedDate.Value;
            var fim = dpFim.SelectedDate.Value;

            if (inicio > fim) 
            {
                MessageBox.Show("A data inicial não pode ser maior que a data final.");
                return;
            }

            var relatorioService = new RelatorioFinanceiroService();
            var dados = relatorioService.Gerar(inicio, fim);

            var pasta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "RelatoriosPDF");

            Directory.CreateDirectory(pasta);

            var caminhoArquivo = Path.Combine( pasta,
                $"Relatorio_Financeiro_{DateTime.Now:yyyyMMdd_HHmm}.pdf");

            var pdfService = new RelatorioFinanceiroPdfService();
            pdfService.Gerar(dados, caminhoArquivo);

            MessageBox.Show($"Relatório gerado com sucesso!\n\nLocal:\n{caminhoArquivo}", "Relatório Financeiro", MessageBoxButton.OK, MessageBoxImage.Information); //Aqui entra o Service

            System.Diagnostics.Process.Start("explorer.exe", pasta);
        }
    }
}
