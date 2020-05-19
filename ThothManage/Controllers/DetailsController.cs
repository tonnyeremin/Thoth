using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using ThothBase;
using ThothManage.Models;

namespace ThothManage.Controllers
{
    [Authorize()]
    public class DetailsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataRepository<QuoteItem> _repository;
        public DetailsController(ILogger<HomeController> logger, IDataRepository<QuoteItem> repository)
        {
            _logger = logger;
            _repository = repository;
           }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var item = await _repository.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
               
                
               return View("Details", Utils.GetDTO(item));
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        }

        
       public async Task<IActionResult> Approve(long id)
       {
            try
            {
                var item = await _repository.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                item.IsApproved = true;
                item.IsVisible = true;
                await _repository.Update(id, item);
                
                return RedirectToAction("NewItems", "Home");
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
       }
        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                 return View("Details", new QuoteItemExDTO(){PostTime = DateTime.UtcNow.ToString(), IsVisible= "false"} );
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        }

         public IActionResult Cancel()
        {
            try
            {
                return RedirectToAction("NewItems", "Home");
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        }

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var item = await _repository.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                await _repository.Delete(item);
                
                return RedirectToAction("NewItems", "Home");
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        }

         [HttpPost]
        public async Task<IActionResult> Edit(long id, QuoteItemExDTO model)
        {
            try
            {
                var newItem = Utils.GetItem(model);
                var item = await _repository.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                item.IsApproved = true;
                await _repository.Update(id, newItem);
                
               return RedirectToAction("NewItems", "Home");
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(QuoteItemExDTO item)
        {
            try
            {
              
                var model = Utils.GetItem(item);
                model.PostTime = DateTime.UtcNow;
                model.IsApproved = true;
                await _repository.Add(model);
               
                return RedirectToAction("NewItems", "Home");
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        } 
        
    }
}