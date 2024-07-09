using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
          new Category { CategoryId = 1, Name = "Technology and Science", Description = "Software development, AI, machine learning, gaming", Color = "#3498db" },
          new Category { CategoryId = 2, Name = "Health and Lifestyle", Description = "Nutrition, fitness, mental health", Color = "#27ae60" },
          new Category { CategoryId = 3, Name = "Fashion and Beauty", Description = "Fashion trends, makeup tips, personal care", Color = "#f39c12" },
          new Category { CategoryId = 4, Name = "Travel and Adventure", Description = "Travel guides, outdoor activities, cultural experiences", Color = "#e74c3c" },
          new Category { CategoryId = 5, Name = "Food and Drink", Description = "Recipes, restaurant reviews, beverages", Color = "#9b59b6" },
          new Category { CategoryId = 6, Name = "Finance and Investment", Description = "Personal finance, stock market, cryptocurrencies", Color = "#2c3e50" },
          new Category { CategoryId = 7, Name = "Art and Culture", Description = "Art exhibitions, film reviews, literature", Color = "#e67e22" }
            );

        }
    }
}