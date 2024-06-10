using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using WeddingRestaurant.Heplers;
using Microsoft.AspNetCore.Authorization;
using WeddingRestaurant.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Repositories;

namespace WeddingRestaurant.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVnPayService _vnPayservice;
        private readonly PaypalClient _paypalClient;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IUnitOfWork unitOfWork, PaypalClient paypalClient, IVnPayService vnPayservice,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _paypalClient = paypalClient;
            _vnPayservice = vnPayservice;
        }
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(Configuration.CART_KEY) ?? new List<CartItem>();
        public Event CartEvent => HttpContext.Session.Get<Event>(Configuration.EVENT_KEY) ?? new Event();
        public List<RoomVM> CartRoom => HttpContext.Session.Get<List<RoomVM>>(Configuration.ROOM_KEY) ?? new List<RoomVM>();
        
        [Authorize(Roles = Configuration.RoleUser)]
        [HttpGet]
        public IActionResult Index(decimal roomPrice)
        {
            ViewBag.PaypalClientId = _paypalClient.ClientId;
            ViewBag.RoomPrice = roomPrice;
            if (CartEvent != null)
            {
                ViewBag.NumberTable = CartEvent.NumberTable;
            }
            return View(Cart);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string payment, int? numberTable = null)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(CartEvent.RoomId);

            decimal roomPrice = room != null ? room.Price : 0;

            var tongTien = (Cart.Sum(t => t.Price) ).ToString();
            if (payment == "Thanh toán VNPay")
            {
                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = (double)(Cart.Sum(p => (decimal)p.Price) * (numberTable ?? 1) + roomPrice),
                    CreatedDate = DateTime.Now,
                    OrderId = new Random().Next(1000, 100000)
                };
                TempData["SuccessMessage"] = "Thanh toán VNPay thành công!";

                return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
            }

            if(payment == "Thanh toán trực tiếp")
            {
                var order = new Order
                {
                    UserId = await _userManager.GetUserAsync(User),
                    PaymentMethods = "cod",
                };

                _unitOfWork.BeginTransaction();

                try
                {
                    await _unitOfWork.Orders.AddAsync(order);
                    await _unitOfWork.SaveChangesAsync();
                    
                    await _unitOfWork.Carts.CreateOrderAsync(order, Cart, CartEvent);

                    TempData["SuccessMessage"] = "Bạn sẽ thanh toán ngay khi tiệc diễn ra!";

                    HttpContext.Session.Set<Event>(Configuration.EVENT_KEY, new Event());
                    HttpContext.Session.Set<List<CartItem>>(Configuration.CART_KEY, new List<CartItem>());
                    HttpContext.Session.Set<List<RoomVM>>(Configuration.ROOM_KEY, new List<RoomVM>());
                    _unitOfWork.CommitTransaction();

                    return View("Index", Cart);
                }
                catch
                {
                    _unitOfWork.RollbackTransaction();
                    TempData["SuccessMessage"] = "Có lỗi xảy ra khi xử lý đơn hàng.";
                    return View("Index", Cart);
                }
            }   
            return View("Index", Cart);
        }
       
        #region paypal 
        [HttpPost("/Cart/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken, int? numberTable = null)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(CartEvent.RoomId);

            decimal roomPrice = room != null ? room.Price : 0;
            decimal tyGiaVNDtoUSD = 0.000039m;

            var tongTienVND = Cart.Sum(t => t.Price) * (numberTable ?? 1) + roomPrice;
            decimal tongTienUSD = Math.Floor(tongTienVND * tyGiaVNDtoUSD);
            string tongTienUSDString = tongTienUSD.ToString(); ;
            var donViTienTe = "USD";    
            var maDH = "DH" + DateTime.Now.Ticks.ToString();

            try
            {
                var response = await _paypalClient.CreateOrder(tongTienUSDString, donViTienTe, maDH);

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

                _unitOfWork.BeginTransaction();

                try
                {
                    await _unitOfWork.Orders.AddAsync(order);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.Carts.CreateOrderAsync(order, Cart, CartEvent);

                    HttpContext.Session.Set<Event>(Configuration.EVENT_KEY, new Event());
                    HttpContext.Session.Set<List<CartItem>>(Configuration.CART_KEY, new List<CartItem>());
                    HttpContext.Session.Set<List<RoomVM>>(Configuration.ROOM_KEY, new List<RoomVM>());
                    _unitOfWork.CommitTransaction();

                    return View("Index", Cart);
                }
                catch
                {
                    _unitOfWork.RollbackTransaction();
                    TempData["SuccessMessage"] = "Có lỗi xảy ra khi xử lý đơn hàng.";
                    return View("Index", Cart);
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
                return RedirectToAction("Index", Cart);
            }
            // Lưu đơn hàng vô database
            var order = new Order
            {
                UserId = await _userManager.GetUserAsync(User),
                PaymentMethods = "vnPay"
            };

            _unitOfWork.BeginTransaction();

            try
            {
                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.Carts.CreateOrderAsync(order, Cart, CartEvent);

                TempData["SuccessMessage"] = "Thanh toán VNPay thành công";

                HttpContext.Session.Set<Event>(Configuration.EVENT_KEY, new Event());
                HttpContext.Session.Set<List<CartItem>>(Configuration.CART_KEY, new List<CartItem>());
                HttpContext.Session.Set<List<RoomVM>>(Configuration.ROOM_KEY, new List<RoomVM>());
                _unitOfWork.CommitTransaction();

                return View("Index", Cart);
            }
            catch
            {
                _unitOfWork.RollbackTransaction(); 
                TempData["SuccessMessage"] = "Có lỗi xảy ra khi xử lý đơn hàng.";
                return View("Index", Cart);
            }

            return View("Index", Cart);
        }

        public async Task<IActionResult> AddToCart(int[] ids, string type = "Normal")
        {
            var gioHang = Cart;
            foreach (var id in ids)
            {
                var item = gioHang.SingleOrDefault(p => p.Id == id);
                if (item == null)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(id);
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
    }
}
