using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnackisAPI.Data.Entities;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Data
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<SiteContent> SiteContent { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            const string adminId = "admin-c0-aa65-4af8-bd17-00bd9344e575";
            const string rootId = "root-0c0-aa65-4af8-bd17-00bd9344e575";
            const string UserRoleId = "user-2c0-aa65-4af8-bd17-00bd9344e575";

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = rootId,
                Name = "root",
                NormalizedName = "ROOT"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = UserRoleId,
                Name = "user",
                NormalizedName = "USER"
            });

            builder.Entity<SiteContent>().HasData(new SiteContent
            {
                Id = Guid.NewGuid().ToString(),
                Title = "MyTitle"
            });


            var hasher = new PasswordHasher<User>();

            builder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@core.api",
                NormalizedEmail = "ADMIN@CORE.API",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "AdminPass1!"),
                SecurityStamp = Guid.NewGuid().ToString(),
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = rootId,
                UserId = adminId
            });
        }
    }
}
