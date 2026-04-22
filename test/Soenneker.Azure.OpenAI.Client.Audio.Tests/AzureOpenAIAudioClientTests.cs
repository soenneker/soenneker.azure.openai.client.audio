using Soenneker.Azure.OpenAI.Client.Audio.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Azure.OpenAI.Client.Audio.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class AzureOpenAIAudioClientTests : HostedUnitTest
{
    private readonly IAzureOpenAIAudioClient _util;

    public AzureOpenAIAudioClientTests(Host host) : base(host)
    {
        _util = Resolve<IAzureOpenAIAudioClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
