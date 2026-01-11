using System;
using System.Globalization;
using EmpreendedoresApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmpreendedoresApp.Services
{
    public class RelatorioFinanceiroPdfService
    {
        public void Gerar(RelatorioFinanceiroDTO dados, string caminhoArquivo)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            if(dados == null)
                throw new ArgumentNullException(nameof(dados));
            if(dados.DataFim < dados.DataInicio)
                throw new ArgumentException("A data de final não pode ser menor que a inicial.");

            Document.Create(container => {
                container.Page(page => {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Relatório Financeiro")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().PaddingVertical(20).Column(column => {
                        column.Spacing(10);

                        column.Item().Text($"Resumo Financeiro").FontSize(16).Bold();

                        column.Item().Text($"Período: {dados.DataInicio:dd/MM/yyyy} até {dados.DataFim:dd/MM/yyyy}");

                        column.Item().LineHorizontal(1);

                        column.Item().Text($"Total de Vendas: {dados.TotalVendas}");
                        column.Item().Text($"Faturamento Bruto: {dados.FaturamentoBruto.ToString("C", CultureInfo.CurrentCulture)}");
                        column.Item().Text($"Total de Descontos: {dados.TotalDescontos.ToString("C", CultureInfo.CurrentCulture)}");

                        column.Item().LineHorizontal(1);

                        column.Item().Text($"Faturamento Líquido: {dados.FaturamentoLiquido.ToString("C", CultureInfo.CurrentCulture)}").FontSize(14).Bold();

                        
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(text => {
                            //text.Span("Gerado em ");
                            text.Span($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}");
                        });
                });
            })
                .GeneratePdf(caminhoArquivo);
        }
    }
}
