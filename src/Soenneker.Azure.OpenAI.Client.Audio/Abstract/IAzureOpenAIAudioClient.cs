using System;
using System.Threading;
using System.Threading.Tasks;
using OpenAI.Audio;

namespace Soenneker.Azure.OpenAI.Client.Audio.Abstract;

/// <summary>
/// An async thread-safe singleton for the Azure OpenAI audio client
/// </summary>
public interface IAzureOpenAIAudioClient : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Not required, but can be used to set the deployment and options for the client
    /// </summary>
    /// <param name="deployment"></param>
    void SetOptions(string deployment);

    /// <summary>
    /// Returns the configured audio Client used by the azure openai audio client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested audio Client.</returns>
    ValueTask<AudioClient> Get(CancellationToken cancellationToken = default);
}
