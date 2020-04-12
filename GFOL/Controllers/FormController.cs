using System;
using System.Linq;
using GFOL.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GFOL.Services;

namespace GFOL.Controllers
{
    public class FormController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFormService _formService;
        private readonly ISubmissionService _submissionService;
        private readonly ISessionService _sessionService;
        private readonly IGFOLValidation _gfolValidation;
        public FormController(ILogger<HomeController> logger, IFormService formService, ISubmissionService submissionService, ISessionService sessionService, IGFOLValidation gfolValidation)
        {
            _logger = logger;
            _formService = formService;
            _submissionService = submissionService;
            _sessionService = sessionService;
            _gfolValidation = gfolValidation;
        }
        /// <summary>
        /// This action is called from the address controller
        /// after the user has selected an address from the address finder list
        /// </summary>
        /// <param name="address"></param>
        /// <param name="nextPageId"></param>
        /// <returns>redirects to the index page to load the full address question</returns>
        [HttpGet]
        public IActionResult SelectAddress(string address, string nextPageId)
        {
            try
            {
                //go and get the address page
                var pageVm = _formService.GetPageById(nextPageId);
                //store the full address in session
                foreach (var question in pageVm.Questions)//there's only one
                {
                    question.Answer = address;
                    _submissionService.SaveUserAnswerIntoSession(question);
                }

                return RedirectToAction("Index", new { id = nextPageId });
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                return RedirectToAction("ProblemWithService", "Error");//Todo let middleware deal with errors
            }
        }
        /// <summary>
        /// THis action shows the question page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string id = "")
        {
            try
            {
                var referer = Request.Headers["Referer"].ToString();
                if (string.IsNullOrWhiteSpace(referer))
                {
                    //url entered into the browser
                    return RedirectToAction("PageNotFound", "Error");
                }
                //flag if user has come back from check your answers
                if (referer.ToLower().Contains("checkyouranswers"))
                {
                    _sessionService.SetUserComeFromCheck();
                }
                if (id == "start")
                {
                    return RedirectToAction("Index", "Home");
                }
                var pageVm = _formService.GetPageById(id);
                //load any existing answers from the session
                foreach (var question in pageVm.Questions)
                {
                    var answer = _submissionService.GetUserAnswerFromSession(question.QuestionId);
                    question.Answer = answer;
                }
                return View(pageVm);

            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                return RedirectToAction("ProblemWithService", "Error");//Todo let middleware deal with errors
            }
        }
        /// <summary>
        /// this action validates and stores the user response and direct the flow to the next question page
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PageVM vm)
        {
            try
            {
                //Get the page we are validating
                var pageVm = _formService.GetPageById(vm.PageId);
                foreach (var question in vm.Questions)
                {
                    //put in the answers from the page
                    pageVm.Questions.FirstOrDefault(x => x.QuestionId == question.QuestionId).Answer = question.Answer;
                }
                //Validate the Response against the page json
                _gfolValidation.ValidatePage(pageVm);

                //Get any errors
                if (pageVm.Questions.Any(x => x.Validation.IsErrored || x.Validation.Required.IsErrored || x.Validation.StringLength.IsErrored))
                {
                    return View(pageVm);
                }
                //store answer/s in session
                foreach (var question in vm.Questions)
                {
                    _submissionService.SaveUserAnswerIntoSession(question);
                }
                //if this is a postcode then get a list of addresses
                var postcodeQuestion = pageVm.Questions.FirstOrDefault(x => x.DataType.ToLower() == "postcode");
                if (postcodeQuestion != null)
                {
                    var postcode = postcodeQuestion?.Answer;
                    return RedirectToAction("Index", "Address", new { postcode = postcode, pageId = pageVm.PageId, nextPageId = pageVm.NextPageId });
                }
                //redirect to the Index page with our new PageId
                var nextPageId = pageVm.NextPageId;
                if (nextPageId == "checkyouranswers" || _sessionService.HasUserComeFromCheck())
                {
                    return RedirectToAction("Index", "CheckYourAnswers");
                }
                else
                {
                    return RedirectToAction("Index", new { id = nextPageId });
                }

            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                return RedirectToAction("ProblemWithService", "Error");//Todo let middleware deal with errors
            }
        }
    }
}
