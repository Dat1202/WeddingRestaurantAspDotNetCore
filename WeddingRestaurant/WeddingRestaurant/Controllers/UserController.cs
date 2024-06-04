using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using WeddingRestaurant.Heplers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared;

namespace WeddingRestaurant.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterVM> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(ModelContext context, IMapper mapper,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            ILogger<RegisterVM> logger,
            IUserStore<ApplicationUser> userStore,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper; 
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model, IFormFile Avatar)
        {
            ModelState.Remove("Avatar");

            if (ModelState.IsValid)
            {
                var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (existingUserByEmail != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                }
                var existingUserByName = await _userManager.FindByNameAsync(model.UserName);
                if (existingUserByName != null)
                {
                    ModelState.AddModelError("UserName", "Tên người dùng đã được sử dụng.");
                }
                var user = _mapper.Map<ApplicationUser>(model);
                user.EmailConfirmed = true;
                if (Avatar != null)
                {
                    user.Avatar = MyUtil.UploadHinh(Avatar, "User", user.Id);
                }
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync("User");
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");   
                }

                foreach (var error in result.Errors)
                {
                    if (error.Code == "InvalidUserName")
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    else if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    else if (error.Code == "InvalidEmail")
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    else if(error.Code == "PasswordTooShort")
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    else if (error.Code == "PasswordRequiresLower")
                    {
                        ModelState.AddModelError("Password", "Mật khẩu phải chứa ít nhất một chữ thường.");
                    }
                    else if (error.Code == "PasswordRequiresUpper")
                    {
                        ModelState.AddModelError("Password", "Mật khẩu phải chứa ít nhất một chữ hoa.");
                    }
                    else if (error.Code == "PasswordRequiresNonAlphanumeric")
                    {
                        ModelState.AddModelError("Password", "Mật khẩu phải chứa ít nhất một ký tự đặc biệt.");
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View("Login");

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserName", "Không có khách hàng này");
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email), 
                            new Claim("Avatar", user.Avatar),
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            if (await _userManager.IsInRoleAsync(user, "Admin"))
                            {
                                return RedirectToAction("Index", "Admin");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Đăng nhập không thành công");
                    }
                }
            }

            return View(model);
        }
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("Login");
        }
        public IActionResult External(string provider, string returnUrl = null)
        {
            var redirectUrl = $"/User/callbackexternal?handler=Callback&returnUrl={returnUrl}&remoteError=";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> CallbackExternal(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user != null)
                {
					var role = await _roleManager.FindByNameAsync("User");
					if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                        await _signInManager.SignInAsync(user, isPersistent: false);

                    }
                }
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                string email = string.Empty;

                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = info.Principal.FindFirstValue(ClaimTypes.Email);
                }

                var md = new ExternalLoginVM
                {
                    Provider = info.ProviderDisplayName,
                    Email = email
                };

                return View(md);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmationExternal(ExternalLoginVM externalLoginModel, string returnUrl = null)
        {
            ViewBag.returnUrl = returnUrl;
            //ViewBag.provider = externalLoginModel.Provider;
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                TempData["ErrorMessage"] = "Lỗi không liên kết được với dịch vụ ngoài";

                return RedirectToPage("Login", new { ReturnUrl = externalLoginModel.ReturnUrl });
            }

            if (ModelState.IsValid)
            {
                string externalEmail = null;
                ApplicationUser externalEmailUser = null;
                var registerUser = await _userManager.FindByEmailAsync(externalLoginModel.Email);

                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    externalEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
                }

                if(externalEmail != null)
                {
                    externalEmailUser = await _userManager.FindByEmailAsync(externalEmail);
                }

                if(externalEmailUser != null && registerUser != null)
                {
                    if(externalEmailUser.Id == registerUser.Id)
                    {
                        var resultLink = await _userManager.AddLoginAsync(registerUser, info);

                        if (resultLink.Succeeded)
                        {
                            await _signInManager.SignInAsync(registerUser, isPersistent: false);
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("", "Home");
                            }
                        }
                    }
                    else
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Không liên kết được tài khoản, hãy sử dụng email khác";
                            return RedirectToAction("CallbackExternal");
                        }
                    }
                }

                if (externalEmailUser != null && registerUser != null)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Không liên kết được hỗ trợ tạo tài khoản mới - có email khác với email từ dịch vụ ngoài";
                        return RedirectToAction("CallbackExternal");
                    }
                }

                if (externalEmailUser == null && externalEmail == externalLoginModel.Email)
                {
                    //var avatarUrl = info.Principal.FindFirstValue("picture");

                    var user = new ApplicationUser
                    {
                        Email = externalLoginModel.Email,
                        UserName = externalLoginModel.Email,
                    };
                    user.Avatar = "";
                    user.EmailConfirmed = true;
                    await _userStore.SetUserNameAsync(user, externalLoginModel.Email, CancellationToken.None);

                    var resultNewUser = await _userManager.CreateAsync(user);
                    if (resultNewUser.Succeeded)
                    {
                        resultNewUser = await _userManager.AddLoginAsync(user, info);
                        if (resultNewUser.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("CallbackExternal");
                            }
                        }
                    }
                }
                else
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Không liên kết được hỗ trợ tạo tài khoản mới - có email khác với email từ dịch vụ ngoài";
                        return RedirectToAction("CallbackExternal");
                    }
                }
            }
            return View("CallbackExternal");  
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

    }
}