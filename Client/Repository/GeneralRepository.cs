using Client.Base.Urls;
using Client.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository
{
	public class GeneralRepository<Entity, Key> : IRepository<Entity, Key>
	  where Entity : class
	{
		private readonly Address Address;
		private readonly string request;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly HttpClient httpClient;

		public GeneralRepository(Address address, string request)
		{
			this.Address = address;
			this.request = request;
			_contextAccessor = new HttpContextAccessor();
			httpClient = new HttpClient
			{
				BaseAddress = new Uri(address.Link)
			};
		}


		public HttpStatusCode Delete(Key id)
		{
			var result = httpClient.DeleteAsync(request + id).Result;
			return result.StatusCode;
		}

		public async Task<List<Entity>> Get()
		{
			List<Entity> entities = new List<Entity>();

			using (var response = await httpClient.GetAsync(request))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entities = JsonConvert.DeserializeObject<List<Entity>>(apiResponse);
			}
			return entities;
		}

		public async Task<Entity> Get(Key id)
		{
			Entity entity = null;

			using (var response = await httpClient.GetAsync(request + id))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entity = JsonConvert.DeserializeObject<Entity>(apiResponse);
			}
			return entity;
		}

		public HttpStatusCode Post(Entity entity)
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
			var result = httpClient.PostAsync(Address.Link + request, content).Result;
			return result.StatusCode;
		}

		public HttpStatusCode Put(Key id, Entity entity)
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
			var result = httpClient.PutAsync(request + id, content).Result;
			return result.StatusCode;
		}
	}
}