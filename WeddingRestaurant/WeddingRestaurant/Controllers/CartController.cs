﻿using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using WeddingRestaurant.Heplers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using System.Security.Policy;
using System;
using WeddingRestaurant.Services;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging.Signing;

namespace WeddingRestaurant.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PaypalClient _paypalClient;
        private readonly ModelContext db;
        private readonly IVnPayService _vnPayservice;

        public CartController(ModelContext context, PaypalClient paypalClient, IVnPayService vnPayservice,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _paypalClient = paypalClient;
            db = context;
            _vnPayservice = vnPayservice;
        }
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(Configuration.CART_KEY) ?? new List<CartItem>();

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.PaypalClientId = _paypalClient.ClientId;
            return View(Cart);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string payment = "COD")
        {
            if (payment == "Thanh toán VNPay")
            {
                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = (double)Cart.Sum(p => (decimal)p.Price),
                    CreatedDate = DateTime.Now,
                    Description = "sèw",
                    FullName = "model.HoTen",
                    OrderId = new Random().Next(1000, 100000)
                };

                return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
            }

            return View("Index", Cart);
        }

        public IActionResult PaymentSuccess()
        {
            return View("Success");
        }
        public IActionResult AddToCart(int[] ids, string type = "Normal")
        {
            var gioHang = Cart;
            foreach (var id in ids)
            {
                var item = gioHang.SingleOrDefault(p => p.Id == id);
                if (item == null)
                {
                    var product = db.Products.SingleOrDefault(p => p.Id == id);
                    if (product == null)
                    {
                        TempData["Message"] = $"Không tìm thấy sản phẩm";
                        return Redirect("/404");
                    }
                    item = new CartItem
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                    };
                    gioHang.Add(item);
                }
            }

            HttpContext.Session.Set(Configuration.CART_KEY, gioHang);

            if (type == "ajax")
            {
                return Json(new
                {
                    success = true
                });
            }

            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int[] ids, string type = "Normal")
        {
            var gioHang = Cart;

            foreach (var id in ids)
            {
                var item = gioHang.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
            }

            HttpContext.Session.Set(Configuration.CART_KEY, gioHang);

            if (type == "ajax")
            {
                return Json(new
                {
                    success = true
                });
            }

            return RedirectToAction("Index");
        }
        #region paypal 
        [HttpPost("/Cart/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
        {
            var tongTien = Cart.Sum(t => t.Price).ToString();
            var donViTienTe = "USD";
            var maDH = "DH" + DateTime.Now.Ticks.ToString();

            try
            {
                var response = await _paypalClient.CreateOrder(tongTien, donViTienTe, maDH);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }

        [HttpPost("/Cart/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderID);

                var order = new Order
                {
                    UserId = await _userManager.GetUserAsync(User),
                    PaymentMethods = "Paypal"
                };

                db.Database.BeginTransaction();
                try
                {
                    db.Database.CommitTransaction();
                    db.Add(order);
                    db.SaveChanges();

                    var cthds = new List<OrderDetail>();
                    foreach (var item in Cart)
                    {
                        cthds.Add(new OrderDetail
                        {
                            OrderId = order.Id,
                            Price = item.Price,
                            ProductId = item.Id,
                        });
                    }
                    db.AddRange(cthds);
                    db.SaveChanges();

                    HttpContext.Session.Set<List<CartItem>>(Configuration.CART_KEY, new List<CartItem>());

                    return View("Success", Cart);
                }
                catch
                {
                    db.Database.RollbackTransaction();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }
        #endregion
        public async Task<IActionResult> PaymentCallBackAsync()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }
            // Lưu đơn hàng vô database
            var order = new Order
            {
                UserId = await _userManager.GetUserAsync(User),
                PaymentMethods = "vnPay"
            };

            db.Database.BeginTransaction();
            try
            {
                db.Database.CommitTransaction();
                db.Add(order);
                db.SaveChanges();

                var cthds = new List<OrderDetail>();
                foreach (var item in Cart)
                {
                    cthds.Add(new OrderDetail
                    {
                        OrderId = order.Id,
                        Price = item.Price,
                        ProductId = item.Id,
                    });
                }
                db.AddRange(cthds);
                db.SaveChanges();

                HttpContext.Session.Set<List<CartItem>>(Configuration.CART_KEY, new List<CartItem>());

                return View("Success", Cart);
            }
            catch
            {
                db.Database.RollbackTransaction();
            }
            TempData["Message"] = $"Thanh toán VNPay thành công";
            return RedirectToAction("PaymentSuccess");
        }
    }
}
