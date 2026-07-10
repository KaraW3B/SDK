using KaraW3B.SDK.Helpers;
using KaraW3B.SDK.Interpreters.Models;
using KaraW3B.SDK.Models.Songs.Alerts;
using KaraW3B.SDK.Models.Songs.Medleys;
using KaraW3B.SDK.Models.Songs.Notes;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KaraW3B.SDK.Interpreters.Helpers
{
    internal static class InterpreterHelper
    {
        public static readonly HashSet<string> DefaultMandatoryHeaders = new()
        {
            "TITLE",
            "ARTIST",
            "BPM"
        };

        public static readonly Dictionary<string, string> DefaultHeaderAliases = new()
        {
            { "AUTHOR", "CREATOR" },
            { "PREVIEW", "PREVIEWSTART" }
        };

        public const int MaxRecommendedHeaderSize = 2048;
        public const char ListSplitter = ',';
        public const char EndOfFileMarker = 'E';

        #region Common regex

        public static readonly Regex HeaderRegex =
            new("^#(?<headerName>[A-Z0-9]+): *(?<headerValue>.*) *$",
                RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

        public static readonly Regex NoteRegex =
            new(@"^(?<noteType>.) (?<startBeat>\d+) (?<duration>\d+) (?<pitch>-?\d+) (?<text>.*)$",
                RegexOptions.Compiled | RegexOptions.Singleline);

        public static readonly Regex EndOfPhraseRegex =
            new(@"^-( (?<startBeat>\d+))?",
                RegexOptions.Compiled | RegexOptions.Singleline);

        // Space is for yass :(
        public static readonly Regex PlayerNumberRegex =
            new("^P *(?<playerNumber>[1-9])$",
                RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

        #endregion

        public static NoteType ParseNoteType(string noteType)
        {
            return noteType.ToUpperInvariant() switch
            {
                ":" => NoteType.Regular,
                "*" => NoteType.Golden,
                "R" => NoteType.Rap,
                "G" => NoteType.GoldenRap,
                _ => NoteType.Freestyle
            };
        }

        public static string GetNoteType(this NoteType noteType)
        {
            return noteType switch
            {
                NoteType.Regular => ":",
                NoteType.Golden => "*",
                NoteType.Rap => "R",
                NoteType.GoldenRap => "G",
                _ => "F"
            };
        }

        public static void ComputeMedleyTimesFromBeats(IInterpretableSong song, int medleyStartBeat, int medleyEndBeat)
        {
            var medleyStartTime = TimesHelper.GetTimeFromBeat(song.Bpm, medleyStartBeat, song.Gap);
            if (!medleyStartTime.HasValue)
            {
                song.AddAlert(AlertType.Parsing, AlertLevel.Error, "Unable to compute the medley start time");
                return;
            }

            var medleyEndTime = TimesHelper.GetTimeFromBeat(song.Bpm, medleyEndBeat, song.Gap);
            if (!medleyEndTime.HasValue)
            {
                song.AddAlert(AlertType.Parsing, AlertLevel.Error, "Unable to compute the medley end time");
                return;
            }

            song.SetMedley(new SongMedleyDto
            {
                MedleyStart = medleyStartTime.Value,
                MedleyEnd = medleyEndTime.Value
            });
        }

        public static bool TryExtractPlayerNumber(string text, out int playerNumber)
        {
            var playerHeaderMatch = PlayerNumberRegex.Match(text);
            if (!playerHeaderMatch.Success)
            {
                playerNumber = 0;
                return false;
            }

            playerNumber = int.Parse(playerHeaderMatch.Groups["playerNumber"].Value, CultureInfo.InvariantCulture);
            return true;
        }
    }
}