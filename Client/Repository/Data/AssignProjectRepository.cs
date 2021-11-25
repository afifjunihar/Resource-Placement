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
    public class AssignProjectRepository : GeneralRepository<InterviewVM, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public AssignProjectRepository(Address address, string request = "Interviews/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }
        public HttpStatusCode AssignProject(InterviewVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "Assign/", content).Result;
            return result.StatusCode;
        }

    }
}
