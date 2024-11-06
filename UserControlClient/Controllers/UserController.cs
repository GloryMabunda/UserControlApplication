using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserControlAPI.Models;

namespace UserControlClient.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index(string msg = null, string  msgType = null)
        {
            try
            {
                var users = new List<Users>();
                var x = await _httpClient.GetAsync("https://localhost:7055/api/users");
                x.EnsureSuccessStatusCode(); 
                if (x.IsSuccessStatusCode)
                {
                    string data = await x.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<Users>>(data);
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    ViewBag.Msg = msg;  
                    ViewBag.MsgType = string.IsNullOrEmpty(msgType) ? "info" : msgType;  
                }
                return View(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = new Users();
            var x = await _httpClient.GetAsync($"https://localhost:7055/api/users/{id}");
            x.EnsureSuccessStatusCode();
            if (x.IsSuccessStatusCode)
            {
                string data = await x.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Users>(data);
            }
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
                var x = await _httpClient.PostAsJsonAsync("https://localhost:7055/api/users", user);
                if (x.IsSuccessStatusCode)
                {
                    var msg = await x.Content.ReadAsStringAsync();
                    return RedirectToAction(nameof(Index), new {msg, msgType = "success"});
                }
            }
      
            return View(user);
        }

        // GET: Users/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Users>($"https://localhost:7055/api/users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Users user)
        {
            if (user.UsertId != user.UsertId) return BadRequest();

            if (ModelState.IsValid)
            {
                var x = await _httpClient.PutAsJsonAsync($"https://localhost:7055/api/users", user);
                if (x.IsSuccessStatusCode)
                {
                    var msg = await x.Content.ReadAsStringAsync();
                    return RedirectToAction(nameof(Index), new { msg, msgType = "success" });
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<Users>($"https://localhost:7055/api/users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var x = await _httpClient.DeleteAsync($"https://localhost:7055/api/users/{id}");
            if (x.IsSuccessStatusCode)
            {
                var msg = await x.Content.ReadAsStringAsync();
                return RedirectToAction(nameof(Index), new { msg, msgType = "success" });
            }
            return View();
        }
    }
}
