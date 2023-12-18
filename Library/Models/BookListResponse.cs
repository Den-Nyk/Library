namespace Library.Models
{
    public class BookListResponse
    {
        public List<BookShowModel> Books { get; set; }
        public bool IsLastPage { get; set; }
    }
}
