using Client.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Base
{
    public class BaseController<Entity, Repository, Key> : Controller
      where Entity : class
       where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var result = await repository.Get();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> Get(Key id)
        {
            var result = await repository.Get(id);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Post(Entity entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }

        [HttpPut]
        public JsonResult Put(Key id, Entity entity)
        {
            var result = repository.Put(id, entity);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult Delete(Key id)
        {
            var result = repository.Delete(id);
            return Json(result);
        }
    }
}