using DEINT_REST_Demo.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DEINT_REST_Demo.MVVM.ViewModels
{
    public class RESTViewModel
    {
        public ICommand GetAllUsersCommand { get; set; }
        public ICommand GetUserCommand { get; set; }
        public ICommand InsertUserCommand { get; set; }
        public ICommand UpdateUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        
        private HttpClient client;
        private JsonSerializerOptions _serializerOptions;

        private List<User> _users;

        public RESTViewModel()
        {
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions {
                WriteIndented = true
            };

            string urlbase = "https://63e0b96d65b57fe606481285.mockapi.io";

            GetAllUsersCommand = new Command(async () => {
                string url = $"{urlbase}/users";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadAsStringAsync();

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var data = await JsonSerializer.DeserializeAsync<List<User>>(responseStream, _serializerOptions);
                        _users = data;
                    }
                }
            });

            GetUserCommand = new Command(async () => {
                string url = $"{urlbase}/users/25";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadAsStringAsync();

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var data = await JsonSerializer.DeserializeAsync<User>(responseStream, _serializerOptions);
                    }
                }
            });

            InsertUserCommand = new Command(async () => {
                string url = $"{urlbase}/users";
                var user = new User {
                    createdAt = DateTime.Now,
                    name = "Pepe",
                    avatar = "prueba"
                };

                string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
                StringContent content = new StringContent(json);
                var response = await client.PostAsync(url, content);
            });

            UpdateUserCommand = new Command(async () => {
                var user = _users.FirstOrDefault(x => x.id.Equals("10"));
                var url = $"{urlbase}/users/10";
                user.name = "Silvia";
                string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
                StringContent content = new StringContent(json);
                var response = await client.PutAsync(url, content);
            });

            DeleteUserCommand = new Command(async () => {
                var url = $"{urlbase}/users/15";
                var response = await client.DeleteAsync(url);
            });
        }
    }
}
