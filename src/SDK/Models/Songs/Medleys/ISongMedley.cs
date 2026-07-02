using System;

namespace KaraW3B.SDK.Models.Songs.Medleys
{
    public interface ISongMedley
    {
        TimeSpan MedleyStart { get; }
        TimeSpan MedleyEnd { get; }
    }
}