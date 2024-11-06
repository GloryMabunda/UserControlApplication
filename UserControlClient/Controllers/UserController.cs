using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using UserControlAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserControlClient.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var users = new List<Users>();
            var user = await _httpClient.GetFromJsonAsync<List<Users>>($"http://localhost:7055/api/users");
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Users>($"http://localhost:7055/api/users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:7055/api/users", user);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Users>($"http://localhost:7055/api/users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Users user)
        {
            if (id != user.UsertId) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"http://localhost:7055/api/users/{id}", user);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Users>($"http://localhost:7055/api/users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:7055/api/users/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View();
        }
    }
}
