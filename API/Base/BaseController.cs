using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Base
{
	public class BaseController<Entity, Repository, Key> : ControllerBase
		where Entity : class
		where Repository : IRepository<Entity, Key>
	{
		private readonly Repository repository;
		public BaseController(Repository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public ActionResult<Entity> Get()
		{
			var result = repository.Get();
			return Ok(result);
		}

		[HttpGet("{Key}")]
		public ActionResult<Entity> Get(Key key)
		{
			var result = repository.Get(key);
			return Ok(result);
		}

		[HttpPost]
		public ActionResult<Entity> Post(Entity entity)
		{
			var result = repository.Insert(entity);
			return Ok(result);
		}

		[HttpPut("{Key}")]
		public ActionResult<Entity> Update(Entity entity, Key key)
		{
			var result = repository.Update(entity, key);
			return Ok(result);
		}

		[HttpDelete("{Key}")]
		public ActionResult<Entity> Delete(Key key)
		{
			var result = repository.Delete(key);
			return Ok(result);
		}
	}
}
