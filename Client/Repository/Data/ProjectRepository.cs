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
    public class ProjectRepository : GeneralRepository<KeyVM, string>
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
        public async Task<List<GetListProjectAplicantVM>> GetProjectApplicant(KeyVM key)
        {
            List<GetListProjectAplicantVM> profile = new List<GetListProjectAplicantVM>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(key), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(address.Link + request + "ProjectApplicant/", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            profile = JsonConvert.DeserializeObject<List<GetListProjectAplicantVM>>(apiResponse);

            return profile;
        }

        public async Task<List<GetProjectVM>> Current()
        {
            List<GetProjectVM> entity = new List<GetProjectVM>();
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<List<GetProjectVM>>(apiResponse);
            }
            return entity;
        }

        public async Task<List<GetProjectVM>> openProject()
        {
            List<GetProjectVM> entity = new List<GetProjectVM>();
            using (var response = await httpClient.GetAsync(request + "OpenProject/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<List<GetProjectVM>>(apiResponse);
            }
            return entity;
        }

        public async Task<List<GetProjectVM>> closedProject()
        {
            List<GetProjectVM> entity = new List<GetProjectVM>();

            using (var response = await httpClient.GetAsync(request  + "ClosedProject/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<List<GetProjectVM>>(apiResponse);
            }
            return entity;
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
