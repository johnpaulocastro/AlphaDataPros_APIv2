using ADPv2.ClassHelpers;
using ADPv2.Client;
using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.Middleware;
using ADPv2.Models.Services;
using ADPv2.Models.ViewModels;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Square.Models;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Transactions;

namespace ADPv2.Controllers.ApiControllers
{
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/transaction")]
    [ApiController]
    public class ApiTransactionHistoryController : BaseController
    {
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly ITransactionHistoryDetailsService _transactionHistoryDetailsService;
        private readonly ICodeService _codeService;
        private readonly IStatusService _statusService;
        private readonly ICodeTransactionService _codeTransactionService;
        private readonly IMerchantService _merchantService;
        private readonly IEWalletTransactionService _eWalletTransactionService;
        private readonly IEWalletRepository _eWalletRepository;
        private readonly IPersonalInfoService _personalInfoService;
        private readonly ISendInBlueClient _sendInBlueClient;

        public ApiTransactionHistoryController(
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService,
            ITokenEndpoint tokenEndpoint,

            ITransactionHistoryService transactionHistoryService,
            ITransactionHistoryDetailsService transactionHistoryDetailsService,
            ICodeService codeService,
            IStatusService statusService,
            ICodeTransactionService codeTransactionService,
            IMerchantService merchantService,
            IEWalletTransactionService _eWalletTransactionService,
            IEWalletRepository eWalletRepository,
            IPersonalInfoService personalInfoService,
            ISendInBlueClient sendInBlueClient,

            ILogger<AccountController> logger
            ) : base(httpContextAccessor, tokenEndpoint)
        {
            this._accountService = accountService;
            this._tokenEndpoint = tokenEndpoint;
            this._logger = logger;

            this._transactionHistoryService = transactionHistoryService;
            this._transactionHistoryDetailsService = transactionHistoryDetailsService;
            this._codeService = codeService;
            this._statusService = statusService;
            this._codeTransactionService = codeTransactionService;
            this._merchantService = merchantService;
            this._eWalletTransactionService = _eWalletTransactionService;
            this._eWalletRepository = eWalletRepository;
            this._personalInfoService = personalInfoService;
            this._sendInBlueClient = sendInBlueClient;
        }

