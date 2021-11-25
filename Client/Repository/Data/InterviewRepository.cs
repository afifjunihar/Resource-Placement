using API.Models.ViewModels;
using Client.Base.Urls;
using Client.Views.ViewModels;
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
    public class InterviewRepository : GeneralRepository<KeyVM, string>
    {

        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public InterviewRepository(Address address, string request = "Interviews/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }
        public HttpStatusCode Accept(KeyVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "Accept/", content).Result;
            return result.StatusCode;
        }
        public HttpStatusCode Reject(KeyVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "Reject/", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Application(KeyVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "Applicant/", content).Result;
            return result.StatusCode;
        }
    }
}
