using API.Models;
using API.Models.ViewModels;
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
   public class AccountRepository : GeneralRepository<Account, int>
   {
      private readonly Address _address;
      private readonly string _request;
      private readonly HttpClient httpClient;

      public AccountRepository(Address address, string request = "Accounts/") : base(address, request)
		{
         this._address = address;
         this._request = request;
         httpClient = new HttpClient
         {
            BaseAddress = new Uri(address.Link)
         };
      }

      //get jwtoken from login
      public async Task<JWTokenVM> Auth(LoginVM login)
      {
         JWTokenVM token = null;

         StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
         var result = await httpClient.PostAsync(_request + "Login", content);

         string apiResponse = await result.Content.ReadAsStringAsync();
         token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

         return token;
      }
   }
}
