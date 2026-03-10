using System.ComponentModel.DataAnnotations;
using WebClient.Models;

namespace WebClient.Models
{
    public class BookCreateUpdateDto
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        public int PubId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be >= 0")]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Advance must be >= 0")]
        public decimal Advance { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Royalty must be >= 0")]
        public decimal Royalty { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "YtdSales must be >= 0")]
        public int YtdSales { get; set; }

        public string Notes { get; set; }

        public DateTime? PublishedDate { get; set; }

        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
