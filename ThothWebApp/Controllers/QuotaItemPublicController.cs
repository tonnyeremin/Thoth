using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thoth.Data;

namespace Thoth
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
       public async Task<ActionResult<QuoteItemDTO>> Get()
       {
           try
           {
                var item =  await _repository.GetRandom();
                return item.ToDTO();
           }
           catch(Exception)
           {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
           }
       }

       //POST: api/quotaitem
       [HttpPost]
       public async Task<IActionResult> Post([FromBody]QuoteItemDTO item)
       {
          try
          {
            var model = item.ToModel();
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


    }
}