using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GFOL.Models;

namespace GFOL.Controllers
{
    public class ConfirmationController : Controller
    {
        private readonly ILogger<ConfirmationController> _logger;

        public ConfirmationController(ILogger<ConfirmationController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// this action formats the submission id and displays the confirmation page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int id)
        {
            try
            {
                var model = new ConfirmationVM { SubmissionId = "GFOL-123-" + id };
                return View(model);
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
