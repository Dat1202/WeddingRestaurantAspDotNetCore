﻿using AutoMapper;
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

namespace WeddingRestaurant.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ModelContext db;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterVM> _logger;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(ModelContext context, IMapper mapper,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            ILogger<RegisterVM> logger,
            IUserStore<ApplicationUser> userStore)
        {
            _userStore = userStore;
            _signInManager = signInManager;
            _userManager = userManager;
            db = context; 
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
                var user = _mapper.Map<ApplicationUser>(model);
                user.EmailConfirmed = true;
                if (Avatar != null)
                {
                    user.Avatar = MyUtil.UploadHinh(Avatar, "User");
                }
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
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
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    else if (error.Code == "PasswordRequiresLower")
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    else if (error.Code == "PasswordRequiresUpper")
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    else if (error.Code == "PasswordRequiresNonAlphanumeric")
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            //if (model.Password == model.ConfirmPassword)
            //{
            //    ModelState.Remove("Avatar");
            //    if (ModelState.IsValid)
            //    {
            //        var user = _mapper.Map<ApplicationUser>(model);

            //        user.PasswordHash = model.Password.ToMd5Hash();

            //        if (Avatar != null)
            //        {
            //            user.Avatar = MyUtil.UploadHinh(Avatar, "User");
            //        }

            //        db.Users.Add(user);
            //        await db.SaveChangesAsync();
            //        return RedirectToAction("Index", "Home");
            //    }
            //}
            return View();
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
                var user = db.Users.SingleOrDefault(u => u.UserName == model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("loi", "Không có khách hàng này");
                }
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                
            }
            else
            {

            }

            //if (ModelState.IsValid)
            //{
            //    var user = db.Users.SingleOrDefault(u => u.UserName == model.UserName);

            //    if (user == null)
            //    {
            //        ModelState.AddModelError("loi", "Không có khách hàng này");
            //    }
            //    else
            //    {
            //        if (user.PasswordHash != model.Password.ToMd5Hash())
            //        {
            //            ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
            //        }
            //        else
            //        {
            //            var claims = new List<Claim>
            //            {

            //                //new Claim(ClaimTypes.User, user.UserName),
            //                new Claim(ClaimTypes.Email, user.Email),
            //                //new Claim(ClaimTypes.Role, user.UserRole),
            //                new Claim(Configuration.Claim_User_Id, user.Id.ToString()),
            //            };

            //            var claimsIdentity = new ClaimsIdentity(claims,
            //            CookieAuthenticationDefaults.AuthenticationScheme);
            //            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //            await HttpContext.SignInAsync(claimsPrincipal);

            //            if (Url.IsLocalUrl(ReturnUrl))
            //            {
            //                return Redirect(ReturnUrl);
            //            }
            //            else
            //            {
            //                return RedirectToAction("Index", "Home");
            //            }
            //        }
            //    }
            //}

            return View();
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
        public async Task<IActionResult> ConfirmationExternal(ExternalLoginVM externalLoginModel)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = externalLoginModel.ReturnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = CreateUser();
                //user.EmailConfirmed = true;
                user.Avatar = "null";
                await _userStore.SetUserNameAsync(user, externalLoginModel.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        return RedirectToAction("Index", "Home");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("", "Home");
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