using System.Configuration;
using System.Net;
using System.Text.Json;
using DesafioInoa.src.dto;
using DesafioInoa.src.interfaces;

namespace DesafioInoa.src.classes;

public class StockApiReader : IStockApiReader
{
    public void GetStockQuote(AtivoDTO ativoDTO)
    {
        string key = ConfigurationManager.AppSettings["API_KEY"];

        string QUERY_URL =
            "https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=" + ativoDTO.Name + "&apikey=" + key;

        Uri queryUri = new Uri(QUERY_URL);

        using (WebClient client = new WebClient())
        {
            ApiResponseDTO? response = JsonSerializer.Deserialize<ApiResponseDTO>(client.DownloadString(queryUri));

            if (response != null && response.Quote != null && response.Quote.Price != null)
            {
                SetInterval(() =>
                    {
                        response = JsonSerializer.Deserialize<ApiResponseDTO>(client.DownloadString(queryUri));

                        decimal price = Decimal.Parse(response.Quote.Price);

                        string analysis = AnalyzePrice(price, ativoDTO.SaleValue, ativoDTO.PurchaseValue);

                        if (analysis == "sell" || analysis == "buy")
                        {
                            ISmtpService smtpService = new SmtpService();
                            smtpService.sendEmail(ativoDTO.Name, analysis, price);
                        }
                    }, TimeSpan.FromSeconds(15));
                Thread.Sleep(TimeSpan.FromDays(1));
            } else
            {
                Console.WriteLine("A consulta não retornou valores");
            }
        }
    }

    private string AnalyzePrice(decimal? referencePrice, decimal? salePrice, decimal? purchasePrice)
    {
        if (referencePrice > salePrice)
        {
            return "sell";
        } else if (referencePrice < purchasePrice)
        {
            return "buy";
        } else
        {
            return "none";
        }
    }

    private async void SetInterval(Action action, TimeSpan timeout)
    {
        await Task.Delay(timeout).ConfigureAwait(false);

        action();

        SetInterval(action, timeout);
    }
}

