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
    public class ProfileRepository : GeneralRepository<GetProfileVM, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public ProfileRepository(Address address, string request = "Users/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }

        public async Task<List<GetProfileVM>> GetProfileClient()
        {

            List<GetProfileVM> entities1 = new List<GetProfileVM>();
            using (var response = await httpClient.GetAsync(request + "ClientProfile/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities1 = JsonConvert.DeserializeObject<List<GetProfileVM>>(apiResponse);
            }
            return entities1;
        }
        public async Task<List<GetProfileVM>> GetProfileTrainer()
        {

            List<GetProfileVM> entities1 = new List<GetProfileVM>();
            using (var response = await httpClient.GetAsync(request + "TrainerProfile/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities1 = JsonConvert.DeserializeObject<List<GetProfileVM>>(apiResponse);
            }
            return entities1;
        }
        public async Task<List<GetProfileVM>> GetProfileCandidate()
        {

            List<GetProfileVM> entities1 = new List<GetProfileVM>();
            using (var response = await httpClient.GetAsync(request + "CandidateProfile/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities1 = JsonConvert.DeserializeObject<List<GetProfileVM>>(apiResponse);
            }
            return entities1;
        }
        public async Task<List<GetProfileVM>> GetProfileUser(string key)
        {
            List<GetProfileVM> profile = new List<GetProfileVM>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(key), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(address.Link + request + "Profile/" + key, content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            profile = JsonConvert.DeserializeObject<List<GetProfileVM>>(apiResponse);

            return profile;
        }

    }

}
