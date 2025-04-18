using ADPv2.Settings;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using ADPv2.ClassHelpers;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;

namespace ADPv2.Client
{
    public interface ISendInBlueClient
    {
        void SendEmail(string recipient, string subject, string body);
        void SendSms(string mobileNumber, string smsBody);
    }

    public class SendInBlueClient : ISendInBlueClient
    {
        private readonly SendInBlueSettings _sendInBlueSettings;
        private SendInBlueCredentials _sendInBlueCredentials;
        private string smsKey;

        public SendInBlueClient(IOptions<SendInBlueSettings> options)
        {
            _sendInBlueSettings = options.Value;
            smsKey = Base64Helper.DecryptFromBase64(_sendInBlueSettings.SmsSigningKey);
            var decryptCredentials = Base64Helper.DecryptFromBase64(_sendInBlueSettings.SigningKey);
            var credentials = JsonConvert.DeserializeObject<SendInBlueCredentials>(decryptCredentials);
            _sendInBlueCredentials = credentials;
        }

        public void SendEmail(string recipient, string subject, string body)
        {
            // Configure mail configuration
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(_sendInBlueSettings.SenderEmail, _sendInBlueSettings.SenderName);
            message.To.Add(new MailAddress(recipient));
            message.Subject = subject;
            message.Body = body;

            // Always BCC TechnicalSupport and Info
            message.Bcc.Add(new MailAddress("info@alphadatapros.com"));
            message.Bcc.Add(new MailAddress("technicalsupport@alphadatapros.com"));

            using (var client = new SmtpClient(_sendInBlueSettings.Host, _sendInBlueSettings.Port))
            {
                client.Credentials = new NetworkCredential(_sendInBlueCredentials.Username, _sendInBlueCredentials.Password);
                client.EnableSsl = true;
                client.Send(message);
            }
        }

        public void SendSms(string mobileNumber, string smsBody)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var sendTransactionalSms = new SendTransacSms(
                    "AlphaCode",
                    mobileNumber,
                    smsBody,
                    SendTransacSms.TypeEnum.Transactional,
                    null
                );

            var apiInstance = new TransactionalSMSApi();
            apiInstance.Configuration.AddDefaultHeader("accept", "application/json");
            apiInstance.Configuration.AddApiKey("api-key", smsKey);
            apiInstance.SendTransacSms(sendTransactionalSms);
        }
    }
}
