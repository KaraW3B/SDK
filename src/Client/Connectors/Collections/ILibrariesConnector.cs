using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KaraW3B.SDK.Models.Libraries;
using KaraW3B.SDK.Models.Songs;

namespace KaraW3B.SDK.Client.Connectors.Collections
{
    public interface ILibrariesConnector
    {
        IAsyncEnumerable<LibraryDto> GetLibrariesAsync(CancellationToken cancellationToken = default);
        Task<LibraryDto> GetLibraryAsync(Guid libraryId, CancellationToken cancellationToken = default);

        IAsyncEnumerable<SongDto> GetSongsAsync(Guid libraryId, bool onlyLoadableSongs,
            CancellationToken cancellationToken = default);
    }
}