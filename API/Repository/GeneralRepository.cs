using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
	public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
		where Entity : class
		where Context : ResourceContext
	{
		private readonly ResourceContext myContext;
		private readonly DbSet<Entity> entities;

		public GeneralRepository(ResourceContext myContext)
		{
			this.myContext = myContext;
			entities = myContext.Set<Entity>();
		}

		public IEnumerable<Entity> Get()
		{
			return entities.ToList();
		}

		public Entity Get(Key key)
		{
			return entities.Find(key);
		}

		public int Insert(Entity entity)
		{
			entities.Add(entity);
			var result = myContext.SaveChanges();
			return result;
		}

		public int Update(Entity entity, Key key)
		{
			myContext.Entry(entity).State = EntityState.Modified;
			var result = myContext.SaveChanges();
			return result;
		}
		public int Delete(Key key)
		{
			var deletedData = entities.Find(key);
			entities.Remove(deletedData);
			var result = myContext.SaveChanges();
			return result;
		}
	}
}
