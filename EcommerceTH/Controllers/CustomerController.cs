using EcommerceTH.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceTH.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EcommerceContext _db;

        public CustomerController(EcommerceContext context)
        {
            _db = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _db.Customers.ToListAsync();
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _db.Customers
                .FirstOrDefaultAsync(m => m.Idcus == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewBag.RoleList = new List<SelectListItem>
    {
        new SelectListItem { Value = "Admin", Text = "Admin" },
        new SelectListItem { Value = "User", Text = "User" },
        new SelectListItem { Value = "Manager", Text = "Manager" }
    };

            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameCus,PhoneCus,EmailCus,Password,Address,Point,Viplevel,Role")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Add(customer);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate roles in case of validation errors
            ViewBag.RoleList = new List<SelectListItem>
    {
        new SelectListItem { Value = "Admin", Text = "Admin" },
        new SelectListItem { Value = "User", Text = "User" },
        new SelectListItem { Value = "Manager", Text = "Manager" }
    };

            return View(customer);
        }
        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _db.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            // Populate Role dropdown
            ViewBag.RoleList = new List<SelectListItem>
    {
        new SelectListItem { Value = "Admin", Text = "Admin" },
        new SelectListItem { Value = "User", Text = "User" },
        new SelectListItem { Value = "Manager", Text = "Manager" }
    };

            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idcus,NameCus,PhoneCus,EmailCus,Password,Address,Point,Viplevel,Role")] Customer customer)
        {
            if (id != customer.Idcus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(customer);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Idcus))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Repopulate Role dropdown in case of validation errors
            ViewBag.RoleList = new List<SelectListItem>
    {
        new SelectListItem { Value = "Admin", Text = "Admin" },
        new SelectListItem { Value = "User", Text = "User" },
        new SelectListItem { Value = "Manager", Text = "Manager" }
    };

            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _db.Customers
                .FirstOrDefaultAsync(m => m.Idcus == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _db.Customers.Any(e => e.Idcus == id);
        }
    }
}