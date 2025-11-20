using System;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPIModel
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<MstUserModel> MstUsers { get; set; }

    }
}
