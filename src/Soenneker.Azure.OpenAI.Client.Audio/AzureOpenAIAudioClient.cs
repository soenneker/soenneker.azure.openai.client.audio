using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.Audio;
using Soenneker.Azure.OpenAI.Client.Abstract;
using Soenneker.Azure.OpenAI.Client.Audio.Abstract;
using Soenneker.Extensions.String;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;

// ReSharper disable InconsistentNaming

namespace Soenneker.Azure.OpenAI.Client.Audio;

/// <inheritdoc cref="IAzureOpenAIAudioClient"/>
public sealed class AzureOpenAIAudioClient : IAzureOpenAIAudioClient
{
    private readonly AsyncSingleton<AudioClient> _client;
    private readonly ILogger<AudioClient> _logger;
    private readonly IConfiguration _configuration;
    private readonly IAzureOpenAIClientUtil _azureOpenAiClientUtil;
    private readonly object _optionsLock = new();

    private string? _deployment;
    private bool _clientCreated;

    public AzureOpenAIAudioClient(ILogger<AudioClient> logger, IConfiguration configuration, IAzureOpenAIClientUtil azureOpenAiClientUtil)
    {
        _logger = logger;
        _configuration = configuration;
        _azureOpenAiClientUtil = azureOpenAiClientUtil;
        _client = new AsyncSingleton<AudioClient>(CreateClient);
    }

    private async ValueTask<AudioClient> CreateClient(CancellationToken ct)
    {
        AzureOpenAIClient azureClient = await _azureOpenAiClientUtil.Get(ct).NoSync();

        string? deployment = _configuration.GetValue<string?>("Azure:OpenAI:Audio:Deployment");

        lock (_optionsLock)
        {
            if (!_deployment.IsNullOrEmpty())
                deployment = _deployment;

            deployment.ThrowIfNullOrWhiteSpace();

            _logger.LogDebug("Creating Azure OpenAI Audio client with deployment ({deployment})...", deployment);

            AudioClient client = azureClient.GetAudioClient(deployment);
            _clientCreated = true;
            return client;
        }
    }

    public void SetOptions(string deployment)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(deployment);

        lock (_optionsLock)
        {
            if (_clientCreated)
                throw new InvalidOperationException("The deployment must be set before the Azure OpenAI audio client is created.");

            _deployment = deployment;
        }
    }

    public ValueTask<AudioClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
