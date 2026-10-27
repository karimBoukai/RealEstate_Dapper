using RealEstate_Dapper_Api.Containers;
using RealEstate_Dapper_Api.Hubs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ContainerDependencies();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:44391", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add HttpClient with certificate validation bypass for development
builder.Services.AddHttpClient().ConfigureHttpClientDefaults(httpClientBuilder =>
{
    httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        if (!builder.Environment.IsProduction())
        {
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        }
        return handler;
    });
});

builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalRHub>("/SignalRHub");
app.Run();
