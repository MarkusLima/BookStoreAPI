using BookStoreAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<ItemOfPurchase> ItemsOfPurchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relacionamento um-para-muitos um Role pode ter muitos Users
            modelBuilder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithOne(r => r.Role)
                .HasForeignKey(u => u.roleId);

            // Configurar relacionamento um-para-muitos um Author pode ter muitos Books
            modelBuilder.Entity<Author>()
                .HasMany(b => b.Books)
                .WithOne(a => a.Author)
                .HasForeignKey(b => b.authorId);

            // Configurar relacionamento um-para-muitos uma Category pode ter muitos Books
            modelBuilder.Entity<Category>()
                .HasMany(b => b.Books)
                .WithOne(c => c.Category)
                .HasForeignKey(b =>b.categoryId);

            // Configurar relacionamento um-para-muitos um Book pode ter muitos ItemOfPurchase
            modelBuilder.Entity<Book>()
                .HasMany(i => i.ItemsOfPurchase)
                .WithOne(b => b.Books)
                .HasForeignKey(i => i.bookId);

            // Configurar relacionamento um-para-muitos um Purchase pode ter muitos ItemOfPurchase
            modelBuilder.Entity<Purchase>()
                .HasMany(i => i.ItemsOfPurchase)
                .WithOne(p => p.Puchases)
                .HasForeignKey(i =>i.purchaseId);

            // Configurar relacionamento um-para-muitos um User pode ter muitas Purchases
            modelBuilder.Entity<User>()
                .HasMany(p => p.Purchases)
                .WithOne(u => u.User)
                .HasForeignKey(p => p.userId);
        }
    }
}
