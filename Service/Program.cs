using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ModelContextProtocol.Protocol;
using NORCE.Drilling.GeodeticDatum.Service;
using NORCE.Drilling.GeodeticDatum.Service.Managers;
using NORCE.Drilling.GeodeticDatum.Service.Mcp;
using NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

var builder = WebApplication.CreateBuilder(args);

// registering the manager of SQLite connections through dependency injection
builder.Services.AddSingleton(sp =>
    new SqlConnectionManager(
        $"Data Source={SqlConnectionManager.HOME_DIRECTORY}{SqlConnectionManager.DATABASE_FILENAME}",
        sp.GetRequiredService<ILogger<SqlConnectionManager>>()));

// registering the database cleaner service through dependency injection
builder.Services.AddHostedService(sp => new DatabaseCleanerService(
    sp.GetRequiredService<ILogger<DatabaseCleanerService>>(),
    sp.GetRequiredService<SqlConnectionManager>()));

// serialization settings (using System.Json)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        JsonSettings.ApplyTo(options.JsonSerializerOptions);
    });

// serialize using short name rather than full names
builder.Services.AddSwaggerGen(config =>
{
    config.CustomSchemaIds(type => type.FullName);
});

// MCP server registrations
var serverVersion = typeof(SqlConnectionManager).Assembly.GetName().Version?.ToString() ?? "1.0.0";

builder.Services.AddMcpServer(options =>
{
    options.ServerInfo = new Implementation
    {
        Name = "GeodeticDatumService",
        Version = serverVersion
    };
    options.Capabilities = new ServerCapabilities
    {
        Tools = new ToolsCapability()
    };
}).WithHttpTransport();

builder.Services.AddLegacyMcpTool<PingMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllSpheroidIdsMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllSpheroidMetaInfoMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllSpheroidMcpTool>();
builder.Services.AddLegacyMcpTool<GetSpheroidByIdMcpTool>();
builder.Services.AddLegacyMcpTool<PostSpheroidMcpTool>();
builder.Services.AddLegacyMcpTool<PutSpheroidByIdMcpTool>();
builder.Services.AddLegacyMcpTool<DeleteSpheroidByIdMcpTool>();
builder.Services.AddLegacyMcpTool<FindSpheroidIdByNameMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllGeodeticDatumIdsMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllGeodeticDatumMetaInfoMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllGeodeticDatumLightMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllGeodeticDatumMcpTool>();
builder.Services.AddLegacyMcpTool<GetGeodeticDatumByIdMcpTool>();
builder.Services.AddLegacyMcpTool<PostGeodeticDatumMcpTool>();
builder.Services.AddLegacyMcpTool<PutGeodeticDatumByIdMcpTool>();
builder.Services.AddLegacyMcpTool<DeleteGeodeticDatumByIdMcpTool>();
builder.Services.AddLegacyMcpTool<FindGeodeticDatumIdByNameMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllGeodeticConversionSetIdsMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllGeodeticConversionSetMetaInfoMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllGeodeticConversionSetLightMcpTool>();
builder.Services.AddLegacyMcpTool<GetAllGeodeticConversionSetMcpTool>();
builder.Services.AddLegacyMcpTool<GetGeodeticConversionSetByIdMcpTool>();
builder.Services.AddLegacyMcpTool<PostGeodeticConversionSetMcpTool>();
builder.Services.AddLegacyMcpTool<PutGeodeticConversionSetByIdMcpTool>();
builder.Services.AddLegacyMcpTool<DeleteGeodeticConversionSetByIdMcpTool>();
builder.Services.AddLegacyMcpTool<ConvertGeodeticDatumCoordinateMcpTool>();

var app = builder.Build();

var basePath = "/GeodeticDatum/api";

app.UsePathBase(basePath);

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(25)
});
app.UseRouting();

string relativeSwaggerPath = "/swagger/merged/swagger.json";
string fullSwaggerPath = $"{basePath}{relativeSwaggerPath}";
string customVersion = "Merged API Version 1";

var mergedDoc = SwaggerMiddlewareExtensions.ReadOpenApiDocument("wwwroot/json-schema/GeodeticDatumMergedModel.json");
app.UseCustomSwagger(mergedDoc, relativeSwaggerPath);
app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("v1/swagger.json", "API Version 1");
    c.SwaggerEndpoint(fullSwaggerPath, customVersion);
});

app.UseCors(cors => cors
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials()
           );

app.MapMcp("/mcp");
app.MapMcpWebSocket("/mcp/ws");
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
