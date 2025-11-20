using System;
namespace LibraryManagementAPIModel.DTO
{
	public class LoginViewModel
	{
        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Language { get; set; }

        public string? Country { get; set; }

        public string? Designation { get; set; }

        public byte[]? ProfilePhoto { get; set; }
    
	}
}

