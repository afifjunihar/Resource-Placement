using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {
      Task<List<Entity>> Get();
      Task<Entity> Get(Key id);
      HttpStatusCode Post(Entity entity);
      HttpStatusCode Put(Key id, Entity entity);
      HttpStatusCode Delete(Key id);
   }
}
