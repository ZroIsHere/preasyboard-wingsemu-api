using System.IO;
using System.Text;
using dotenv.net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Database;
using WingsAPI.Plugins;
using WingsEmu.Communication.gRPC.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PreasyBoard.Api.Configuration;
using PreasyBoard.Api.Managers;
using PreasyBoard.Api.InputFormatters;
using PreasyBoard.Api.Services;
using PreasyBoard.Api.Services.Interfaces;
using PhoenixLib.Caching;
using Plugin.ResourceLoader;

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
builder.Services.TryAddSingleton(typeof(ILongKeyCachedRepository<>), typeof(InMemoryCacheRepository<>));
builder.Services.AddTransient<IDependencyInjectorPlugin, DatabasePlugin>();
new FileResourceLoaderPlugin().AddDependencies(builder.Services);
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
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseMiddleware<JwtManager>();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();
//Observable.Interval(TimeSpan.FromMinutes(5)).Subscribe(s => StaticDataManagement.RemoveTokensLoop());
//Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(s => StaticDataManagement.RemoveAttemptsLoop());
app.Run();