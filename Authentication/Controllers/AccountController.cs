using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Authentication.Core.ViewModel;
using Authentication.Core.Classes;
using Authentication.DataAccessLayer.Entities;
using Authentication.Core.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Authentication.Controllers
{
    public class AccountController : Controller
    {

        private IUser _iuser;
        public AccountController(IUser iuser)
        {
            _iuser = iuser;

        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {

            if (ModelState.IsValid)
            {
                if (_iuser.IsMobileNumberExist(register.Mobile))
                {
                    ModelState.AddModelError("Mobile", "شما قبلا ثبت نام کرده اید");
                    return RedirectToAction("Login");
                }
                else
                {
                    User user = new User()
                    {
                        IsActive = false,
                        Mobile = register.Mobile,
                        Code = CodeGenerators.ActiveCode(),

                        Password = HashGeneretors.EncodingPassWithMd5(register.Password),
                        RoleId = 2,

                    };

                    _iuser.AddUser(user);
                    SMS sms = new SMS();
                    sms.Send(user.Mobile, "  کد فعال سازی  " + user.Code);
                    return RedirectToAction("Active");
                }


            }
            else
            {
                return View(register);
            }


        }


        public IActionResult Active()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Active(ActiveViewModel active)
        {

            if (ModelState.IsValid)
            {
                if (_iuser.ActiveUser(active.Code))
                {
                    return RedirectToAction("Login");

                }
                else
                {
                    ModelState.AddModelError("Code", "کدفعال سازی صحیح نمی باشد");
                    return View(active);
                }
            }
            else
            {
                return View(active);

            }


        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {

            if (ModelState.IsValid)
            {
                var user = _iuser.LoginUser(login.Mobile, login.Password);

                if (user != null)
                {
                    if (user.IsActive)
                    {
                        var Claims = new List<Claim>()
                        {

                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Mobile)
                        };

                        var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(principal);

                        if (_iuser.GetRoleName(user.RoleId) == "admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }

                        else
                        {
                            return RedirectToAction("Index", "User");

                        }


                    }
                    else
                    {
                        return RedirectToAction("Active");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "مشخصات کاربری صحیح نمی باشد.");
                }

            }

            else
            {
                return View(login);
            }
            return View(login);
        }

        public IActionResult Forget()
        {
            return View();


        }

        [HttpPost]
        public IActionResult Forget(ForgetViewModel forget)
        {
            if (ModelState.IsValid)
            {
                var user = _iuser.ForgetPassword(forget.Mobile);

                if (user != null)
                {
                    SMS sms = new SMS();

                    sms.Send(forget.Mobile, "کد تایید برای فراموشی کلمه عبور  " + user.Code + "می بشد ");
                    return RedirectToAction("Reset");
                }
                else
                {
                    ModelState.AddModelError("Moblie", "این شماره موبایل هنوز ثبت نام نشده است");
                    return View(forget);
                }
            }
            else
            {
                return View(forget);
            }



        }

        public IActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reset(ResetViewModel reset)
        {
            if (ModelState.IsValid)
            {
                if (_iuser.ResetPassword(reset.Code, reset.Password))
                {

                    return RedirectToAction("Login");
                }

                else
                {
                    ModelState.AddModelError("Code", "کد وارد شده صحیح نمی باشد");

                    return View(reset);
                }
            }
            else
            {
                return View(reset);
            }


        }


        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}