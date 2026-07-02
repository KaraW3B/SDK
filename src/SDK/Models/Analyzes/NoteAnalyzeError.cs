using KaraW3B.SDK.Models.Songs.Notes;

namespace KaraW3B.SDK.Models.Analyzes
{
    public sealed class NoteAnalyzeError
    {
        public NoteAnalyzeError(string message, IAnalyzableSongNote note = null)
        {
            FileLine = note?.FileLine;
            Message = message;
        }

        public int? FileLine { get; }

        public string Message { get; }
    }
}