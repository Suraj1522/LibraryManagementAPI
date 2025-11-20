using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementAPIModel
{
    public class MstUserModel
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string EmailId { get; set; } = string.Empty;

        [MaxLength(15)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(20)]
        public string? Language { get; set; }

        [MaxLength(50)]
        public string? Country { get; set; }

        [MaxLength(50)]
        public string? Designation { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid AddedBy { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public byte[]? ProfilePhoto { get; set; }
    }
}
