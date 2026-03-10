using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        [ForeignKey("Publisher")]
        public int PubId { get; set; }

        public decimal Price { get; set; }

        public decimal Advance { get; set; }

        public decimal Royalty { get; set; }

        public int YtdSales { get; set; }

        public string Notes { get; set; }

        public DateTime? PublishedDate { get; set; }

        // Navigation
        public Publisher Publisher { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
