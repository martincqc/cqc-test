using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GFOL.Models;
using GFOL.Services;

namespace GFOL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessionService _sessionService;

        public HomeController(ILogger<HomeController> logger, ISessionService sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                _sessionService.ClearSession();
                return View();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                return RedirectToAction("ProblemWithService", "Error");//Todo let middleware deal with errors
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
