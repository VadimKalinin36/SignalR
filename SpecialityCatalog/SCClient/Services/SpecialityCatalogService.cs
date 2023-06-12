using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SCClient.Models.Config;
using SCData.Models;
using System.Net;

namespace SCClient.Services
{
    public class SpecialityCatalogService
    {
        private readonly IOptions<SpecialityCatalogWebApiConfig> _scWebApiConfig;

        public SpecialityCatalogService (IOptions<SpecialityCatalogWebApiConfig> scWebApiConfig)
        {
            _scWebApiConfig = scWebApiConfig;
        }
        public async Task<bool> EditGroup(Group group)
        {
            return await EditItem("Group", group);

        }

        public async Task<bool> EditStudent(Student student)
        {
            return await EditItem("Student", student);  

        }

        public async Task<bool> EditDirection(Direction direction)
        {
            return await EditItem("Direction", direction);

        }

        public async Task<bool> EditItem<T>(string method, T item) where T: IItem
        {
            using var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(100) };


            try
            {
                var url = (string)_scWebApiConfig.Value.Endpoint + method + "/" + item.Id; 
                var response = await httpClient.PostAsJsonAsync(url, item);
                var responceData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    return true;
                }

            }

            catch { }

            return false;


        }

        public async Task<bool> AddGroup(Group group)
        {

            return await AddItem("Group", group);

        }

        public async Task<bool> AddStudent(Student student)
        {

            return await AddItem("Student", student);

        }

        public async Task<bool> AddDirection(Direction direction)
        {

            return await AddItem("Direction", direction);

        }

        public async Task<bool> AddItem<T>(string method, T item)
        {
            using var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };


            try
            {
                var response = await httpClient.PutAsJsonAsync((string)_scWebApiConfig.Value.Endpoint + method, item);
                var responceData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    return true;
                }

            }

            catch { }

            return false;


        }

        public async Task<bool> RemoveGroup(int id)
        {
            return await RemoveItem("Group", id);

        }

        public async Task<bool> RemoveStudent(int id)
        {
            return await RemoveItem("Student", id);

        }

        public async Task<bool> RemoveDirection(int id)
        {
            return await RemoveItem("Direction", id);

        }

        public async Task<bool> RemoveItem(string method, int id)
        {
            using var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };


            try
            {
                var response = await httpClient.DeleteAsync((string)_scWebApiConfig.Value.Endpoint + method + "/" + id);
                var responceData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    return true;
                }

            }

            catch { }

            return false;


        }

        public async Task<Group> GetGroup(int id)
        {
            return await GetItem<Group>("Group", id);

        }

        public async Task<Student> GetStudent(int id)
        {
            return await GetItem<Student>("Student", id);

        }

        public async Task<Direction> GetDirection(int id)
        {
            return await GetItem<Direction>("Direction", id);

        }

        public async Task<T> GetItem<T>(string method, int id) where T :class
        {
            using var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };


            try
            {
                var response = await httpClient.GetAsync(_scWebApiConfig.Value.Endpoint + method + "/" + id);
                var responceData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<T>(responceData);
                    return result;
                }

            }

            catch { }


            return null;

        }

        public async Task<List<Group>> GetGroups()
        {
            return await GetAll<Group>("Group");
        }

        public async Task<List<Student>> GetStudents()
        {
            return await GetAll<Student>("Student");
        }

        public async Task<List<Direction>> GetDirections()
        {
            return await GetAll<Direction>("Direction");
        }

        public async Task<List<T>> GetAll<T>(string method)
        {
            using var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };


            try
            {
                var response = await httpClient.GetAsync((string)_scWebApiConfig.Value.Endpoint + method);
                var responceData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = JsonConvert.DeserializeObject<List<T>>(responceData);
                    return result;
                }

            }

            catch { }


            return null;

        }



    }
}
