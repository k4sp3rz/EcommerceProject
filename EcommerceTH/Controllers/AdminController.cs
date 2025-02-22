using EcommerceTH.data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceTH.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly EcommerceContext _db;
        public AdminController(EcommerceContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            var admins = _db.Customers.Where(c => c.Role == "Admin").ToList();
            return View(admins);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer admin)
        {
            if (ModelState.IsValid)
            {
                admin.Role = "Admin";
                _db.Customers.Add(admin);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        public IActionResult Edit(int id)
        {
            var admin = _db.Customers.Find(id);
            if (admin == null || admin.Role != "Admin") return NotFound();
            return View(admin);
        }

        [HttpPost]
        public IActionResult Edit(Customer admin)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Update(admin);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }
        public IActionResult Delete(int id)
        {
            var admin = _db.Customers.Find(id);
            if (admin == null || admin.Role != "Admin") return NotFound();
            return View(admin);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var admin = _db.Customers.Find(id);
            if (admin == null || admin.Role != "Admin") return NotFound();

            _db.Customers.Remove(admin);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}