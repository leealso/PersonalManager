using PersonalManager.Domain.Entities;
using PersonalManager.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManager.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "Groceries",
                    IconUrl = "https://cdn.iconscout.com/icon/premium/png-256-thumb/chicken-bucket-1689445-1435488.png",
                    ParentId = null
                });
                context.Categories.Add(new Category
                {
                    Id = 2,
                    Name = "Bread",
                    IconUrl = "https://vectorified.com/images/grocery-icon-21.png",
                    ParentId = 1
                });
                context.Categories.Add(new Category
                {
                    Id = 3,
                    Name = "Milk",
                    IconUrl = "https://cdn.iconscout.com/icon/premium/png-256-thumb/food-waste-2130231-1793726.png",
                    ParentId = 1
                });
                context.Categories.Add(new Category
                {
                    Id = 4,
                    Name = "Coffee",
                    IconUrl = "https://cdn.iconscout.com/icon/premium/png-256-thumb/consumer-1660070-1408779.png",
                    ParentId = 1
                });

                context.Categories.Add(new Category
                {
                    Id = 5,
                    Name = "Entertainment",
                    IconUrl = "https://cdn.iconscout.com/icon/premium/png-256-thumb/meat-162-916382.png",
                    ParentId = null
                });
                context.Categories.Add(new Category
                {
                    Id = 6,
                    Name = "Movies",
                    IconUrl = "https://cdn.iconscout.com/icon/premium/png-256-thumb/meat-162-916382.png",
                    ParentId = 5
                });
                context.Categories.Add(new Category
                {
                    Id = 7,
                    Name = "Games",
                    IconUrl = "https://vectorified.com/images/grocery-icon-21.png",
                    ParentId = 5
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
