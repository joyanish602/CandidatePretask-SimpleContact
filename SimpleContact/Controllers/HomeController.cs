using Microsoft.AspNetCore.Mvc;
using SimpleContact.Models;
using SimpleContact.Services;
using System.Diagnostics;

namespace SimpleContact.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _services;

        public HomeController(ILogger<HomeController> logger, IEmailService services)
        {
            _logger = logger;
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Index(string senderName, string senderEmail, string senderMessage)
        {
            try
            {
                _services.SendContactEmail(senderName, senderEmail, senderMessage);
                
                ViewBag.Message = "Email sent successfully";
            }
            catch(Exception ex)
            {
                ViewBag.ErrMessage = ex.Message;
            }
            return View();
        }
    }
}