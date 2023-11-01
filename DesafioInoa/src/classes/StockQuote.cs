using DesafioInoa.src.classes;
using DesafioInoa.src.dto;
using DesafioInoa.src.interfaces;

public class StockQuote : IStockQuote
{
    public void readerService()
    {
        AtivoDTO ativoDTO = new AtivoDTO();

        bool execute = true;

        do
        {
            Console.WriteLine("Informe o ativo que quer monitorar: ");
            ativoDTO.Name = Console.ReadLine();
            Console.WriteLine("Qual o preço de referência para venda? ");
            ativoDTO.SaleValue = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Qual o preço de referência para compra? ");
            ativoDTO.PurchaseValue = Decimal.Parse(Console.ReadLine());

            Console.WriteLine("O ativo a ser monitorado é o " + ativoDTO.Name +
                " e os preços de venda e compra são " + ativoDTO.SaleValue +
                " e " + ativoDTO.PurchaseValue + ", respectivamente, correto? [S / N]");
            String validation = Console.ReadLine();

            if (validation?.ToUpper() == "S")
            {
                execute = !execute;
            }

        } while (execute);
        // YR9XIT6K57023G9W
        IStockApiReader stockApiReader = new StockApiReader();
        stockApiReader.GetStockQuote(ativoDTO);

        Console.WriteLine("Fim da execução.");
    }
}