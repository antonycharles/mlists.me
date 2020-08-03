using me.mlists.domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.data.Data
{
    public class AppIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable(name: "mltb_users");
            builder.Entity<ApplicationUser>().Property(x => x.Nome).HasMaxLength(100).IsRequired();
            builder.Entity<ApplicationUser>().Property(x => x.Email).IsRequired();

            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("mltb_claims");
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("mltb_user_logins");
            });

            builder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("mltb_user_tokens");
            });

            builder.Entity<IdentityRole>(b =>
            {
                b.ToTable("mltb_roles");
            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("mltb_role_claims");
            });

            builder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("mltb_user_roles");
            });
        }
    }
}
