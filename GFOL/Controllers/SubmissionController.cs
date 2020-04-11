using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFOL.Models;
using GFOL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GFOL.Controllers
{
    [Authorize]
    public class SubmissionController : Controller
    {
        private readonly ILogger<SubmissionController> _logger;
        private readonly ISubmissionService _submissionService;

        public SubmissionController(ILogger<SubmissionController> logger, ISubmissionService submissionService)
        {
            _logger = logger;
            _submissionService = submissionService;
        }
        //this action gets all the saved submissions and displays them in the page
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var submissions = await _submissionService.GetAllSubmissionsAsync();
                var model = new ViewSubmissionsVM { Submissions = submissions };
                return View(model);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                return RedirectToAction("ProblemWithService", "Error");//Todo let middleware deal with errors
            }
        }
        [HttpGet]
        public async Task<IActionResult> ViewSubmission(int id)
        {
            try
            {
                var model = await _submissionService.GetSubmissionAsync(id);
                return View(model);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                return RedirectToAction("ProblemWithService", "Error");//Todo let middleware deal with errors
            }
        }
    }
}