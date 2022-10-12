using System.IO;
using System.Security.Cryptography;
using System.Text;
using dotenv.net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using noswebapp_api;
using noswebapp.Controllers;
using Plugin.Database;
using WingsAPI.Plugins;
using WingsEmu.Communication.gRPC.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Web.Mvc;
using System.Collections.Generic;

using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Http;
using noswebapp_api.Helpers;
using noswebapp_api.InputFormatters;
using noswebapp_api.InternalEntities;
using noswebapp_api.Services;
using noswebapp_api.Services.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Load environment variables
string envfile = "noswebapp.env";
if (!File.Exists(envfile))
{
    envfile = "../../" + envfile;
}
DotEnv.Load(new DotEnvOptions(true, new[] { envfile }, Encoding.UTF8));
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
builder.Services.AddTransient<IDependencyInjectorPlugin, DatabasePlugin>();
builder.Services.AddTransient(typeof(AccountController));
builder.Services.AddTransient(typeof(MailController));
builder.Services.AddTransient(typeof(NoteController));
builder.Services.AddTransient(typeof(BazaarController));
builder.Services.AddTransient(typeof(AccountWarehouseController));
builder.Services.AddTransient(typeof(CharacterController));
builder.Services.AddTransient(typeof(LoginRequestsController));
new DatabasePlugin().AddDependencies(builder.Services);
builder.Services.AddMvc(options =>
{
    options.InputFormatters.Insert(0, new RawBodyInputFormatter());
});

builder.WebHost.UseUrls("http://0.0.0.0:21487/");
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// TODO: Double check that keeping a single instance of this service is actually fine.
builder.Services.AddSingleton<IWebAuthRequestService, WebAuthRequestService>();



var app = builder.Build();

app.UseAuthentication();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseSwagger();
//app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();
Observable.Interval(TimeSpan.FromMinutes(5)).Subscribe(s => StaticDataManagement.RemoveTokensLoop());
Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(s => StaticDataManagement.RemoveAttemptsLoop());
app.Run();