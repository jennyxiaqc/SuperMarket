﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xy.SuperMarket.Domain.Abstract;
using Xy.SuperMarket.Domain.Entities;

namespace Xy.SuperMarket.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} subtotal:{2:c}", line.Product.Name, line.Quantity, subtotal);
                }
                body.AppendFormat("Total value:{0:c}", cart.ComputeTotalValue())
                     .AppendLine("---")
                     .AppendLine("ship to");
                PropertyInfo[] propertyInfo;
                propertyInfo = shippingDetails.GetType().GetProperties();
                for (int i = 0; i < propertyInfo.Length; i++)
                {
                    if (propertyInfo[i].Name != "GiftWrap")
                    {
                        if (propertyInfo[i].GetValue(shippingDetails) != null)
                        {
                            body.AppendLine(propertyInfo[i].GetValue(shippingDetails).ToString());
                        }
                    }
                }
                body.AppendLine("---")
                    .AppendFormat("Gift Wrap:{0}", shippingDetails.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                     emailSettings.MailFromAddress, // From
                     emailSettings.MailToAddress, // To
                        "New order submitted!", // Subject
                     body.ToString()); // Body
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }

    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "supermarket@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\Super_Market_emails";
    }
}
