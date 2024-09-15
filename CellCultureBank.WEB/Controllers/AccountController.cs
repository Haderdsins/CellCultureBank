using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CellCultureBank.DAL.Database;
using CellCultureBank.DAL.Models;
using CellCultureBank.WEB.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace CellCultureBank.WEB.Controllers;

public class AccountController: TodoBaseController
    {
        private readonly BankDbContext _context;

        public AccountController(BankDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Вычисление хеш значения пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        /// <summary>
        /// Страница авторизации и регистрации
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new AccountViewModel
            {
                LoginViewModel = new LoginViewModel(),
                RegisterViewModel = new RegisterViewModel()
            });
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View("Index", new AccountViewModel
            {
                LoginViewModel = new LoginViewModel(),
                RegisterViewModel = new RegisterViewModel()
            });
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([Bind(Prefix = "l")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = model,
                    RegisterViewModel = new RegisterViewModel()
                });
            }

            // Проверка пользователя по логину и хешированному паролю
            if (model.Password != null)
            {
                var hashedPassword = HashPassword(model.Password);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.PasswordHash == hashedPassword);

                if (user is null)
                {
                    ViewBag.Error = "Некорректные логин и(или) пароль";
                    return View("Index", new AccountViewModel
                    {
                        LoginViewModel = model,
                        RegisterViewModel = new RegisterViewModel()
                    });
                }

                await AuthenticateAsync(user);
            }

            return RedirectToAction("Index", "Home");
        }



        private async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Index", new AccountViewModel
            {
                LoginViewModel = new LoginViewModel(),
                RegisterViewModel = new RegisterViewModel()
            });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([Bind(Prefix = "r")] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = new LoginViewModel(),
                    RegisterViewModel = model
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
            if (user != null)
            {
                ViewBag.RegisterError = "Пользователь с таким логином уже существует";
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = new LoginViewModel(),
                    RegisterViewModel = model
                });
            }

            // Создание нового пользователя с хешированным паролем
            user = new User(model.Login ?? string.Empty, HashPassword(model?.Password ?? string.Empty), model.FullName ?? string.Empty);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }