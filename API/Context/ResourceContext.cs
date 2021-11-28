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

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			// Role with AccRole
			//modelBuilder.Entity<AccountRole>()
			//	.HasKey(t => new { t.Account_Id, t.Role_Id });

			modelBuilder.Entity<AccountRole>()
				.HasOne(pt => pt.Account)
				.WithMany(p => p.AccountRole)
				.HasForeignKey(pt => pt.Account_Id);

			modelBuilder.Entity<AccountRole>()
				.HasOne(pt => pt.Role)
				.WithMany(p => p.AccountRole)
				.HasForeignKey(pt => pt.Role_Id);			
		
			// User with Account
			modelBuilder.Entity<User>()
				.HasOne(u => u.Account)
				.WithOne(a => a.User);

			//  User with SkillHandler
			modelBuilder.Entity<User>()
				.HasMany(s => s.SkillHandler)
				.WithOne(u => u.User);

			// User with Interview
			modelBuilder.Entity<User>()
				.HasMany(i => i.Interview)
				.WithOne(u => u.User);

			// User with project
			modelBuilder.Entity<User>()
				.HasMany(p => p.Project)
				.WithOne(u => u.User);

			// SkillHandler with Skill
			modelBuilder.Entity<Skill>()
				.HasMany(h => h.SkillHandler)
				.WithOne(s => s.Skill);

			// Project with Interview
			modelBuilder.Entity<Project>()
				.HasMany(i => i.Interview)
				.WithOne(p => p.Project);

			// Convert User - Gender Enum
			modelBuilder.Entity<User>()
				.Property(o => o.Gender)
				.HasConversion<string>();

			// Convert User - Status Enum
			modelBuilder.Entity<User>()
				.Property(o => o.User_Status)
				.HasConversion<string>();

			// Convert Interview - Status Enum
			modelBuilder.Entity<Interview>()
				.Property(o => o.Interview_Result)
				.HasConversion<string>();

			// Convert Project - Status Enum
			modelBuilder.Entity<Project>()
				.Property(o => o.Status)
				.HasConversion<string>();

		}
	}
}
