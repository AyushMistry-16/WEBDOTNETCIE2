using System.Text.Json;
using HospitalAppointmentMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentMVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly HttpClient _httpClient;

        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            string url =
                "https://jsonplaceholder.typicode.com/posts";

            try
            {
                var response =
                    await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return View(new List<Post>());
                }

                var json =
                    await response.Content
                        .ReadAsStringAsync();

                var posts =
                    JsonSerializer.Deserialize<List<Post>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );

                return View(
                    posts ?? new List<Post>()
                );
            }
            catch
            {
                return View(new List<Post>());
            }
        }
    }
}