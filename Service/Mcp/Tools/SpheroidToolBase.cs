using System;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NORCE.Drilling.GeodeticDatum.Service.Controllers;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal abstract class SpheroidToolBase : IMcpTool
{
    private readonly IServiceScopeFactory _scopeFactory;

    protected SpheroidToolBase(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public abstract string Name { get; }

    public abstract string Description { get; }

    public abstract JsonNode? InputSchema { get; }

    public abstract Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken);

    protected ControllerAccessor CreateControllerScope()
    {
        var scope = _scopeFactory.CreateScope();
        var controller = scope.ServiceProvider.GetRequiredService<SpheroidController>();
        return new ControllerAccessor(scope, controller);
    }

    protected readonly struct ControllerAccessor : IDisposable
    {
        private readonly IServiceScope _scope;

        public ControllerAccessor(IServiceScope scope, SpheroidController controller)
        {
            _scope = scope;
            Controller = controller;
        }

        public SpheroidController Controller { get; }

        public IServiceProvider Services => _scope.ServiceProvider;

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
