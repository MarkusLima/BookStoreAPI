using BookStoreAPI.Data;
using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Seeders
{
    public class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Verifique se o banco de dados está vazio antes de adicionar dados
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { name = "Adm" },
                    new Role { name = "Client" }
                );
                context.SaveChanges();

            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { name = "Aventura" },
                    new Category { name = "Ficção" },
                    new Category { name = "Romance" },
                    new Category { name = "Suspense" },
                    new Category { name = "Terror" }
                );
                context.SaveChanges();
            }

            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                    new Author { name = "Author 1", dateOfBirth = new DateOnly(2023, 1, 1), country = "Noruega" },
                    new Author { name = "Author 2", dateOfBirth = new DateOnly(2023, 1, 1), country = "Espanha" }
                );
                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                // Pegar o id do primeiro autor e da primeira categoria
                var autorId = context.Authors.First().Id;
                var categoryId = context.Categories.First().Id;

                context.Books.AddRange(
                    new Book { title = "Book 1", authorId = autorId, categoryId = categoryId, publicationDate = new DateOnly(2023, 1, 1), price = 19.99M, stockQuantity = 100 },
                    new Book { title = "Book 2", authorId = autorId, categoryId = categoryId, publicationDate = new DateOnly(2023, 1, 1), price = 19.99M, stockQuantity = 100 },
                    new Book { title = "Book 3", authorId = autorId, categoryId = categoryId, publicationDate = new DateOnly(2023, 1, 1), price = 19.99M, stockQuantity = 100 },
                    new Book { title = "Book 4", authorId = autorId, categoryId = categoryId, publicationDate = new DateOnly(2023, 2, 1), price = 29.99M, stockQuantity = 100 }
                );
                context.SaveChanges();
            }

        }
    }
}
