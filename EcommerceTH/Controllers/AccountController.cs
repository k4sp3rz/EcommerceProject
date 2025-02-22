using EcommerceTH.data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceTH.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcommerceContext db;

        public AccountController(EcommerceContext context)
        {
            db = context;
        }
        // GET: Account/Register
        public IActionResult Register()
        {
            Console.WriteLine(User.Identity.IsAuthenticated); // Should print TRUE
            Console.WriteLine(User.IsInRole("Admin")); // Should print TRUE if role is stored correctly
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string phone, string address, string password)
        {
            // Check if email already exists
            if (db.Customers.Any(c => c.EmailCus == email))
            {
                ViewBag.ErrorMessage = "Email already registered!";
                return View();
            }

            // Create a new customer
            var newUser = new Customer
            {
                NameCus = name,
                EmailCus = email,
                PhoneCus = phone,
                Address = address,
                Password = password,
                Role = "User",
                Viplevel = "Standard",
                Point = 0
            };

            db.Customers.Add(newUser);
            db.SaveChanges();

            // Auto-login after registration
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, newUser.NameCus),
                new Claim(ClaimTypes.Email, newUser.EmailCus),
                new Claim(ClaimTypes.Role, newUser.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = false };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);

            return RedirectToAction("Index", "Home");
        }
        // GET: Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = db.Customers.SingleOrDefault(c => c.EmailCus == email && c.Password == password);

            if (user != null)
            {
                var role = user.Role.Trim();
                var claims = new List<Claim>
                {   new Claim(ClaimTypes.Name, user.NameCus),
                    new Claim(ClaimTypes.Email, user.EmailCus),
                    new Claim(ClaimTypes.Role, role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity),
                                              authProperties);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid Email or Password!";
            return View();
        }
        [Authorize]
        public IActionResult Details()
        {
            var user = HttpContext.User;
            if (user.Identity?.IsAuthenticated == true)
            {
                var model = new
                {
                    Name = user.Identity.Name,
                    Email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    Role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
                };

                return View(model);
            }

            return RedirectToAction("Login");
        }
        // Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
