[![](https://img.shields.io/nuget/v/soenneker.azure.openai.client.audio.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client.audio/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.openai.client.audio/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.openai.client.audio/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.openai.client.audio.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.openai.client.audio/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Azure.OpenAI.Client.Audio
### An async thread-safe singleton for the Azure OpenAI audio client

## Installation

```
dotnet add package Soenneker.Azure.OpenAI.Client.Audio
```


Register:

```
builder.services.AddAzureOpenAIAudioClientAsSingleton();
```

`IConfiguration` values:

```
"Azure:OpenAI:Audio:Deployment"
"Azure:OpenAI:ApiKey"
"Azure:OpenAI:Uri"
```