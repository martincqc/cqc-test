using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFOL.AddressFinder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GFOL.Controllers
{
    public class AddressController : Controller
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressFinder _addressFinder;

        public AddressController(ILogger<AddressController> logger, IAddressFinder addressFinder)
        {
            _logger = logger;
            _addressFinder = addressFinder;
        }
        public IActionResult Index(string postcode, string pageId, string nextPageId)
        {
            try
            {
                var apiKey = "PCWT2-KQF2C-CSL92-7P85A";//TODO put this into config
                //var addressList = _addressFinder.GetAddressesTest(postcode, apiKey).Result;
                //TODO uncomment this to switch on real address finder
                var addressList = _addressFinder.GetAddresses(postcode, apiKey).Result;
                return View(new AddressListVM { PostCode = postcode, Addresses = addressList, PageId = pageId, NextPageId = nextPageId });
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e);
                return RedirectToAction("ProblemWithService", "Error");//Todo let middleware deal with errors
            }
        }
    }
}