using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using UserControlAPI.Models;

namespace UserControlClient.Controllers
{
    public class GroupController : Controller
    {
        private readonly HttpClient _httpClient;

        public GroupController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: GroupController
        public async Task<IActionResult> Index()
        {
            var list = new List<Groups>();
            var x = await _httpClient.GetAsync("https://localhost:7055/api/users/GetGroups");
            x.EnsureSuccessStatusCode();
            if (x.IsSuccessStatusCode)
            {
                string data = await x.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Groups>>(data);
            }
            return View(list);
        }

        // GET: GroupController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GroupController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GroupController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
