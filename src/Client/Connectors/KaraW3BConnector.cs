using System;
using System.Net.Http;
using KaraW3B.SDK.Client.Connectors.Collections;
using KaraW3B.SDK.Client.Connectors.Songs;

namespace KaraW3B.SDK.Client.Connectors
{
    public sealed class KaraW3BConnector : IKaraW3BConnector, IDisposable
    {
        private readonly HttpClient _httpClient;

        public KaraW3BConnector(Uri baseUri, TimeSpan? timeout = null)
        {
            _httpClient = new HttpClient { Timeout = timeout ?? TimeSpan.FromSeconds(30) };

            Libraries = new LibrariesConnector(_httpClient, baseUri);
            Songs = new SongsConnector(_httpClient, baseUri);
        }

        public ILibrariesConnector Libraries { get; }

        public ISongsConnector Songs { get; }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}