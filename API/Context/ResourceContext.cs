using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
	public class ResourceContext : DbContext
	{
		public ResourceContext(DbContextOptions<ResourceContext> options) : base(options)
		{

		}

		public DbSet<Account> Accounts { get; set; }
		public DbSet<AccountRole> AccountRoles { get; set; }
		public DbSet<Interview> Interviews { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<SkillHandler> SkillHandlers { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Role with AccRole
			modelBuilder.Entity<Role>()
				.HasMany(r => r.AccountRole)
				.WithOne(a => a.Role);
			// Account with AccRole
			modelBuilder.Entity<Account>()
				.HasMany(a => a.AccountRole)
				.WithOne(r => r.Account);
			// User with Account
			modelBuilder.Entity<User>()
				.HasOne(u => u.Account)
				.WithOne(a => a.User);
			// SkillHandler with User

			// Convert User - Gender Enum
			modelBuilder.Entity<User>()
				.Property(o => o.Gender)
				.HasConversion<string>();
			// Convert User - Status Enum
			modelBuilder.Entity<User>()
				.Property(o => o.User_Status)
				.HasConversion<string>();
		}
	}
}
