using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPIModel
{
	public class BookModel
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;

    }
}

