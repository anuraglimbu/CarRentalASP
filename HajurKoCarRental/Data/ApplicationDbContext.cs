using HajurKoCarRental.Areas.Identity.Data;
using HajurKoCarRental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Xml.Linq;

namespace HajurKoCarRental.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
#nullable enable
		private readonly IWebHostEnvironment _appEnvironment; // in order to access the wwwroot folder for static files
#nullable disable

		public DbSet<VehicleType> Types { get; set; }

		public DbSet<VehicleSubType> SubTypes { get; set; }

		public DbSet<Brands> Brands { get; set; }

		public DbSet<Inventory> Inventory { get; set; }

		public DbSet<Request> Requests { get; set; }

		public DbSet<Damage> Damages { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IWebHostEnvironment appEnvironment)
			: base(options)
		{
			_appEnvironment = appEnvironment;// making wwwroot path available class wide
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Calling seed functions in appropriate order to avoid error
			SeedRoles(builder);
			SeedUsersAndRoles(builder);
			SeedTypes(builder);
			SeedSubTypes(builder);
			SeedBrands(builder);
		}

		// We have to create an admin, staff and user default users whenever the app is launched first time
		// Thus these functions are called Seed functions
		private void SeedRoles(ModelBuilder builder) // Function to seed roles
		{
			// Seeding the 'Administrator','Staff' and 'User' roles in the database to AspNetRoles table
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole()
				{
					Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
					Name = "Admin",
					ConcurrencyStamp = "1",
					NormalizedName = "ADMIN"
				},
				new IdentityRole()
				{
					Id = "ade6f4e6-71b8-4afa-9e39-15115e370333",
					Name = "Staff",
					ConcurrencyStamp = "2",
					NormalizedName = "STAFF"
				},
				new IdentityRole()
				{
					Id = "52264fb1-4ce1-d839-d597-213b6ae2b08e",
					Name = "User",
					ConcurrencyStamp = "3",
					NormalizedName = "USER"
				}
			);
		}

		private void SeedUsersAndRoles(ModelBuilder builder)
		{
			var adminData = new[] {
				new
				{
					guid = "8e445865-a24d-4543-a6c6-9443d048cdb9", // Primary Key
					firstname = "John",
					lastname = "Doe",
					email = "admin@carrental.com",
					phone = "(04) 1939 1487",
					address = "249-2519 Morbi Rd.",
				},
				new
				{
					guid = "280b32ae-81fe-e6d6-6690-a6d573352b63",
					firstname = "Zia",
					lastname = "Benjamin",
					email = "ultrices@google.couk",
					phone = "(01) 2429 6578",
					address = "393-2886 Felis Av."
				}
			};
			var staffData = new[]
			{
				new
				{
					guid = "cc2dde8a-a19b-4c94-bbc1-f0007f06cc22",
					firstname = "Belle",
					lastname = "Marsh",
					email = "staff@carrental.com",
					phone = "(04) 6266 7210",
					address = "394-9313 Ut Street"
				},
				new
				{
					guid = "6693a7cb-9cfa-8619-2627-437d832fb9c6",
					firstname = "Levi",
					lastname = "Mendoza",
					email = "levimendoza@aol.ca",
					phone = "(06) 4093 6586",
					address = "Ap #310-4679 Convallis Avenue"
				}
			};
			var userData = new[]
			{
				new
				{
					guid = "34fdd202-6cb4-3bea-3537-37d5147dc812",
					firstname = "Kaseem",
					lastname = "Monroe",
					email = "user@carrental.com",
					phone = "(09) 8163 2680",
					address = "7540 Porttitor Street"
				},
				new {
					guid = "a4581e92-25c7-ad88-f114-2be0fb5caeb7",
					firstname = "Chester",
					lastname = " Sharp",
					email = "lacus@google.net",
					phone = "(04) 2133 6587",
					address = "Ap #174-1881 Sed Avenue"
				}
			};

			// a hasher to hash the password before seeding the user to the db
			var hasher = new PasswordHasher<IdentityUser>();

			List<ApplicationUser> users = new List<ApplicationUser>();
			List<IdentityUserRole<String>> roles = new List<IdentityUserRole<String>>();

			var img = 1;

			foreach (var data in adminData) // insert Admin seed data
			{
				var pic = Image.FromFile(_appEnvironment.WebRootPath + "/images/profile_pictures/" + img.ToString() + ".png");

				ApplicationUser user = new ApplicationUser()
				{
					Id = data.guid,
					UserName = data.email,
					NormalizedUserName = data.email.ToUpper(),
					Email = data.email,
					NormalizedEmail = data.email.ToUpper(),
					PhoneNumber = data.phone,
					FirstName = data.firstname,
					LastName = data.lastname,
					Address = data.address,
					SecurityStamp = Guid.NewGuid().ToString(),
					Picture = (byte[])(new ImageConverter()).ConvertTo(pic, typeof(byte[]))!,
				};


				// Generating Password Hashes for secure password
				user.PasswordHash = hasher.HashPassword(user, "Admin@1234");

				users.Add(user);

				roles.Add(new IdentityUserRole<string>
				{
					RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
					UserId = data.guid,
				});

				img++;
			}

			foreach (var data in staffData) // insert Staff seed data
			{
				var pic = Image.FromFile(_appEnvironment.WebRootPath + "/images/profile_pictures/" + img.ToString() + ".png");

				ApplicationUser user = new ApplicationUser()
				{
					Id = data.guid,
					UserName = data.email,
					NormalizedUserName = data.email.ToUpper(),
					Email = data.email,
					NormalizedEmail = data.email.ToUpper(),
					PhoneNumber = data.phone,
					FirstName = data.firstname,
					LastName = data.lastname,
					Address = data.address,
					SecurityStamp = Guid.NewGuid().ToString(),
					Picture = (byte[])(new ImageConverter()).ConvertTo(pic, typeof(byte[]))!,
				};


				// Generating Password Hashes for secure password
				user.PasswordHash = hasher.HashPassword(user, "Staff@1234");

				users.Add(user);

				roles.Add(new IdentityUserRole<string>
				{
					RoleId = "ade6f4e6-71b8-4afa-9e39-15115e370333",
					UserId = data.guid,
				});

				img++;
			}

			foreach (var data in userData) // insert User seed data
			{
				var pic = Image.FromFile(_appEnvironment.WebRootPath + "/images/profile_pictures/" + img.ToString() + ".png");

				ApplicationUser user = new ApplicationUser()
				{
					Id = data.guid,
					UserName = data.email,
					NormalizedUserName = data.email.ToUpper(),
					Email = data.email,
					NormalizedEmail = data.email.ToUpper(),
					PhoneNumber = data.phone,
					FirstName = data.firstname,
					LastName = data.lastname,
					Address = data.address,
					SecurityStamp = Guid.NewGuid().ToString(),
					Picture = (byte[])(new ImageConverter()).ConvertTo(pic, typeof(byte[]))!,
				};


				// Generating Password Hashes for secure password
				user.PasswordHash = hasher.HashPassword(user, "User@1234");

				users.Add(user);

				roles.Add(new IdentityUserRole<string>
				{
					RoleId = "52264fb1-4ce1-d839-d597-213b6ae2b08e",
					UserId = data.guid,
				});

				img++;
			}

			// Seeding the Admin, Staff and User to AspNetUsers table
			builder.Entity<ApplicationUser>().HasData(users);

			// Seeding the Admin, Staff and User to AspNetUserRoles table
			builder.Entity<IdentityUserRole<String>>().HasData(roles);
		}

		private void SeedTypes(ModelBuilder builder)
		{
			var types = new[]
			{
				new
				{
					Id = 1,
					Name = "SUV"
				},
				new
				{
					Id = 2,
					Name = "Hatchback"
				},
				new
				{
					Id = 3,
					Name = "Sedan"
				},
				new
				{
					Id = 4,
					Name = "Coupe"
				},
				new
				{
					Id = 5,
					Name = "Van"
				},
			};

			builder.Entity<VehicleType>().HasData(types);
		}

		private void SeedSubTypes(ModelBuilder builder)
		{
			var subTypes = new[]
			{
				new
				{
					Id = 1,
					TypeId = 1,
					Name = "Compact crossover"
				},
				new
				{
					Id = 2,
					TypeId = 1,
					Name = "Crossover"
				},
				new
				{
					Id = 3,
					TypeId = 1,
					Name = "Mid-size"
				},
				new
				{
					Id = 4,
					TypeId = 1,
					Name = "Full-size"
				},
				new
				{
					Id = 5,
					TypeId = 3,
					Name = "Subcompact"
				},
				new
				{
					Id = 6,
					TypeId = 3,
					Name = "Compact"
				},
				new
				{
					Id = 7,
					TypeId = 3,
					Name = "Mid-size"
				},
				new
				{
					Id = 8,
					TypeId = 3,
					Name = "Full-size"
				},
			};

			builder.Entity<VehicleSubType>().HasData(subTypes);
		}

		private void SeedBrands(ModelBuilder builder)
		{
			var types = new[]
			{
				new
				{
					Id = 1,
					Name = "Toyota"
				},
				new
				{
					Id = 2,
					Name = "Honda"
				},
				new
				{
					Id = 3,
					Name = "Dodge"
				},
				new
				{
					Id = 4,
					Name = "Jeep"
				},
				new
				{
					Id = 5,
					Name = "Kia"
				},
				new {
					Id = 6,
					Name = "Luxury"
				},
			};

			builder.Entity<Brands>().HasData(types);
		}
	}
}