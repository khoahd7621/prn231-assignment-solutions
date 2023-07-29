using BusinessObjects;
using DataTransfer;
using FlowerSystemWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FlowerSystemWebClient.Controllers
{
    public class OrderController : Controller
    {
        private const string ORDER_ITEMS_KEY = "ORDER_ITEMS";

        private readonly HttpClient client = null;
        private string OrderApiUrl = "";
        private string OrderDetailApiUrl = "";
        private string CustomerApiUrl = "";
        private string FlowerBouquetApiUrl = "";

        public OrderController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "http://localhost:5274/api/Order";
            OrderDetailApiUrl = "http://localhost:5274/api/OrderDetail";
            CustomerApiUrl = "http://localhost:5274/api/Customer";
            FlowerBouquetApiUrl = "http://localhost:5274/api/FlowerBouquet";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            string role = HttpContext.Session.GetString("ROLE");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must login to view your order history.";
                return RedirectToAction("Index", "Home", TempData);
            }
            else if (role != "Admin")
            {
                TempData["ErrorMessage"] = "You must login as a admin to view orders.";
                return RedirectToAction("Profile", "Customer", TempData);
            }

            List<Order> listOrders = await ApiHandler.DeserializeApiResponse<List<Order>>(OrderApiUrl, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(listOrders);
        }

        [HttpGet]
        public async Task<IActionResult> OrderHistory()
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            string role = HttpContext.Session.GetString("ROLE");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must login to view your order history.";
                return RedirectToAction("Index", "Home", TempData);
            }
            else if (role != "Customer")
            {
                TempData["ErrorMessage"] = "You must login as a customer to view your order history.";
                return RedirectToAction("Index", "Customer", TempData);
            }

            List<Order> listOrders = await ApiHandler.DeserializeApiResponse<List<Order>>(OrderApiUrl + $"/customer/{userId.Value}", HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View("Index", listOrders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            string role = HttpContext.Session.GetString("ROLE");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must login to view order details.";
                return RedirectToAction("Index", "Home", TempData);
            }

            Order order = await ApiHandler.DeserializeApiResponse<Order>(OrderApiUrl + "/" + id, HttpMethod.Get);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";

                if (role == "Admin")
                    return RedirectToAction("Index", "Order", TempData);
                else
                    return RedirectToAction("OrderHistory", "Order", TempData);
            }
            if (role == "Customer" && order.CustomerID != userId.Value)
            {
                TempData["ErrorMessage"] = "You don't have permission to view this order.";
                return RedirectToAction("OrderHistory", "Order", TempData);
            }
            
            List<OrderDetail> listOrderDetails = await ApiHandler.DeserializeApiResponse<List<OrderDetail>>(OrderDetailApiUrl + $"/order/{id}", HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["Order"] = order;
            ViewData["OrderDetails"] = listOrderDetails;

            return View("OrderDetail");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to delete order";
                return RedirectToAction("Index", "Home", TempData);
            }
            if (role == "Customer")
            {
                TempData["ErrorMessage"] = "You don't have permission to delete order";
                return RedirectToAction("OrderHistory", "Order", TempData);
            }

            Order order = await ApiHandler.DeserializeApiResponse<Order>(OrderApiUrl + "/" + id, HttpMethod.Get);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction("Index", "Order", TempData);
            }
            if (order.OrderStatus != 0)
            {
                TempData["ErrorMessage"] = "Order cannot be deleted.";
                return RedirectToAction("Index", "Order", TempData);
            }

            List<OrderDetail> listOrderDetails = await ApiHandler.DeserializeApiResponse<List<OrderDetail>>(OrderDetailApiUrl + $"/order/{id}", HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["Order"] = order;
            ViewData["OrderDetails"] = listOrderDetails;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string orderIdStr)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to delete order";
                return RedirectToAction("Index", "Home", TempData);
            }
            if (role == "Customer")
            {
                TempData["ErrorMessage"] = "You don't have permission to delete order";
                return RedirectToAction("OrderHistory", "Order", TempData);
            }

            int orderId = int.Parse(orderIdStr);
            Order order = await ApiHandler.DeserializeApiResponse<Order>(OrderApiUrl + "/" + orderId, HttpMethod.Get);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction("Index", "Order", TempData);
            }
            if (order.OrderStatus != 0)
            {
                TempData["ErrorMessage"] = "Order cannot be deleted.";
                return RedirectToAction("Index", "Order", TempData);
            }

            List<OrderDetail> listOrderDetails = await ApiHandler.DeserializeApiResponse<List<OrderDetail>>(OrderDetailApiUrl + $"/order/{order.OrderID}", HttpMethod.Get);

            foreach (OrderDetail orderDetail in listOrderDetails)
            {
                await ApiHandler.DeserializeApiResponse<OrderDetail>(OrderDetailApiUrl + "/" + orderDetail.OrderID + "/" + orderDetail.FlowerBouquetID, HttpMethod.Delete);
            }

            await ApiHandler.DeserializeApiResponse<Order>(OrderApiUrl + "/" + order.OrderID, HttpMethod.Delete);

            TempData["SuccessMessage"] = "Order deleted successfully.";
            return RedirectToAction("Index", "Order", TempData);
        }

        public async Task<IActionResult> Report(string startDate, string endDate)
        {
            List<Order> listOrders = await ApiHandler.DeserializeApiResponse<List<Order>>(OrderApiUrl, HttpMethod.Get);
            
            if (startDate != null && endDate == null)
            {
                DateTime start = DateTime.Parse(startDate);
                listOrders = listOrders.Where(o => o.OrderStatus == 1 && o.OrderDate >= start).ToList();
            } else if (startDate == null && endDate != null)
            {
                DateTime end = DateTime.Parse(endDate);
                listOrders = listOrders.Where(o => o.OrderStatus == 1 && o.OrderDate <= end).ToList();
            } else if (startDate != null && endDate != null)
            {
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);

                if (start > end)
                {
                    TempData["ErrorMessage"] = "Start date must be before end date.";
                    return RedirectToAction("Index", "Order", TempData);
                }

                listOrders = listOrders.Where(o => o.OrderStatus == 1 && o.OrderDate >= start && o.OrderDate <= end).ToList();
            } else
            {
                TempData["ErrorMessage"] = "Please select a date range.";
                return RedirectToAction("Index", "Order", TempData);
            }

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["Orders"] = listOrders;

            return View();
        }

        public async Task<IActionResult> Shipped(int id)
        {
            await ApiHandler.DeserializeApiResponse(OrderApiUrl + "/shipped/" + id, HttpMethod.Put, "");
            TempData["SuccessMessage"] = "Order shipped successfully.";
            return RedirectToAction("Index", "Order", TempData);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await ApiHandler.DeserializeApiResponse(OrderApiUrl + "/cancel/" + id, HttpMethod.Put, "");
            TempData["SuccessMessage"] = "Order canceled successfully.";
            return RedirectToAction("Index", "Order", TempData);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must login to create order.";
                return RedirectToAction("Index", "Home", TempData);
            }

            List<Customer> listCustomers = await ApiHandler.DeserializeApiResponse<List<Customer>>(CustomerApiUrl, HttpMethod.Get);
            List<FlowerBouquet> listFlowerBouquets = await ApiHandler.DeserializeApiResponse<List<FlowerBouquet>>(FlowerBouquetApiUrl, HttpMethod.Get);
            listFlowerBouquets = listFlowerBouquets.Where(fb => fb.FlowerBouquetStatus == 1).ToList();

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["OrderItems"] = GetOrderItems();
            ViewData["Customers"] = listCustomers;
            ViewData["FlowerBouquets"] = listFlowerBouquets;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequest orderRequest)
        {
            List<OrderItemRequest> listItemsRequest = GetOrderItems();
            if (listItemsRequest.Count == 0)
            {
                TempData["ErrorMessage"] = "Order items are empty.";
                return RedirectToAction("Create", TempData);
            }

            string role = HttpContext.Session.GetString("ROLE");
            int customerID = orderRequest.CustomerID;
            if (role == "Customer")
            {
                customerID = HttpContext.Session.GetInt32("USERID").Value;
            }

            Order order = new Order()
            {
                CustomerID = customerID,
                OrderDate = DateTime.Now,
                OrderStatus = 0,
                Freight = orderRequest.Freight,
                Total = listItemsRequest.Sum(p => p.FlowerBouquet.UnitPrice * p.Quantity)
            };
            Order orderSaved = await ApiHandler.DeserializeApiResponse<Order>(OrderApiUrl, HttpMethod.Post, order);

            foreach (OrderItemRequest itemRequest in listItemsRequest)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderID = orderSaved.OrderID,
                    FlowerBouquetID = itemRequest.FlowerBouquet.FlowerBouquetID,
                    Quantity = itemRequest.Quantity,
                    UnitPrice = itemRequest.FlowerBouquet.UnitPrice,
                    Discount = 0
                };
                await client.PostAsJsonAsync(OrderDetailApiUrl, orderDetail);
            }

            // Clear order items in Session
            ClearOrderItemsSession();

            if (role == "Customer")
            {
                TempData["SuccessMessage"] = "Create order successfully.";
                return RedirectToAction("OrderHistory", TempData);
            }
            else
            {
                TempData["SuccessMessage"] = "Create order successfully.";
                return RedirectToAction("Index", TempData);
            }
        }

        public async Task<IActionResult> AddOrderItem(OrderRequest orderRequest)
        {
            List<FlowerBouquet> listFlowerBouquets = await ApiHandler.DeserializeApiResponse<List<FlowerBouquet>>(FlowerBouquetApiUrl, HttpMethod.Get);

            FlowerBouquet product = listFlowerBouquets.Where(p => p.FlowerBouquetID == orderRequest.ProductID).FirstOrDefault();
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product doesn't exist.";
                return RedirectToAction("Create", TempData);
            }

            List<OrderItemRequest> listItemsRequest = GetOrderItems();
            OrderItemRequest itemRequest = listItemsRequest.Find(p => p.FlowerBouquet.FlowerBouquetID == orderRequest.ProductID);
            if (itemRequest != null)
            {
                if (itemRequest.Quantity + orderRequest.Quantity > product.UnitsInStock)
                {
                    TempData["ErrorMessage"] = "Quantity exceeds the number of products in stock.";
                    return RedirectToAction("Create", TempData);
                }
                itemRequest.Quantity += orderRequest.Quantity;
            }
            else
            {
                if (orderRequest.Quantity > product.UnitsInStock)
                {
                    TempData["ErrorMessage"] = "Quantity exceeds the number of products in stock.";
                    return RedirectToAction("Create", TempData);
                }
                listItemsRequest.Add(new OrderItemRequest() { Quantity = orderRequest.Quantity, FlowerBouquet = product });
            }

            SaveOrderItemsSession(listItemsRequest);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> RemoveOrderItem(OrderRequest orderRequest)
        {
            List<OrderItemRequest> listItemsRequest = GetOrderItems();
            listItemsRequest.RemoveAll(p => p.FlowerBouquet.FlowerBouquetID == orderRequest.ProductID);
            SaveOrderItemsSession(listItemsRequest);
            return RedirectToAction("Create");
        }

        // Get list order items in Session
        private List<OrderItemRequest> GetOrderItems()
        {
            var session = HttpContext.Session;
            string jsonOrderItems = session.GetString(ORDER_ITEMS_KEY);
            if (jsonOrderItems != null)
            {
                return JsonConvert.DeserializeObject<List<OrderItemRequest>>(jsonOrderItems);
            }
            return new List<OrderItemRequest>();
        }

        private void SaveOrderItemsSession(List<OrderItemRequest> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(ORDER_ITEMS_KEY, jsoncart);
        }

        private void ClearOrderItemsSession()
        {
            var session = HttpContext.Session;
            session.Remove(ORDER_ITEMS_KEY);
        }
    }
}
