using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using UserControlAPI.Models;

using System.Net.Http;

namespace UserControlClient.Controllers
{
    public class GroupPermissionsController : Controller
    {
        private readonly HttpClient _httpClient;

        public GroupPermissionsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: GroupPermissionsController
        public async Task<IActionResult> Index()
        {
            var list = new List<GroupPermissions>();
            var x = await _httpClient.GetAsync("https://localhost:7055/api/users/GetGroupPermissions");
            x.EnsureSuccessStatusCode();
            if (x.IsSuccessStatusCode)
            {
                string data = await x.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<GroupPermissions>>(data);
            }
            return View(list);
        }

        // GET: GroupPermissionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GroupPermissionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupPermissionsController/Create
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

        // GET: GroupPermissionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GroupPermissionsController/Edit/5
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

        // GET: GroupPermissionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GroupPermissionsController/Delete/5
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
