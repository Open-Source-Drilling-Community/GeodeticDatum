using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSingleton<IMcpTool, PingMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllSpheroidIdsMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllSpheroidMetaInfoMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllSpheroidMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetSpheroidByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, PostSpheroidMcpTool>();
builder.Services.AddSingleton<IMcpTool, PutSpheroidByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, DeleteSpheroidByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, FindSpheroidIdByNameMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllGeodeticDatumIdsMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllGeodeticDatumMetaInfoMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllGeodeticDatumLightMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllGeodeticDatumMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetGeodeticDatumByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, PostGeodeticDatumMcpTool>();
builder.Services.AddSingleton<IMcpTool, PutGeodeticDatumByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, DeleteGeodeticDatumByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, FindGeodeticDatumIdByNameMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllGeodeticConversionSetIdsMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllGeodeticConversionSetMetaInfoMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllGeodeticConversionSetLightMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetAllGeodeticConversionSetMcpTool>();
builder.Services.AddSingleton<IMcpTool, GetGeodeticConversionSetByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, PostGeodeticConversionSetMcpTool>();
builder.Services.AddSingleton<IMcpTool, PutGeodeticConversionSetByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, DeleteGeodeticConversionSetByIdMcpTool>();
builder.Services.AddSingleton<IMcpTool, ConvertGeodeticDatumCoordinateMcpTool>();
builder.Services.AddSingleton<McpToolRegistry>();
builder.Services.AddSingleton<McpServer>();

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

app.MapMcpEndpoints();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
