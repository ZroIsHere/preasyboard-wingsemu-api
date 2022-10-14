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
using noswebapp_api.Configuration;
using noswebapp_api.Controllers;
using noswebapp_api.Managers;
using noswebapp_api.InputFormatters;

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
builder.Services.AddControllers();
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
new DatabasePlugin().AddDependencies(builder.Services);
builder.Services.AddMvc(options =>
{
    options.InputFormatters.Insert(0, new RawBodyInputFormatter());
});

builder.WebHost.UseUrls(NosWebAppEnvVariables.WebApiUrl);

// TODO: Double check that keeping a single instance of this service is actually fine.
builder.Services.AddSingleton<IWebAuthService, WebAuthService>();



var app = builder.Build();

app.UseAuthentication();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseSwagger();
//app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<JwtManager>();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();
//Observable.Interval(TimeSpan.FromMinutes(5)).Subscribe(s => StaticDataManagement.RemoveTokensLoop());
//Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(s => StaticDataManagement.RemoveAttemptsLoop());
app.Run();