namespace WebClient.Models
{
    public class BookAuthor
    {
        public int AuthorId { get; set; }

        public int BookId { get; set; }

        public int AuthorOrder { get; set; }

        public decimal RoyaltyPercentage { get; set; }

        // Navigation
        public Author Author { get; set; }

        public Book Book { get; set; }
    }
}
