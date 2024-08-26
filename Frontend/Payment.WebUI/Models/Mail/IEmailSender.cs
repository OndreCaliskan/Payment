﻿namespace Payment.WebUI.Models.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
