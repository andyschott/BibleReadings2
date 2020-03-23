namespace BibleReadings2.Repository
{
    public class Reading
    {
        public string Book { get; set; } = string.Empty;
        public int StartChapter { get; set; }
        public int? StartVerse { get; set; }
        public int? EndChapter { get; set; }
        public int? EndVerse { get; set; }
    }
}