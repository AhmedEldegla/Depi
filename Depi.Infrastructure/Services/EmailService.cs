using DEPI.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace DEPI.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendAsync(string toEmail, string subject, string body)
    {
        var smtpServer = _configuration["Email:SmtpServer"];
        var smtpPort = int.TryParse(_configuration["Email:SmtpPort"], out var port) ? port : 587;
        var fromEmail = _configuration["Email:FromEmail"] ?? "noreply@depi.com";
        var fromName = _configuration["Email:FromName"] ?? "DEPI Platform";
        var username = _configuration["Email:Username"];
        var password = _configuration["Email:Password"];

        if (string.IsNullOrEmpty(smtpServer))
        {
            _logger.LogInformation(
                "[DEV EMAIL] To: {To}, Subject: {Subject}, Body: {Body}",
                toEmail, subject, body);
            return;
        }

        using var mail = new MailMessage
        {
            From = new MailAddress(fromEmail, fromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mail.To.Add(toEmail);

        using var smtp = new SmtpClient(smtpServer, smtpPort)
        {
            EnableSsl = true,
            Credentials = string.IsNullOrEmpty(username)
                ? null
                : new NetworkCredential(username, password)
        };

        await smtp.SendMailAsync(mail);
        _logger.LogInformation("Email sent to {To}: {Subject}", toEmail, subject);
    }
}
