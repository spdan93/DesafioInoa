using System;
using DesafioInoa.src.dto;

namespace DesafioInoa.src.interfaces
{
	public interface IStockApiReader
	{
		void GetStockQuote(AtivoDTO ativoDTO);
	}
}

