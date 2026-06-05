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
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<AudioClient> Get(CancellationToken cancellationToken = default);
}
