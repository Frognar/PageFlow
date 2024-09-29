using System.Text.Json.Serialization;
using AutomaticEndpoints;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddMediator();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

WebApplication app = builder.Build();

app.RegisterEndpoints();

app.Run();

[JsonSerializable(typeof(string))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;