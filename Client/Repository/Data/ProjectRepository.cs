using API.Models;
using Client.Base.Urls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
	public class ProjectRepository : GeneralRepository<Project, int>
	{
		private readonly Address address;
		private readonly string request;
		private readonly HttpClient httpClient;
		public ProjectRepository(Address address, string request = "Projects/") : base(address, request)
		{
			this.address = address;
			this.request = request;
			httpClient = new HttpClient
			{
				BaseAddress = new Uri(address.Link)
			};
		}

		public async Task<Object> Details(string Id)
		{
			Object entities = null;

			using (var response = await httpClient.GetAsync(request + "Details/" + Id))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entities = JsonConvert.DeserializeObject<Object>(apiResponse);
			}
			return entities;
		}

		public async Task<List<Object>> Handler(string id)
		{
			List<Object> entities = new List<Object>();

			using (var response = await httpClient.GetAsync(request + "Handler/" + id))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entities = JsonConvert.DeserializeObject<List<Object>>(apiResponse);
			}
			return entities;
		}

	}
}
