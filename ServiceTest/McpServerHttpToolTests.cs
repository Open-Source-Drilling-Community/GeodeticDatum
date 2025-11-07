using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using NORCE.Drilling.GeodeticDatum.Service.Mcp;
using NUnit.Framework;

namespace ServiceTest;

[TestFixture]
public sealed class McpServerHttpToolTests
{
    private const string McpEndpoint = "http://localhost:8080/GeodeticDatum/api/mcp";
    //private const string McpEndpoint = "http://localhost:5002/GeodeticDatum/api/mcp";
    //private const string McpEndpoint = "https://localhost:5001/GeodeticDatum/api/mcp";
    //private const string McpEndpoint = "https://dev.DigiWells.no/GeodeticDatum/api/mcp";
    //private const string McpEndpoint = "https://app.DigiWells.no/GeodeticDatum/api/mcp";

    private ServiceProvider _localProvider = default!;
    private IReadOnlyDictionary<string, IMcpTool> _localTools = default!;
    private HttpClientTransport _transport = default!;
    private McpClient _client = default!;
    private IReadOnlyDictionary<string, RemoteToolSnapshot> _remoteTools = default!;

    public static IEnumerable<string> ToolNames()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        foreach (var type in typeof(IMcpTool).Assembly.GetTypes().Where(IsToolType))
        {
            services.AddSingleton(typeof(IMcpTool), type);
        }

        using var provider = services.BuildServiceProvider();
        return provider
            .GetServices<IMcpTool>()
            .Select(tool => tool.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();
    }

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        foreach (var type in typeof(IMcpTool).Assembly.GetTypes().Where(IsToolType))
        {
            services.AddSingleton(typeof(IMcpTool), type);
        }

        _localProvider = services.BuildServiceProvider();
        _localTools = _localProvider
            .GetServices<IMcpTool>()
            .ToDictionary(tool => tool.Name, tool => tool, StringComparer.Ordinal);

        var httpOptions = new HttpClientTransportOptions
        {
            Endpoint = new Uri(McpEndpoint),
            TransportMode = HttpTransportMode.AutoDetect
        };

        var loggerFactory = NullLoggerFactory.Instance;
        _transport = new HttpClientTransport(httpOptions, loggerFactory);

        var clientOptions = new McpClientOptions
        {
            ClientInfo = new Implementation
            {
                Name = "ServiceTestSuite",
                Version = typeof(McpServerHttpToolTests).Assembly.GetName().Version?.ToString() ?? "1.0.0"
            }
        };

        try
        {
            _client = await McpClient.CreateAsync(_transport, clientOptions, loggerFactory, CancellationToken.None)
                .ConfigureAwait(false);

            var tools = await _client.ListToolsAsync(cancellationToken: CancellationToken.None).ConfigureAwait(false);
            _remoteTools = tools.ToDictionary(
                t => t.Name,
                t => new RemoteToolSnapshot(t.Name, t.Description, ToJsonNode(t.JsonSchema)),
                StringComparer.Ordinal);
        }
        catch (Exception ex)
        {
            Assert.Inconclusive(
                $"Unable to connect to the MCP server at {McpEndpoint}. Start the Service project and retry. Details: {ex.Message}");
        }
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (_client is not null)
        {
            await _client.DisposeAsync().ConfigureAwait(false);
        }

        if (_transport is not null)
        {
            await _transport.DisposeAsync().ConfigureAwait(false);
        }

        (_localProvider as IDisposable)?.Dispose();
    }

    [Test]
    public void Http_endpoint_exposes_all_registered_tools()
    {
        Assert.That(_remoteTools.Keys, Is.EquivalentTo(_localTools.Keys));
    }

    [TestCaseSource(nameof(ToolNames))]
    public void Tool_metadata_round_trips_through_http_server(string toolName)
    {
        var localTool = _localTools[toolName];
        var remoteTool = _remoteTools[toolName];

        Assert.That(remoteTool.Name, Is.EqualTo(localTool.Name));
        Assert.That(remoteTool.Description, Is.EqualTo(localTool.Description));

        var remoteSchema = remoteTool.Schema;
        if (localTool.InputSchema is null)
        {
            Assert.That(remoteSchema is null || IsDefaultSchema(remoteSchema),
                $"Expected '{toolName}' schema to be empty but received {remoteSchema}.");
        }
        else
        {
            Assert.That(remoteSchema, Is.Not.Null, $"Server did not publish a schema for '{toolName}'.");
            Assert.That(JsonNode.DeepEquals(localTool.InputSchema, remoteSchema), Is.True,
                $"Schema mismatch for tool '{toolName}'.");
        }
    }

    [Test]
    public async Task Ping_tool_can_be_invoked_over_http_transport()
    {
        var result = await _client.CallToolAsync(
            "ping",
            new Dictionary<string, object?>(),
            cancellationToken: CancellationToken.None).ConfigureAwait(false);

        Assert.That(result.StructuredContent, Is.Not.Null);
        var payload = result.StructuredContent as JsonObject;
        Assert.That(payload, Is.Not.Null);
        Assert.That(payload!["message"]?.GetValue<string>(), Is.EqualTo("pong"));
    }

    private static bool IsToolType(Type type) =>
        typeof(IMcpTool).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract;

    private static JsonNode? ToJsonNode(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Undefined || element.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return JsonNode.Parse(element.GetRawText());
    }

    private static bool IsDefaultSchema(JsonNode? schema)
    {
        if (schema is not JsonObject obj)
        {
            return false;
        }

        return obj.Count == 1 &&
               obj.TryGetPropertyValue("type", out var typeNode) &&
               string.Equals(typeNode?.GetValue<string>(), "object", StringComparison.Ordinal);
    }

    private sealed record RemoteToolSnapshot(string Name, string Description, JsonNode? Schema);
}
