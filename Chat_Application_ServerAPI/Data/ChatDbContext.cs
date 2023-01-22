using Chat_Application_ServerAPI.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application_ServerAPI.Data;

public class ChatDbContext : IdentityDbContext<ApplicationUser>
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Message> Messages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<IdentityRole>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Entity<ApplicationUser>()

            .Ignore(c => c.AccessFailedCount)
            .Ignore(c => c.LockoutEnabled)
            .Ignore(c => c.TwoFactorEnabled)
            .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.EmailConfirmed)
            .Ignore(c => c.TwoFactorEnabled)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.AccessFailedCount)
            .Ignore(c => c.PhoneNumberConfirmed)
            .Ignore(c => c.Email)
            .Ignore(c => c.NormalizedEmail);
            




        modelBuilder.Entity<ApplicationUser>().ToTable("Users");//to change the name of table.

    }
}

