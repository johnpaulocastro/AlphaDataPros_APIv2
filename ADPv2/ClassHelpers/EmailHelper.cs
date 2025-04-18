using ADPv2.Settings;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace ADPv2.ClassHelpers
{
    public class EmailHelper
    {
        SendInBlueSettings _sendInBlueSettings;

        public EmailHelper(IOptions<SendInBlueSettings> options)
        {
            _sendInBlueSettings = options.Value;
        }

        //public void SendEmail(string recipient, string subject, string body)
        //{
        //    // Configure mail configuration
        //    MailMessage message = new MailMessage();
        //    message.IsBodyHtml = true;
        //    message.From = new MailAddress(_sendInBlueSettings.SenderEmail, _sendInBlueSettings.SenderName);
        //    message.To.Add(new MailAddress(recipient));
        //    message.Subject = subject;
        //    message.Body = body;

        //    // Always BCC TechnicalSupport and Info
        //    message.Bcc.Add(new MailAddress("info@alphadatapros.com"));
        //    message.Bcc.Add(new MailAddress("technicalsupport@alphadatapros.com"));

        //    using (var client = new SmtpClient(_sendInBlueSettings.Host, _sendInBlueSettings.Port))
        //    {
        //        client.Credentials = new NetworkCredential(_sendInBlueSettings.SenderEmail, _sendInBlueSettings.Password);
        //        client.EnableSsl = true;
        //        client.Send(message);
        //    }
        //}

        public static string AlphaCodeBodyContent(string company, string amount)
        {
            var content = $@"<html>
            <head>
            </head>
            <body style='margin:0; padding:0'>
                <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                    <tr>
                        <td style='padding: 10px 0 30px 0'>
                            <table align='center' border='0' cellpadding='0' cellspacing='0' width='600' style='border: 1px solid #cccccc; border-collapse: collapse;'>
                                <tr>
                                    <td align='center' bgcolor='#d3d3d3' style='width: 200px;padding: 20px 0 20px 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif;'>
                                        <img src='https://account-alphadatapros.com/Content/img_gst/AlphaDataProsLogo.jpg' alt='Alpha DataPros' style='display: block;width: 125px; height: auto;
                                            font-size: .92857143rem;border: 1ex solid lightgray;border-radius: 2rem;' />
                                    </td>
                                    <td align='left' bgcolor='#d3d3d3' style='padding: 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif;'>
                                        <h4>AlphaCode Transaction</h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor='#ffffff' style='padding: 40px 30px 10px 30px;' colspan='2'>
                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                            <tr>
                                                <td style='color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
                                                    <p>
                                                        Thank you for choosing AlphaCode EWallet as your payment.
                                                    </p>
                                                    <p>
                                                        Your payment for {company} has been successful. An amount of {amount} has been debited from your wallet.
                                                    </p>
                                                    <p>
                                                        Regards,
                                                    </p>
                                                    <p>
                                                        Alpha DataPros Team
                                                    </p>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='2' align='left' bgcolor='#d3d3d3' style='padding: 10px; color: #153643; font-size: 12px; line-height: 20px; font-weight: bold; font-family: Arial, sans-serif;'>
                                        <p>
                                            This email is a notification about your Alpha DataPros account. If you have any questions or comments pertaining to this email, 
                                            please feel free to email us at application@alphadatapros.com.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

            return content;
        }
    }
}
