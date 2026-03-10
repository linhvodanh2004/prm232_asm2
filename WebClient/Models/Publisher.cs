using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Publisher
    {
        [Key]
        public int PubId { get; set; }

        [Required]
        [MaxLength(200)]
        public string PublisherName { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        // Navigation
        public ICollection<Book>? Books { get; set; }
    }
}
