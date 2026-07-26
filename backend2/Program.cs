using backend2.Data;
using backend2.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();




builder.Services.AddSingleton<MongoDBContext>();


builder.Services.AddScoped<IPartyService, PartyService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});




var app = builder.Build();




app.UseCors("AllowAll");


app.UseAuthorization();


app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();