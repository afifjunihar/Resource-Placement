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
    public class SkillRepository : GeneralRepository<AddSkillVM, string>
    {

        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        public SkillRepository(Address address, string request = "Skills/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }
        public async Task<List<GetListSkillVM>> GetListSkill()
        {

            List<GetListSkillVM> entities1 = new List<GetListSkillVM>();
            using (var response = await httpClient.GetAsync(address.Link + "users/Profile/ListSkill"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities1 = JsonConvert.DeserializeObject<List<GetListSkillVM>>(apiResponse);
            }
            return entities1;
        }

        public async Task<List<GetSkillVM>> GetSkill(string Id)
        {
            List<GetSkillVM> entities1 = new List<GetSkillVM>();

            using (var response = await httpClient.GetAsync(address.Link + "users/Profile/Skill/" + Id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities1 = JsonConvert.DeserializeObject<List<GetSkillVM>>(apiResponse);
            }
            return entities1;
        }

        public HttpStatusCode AddSkill(AddSkillVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "Add/", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode UpdateSkill(AddSkillVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "Update/", content).Result;
            return result.StatusCode;
        }

    }



}
