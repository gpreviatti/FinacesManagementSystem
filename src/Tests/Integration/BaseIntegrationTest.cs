using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Application;
using AutoMapper;
using CrossCutting.Mappings;
using Data.Context;
using Domain.Dtos;
using Domain.Dtos.Login;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Tests.Integration
{
    public abstract class BaseIntegrationTest
    {
        protected readonly IMapper _mapper;
        private static HttpClient _client;
        private static string _hostApi;
        protected readonly MyContext _myContext;

        public BaseIntegrationTest()
        {
            _hostApi = "http://localhost/";

            var builder = new WebHostBuilder()
               .UseEnvironment("Testing")
               .UseStartup<Startup>();

            var server = new TestServer(builder);

            _myContext = (MyContext) server.Host.Services.GetService(typeof(MyContext));
            _myContext.Database.Migrate();

            _mapper = GetMapper();

            _client = server.CreateClient();
        }

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            return config.CreateMapper();
        }

        #region "Set Api Communication"
        public async Task AdicionarToken()
        {
            var loginDto = new LoginDto()
            {
                Email = "admin@admin.com",
                Password = "mudar@123"
            };

            var resultLogin = await PostAsync(loginDto, "login");
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResultDto>(jsonLogin);

            // Add default authorization in each request
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                loginObject.AccessToken
            );
        }

        public static async Task<HttpResponseMessage> GetAsync(string url) => await _client.GetAsync(_hostApi + url);

        public static async Task<HttpResponseMessage> PostAsync(object dataclass, string url)
        {
            return await _client.PostAsync(
                _hostApi + url,
                new StringContent(JsonConvert.SerializeObject(dataclass), Encoding.UTF8, "application/json")
            );
        }

        public static async Task<HttpResponseMessage> PutAsync(object dataclass, string url)
        {
            return await _client.PutAsync(
                _hostApi + url,
                new StringContent(JsonConvert.SerializeObject(dataclass), Encoding.UTF8, "application/json")
            );
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string url) => await _client.DeleteAsync(_hostApi + url);

        #endregion
    }
}
