using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaginationAndSearch.Helpers;
using PaginationAndSearch.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static PaginationAndSearch.Helpers.DataTableHelper;

namespace PaginationAndSearch.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration iconfiguration)
        {
            _configuration = iconfiguration;
        }
        public async Task<DataTableHelper.Result<UserViewModel>> GetUsers(Parameters param)
        {
            string Baseurl = _configuration.GetValue<string>("API_URL");
            DataTableHelper.Result<UserViewModel> EmpInfo = new DataTableHelper.Result<UserViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                var json = JsonConvert.SerializeObject(param);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                //Sending request to find web api REST service resource User List using HttpClient               
                HttpResponseMessage Res = await client.PostAsync("user/GetUserList", data);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<DataTableHelper.Result<UserViewModel>>(EmpResponse);
                }
            }
            return EmpInfo;
        }
    }
}
