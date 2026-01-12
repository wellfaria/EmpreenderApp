namespace EmpreendedoresApp.Models
{
    public class ItemVenda
    {

        // Produto/Serviço vendido em uma venda - Não tem distincao entre produto ou serviço
        public int Id { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public string NomeProduto => Produto?.Nome;

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public int VendaId { get; set; }
        public Venda Venda { get; set; }

        // Retorna o subtotal do item (quantidade * preço unitário)
        public decimal Subtotal => Quantidade * PrecoUnitario; 
        
    }
}