        /// <summary>
        /// Create a new transaction
        /// </summary>
        /// <returns>returns transaction number and status</returns>
        [HttpPost("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create(TransactionHistoryRequestDto request)
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

                    _logger.LogError($"{Username} : Creating transaction failed. data : {JsonConvert.SerializeObject(errors)}");
                    return BadRequest(new
                    {
                        TransactionId = "",
                        Status = "400",
                        Message = errors
                    });
                }

                var account = _accountService.GetAccount(Username);
                if (account == null)
                {
                    _logger.LogError($"{Username} : User attempted to create transaction with invalid token.");
                    return BadRequest("Invalid Token");
                }

                _logger.LogInformation($"{Username} : Creating transaction. Data : {JsonConvert.SerializeObject(request)}");
                var result = await _transactionHistoryService.CreateTransaction(request, account.Result.PersonalInfoId);
                if (result == null)
                {
                    _logger.LogError($"{Username} : Creating transaction failed. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Something went wrong, transaction failed.");
                }

                var initialTransactionDetails = new TransactionHistoryCreateRequestDto
                {
                    TransactionId = result.TransactionId,
                    ApprovalStatusId = result.ApprovalStatusId,
                    TransactionStatusId = result.TransactionStatusId,
                    TransactionNotes = request.Notes,
                    UserLogged = account.Result.PersonalInfoId
                };

                _logger.LogInformation($"{Username} : Creating inital transaction details. Data : {JsonConvert.SerializeObject(initialTransactionDetails)}");
                var isSuccess = await _transactionHistoryDetailsService.CreateInitialTransactionHistoryDetails(initialTransactionDetails, true);
                if (isSuccess)
                {
                    var approvalStatusDescription = await _statusService.GetDescription(result.ApprovalStatusId);
                    var transactionStatusDescription = await _statusService.GetDescription(result.TransactionStatusId);

                    _logger.LogInformation($"{Username} : user successfully created transaction.");
                    return Ok(new
                    {
                        Message = "Transaction Successfully Created",
                        TransactionID = result.TransactionId,
                        ApprovalStatus = approvalStatusDescription,
                        TransactionStatus = transactionStatusDescription,
                    });
                }

                _logger.LogInformation($"{Username} : Creating transaction failed.");
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
                _logger.LogInformation($"{Username} : Creating transaction failed. Data : {er.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Something went wrong, transaction failed.",
                    TransactionID = "",
                    ApprovalStatus = "",
                    TransactionStatus = ""
                });
            }
        }

        [HttpPost("alphacode/create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateByAlphaCode(TransactionHistoryAlphaCodeRequestDto request)
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

                    _logger.LogError($"{Username} : Creating AlphaCode transaction failed. data : {JsonConvert.SerializeObject(errors)}");
                    return BadRequest(new
                    {
                        TransactionId = "",
                        Status = "400",
                        Message = errors
                    });
                }

                var alphaCode = await _codeService.GetAlphaCode(request.AlphaCode);
                if (alphaCode == null)
                {
                    _logger.LogError($"{Username} : Alpha Code invalid. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Alpha Code invalid.");
                }

                if (alphaCode.ExpirationDate < DateTime.Now)
                {
                    _logger.LogError($"{Username} : Alpha Code expired. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Alpha Code expired.");
                }

                if (request.Amount <= 0)
                {
                    _logger.LogError($"{Username} : Amount should be greater than zero. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Amount should be greater than zero.");
                }

                var customerInfo = await _personalInfoService.GetPersonalInfo(alphaCode.CreatedBy);

                var customerEWallet = await _eWalletRepository.GetEwallet(customerInfo.PersonalInfoId);
                if (customerEWallet == null)
                {
                    _logger.LogError($"{Username} : EWallet is not activated. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Customer's e-wallet is not activated.");
                }

                if (customerEWallet.Balance < request.Amount)
                {
                    _logger.LogError($"{Username} : Insufficient balance. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Insufficient balance.");
                }

                var userLogged = await _accountService.GetAccount(Username);
                var merchantCompanyInfo = await _merchantService.GetMerchantCompanyInfo(userLogged.PersonalInfoId);
                
                var company = string.Format("{0} ({1})", merchantCompanyInfo.CompanyName, merchantCompanyInfo.CompanyWebsiteURL);
                var transactionId = await _codeTransactionService.CreateCodeTransaction(request, customerInfo.PersonalInfoId, company, userLogged.PersonalInfoId);
                if (string.IsNullOrEmpty(transactionId))
                {
                    _logger.LogError($"{Username} : Creating AlphaCode transaction failed. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Something went wrong, transaction failed.");
                }

                var ewalletTransactionRequestDto = new EWalletTransactionRequestDto
                {
                    TransactionId = transactionId,
                    CustomerId = customerInfo.PersonalInfoId,
                    MerchantId = userLogged.PersonalInfoId,
                    TransactionDescription = company
                };

                var ewalletTransactionResult = await _eWalletTransactionService.CreateEWalletTransaction(ewalletTransactionRequestDto);
                if(ewalletTransactionResult <= 0)
                {
                    _logger.LogError($"{Username} : Creating AlphaCode transaction failed. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Something went wrong, transaction failed.");
                }

                customerEWallet.Balance = customerEWallet.Balance - request.Amount;
                customerEWallet.UpdatedBy = userLogged.PersonalInfoId;
                customerEWallet.DateUpdated = DateTime.Now;
                var operation = await _eWalletRepository.UpdateEWallet(customerEWallet);

                if (!operation)
                {
                    _logger.LogError($"{Username} : Updating EWallet failed. data : {JsonConvert.SerializeObject(request)}");
                    return BadRequest("Something went wrong, transaction failed.");
                }

                alphaCode.ExpirationDate = alphaCode.DateCreated.AddMinutes(-5);
                alphaCode.IsUsed = true;
                await _codeService.UpdateAlphaCode(alphaCode);

                var emailContent = EmailHelper.AlphaCodeBodyContent(company, request.Amount.ToString("C2"));
                _sendInBlueClient.SendEmail(customerInfo.EmailAddress, DataConstant.Subject_AlphaCodeTransaction, emailContent);

                var smsContent = SmsHelper.SmsTransactionAlphaCodeContent(company, request.Amount.ToString("C2"));
                _sendInBlueClient.SendSms(customerInfo.MobileNumber, smsContent);

                _logger.LogInformation($"{Username} : AlphaCode Transaction Completed. data : {JsonConvert.SerializeObject(request)}");
                return Ok(new
                {
                    TransactionId = transactionId,
                    Status = "success",
                    Message = "Successfully created alpha code transaction."
                });
            }
            catch (Exception er)
            {
                _logger.LogError($"{Username} : {er.Message}. data : {JsonConvert.SerializeObject(request)}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { 
                    TransactionId = "",
                    Status = "Internal Server Error",
                    Message = "Something went wrong during AlphaCode Transaction."
                });
            }
        }

        /// <summary>
        /// Get the status of the transaction number
        /// </summary>
        /// <returns>return the status of the transaction number</returns>
        [HttpGet("status/{transactionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTransaction(int transactionId)
        {
            var result = await _transactionHistoryService.GetTransaction(transactionId);
            if (result == null)
            {
                _logger.LogError($"{Username} : Transaction not found. data : {transactionId}");
                return BadRequest("Transaction not found.");
            }

            var transactionStatus = await _statusService.GetDescription(result.TransactionStatusID);
            var approvalStatus = await _statusService.GetDescription(result.TransactionApprovalStatusID);
            var data = new
            {
                TransactionID = transactionId,
                TransactionStatus = transactionStatus,
                ApprovalStatus = approvalStatus
            };

            return Ok(data);
        }

        [HttpPost("woo/status")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetWooTransactionStatus(WooTransactionRequestDto request)
        {
            var result = await _transactionHistoryService.GetTransaction(request.OrderNumber);
            if (result == null)
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogError($"{Username} : Transaction not found. data : {jsonRequest}");
                return BadRequest("Transaction not found.");
            }

            var transactionStatus = await _statusService.GetDescription(result.TransactionStatusID);
            var approvalStatus = await _statusService.GetDescription(result.TransactionApprovalStatusID);
            var data = new
            {
                TransactionID = result.TransactionNo,
                TransactionStatus = transactionStatus,
                ApprovalStatus = approvalStatus
            };

            return Ok(data);
        }

        /// <summary>
        /// Get the list of transaction's remarks
        /// </summary>
        /// <returns>return the list of transaction's remarks</returns>
        [HttpGet("remarks/{transactionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRemarks(int transactionId)
        {
            var result = await _transactionHistoryDetailsService.GetTransactionHistoryDetails(transactionId);

            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return Ok("Transaction is currently queued for verification.");
            }
        }
    }
}
