using API.Models;
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
    public class RegisterRepository : GeneralRepository<RegisterVM, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public RegisterRepository(Address address, string request = "Users/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }
        public HttpStatusCode RegisterCandidate(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "Registration/Candidate/", content).Result;
            return result.StatusCode;
        }
        public HttpStatusCode RegisterTrainer(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "Registration/Trainer/", content).Result;
            return result.StatusCode;
        }
        public HttpStatusCode RegisterClient(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "Registration/Client/", content).Result;
            return result.StatusCode;
        }

        public async Task<List<GetProfileVM>> GetProfileUser(KeyVM key)
        {
            List<GetProfileVM> profile = new List<GetProfileVM>();
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(key), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(address.Link + request + "Profile/", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            profile = JsonConvert.DeserializeObject<List<GetProfileVM>>(apiResponse);

            return profile;
        }
      

    }

}
