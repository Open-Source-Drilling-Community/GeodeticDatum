using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NORCE.Drilling.GeodeticDatum.Service.Managers;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public sealed class McpHubRegistrationService : BackgroundService
{
    public static readonly Guid ServiceTypeId = Guid.Parse("75810d5b-0db8-42b7-9054-9e450ec5a3c2");

    private const string InstanceIdFileName = "mcp-hub-instance-id.txt";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<McpHubRegistrationService> _logger;
    private readonly IOptionsMonitor<McpHubOptions> _options;
    private Guid? _registeredInstanceId;

    public McpHubRegistrationService(
        IHttpClientFactory httpClientFactory,
        IOptionsMonitor<McpHubOptions> options,
        ILogger<McpHubRegistrationService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        McpHubOptions options = _options.CurrentValue;
        if (!options.Enabled)
        {
            _logger.LogInformation("MCP hub registration is disabled.");
            return;
        }

        if (string.IsNullOrWhiteSpace(options.HubBaseUrl))
        {
            _logger.LogInformation("MCP hub registration is enabled but no hub URL is configured.");
            return;
        }

        if (string.IsNullOrWhiteSpace(options.PublicBaseUrl))
        {
            _logger.LogWarning("MCP hub registration is enabled but no public base URL is configured.");
            return;
        }

        TimeSpan retryInterval = GetRetryInterval(options);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                options = _options.CurrentValue;
                retryInterval = GetRetryInterval(options);
                if (!options.Enabled || string.IsNullOrWhiteSpace(options.HubBaseUrl) || string.IsNullOrWhiteSpace(options.PublicBaseUrl))
                {
                    _logger.LogInformation("MCP hub registration stopped because configuration is no longer complete.");
                    return;
                }

                if (await RegisterAsync(options, stoppingToken).ConfigureAwait(false))
                {
                    await Task.Delay(retryInterval, stoppingToken).ConfigureAwait(false);
                }
                else
                {
                    await Task.Delay(retryInterval, stoppingToken).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "MCP hub registration attempt failed. Retrying later.");
                try
                {
                    await Task.Delay(retryInterval, stoppingToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        McpHubOptions options = _options.CurrentValue;
        if (options.Enabled && options.UnregisterOnShutdown && _registeredInstanceId.HasValue)
        {
            await UnregisterAsync(options, _registeredInstanceId.Value, cancellationToken).ConfigureAwait(false);
        }

        await base.StopAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task<bool> RegisterAsync(McpHubOptions options, CancellationToken cancellationToken)
    {
        Guid instanceId = ResolveInstanceId(options);
        McpMicroserviceRegistration registration = BuildRegistration(options, instanceId);
        Uri collectionUri = BuildCollectionUri(options);
        Uri itemUri = new(collectionUri, Uri.EscapeDataString(instanceId.ToString()));
        HttpClient httpClient = _httpClientFactory.CreateClient(nameof(McpHubRegistrationService));

        HttpResponseMessage response = await httpClient.PutAsJsonAsync(itemUri, registration, cancellationToken).ConfigureAwait(false);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            response.Dispose();
            response = await httpClient.PostAsJsonAsync(collectionUri, registration, cancellationToken).ConfigureAwait(false);
        }

        using (response)
        {
            if (response.IsSuccessStatusCode)
            {
                _registeredInstanceId = instanceId;
                _logger.LogInformation("Registered MCP microservice instance {InstanceId} on hub {HubUrl}.", instanceId, collectionUri);
                return true;
            }

            _logger.LogWarning("MCP hub registration failed with HTTP status {StatusCode}. Retrying later.", response.StatusCode);
            return false;
        }
    }

    private async Task UnregisterAsync(McpHubOptions options, Guid instanceId, CancellationToken cancellationToken)
    {
        try
        {
            Uri collectionUri = BuildCollectionUri(options);
            Uri itemUri = new(collectionUri, Uri.EscapeDataString(instanceId.ToString()));
            HttpClient httpClient = _httpClientFactory.CreateClient(nameof(McpHubRegistrationService));
            using HttpResponseMessage response = await httpClient.DeleteAsync(itemUri, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogInformation("Unregistered MCP microservice instance {InstanceId} from hub {HubUrl}.", instanceId, collectionUri);
            }
            else
            {
                _logger.LogWarning("MCP hub unregistration failed with HTTP status {StatusCode}.", response.StatusCode);
            }
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(ex, "MCP hub unregistration failed.");
        }
    }

    private static McpMicroserviceRegistration BuildRegistration(McpHubOptions options, Guid instanceId)
    {
        string publicBaseUrl = options.PublicBaseUrl!.TrimEnd('/');
        string mcpHttpUrl = $"{publicBaseUrl}/GeodeticDatum/api/mcp";
        string mcpWebSocketUrl = ToWebSocketUrl($"{publicBaseUrl}/GeodeticDatum/api/mcp/ws");

        return new McpMicroserviceRegistration(
            ServiceTypeId,
            instanceId,
            string.IsNullOrWhiteSpace(options.ServiceName) ? "GeodeticDatum" : options.ServiceName,
            mcpHttpUrl,
            mcpWebSocketUrl,
            DateTimeOffset.UtcNow);
    }

    private static Uri BuildCollectionUri(McpHubOptions options)
    {
        string hubBaseUrl = options.HubBaseUrl!.TrimEnd('/') + "/";
        string endpoint = string.IsNullOrWhiteSpace(options.RegistrationEndpoint)
            ? "McpMicroservice"
            : options.RegistrationEndpoint.Trim('/');
        return new Uri(new Uri(hubBaseUrl, UriKind.Absolute), endpoint + "/");
    }

    private static TimeSpan GetRetryInterval(McpHubOptions options)
    {
        return TimeSpan.FromSeconds(Math.Max(1, options.RetryIntervalSeconds));
    }

    private static string ToWebSocketUrl(string httpUrl)
    {
        UriBuilder builder = new(httpUrl);
        builder.Scheme = builder.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase) ? "wss" : "ws";
        return builder.Uri.ToString();
    }

    private static Guid ResolveInstanceId(McpHubOptions options)
    {
        if (Guid.TryParse(options.InstanceId, out Guid configuredInstanceId) && configuredInstanceId != Guid.Empty)
        {
            return configuredInstanceId;
        }

        Directory.CreateDirectory(SqlConnectionManager.HOME_DIRECTORY);
        string instanceIdFile = Path.Combine(SqlConnectionManager.HOME_DIRECTORY, InstanceIdFileName);
        if (File.Exists(instanceIdFile) && Guid.TryParse(File.ReadAllText(instanceIdFile).Trim(), out Guid persistedInstanceId) && persistedInstanceId != Guid.Empty)
        {
            return persistedInstanceId;
        }

        Guid generatedInstanceId = Guid.NewGuid();
        File.WriteAllText(instanceIdFile, generatedInstanceId.ToString());
        return generatedInstanceId;
    }

    private sealed record McpMicroserviceRegistration(
        [property: JsonPropertyName("serviceTypeId")] Guid ServiceTypeId,
        [property: JsonPropertyName("instanceId")] Guid InstanceId,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("mcpHttpUrl")] string McpHttpUrl,
        [property: JsonPropertyName("mcpWebSocketUrl")] string McpWebSocketUrl,
        [property: JsonPropertyName("lastSeenUtc")] DateTimeOffset LastSeenUtc);
}
