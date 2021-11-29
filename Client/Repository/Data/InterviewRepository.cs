using API.Models;
using API.Models.ViewModels;
using Client.Base.Urls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
	public class InterviewRepository : GeneralRepository<Interview, int>
	{
		private readonly Address _address;
		private readonly string _request;
		private readonly HttpClient httpClient;

		public InterviewRepository(Address address, string request = "interviews/") : base(address, request)
		{
			this._address = address;
			this._request = request;
			httpClient = new HttpClient
			{
				BaseAddress = new Uri(address.Link)
			};
		}

      public HttpStatusCode Accept(KeyVM entity)
      {
         StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
         var result = httpClient.PatchAsync(_request + "accept/", content).Result;
         return result.StatusCode;
      }
      public HttpStatusCode Reject(KeyVM entity)
      {
         StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
         var result = httpClient.PatchAsync(_request + "reject/", content).Result;
         return result.StatusCode;
      }

      public HttpStatusCode Assign(KeyVM entity)
      {
         StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
         var result = httpClient.PostAsync(_request + "assign/", content).Result;
         return result.StatusCode;
      }

		public async Task<Object> Current(string id)
		{
			Object entity = null;

			using (var response = await httpClient.GetAsync(_request + "current/" + id))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entity = JsonConvert.DeserializeObject<object>(apiResponse);
			}
			return entity;
		}

		public async Task<List<Object>> History(string id)
		{
			List<Object> entities = new List<Object>();

			using (var response = await httpClient.GetAsync(_request + "history/" + id))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				entities = JsonConvert.DeserializeObject<List<Object>>(apiResponse);
			}
			return entities;
		}
	}
}
