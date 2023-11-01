using System;
using DesafioInoa.src.interfaces;

namespace DesafioInoa;

class Program
{
    static void Main(string[] args)
    {
        IStockQuote stockService = new StockQuote();
        stockService.readerService();
    }
}