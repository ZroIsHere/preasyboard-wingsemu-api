using Microsoft.AspNetCore.Components.Web;
using WebApplication1.Controllers;
using WingsEmu.Communication.gRPC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServerApiServiceClient();
builder.Services.AddGrpcSessionServiceClient();
builder.Services.AddGrpcClusterStatusServiceClient();
builder.Services.AddClusterCharacterServiceClient();
builder.Services.AddTranslationsGrpcClient();
builder.Services.AddGrpcRelationServiceClient();
builder.Services.AddGrpcBazaarServiceClient();
builder.Services.AddGrpcFamilyServiceClient();
builder.Services.AddGrpcMailServiceClient();
builder.Services.AddGrpcDbServerServiceClient();
builder.Services.AddTransient(typeof(AccountController));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();