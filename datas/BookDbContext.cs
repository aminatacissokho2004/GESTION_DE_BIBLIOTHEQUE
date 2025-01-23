using Microsoft.EntityFrameworkCore;

namespace Gestion_de_Bibliothéque.datas
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options){}
        public DbSet<Book> Books { get;}
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "sous l'orage", 
                    Author = "Mariama Ba", 
                    PubDate = new DateOnly(2000,10,29)
                },
                new Book
                {
                    Id = 2,
                    Title = "sous l'orage", 
                    Author = "Mariama Ba", 
                    PubDate = new DateOnly(2000,10,29)
                }
                );
        }   
    }
}
