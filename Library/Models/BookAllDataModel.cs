namespace Library.Models
{
    public class BookAllDataModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int YearOfCreation { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string LinkToYaBook { get; set; }
        public List<byte[]> Imgs { get; set; }
        public List<string> Types { get; set; }
        public List<string> PublishingHouses { get; set; }
        public List<string> Languages { get; set; }
        public List<int> YearsOfPublication { get; set; }
        public int Weight { get; set; }
        public bool Illustrations { get; set; }
        public string Format { get; set; }
        public string AuthorDescription { get; set; }
        public string BookType { get; set; }
        public string AuthorLinkToYaBook { get; set; }
        public bool isHearted { get; set; }
    }
}
