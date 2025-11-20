using System;
namespace LibraryManagementAPIModel.DTO
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public int Year { get; set; }
    }
}

