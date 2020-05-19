using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThothBase;
using ThothManage;

namespace ThothManage.Controllers
{
    [Route("api/quoteitem")]
    public class QuotaItemPublicController  : ControllerBase
    {
        private readonly IDataRepository<QuoteItem> _repository;
        public QuotaItemPublicController(IDataRepository<QuoteItem> repository)
        {
            _repository = repository;
        }

         //GET: api/quotaitem
       [HttpGet]
       [AllowAnonymous]
       public async Task<ActionResult<QuoteItemDTO>> Get()
       {
           try
           {
                var item =  await _repository.GetRandom();
                
                if(item== null)
                     return NoContent();

                return GetQuoteDtoItem(item);
           }
           catch(Exception)
           {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
           }
       }

       //POST: api/quotaitem
       [HttpPost]
       [AllowAnonymous]
       public async Task<IActionResult> Post([FromBody]QuoteItemDTO item)
       {
          try
          {
            var model = GetQuoteItem(item);
            model.PostTime = DateTime.UtcNow;
            model.IsVisible = false;

            await _repository.Add(model);

            return NoContent();
          }
          catch(Exception)
          {
              return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
          }
       }

       private QuoteItem GetQuoteItem(QuoteItemDTO itemDTO)
       {
           return new QuoteItem(){
               Id = 0,
               Author = itemDTO.Author,
               PrimaryText = itemDTO.PrimaryText,
               SecondaryText = itemDTO.SecondaryText,
               IsVisible = false,
               IsApproved = false
           };
       }
       private QuoteItemDTO GetQuoteDtoItem(QuoteItem item)
       {
           return new QuoteItemDTO(){
               Author = item.Author,
               PrimaryText = item.PrimaryText,
               SecondaryText = item.SecondaryText,
           };
       }


    }
}