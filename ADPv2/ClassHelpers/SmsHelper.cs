using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Net;

namespace ADPv2.ClassHelpers
{
    public class SmsHelper
    {
        public static string SmsTransactionAlphaCodeContent(string company, string amount)
        {
            var content = $"Thank you for choosing AlphaCode EWallet as your payment. Your payment for {company} has been successful. An amount of {amount} has been debited from your wallet.";

            return content;
        }
    }
}
