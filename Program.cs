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
new DatabasePlugin().AddDependencies(builder.Services);
builder.Services.AddAuthentication("jwt").AddJwtBearer("jwt", options =>
{
    options.Authority = "https://0.0.0.0:5678";
    options.Audience = "api1";
    options.TokenValidationParameters.TokenDecryptionKey = new RsaSecurityKey(RSA.Create(NosWebAppEnvVariables.EncryptionKey));
});

builder.WebHost.UseUrls("http://0.0.0.0:21487/");

var app = builder.Build();
app.UseAuthentication();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();