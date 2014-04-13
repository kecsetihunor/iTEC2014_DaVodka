using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF
{
    public class iTECDbInitializer : DropCreateDatabaseIfModelChanges<ITECContext>
    {
        private static Random random = new Random(257);

        protected override void Seed(ITECContext context)
        {
            var profile = new Profile()
            {
                Email = "admin@admin.ro",
            };

            var profileUser1 = new Profile()
            {
                Email = "user1@user.ro",
            };

            var profileUser2 = new Profile()
            {
                Email = "user2@user.ro",
            };

            var profileUser3 = new Profile()
            {
                Email = "user3@user.ro",
            };

            context.Profiles.Add(profile);
            context.Profiles.Add(profileUser1);
            context.Profiles.Add(profileUser2);
            context.Profiles.Add(profileUser3);

            var userBaseGroup = new UserGroup()
            {
                Name = "BaseGroupForUsers",
                Points = 0
            };

            var userGroupDev = new UserGroup()
            {
                Name = "Developers",
                Points = 100
            };

            var userGroupManagers = new UserGroup()
            {
                Name = "Managers",
                Points = 300
            };

            context.UserGroups.Add(userBaseGroup);
            context.UserGroups.Add(userGroupDev);
            context.UserGroups.Add(userGroupManagers);

            var account = new Account()
            {
                Profile = profile,
                Remember = false,
                Role = Role.Administrator,
                UserGroup = userBaseGroup,
                Username = "admin",
                Password = "admin"
            };

            var accountDev1 = new Account()
            {
                Profile = profileUser1,
                Remember = false,
                Role = Role.User,
                UserGroup = userGroupDev,
                Username = "dev1",
                Password = "12345"
            };

            var accountDev2 = new Account()
            {
                Profile = profileUser2,
                Remember = false,
                Role = Role.User,
                UserGroup = userGroupDev,
                Username = "dev2",
                Password = "12345"
            };

            var accountDev3 = new Account()
            {
                Profile = profile,
                Remember = false,
                Role = Role.User,
                UserGroup = userGroupDev,
                Username = "dev3",
                Password = "12345"
            };

            var accountManager1 = new Account()
            {
                Profile = profileUser3,
                Remember = false,
                Role = Role.User,
                UserGroup = userGroupManagers,
                Username = "man1",
                Password = "12345"
            };

            var accountManager2 = new Account()
            {
                Profile = profile,
                Remember = false,
                Role = Role.User,
                UserGroup = userGroupManagers,
                Username = "man2",
                Password = "12345"
            };

            context.Accounts.Add(account);
            context.Accounts.Add(accountDev1);
            context.Accounts.Add(accountDev2);
            context.Accounts.Add(accountDev3);
            context.Accounts.Add(accountManager1);
            context.Accounts.Add(accountManager2);

            var baseCategory = new Category()
            {
                Name = "BaseCategoryForProducts"
            };

            var drinks = new Category()
            {
                Name = "Drinks"
            };

            var fruits = new Category()
            {
                Name = "Fruits"
            };

            var snacks = new Category()
            {
                Name = "Snacks"
            };

            var foods = new Category()
            {
                Name = "Foods"
            };

            context.Categories.Add(baseCategory);
            context.Categories.Add(drinks);
            context.Categories.Add(fruits);
            context.Categories.Add(snacks);
            context.Categories.Add(foods);

            var drinkProd1 = new Product()
            {
                Name = "Tea",
                Price = 5,
                Category = drinks
            };

            var drinkProd2 = new Product()
            {
                Name = "Coffee",
                Price = 30,
                Category = drinks
            };

            var drinkProd3 = new Product()
            {
                Name = "Milk",
                Price = 15,
                Category = drinks
            };

            var drinkProd4 = new Product()
            {
                Name = "Wine",
                Price = 50
            };

            context.Products.Add(drinkProd1);
            context.Products.Add(drinkProd2);
            context.Products.Add(drinkProd3);
            context.Products.Add(drinkProd4);

            var fruitsProd1 = new Product()
            {
                Name = "Apple",
                Price = 7,
                Category = fruits
            };

            var fruitsProd2 = new Product()
            {
                Name = "Pear",
                Price = 9,
                Category = fruits
            };

            var fruitsProd3 = new Product()
            {
                Name = "Grapes",
                Price = 14,
                Category = fruits
            };

            context.Products.Add(fruitsProd1);
            context.Products.Add(fruitsProd2);
            context.Products.Add(fruitsProd3);

            var snacks1 = new Product()
            {
                Name = "Biscuits",
                Price = 42,
                Category = snacks
            };

            var snacks2 = new Product()
            {
                Name = "Chocolate",
                Price = 48,
                Category = snacks
            };

            context.Products.Add(snacks1);
            context.Products.Add(snacks2);

            var foodProd1 = new Product()
            {
                Name = "Shaorma",
                Price = 36,
                Category = foods
            };

            var foodProd2 = new Product()
            {
                Name = "Pizza",
                Price = 75,
                Category = foods
            };

            var foodProd3 = new Product()
            {
                Name = "Pasta",
                Price = 48,
                Category = foods
            };

            context.Products.Add(foodProd1);
            context.Products.Add(foodProd2);
            context.Products.Add(foodProd3);

            for (int j = 0; j < 5; j++)
            {
                var session = new Session()
                {
                    StartDate = DateTime.Now
                };

                context.Sessions.Add(session);
                context.SaveChanges();

                for (int i = 0; i < 30; i++)
                {
                    int accId = random.Next(2, 6);
                    int prodId = random.Next(1, 11);

                    if (!(context.Votes.Any(x => x.Account.Id == accId && x.Product.Id == prodId)))
                    {
                        var acc = context.Accounts.ToList().ElementAt(accId);
                        var prod = context.Products.ToList().ElementAt(prodId);

                        var vote = new Vote()
                        {
                            Account = acc,
                            Product = prod,
                            Points = random.Next(0, 20),
                            Session = session
                        };
                        context.Votes.Add(vote);
                    }
                }

                session.EndDate = DateTime.Now;
                context.SaveChanges();
            }

            var sessionCurrent = new Session()
            {
                StartDate = DateTime.Now
            };

            context.Sessions.Add(sessionCurrent);
            context.SaveChanges();

            context.Messages.Add(new Message()
            {
                MessageBody = "Hello user!"
            });

            context.SaveChanges();
        }
    }

    public class ITECContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ITECContext()
            : base("DefaultConnection")
        {
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
