using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(20)]
        public string Zip { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        // Navigation
        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
