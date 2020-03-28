namespace BibleReadings2.Repository
{
    public class Translation
    {
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;

        public override string ToString() => $"{Name} ({ShortName})";
    }
}