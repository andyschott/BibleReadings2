using BibleReadings2.Repository;

namespace BibleReadings2.Models
{
    public class ReadingViewModel
    {
        private const string BaseUrl = "http://www.biblegateway.com/passage/?search=";

        public Reading Reading { get; }

        public ReadingViewModel(Reading reading)
        {
            Reading = reading;
        }

        public string ReadingText {
            get {
                if(Reading.StartChapter == Reading.EndChapter)
                {
                    if(Reading.StartVerse == Reading.EndVerse)
                    {
                        return $"{Reading.Book} {Reading.StartChapter}";
                    }
                    else
                    {
                        return $"{Reading.Book} {Reading.StartChapter}:{Reading.StartVerse} - {Reading.EndVerse}";
                    }
                }

                if(Reading.StartVerse == Reading.EndVerse)
                {
                    return $"{Reading.Book} {Reading.StartChapter} - {Reading.EndChapter}";
                }

                return $"{Reading.Book} {Reading.StartChapter}:{Reading.StartVerse} - {Reading.EndChapter}:{Reading.EndVerse}";
            }
        }

        public string Url => BaseUrl + ReadingText;
    }
}