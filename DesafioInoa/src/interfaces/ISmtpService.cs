using System;
namespace DesafioInoa.src.interfaces;

public interface ISmtpService
{
    void sendEmail(string symbol, string order, decimal actualValue);
}

