using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThothBase;
using ThothManage.Models;
using Microsoft.AspNetCore.Authorization;

namespace ThothManage.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
         private readonly IDataRepository<QuoteItem> _repository;
        public HomeController(ILogger<HomeController> logger, IDataRepository<QuoteItem> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
             var items = await _repository.GetAll(new  QuoteItemParameters());
                var list =  PagedList<QuoteItemExDTO>.ToPagedList(items.Select(s=> Utils.GetDTO(s)).AsQueryable(), 1, 20, false);
                return View("Index", list);
        }


       public async Task<ActionResult<PagedList<QuoteItemExDTO>>> NewItems([FromQuery] QuoteItemParameters parameters)
       {
           try
           {
                var items = await _repository.GetAll(parameters);
                var list =  PagedList<QuoteItemExDTO>.ToPagedList(items.Select(s=>  Utils.GetDTO(s)).AsQueryable(), parameters.PageNumber, parameters.PageSize,  parameters.NewOnly ==0);
                return View("Index", list);
           }
           catch(Exception)
           {
                return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
           }

       }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}
