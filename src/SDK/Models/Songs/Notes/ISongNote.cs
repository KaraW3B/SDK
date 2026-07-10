namespace KaraW3B.SDK.Models.Songs.Notes
{
    public interface ISongNote
    {
        int FileLine { get; }
        NoteType Type { get; }
        int PlayerNumber { get; }
        int StartBeat { get; }
        int? Duration { get; }
        int? Pitch { get; }
        string Text { get; }
    }
}