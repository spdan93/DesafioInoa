using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using DesafioInoa.src.interfaces;

namespace DesafioInoa.src.classes;

public class SmtpService : ISmtpService
{
	public SmtpService()
	{
	}

    public void sendEmail(string symbol, string order, decimal actualValue)
    {
        try
        {
            string emailSender = ConfigurationManager.AppSettings["EMAIL_SENDER"] ?? "";
            string password = ConfigurationManager.AppSettings["EMAIL_PASSWORD"] ?? "";
            string emailRecipient = ConfigurationManager.AppSettings["EMAIL_RECIPIENT"] ?? "";
            string client = ConfigurationManager.AppSettings["SMTP_CLIENT"] ?? "";

            Console.WriteLine("Encaminhando email.");
            MailMessage mailMessage = new(emailSender, emailRecipient)
            {
                Subject = "[" + (order == "sell" ? "VENDA" : "COMPRA") + "] - Análise do ativo " + symbol,

                IsBodyHtml = true,

                Body = "O preço atual do ativo " + symbol + " é: " + actualValue +
                ". \tOrdem indicada: " + (order == "sell" ? "VENDA" : "COMPRA") + ".",

                SubjectEncoding = Encoding.GetEncoding("UTF-8"),
                BodyEncoding = Encoding.GetEncoding("UTF-8")
            };

            SmtpClient smtpClient = new(client, 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailSender, password),
                EnableSsl = true
            };

            smtpClient.Send(mailMessage);
        }
        catch (Exception)
        {
            Console.WriteLine("Erro ao enviar email.");
        }
    }
}

