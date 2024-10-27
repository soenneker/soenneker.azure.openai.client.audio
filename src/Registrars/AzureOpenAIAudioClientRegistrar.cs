using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Azure.OpenAI.Client.Audio.Abstract;
using Soenneker.Azure.OpenAI.Client.Registrars;

namespace Soenneker.Azure.OpenAI.Client.Audio.Registrars;

/// <summary>
/// An async thread-safe singleton for the Azure OpenAI audio client
/// </summary>
public static class AzureOpenAIAudioClientRegistrar
{
    /// <summary>
    /// Adds <see cref="IAzureOpenAIAudioClient"/> as a singleton service. <para/>
    /// </summary>
    public static void AddAzureOpenAIAudioClientAsSingleton(this IServiceCollection services)
    {
        services.AddAzureOpenAIClientUtilAsSingleton();
        services.TryAddSingleton<IAzureOpenAIAudioClient, AzureOpenAIAudioClient>();
    }

    /// <summary>
    /// Adds <see cref="IAzureOpenAIAudioClient"/> as a scoped service. <para/>
    /// </summary>
    public static void AddAzureOpenAIAudioClientAsScoped(this IServiceCollection services)
    {
        services.AddAzureOpenAIClientUtilAsScoped();
        services.TryAddScoped<IAzureOpenAIAudioClient, AzureOpenAIAudioClient>();
    }
}
