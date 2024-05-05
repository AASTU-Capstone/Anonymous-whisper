using  ComplaintSystem.Infrastructure;
using  ComplaintSystem.Domain.Entities;
using System.Net;
using System.Net.Mail;
using  ComplaintSystem.Application.Responses;
using  ComplaintSystem.Application.Persistence.Contracts.Common;


namespace  ComplaintSystem.Infrastructure.Mail;

public class EmailSender : IEmailSender
{
    private const string SenderEmail = "xbebe346@gmail.com";
    private const string SenderPassword = "uqsasuybillessch";
    private const string DisplayName = "Anonymous Whispher";

    public async Task<BaseResponseClass> SendEmail(Email email)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress(SenderEmail, DisplayName);
        message.To.Add(email.To);
        message.Subject = email.Subject ?? DefaultMail.RegistrationSubject;
        message.Body = email.Body ?? DefaultMail.RegistrationBody;
        message.IsBodyHtml = true;

        var client = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(SenderEmail, SenderPassword),
            EnableSsl = true
        };

        var resp = await SendEmailAsync(client, message);
        return resp;
    }

    private static async Task<BaseResponseClass> SendEmailAsync(SmtpClient client, MailMessage message)
    {
        var response = new BaseResponseClass();
        try
        {
            await client.SendMailAsync(message);
            response.Success = true;
            response.Message = "sent succesfully";

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Error = new List<string> { ex.Message };
        }

        return response;
    }

}