using Soenneker.Azure.OpenAI.Client.Audio.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Azure.OpenAI.Client.Audio.Tests;

[Collection("Collection")]
public class AzureOpenAIAudioClientTests : FixturedUnitTest
{
    private readonly IAzureOpenAIAudioClient _util;

    public AzureOpenAIAudioClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IAzureOpenAIAudioClient>(true);
    }
}
