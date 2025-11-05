using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NORCE.Drilling.GeodeticDatum.Service.Mcp;

namespace ServiceTest;

[TestFixture]
public class McpToolsMetadataTests
{
    private static readonly Type[] ToolTypes = typeof(IMcpTool).Assembly
        .GetTypes()
        .Where(t => typeof(IMcpTool).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
        .ToArray();

    private ServiceProvider _provider = default!;
    private Dictionary<Type, IMcpTool> _toolLookup = default!;
    private McpToolRegistry _registry = default!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        foreach (var type in ToolTypes)
        {
            services.AddSingleton(typeof(IMcpTool), type);
        }

        services.AddSingleton<McpToolRegistry>();

        _provider = services.BuildServiceProvider();
        _toolLookup = _provider.GetServices<IMcpTool>().ToDictionary(t => t.GetType());
        _registry = _provider.GetRequiredService<McpToolRegistry>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => _provider.Dispose();

    private IMcpTool GetTool(Type toolType) => _toolLookup[toolType];

    public static IEnumerable<Type> ToolTypeCases => ToolTypes;

    [Test]
    public void All_tools_are_registered()
    {
        Assert.That(_toolLookup.Count, Is.EqualTo(ToolTypes.Length));
    }

    [Test]
    public void Tool_names_are_unique()
    {
        var names = _toolLookup.Values.Select(t => t.Name).ToList();
        Assert.That(names, Is.Unique);
    }

    [TestCaseSource(nameof(ToolTypeCases))]
    public void Tool_name_is_not_empty(Type toolType)
    {
        var tool = GetTool(toolType);
        Assert.That(tool.Name, Is.Not.Null.And.Not.Empty);
    }

    [TestCaseSource(nameof(ToolTypeCases))]
    public void Tool_description_is_not_blank(Type toolType)
    {
        var tool = GetTool(toolType);
        Assert.That(tool.Description, Is.Not.Null.And.Not.Empty);
    }

    [TestCaseSource(nameof(ToolTypeCases))]
    public void Input_schema_is_null_or_json_node(Type toolType)
    {
        var tool = GetTool(toolType);
        Assert.That(tool.InputSchema, Is.Null.Or.InstanceOf<JsonNode>());
    }

    [Test]
    public void Registry_lists_all_registered_tools()
    {
        var registryNames = _registry.DescribeTools().Select(t => t.Name).ToList();
        var toolNames = _toolLookup.Values.Select(t => t.Name).ToList();
        Assert.That(registryNames, Is.EquivalentTo(toolNames));
    }
}

[TestFixture]
public class McpToolBehaviorTests
{
    private ServiceProvider _provider = default!;
    private Dictionary<string, IMcpTool> _toolsByName = default!;

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        foreach (var type in McpToolsMetadataTests.ToolTypeCases)
        {
            services.AddSingleton(typeof(IMcpTool), type);
        }

        _provider = services.BuildServiceProvider();
        _toolsByName = _provider
            .GetServices<IMcpTool>()
            .ToDictionary(t => t.Name, t => t);
    }

    [TearDown]
    public void TearDown() => _provider.Dispose();

    [Test]
    public async Task Ping_tool_returns_pong()
    {
        var tool = _toolsByName["ping"];
        var result = await tool.InvokeAsync(null, CancellationToken.None) as JsonObject;
        Assert.That(result, Is.Not.Null);
        Assert.That(result!["message"]?.GetValue<string>(), Is.EqualTo("pong"));
    }

    [Test]
    public async Task Find_spheroid_by_name_requires_argument()
    {
        var tool = _toolsByName["spheroid.find_id_by_name"];
        var response = await tool.InvokeAsync(new JsonObject(), CancellationToken.None) as JsonObject;
        Assert.That(response, Is.Not.Null);
        Assert.That(response!["status"]?.GetValue<int>(), Is.EqualTo(400));
    }

    [Test]
    public async Task Find_geodetic_datum_by_name_requires_argument()
    {
        var tool = _toolsByName["geodetic_datum.find_id_by_name"];
        var response = await tool.InvokeAsync(new JsonObject(), CancellationToken.None) as JsonObject;
        Assert.That(response, Is.Not.Null);
        Assert.That(response!["status"]?.GetValue<int>(), Is.EqualTo(400));
    }

    [Test]
    public async Task Convert_coordinate_requires_source_datum_id()
    {
        var tool = _toolsByName["geodetic_datum.convert_coordinate"];
        var response = await tool.InvokeAsync(new JsonObject(), CancellationToken.None) as JsonObject;
        Assert.That(response, Is.Not.Null);
        Assert.That(response!["status"]?.GetValue<int>(), Is.EqualTo(400));
        Assert.That(response!["error"]?.GetValue<string>(), Does.Contain("sourceDatumId"));
    }
}
