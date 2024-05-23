using StockManager.API.Database;
using StockManager.API.MicroServices.AccountService;
using StockManager.API.MicroServices.PortfolioService;
using StockManager.API.MicroServices.StockService;
using StockManager.API.MicroServices.SearchSymbolDataService;
using StockManager.API.MicroServices.AnalysisService;
using StockManager.TwelveDataDotNet.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<ISearchSymbolDataService, SearchSymbolDataService>();
builder.Services.AddScoped<IAnalysisService, AnalysisService>();
builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<DatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

