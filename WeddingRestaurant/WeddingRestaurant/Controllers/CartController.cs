using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using WeddingRestaurant.Heplers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using System.Security.Policy;
using System;
using WeddingRestaurant.Services;

namespace WeddingRestaurant.Controllers
{
    public class CartController : Controller
    {
        private readonly PaypalClient _paypalClient;
        private readonly ModelContext db;
        private readonly IVnPayService _vnPayservice;

        public CartController(ModelContext context, PaypalClient paypalClient, IVnPayService vnPayservice)
        {
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
        public IActionResult Index(string payment = "COD")
        {
            //var customerId = int.Parse(HttpContext.User.Claims.SingleOrDefault(p => p.Type == Configuration.Claim_User_Id).Value);

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


            var order = new Order
            {
                UserID = 5,
                PaymentMethods = ""
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

                return View("Index", Cart);
            }
            catch
            {
                db.Database.RollbackTransaction();
            }

            return View("Index", Cart);
        }

        public IActionResult PaymentSuccess()
        {
            return View("Success");
        }
        public IActionResult AddToCart(int id, string type = "Normal")
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.Id == id);
            if (item == null)
            {
                var products = db.Products.SingleOrDefault(p => p.Id == id);
                if (products == null)
                {
                    TempData["Message"] = $"khong tim thay";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    Id = products.Id,
                    Name = products.Name,
                    Price = products.Price,
                };
                gioHang.Add(item);
            }
            
            HttpContext.Session.Set(Configuration.CART_KEY, gioHang);

            if (type == "ajax")
            {
                return Json(new
                {
                    Name = item.Name,
                    Price = item.Price,
                    ProductId = item.Id,
                    success = true
                });
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id, string type = "Normal")
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.Id == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(Configuration.CART_KEY, gioHang);

            }
            if (type == "ajax")
            {
                return Json(new
                {
                    Name = item.Name,
                    Price = item.Price,
                    ProductId = item.Id,
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

                // Lưu database đơn hàng của mình
                //var customerId = int.Parse(HttpContext.User.Claims.SingleOrDefault(p => p.Type == Configuration.Claim_User_Id).Value);

                var order = new Order
                {
                    UserID = 5,
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
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }


            // Lưu đơn hàng vô database

            TempData["Message"] = $"Thanh toán VNPay thành công";
            return RedirectToAction("PaymentSuccess");
        }
    }
    
}
