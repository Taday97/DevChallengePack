using Microsoft.OpenApi.Models;
using RestApi.Middleware;
using RestAPI.Services.IService;
using RestAPI.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rest API", Version = "v1" });

   
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter your API key in this field",
        Name = "X-API-KEY", 
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] { }
        }
    });
});


builder.Services.AddScoped<IPerformanceLoggerService, PerformanceLoggerService>();
builder.Services.AddScoped<IWorkstationBookingService, WorkstationBookingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseStaticFiles(); // Solo si es necesario
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();
