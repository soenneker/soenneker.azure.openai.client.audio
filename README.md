[![](https://img.shields.io/nuget/v/soenneker.azure.openai.client.audio.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client.audio/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client.audio/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client.audio/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.openai.client.audio.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client.audio/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client.audio/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client.audio/actions/workflows/codeql.yml)

# Soenneker.Azure.OpenAI.Client.Audio

An async thread-safe singleton for the Azure OpenAI audio client.

## Install

```bash
dotnet add package Soenneker.Azure.OpenAI.Client.Audio
```

## Quick start

```csharp
using Soenneker.Azure.OpenAI.Client.Audio.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddAzureOpenAIAudioClientAsSingleton();
```

Adds `IAzureOpenAIAudioClient` as a singleton service.

## What you get

- `IAzureOpenAIAudioClient` — An async thread-safe singleton for the Azure OpenAI audio client.
- `AzureOpenAIAudioClientRegistrar` — An async thread-safe singleton for the Azure OpenAI audio client.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `AzureOpenAIAudioClientRegistrar.AddAzureOpenAIAudioClientAsSingleton(services)` | Adds `IAzureOpenAIAudioClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `AzureOpenAIAudioClientRegistrar.AddAzureOpenAIAudioClientAsScoped(services)` | Adds `IAzureOpenAIAudioClient` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
