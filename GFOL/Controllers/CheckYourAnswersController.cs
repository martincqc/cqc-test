using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GFOL.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GFOL.Models;
using GFOL.Services;
using Newtonsoft.Json;

namespace GFOL.Controllers
{
    public class CheckYourAnswersController : Controller
    {
        private readonly ILogger<CheckYourAnswersController> _logger;
        private readonly ISubmissionService _submissionService;
        private readonly ISessionService _sessionService;

        public CheckYourAnswersController(ILogger<CheckYourAnswersController> logger, ISubmissionService submissionService, ISessionService sessionService)
        {
            _logger = logger;
            _submissionService = submissionService;
            _sessionService = sessionService;
        }
        /// <summary>
        /// this action displays the check your answers page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                _sessionService.ClearUserComeFromCheck();
                var submissionVm = _submissionService.GetSubmissionFromSession();
                return View(submissionVm);

            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                return RedirectToAction("ProblemWithService", "Error");//Todo let middleware deal with errors
            }
        }
        /// <summary>
        /// this action takes the user submission and sends it to the submission service for saving to the db
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SubmissionVM vm)
        {
            try
            {
                var submissionVm = _submissionService.GetSubmissionFromSession();
                submissionVm.DateCreated = DateTime.Now.ToLongDateString();
                //save to db
                var returnId = await _submissionService.PostSubmissionAsync(submissionVm);

                return RedirectToAction("Index", "Confirmation", new { id = returnId });
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
