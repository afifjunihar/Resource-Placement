using API.Models;
using Client.Base.Urls;
using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}
