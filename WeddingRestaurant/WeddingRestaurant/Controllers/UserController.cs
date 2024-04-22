using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using WeddingRestaurant.Heplers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace WeddingRestaurant.Controllers
{
    public class UserController : Controller
    {
        private readonly ModelContext db;
        private readonly IMapper _mapper;

        public UserController(ModelContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserVM model, IFormFile Avatar)
        {
            if (model.Password == model.ConfirmPassword)
            {
                ModelState.Remove("Avatar");
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(model);
                    user.UserRole = "Admin";

                    user.Password = model.Password.ToMd5Hash();

                    if (Avatar != null)
                    {
                        user.Avatar = MyUtil.UploadHinh(Avatar, "User");
                    }

                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == model.UserName);

                if (user == null)
                {
                    ModelState.AddModelError("loi", "Không có khách hàng này");
                }
                else 
                {
                    if (user.Password != model.Password.ToMd5Hash())
                    {
                        ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.UserRole),
                            new Claim(Configuration.Claim_User_Id, user.Id.ToString()),
                        };

                        var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(claimsPrincipal);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
