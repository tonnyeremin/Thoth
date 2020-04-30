using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thoth.Data;

namespace ThothWebApp
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
       public async Task<ActionResult<QuoteItem>> Get()
       {
           return await _repository.GetRandom();
       }

       //POST: api/quotaitem
       [HttpPost]
       public async Task<IActionResult> Post([FromBody]QuoteItem item)
       {
          await _repository.Add(item);

          return NoContent();
       }


    }
}