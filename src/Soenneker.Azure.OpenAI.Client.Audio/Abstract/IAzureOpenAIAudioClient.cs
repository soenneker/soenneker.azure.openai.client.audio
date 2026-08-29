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
    /// Overrides the configured deployment before the client is first created.
    /// </summary>
    /// <param name="deployment">Azure OpenAI deployment name.</param>
    /// <exception cref="ArgumentException"><paramref name="deployment"/> is blank.</exception>
    /// <exception cref="InvalidOperationException">The audio client has already been created.</exception>
    void SetOptions(string deployment);

    /// <summary>
    /// Returns the configured audio Client used by the azure openai audio client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested audio Client.</returns>
    ValueTask<AudioClient> Get(CancellationToken cancellationToken = default);
}
