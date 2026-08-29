[![](https://img.shields.io/nuget/v/soenneker.azure.openai.client.audio.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client.audio/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client.audio/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client.audio/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.openai.client.audio.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client.audio/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client.audio/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client.audio/actions/workflows/codeql.yml)

# Soenneker.Azure.OpenAI.Client.Audio

Creates and caches an OpenAI SDK `AudioClient` for an Azure OpenAI deployment.

## Installation

```bash
dotnet add package Soenneker.Azure.OpenAI.Client.Audio
```

## Configuration and registration

```json
{
  "Azure": {
    "OpenAI": {
      "Uri": "https://your-resource.openai.azure.com",
      "ApiKey": "your-api-key",
      "Audio": {
        "Deployment": "audio-deployment-name"
      }
    }
  }
}
```

```csharp
using Soenneker.Azure.OpenAI.Client.Audio.Registrars;

builder.Services.AddAzureOpenAIAudioClientAsSingleton();
```

The registrar includes the shared Azure OpenAI client. Keep the API key in a secret provider.

## Transcribe audio

```csharp
using OpenAI.Audio;
using Soenneker.Azure.OpenAI.Client.Audio.Abstract;

public sealed class TranscriptionService(IAzureOpenAIAudioClient audioClientUtil)
{
    public async Task<string> Transcribe(
        string filePath,
        CancellationToken cancellationToken)
    {
        AudioClient client = await audioClientUtil.Get(cancellationToken);
        await using FileStream audio = File.OpenRead(filePath);

        AudioTranscription transcription = await client.TranscribeAudioAsync(
            audio,
            Path.GetFileName(filePath),
            options: null,
            cancellationToken: cancellationToken);

        return transcription.Text;
    }
}
```

The returned SDK client also exposes speech-generation operations when supported by the configured deployment.

## Deployment and lifecycle

- `Azure:OpenAI:Audio:Deployment` is required unless `SetOptions(deployment)` is called before the first `Get()`.
- `SetOptions()` overrides configuration for that utility instance.
- Calling `SetOptions()` after client creation throws; it does not silently change the cached deployment.
- The audio client and underlying Azure client are cached. Replace the DI scope or singleton to switch deployments or credentials.
- Audio content may contain sensitive data. Apply appropriate retention, logging, and access controls in the consuming application.
