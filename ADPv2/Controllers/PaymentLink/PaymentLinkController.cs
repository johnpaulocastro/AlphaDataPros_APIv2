using ADPv2.Models.Interfaces;
using ADPv2.Models.Services;
using ADPv2.Models.ViewModels;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Square.Models;

namespace ADPv2.Controllers.PaymentLink
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("payment-link/")]
    [ApiController]
    public class PaymentLinkController : Controller
    {
        private readonly IPaymentLinkService _paymentLinkService;
        private readonly ICreditCardAccountNumberService _creditCardAccountNumberService;
        private readonly IRoutingNumbersService _routingNumbersService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly ITransactionHistoryDetailsService _transactionHistoryDetailsService;
        private readonly IStatusService _statusService;
        private readonly IMerchantService _merchantService;
        private readonly IMapper _mapper;

        private readonly ILogger<PaymentLinkController> _logger;
        public PaymentLinkController(IPaymentLinkService paymentLinkService,
            ICreditCardAccountNumberService creditCardAccountNumberService,
            IRoutingNumbersService routingNumbersService,
            ITransactionHistoryService transactionHistoryService,
            ITransactionHistoryDetailsService transactionHistoryDetailsService,
            IStatusService statusService,
            ILogger<PaymentLinkController> logger,
            IMerchantService merchantService,
            IMapper mapper)
        {
            _paymentLinkService = paymentLinkService;
            _creditCardAccountNumberService = creditCardAccountNumberService;
            _routingNumbersService = routingNumbersService;
            _transactionHistoryService = transactionHistoryService;
            _transactionHistoryDetailsService = transactionHistoryDetailsService;
            _statusService = statusService;
            _logger = logger;
            _merchantService = merchantService;
            _mapper = mapper;
        }

        [HttpGet("mid/{merchantId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPaymentLinkList(string merchantId)
        {
            var response = await _paymentLinkService.GetPaymentLinkList(merchantId);
            if (response?.Count > 0) return Ok(response);

            return NoContent();
        }

        [HttpGet("plid/{paymentLinkId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPaymentLink(string paymentLinkId)
        {
            var response = await _paymentLinkService.GetPaymentLink(paymentLinkId);
            if (response != null)
            {
                var merchantCompanyInfo = await _merchantService.GetMerchantCompanyInfo(response.MerchantId);
                if (merchantCompanyInfo != null)
                {
                    _ = decimal.TryParse(response.Amount, out decimal formattedAmount);

                    return Ok(new {
                        PaymentLinkId = response.PaymentLinkId,
                        CreatedDate = response.CreatedDate,
                        MerchantId = response.MerchantId,
                        CompanyName = merchantCompanyInfo.CompanyName,
                        Amount = formattedAmount.ToString("C2")
                    });
                }
            }

            return NoContent();
        }

        [HttpPost("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePaymentLink(string merchantId)
        {
            var response = await _paymentLinkService.CreatePaymentLink(merchantId);
            if (response != null) return Ok(response);
            return BadRequest();
        }

        [HttpPost("update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdatePaymentLink(string paymentLinkId)
        {
            var paymentLink = await _paymentLinkService.GetPaymentLink(paymentLinkId);
            paymentLink.ExpiryDate = paymentLink.CreatedDate;
            paymentLink.UpdatedDate = DateTime.Now;

            var response = await _paymentLinkService.UpdatePaymentLink(paymentLink);
            return response == null ? BadRequest() : Ok(Response);
        }

        [HttpPost("account")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAccountNumber(AccountRoutingRequestDto request)
        {
            try
            {
                var paymentLink = await _paymentLinkService.GetPaymentLink(request.PaymentLinkId);
                var merchantId = paymentLink.MerchantId;

                var creditCardAccountNumber = await _creditCardAccountNumberService.GetCreditCardAccountNumberByAccountNumber(request.AccountRoutingNumber);
                if (creditCardAccountNumber != null && creditCardAccountNumber.MerchantID == merchantId)
                {
                    return Ok(new
                    {
                        BankName = creditCardAccountNumber.BankName,
                        RoutingNumber = creditCardAccountNumber.RoutingNumber,
                        CheckNumber = GenerateRandomNo()
                    });
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("routing")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRoutingNumber(AccountRoutingRequestDto request)
        {
            try
            {
                var paymentLink = await _paymentLinkService.GetPaymentLink(request.PaymentLinkId);
                var merchantId = paymentLink.MerchantId;

                var routingNumber = await _routingNumbersService.GetRoutingNumberByRoutingNumber(request.AccountRoutingNumber);
                if (routingNumber != null)
                {
                    return Ok(new
                    {
                        BankName = routingNumber.BankName
                    });
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost("transaction/create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateTransaction(PaymentLinkTransactionHistoryRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }

                    _logger.LogError($"{request.PaymentLinkId} : Creating transaction failed. data : {JsonConvert.SerializeObject(errors)}");
                    return BadRequest(errors);
                }

                var paymentLinkInfo = await _paymentLinkService.GetPaymentLink(request.PaymentLinkId);
                if (paymentLinkInfo == null) return BadRequest("Payment Link is invalid.");

                paymentLinkInfo.ExpiryDate = paymentLinkInfo.CreatedDate;
                paymentLinkInfo.UpdatedDate = DateTime.UtcNow;
                await _paymentLinkService.UpdatePaymentLink(paymentLinkInfo);

                _logger.LogInformation($"{paymentLinkInfo.MerchantId} : Creating transaction. Data : {JsonConvert.SerializeObject(request)}");
                request.Transactions.Amount = paymentLinkInfo.Amount;
                var result = await _transactionHistoryService.CreateTransaction(request.Transactions, paymentLinkInfo.MerchantId);
                if (result == null)
                {
                    _logger.LogError($"{paymentLinkInfo.MerchantId} : Creating transaction failed. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Something went wrong, transaction failed.");
                }

                var initialTransactionDetails = new TransactionHistoryCreateRequestDto
                {
                    TransactionId = result.TransactionId,
                    ApprovalStatusId = result.ApprovalStatusId,
                    TransactionStatusId = result.TransactionStatusId,
                    TransactionNotes = "Payment Link Transaction",
                    UserLogged = paymentLinkInfo.MerchantId
                };

                _logger.LogInformation($"{paymentLinkInfo.MerchantId} : Creating inital transaction details. Data : {JsonConvert.SerializeObject(initialTransactionDetails)}");
                var isSuccess = await _transactionHistoryDetailsService.CreateInitialTransactionHistoryDetails(initialTransactionDetails, true);
                if (isSuccess)
                {
                    var approvalStatusDescription = await _statusService.GetDescription(result.ApprovalStatusId);
                    var transactionStatusDescription = await _statusService.GetDescription(result.TransactionStatusId);

                    _logger.LogInformation($"{paymentLinkInfo.MerchantId} : user successfully created transaction.");
                    return Ok(new
                    {
                        Message = "Transaction Successfully Created",
                        TransactionID = result.TransactionId,
                        ApprovalStatus = approvalStatusDescription,
                        TransactionStatus = transactionStatusDescription,
                    });
                }

                _logger.LogInformation($"{paymentLinkInfo.MerchantId} : Creating transaction failed.");
                return BadRequest(new
                {
                    Message = "Something went wrong, transaction failed.",
                    TransactionID = "",
                    ApprovalStatus = "",
                    TransactionStatus = ""
                });
            }
            catch (Exception er)
            {
                _logger.LogInformation($"{request.PaymentLinkId} : Creating transaction failed. Data : {er.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Something went wrong, transaction failed.",
                    TransactionID = "",
                    ApprovalStatus = "",
                    TransactionStatus = ""
                });
            }
        }

        [HttpPost("create/ccard")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePaymentLink(string merchantId, string amount)
        {
            var response = await _paymentLinkService.CreateCreditCardPaymentLink(merchantId, amount);
            if (response != null) return Ok(response);
            return BadRequest();
        }

        [HttpPost("transaction/create/ccard")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCCardTransaction(PaymentLinkCCardTransactionHistoryRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }

                    _logger.LogError($"{request.PaymentLinkId} : Creating transaction failed. data : {JsonConvert.SerializeObject(errors)}");
                    return BadRequest(errors);
                }

                var paymentLinkInfo = await _paymentLinkService.GetPaymentLink(request.PaymentLinkId);
                if (paymentLinkInfo == null) return BadRequest("Payment Link is invalid.");

                paymentLinkInfo.ExpiryDate = paymentLinkInfo.CreatedDate;
                paymentLinkInfo.UpdatedDate = DateTime.UtcNow;
                await _paymentLinkService.UpdatePaymentLink(paymentLinkInfo);

                var merchantPayeeList = await _creditCardAccountNumberService.GetCreditCardAccountNumberByMerchantId(paymentLinkInfo.MerchantId);
                if(!merchantPayeeList.Where(r => r.BankName == "CreditCard").Any())
                {
                    return BadRequest("Something went wrong, transaction failed.");
                }

                _logger.LogInformation($"{paymentLinkInfo.MerchantId} : Creating transaction. Data : {JsonConvert.SerializeObject(request)}");
                var merchantPayee = merchantPayeeList.Where(r => r.BankName == "CreditCard").First();

                request.Transactions.BankName = merchantPayee.BankName;
                request.Transactions.AccountNumber = merchantPayee.AccountNumber;
                request.Transactions.RoutingNumber = merchantPayee.RoutingNumber;
                request.Transactions.Amount = paymentLinkInfo.Amount;
                var transactionRequest = _mapper.Map<TransactionHistoryRequestDto>(request.Transactions);

                var result = await _transactionHistoryService.CreateTransaction(transactionRequest, paymentLinkInfo.MerchantId, paymentLinkInfo.IsCreditCard);
                if (result == null)
                {
                    _logger.LogError($"{paymentLinkInfo.MerchantId} : Creating transaction failed. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Something went wrong, transaction failed.");
                }

                var initialTransactionDetails = new TransactionHistoryCreateRequestDto
                {
                    TransactionId = result.TransactionId,
                    ApprovalStatusId = result.ApprovalStatusId,
                    TransactionStatusId = result.TransactionStatusId,
                    TransactionNotes = "Payment Link CCard Transaction",
                    UserLogged = paymentLinkInfo.MerchantId
                };

                _logger.LogInformation($"{paymentLinkInfo.MerchantId} : Creating initial transaction details. Data : {JsonConvert.SerializeObject(initialTransactionDetails)}");
                var isSuccess = await _transactionHistoryDetailsService.CreateInitialTransactionHistoryDetails(initialTransactionDetails, true);
                if (isSuccess)
                {
                    var approvalStatusDescription = await _statusService.GetDescription(result.ApprovalStatusId);
                    var transactionStatusDescription = await _statusService.GetDescription(result.TransactionStatusId);

                    _logger.LogInformation($"{paymentLinkInfo.MerchantId} : user successfully created transaction.");
                    return Ok(new
                    {
                        Message = "Transaction Successfully Created",
                        TransactionID = result.TransactionId,
                        ApprovalStatus = approvalStatusDescription,
                        TransactionStatus = transactionStatusDescription,
                    });
                }

                _logger.LogInformation($"{paymentLinkInfo.MerchantId} : Creating transaction failed.");
                return BadRequest(new
                {
                    Message = "Something went wrong, transaction failed.",
                    TransactionID = "",
                    ApprovalStatus = "",
                    TransactionStatus = ""
                });
            }
            catch (Exception er)
            {
                _logger.LogInformation($"{request.PaymentLinkId} : Creating transaction failed. Data : {er.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Something went wrong, transaction failed.",
                    TransactionID = "",
                    ApprovalStatus = "",
                    TransactionStatus = ""
                });
            }
        }

        private int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}
