using KaraW3B.SDK.Connectors.Collections;
using KaraW3B.SDK.Connectors.Songs;

namespace KaraW3B.SDK.Connectors
{
    public interface IKaraW3BConnector
    {
        ILibrariesConnector Libraries { get; }
        ISongsConnector Songs { get; }
    }
}