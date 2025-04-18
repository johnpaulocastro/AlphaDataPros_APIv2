using ADPv2.Client;
using ADPv2.Logger.Class;
using ADPv2.Logger.Filters;
using ADPv2.Logger.Interface;
using ADPv2.Logger.Service;
using ADPv2.Middlewares;
using ADPv2.Models.Interfaces;
using ADPv2.Models.Middleware;
using ADPv2.Models.Repositories;
using ADPv2.Models.Services;
using ADPv2.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", true, true);

// Override Logger to Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//Serilog.Debugging.SelfLog.Enable(msg =>
//{
//    var file = File.CreateText("C:/LogDebug.json");
//    Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));
//});

// Add services to the container.
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddXmlSerializerFormatters();

builder.Services.AddHttpContextAccessor();

//required to add this line for IOptions Dependency Injection
builder.Services.AddOptions();
builder.Services.AddOptions<RequestResponseLoggerSetting>().Bind(
    builder.Configuration.GetSection("RequestResponseLogger"))
    .ValidateDataAnnotations();

//Add here the configuration
builder.Services.Configure<ConnectionSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Stripe"));
builder.Services.Configure<PaymentLinkSettings>(builder.Configuration.GetSection("PaymentLink"));
builder.Services.Configure<ElavonConvergeSettings>(builder.Configuration.GetSection("ElavonConverge"));
builder.Services.Configure<SendInBlueSettings>(builder.Configuration.GetSection("SendInBlue"));

// Configuring the Authentication Service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        byte[] signingKeyBytes = Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SigningKey"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
            ValidAudience = builder.Configuration["JwtOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
        };

        options.Events = new JwtBearerEvents { 
            OnAuthenticationFailed = context =>
            {
                if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IRequestResponseLogger, RequestResponseLogger>();
builder.Services.AddScoped<IRequestResponseLogModelCreator, RequestResponseLogModelCreator>();

//Add here the repository
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAuditTrailRepository, AuditTrailRepository>();
builder.Services.AddTransient<ITransactionHistoryRepository, TransactionHistoryRepository>();
builder.Services.AddTransient<ITransactionHistoryDetailsRepository, TransactionHistoryDetailsRepository>();
builder.Services.AddTransient<ITransactionTypeRepository, TransactionTypeRepository>();
builder.Services.AddTransient<IStatusRepository, StatusRepository>();
builder.Services.AddTransient<ICodeRepository, CodeRepository>();
builder.Services.AddTransient<ICodeTransactionRepository, CodeTransactionRepository>();
builder.Services.AddTransient<IUserBuyRateRepository, UserBuyRateRepository>();
builder.Services.AddTransient<IMerchantRepository, MerchantRepository>();
builder.Services.AddTransient<IEWalletTransactionRepository, EWalletTransactionRepository>();
builder.Services.AddTransient<IEWalletRepository, EWalletRepository>();
builder.Services.AddTransient<IPersonalInfoRepository, PersonalInfoRepository>();
builder.Services.AddTransient<IRequestResponseLoggerRepository, RequestResponseLoggerRepository>();
builder.Services.AddTransient<IPaymentLinkRepository, PaymentLinkRepository>();
builder.Services.AddTransient<ICreditCardAccountNumberRepository, CreditCardAccountNumberRepository>();
builder.Services.AddTransient<IRoutingNumbersRepository, RoutingNumbersRepository>();

//Add here the service
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAuditTrailService, AuditTrailService>();
builder.Services.AddTransient<ITransactionHistoryService, TransactionHistoryService>();
builder.Services.AddTransient<ITransactionHistoryDetailsService, TransactionHistoryDetailsService>();
builder.Services.AddTransient<ITransactionTypeService, TransactionTypeService>();
builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<ICodeService, CodeService>();
builder.Services.AddTransient<ICodeTransactionService, CodeTransactionService>();
builder.Services.AddTransient<IUserBuyRateService, UserBuyRateService>();
builder.Services.AddTransient<IMerchantService, MerchantService>();
builder.Services.AddTransient<IEWalletTransactionService, EWalletTransactionService>();
builder.Services.AddTransient<IEWalletService, EWalletService>();
builder.Services.AddTransient<IPersonalInfoService, PersonalInfoService>();
builder.Services.AddTransient<IRequestResponseLoggerService, RequestResponseLoggerService>();
builder.Services.AddTransient<ISquareUpService, SquareUpService>();
builder.Services.AddTransient<IPaymentLinkService, PaymentLinkService>();
builder.Services.AddTransient<ICreditCardAccountNumberService, CreditCardAccountNumberService>();
builder.Services.AddTransient<IRoutingNumbersService, RoutingNumbersService>();

//Add here the Client Service
builder.Services.AddTransient<IElavonConvergeClient, ElavonConvergeClient>();
builder.Services.AddTransient<ISendInBlueClient, SendInBlueClient>();

//Add here the Middleware
builder.Services.AddTransient<ITokenEndpoint, TokenEndpointMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Alpha DataPros API - v1",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    options.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}ADPv2.xml");
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new RequestResponseLoggerActionFilter());
    options.Filters.Add(new RequestResponseLoggerErrorFilter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestResponseLoggerMiddleware>();
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
    .Get<IExceptionHandlerPathFeature>()
    .Error;

    var response = new { details = "An error occured" };
    await context.Response.WriteAsJsonAsync(response);
}));

app.UseCors("corsapp");
app.UseAuthentication();
app.UseAuthorization();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder =>
{
    appBuilder.UseMiddleware<ApiKeyMiddleware>();
});

app.MapControllers();

app.Run();
