using BlogApp1.Entity;
using BlogApp1.Models;
using BlogApp1.Data.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BlogApp1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Register - GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register - POST
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı var mı diye kontrol et
                var existingUser = await _userRepository.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Bu e-posta adresi zaten kayıtlı.");
                    return View(model);
                }

                // Şifreyi hashleyerek kaydediyoruz
                string hashedPassword = HashPassword(model.Password);

                var newUser = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = hashedPassword,
                    
                };

                // Kullanıcıyı kaydediyoruz
                _userRepository.CreateUser(newUser);
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // Login - GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login - POST
        [HttpPost]
public async Task<IActionResult> Login(LoginViewModel model)
{
    if (ModelState.IsValid)
    {
        // Sadece email'e göre kullanıcıyı bul
        var user = await _userRepository.Users
            .FirstOrDefaultAsync(u => u.UserName == model.UserName);

        if (user == null)
        {
            ModelState.AddModelError("", "Kullanıcı bulunamadı.");
            return View(model);
        }

        // Şifreyi kontrol et
        if (!VerifyPassword(model.Password, user.Password))
        {
            ModelState.AddModelError("", "Şifre yanlış.");
            return View(model);
        }

        // Giriş başarılıysa kullanıcıyı giriş yapmış say
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );

        return RedirectToAction("RegisteredUser", "Home");
    }

    return View(model);
}


       
        // Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // Şifreyi hashlemek için basit bir yöntem
        private string HashPassword(string password)
        {
            // Basit şifre hashleme (güvenlik için daha iyi bir algoritma kullanabilirsiniz)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        // Şifreyi doğrulamak için basit bir yöntem
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            var inputPasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(inputPassword));
            return inputPasswordHash == storedPasswordHash;
        }

    }
}
