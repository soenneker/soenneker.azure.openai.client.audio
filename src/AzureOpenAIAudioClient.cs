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

    private string? _deployment;

    public AzureOpenAIAudioClient(ILogger<AudioClient> logger, IConfiguration configuration, IAzureOpenAIClientUtil azureOpenAiClientUtil)
    {
        _client = new AsyncSingleton<AudioClient>(async (ct, _) =>
        {
            AzureOpenAIClient azureClient = await azureOpenAiClientUtil.Get(ct).NoSync();

            var deployment = configuration.GetValue<string?>("Azure:OpenAI:Audio:Deployment");

            if (!_deployment.IsNullOrEmpty())
                deployment = _deployment;

            deployment.ThrowIfNullOrWhiteSpace();

            logger.LogDebug("Creating Azure OpenAI Audio client with deployment ({deployment})...", deployment);

            return azureClient.GetAudioClient(deployment);
        });
    }

    public void SetOptions(string deployment)
    {
        _deployment = deployment;
    }

    public ValueTask<AudioClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}