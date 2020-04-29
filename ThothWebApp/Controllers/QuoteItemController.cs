using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thoth.Data;

namespace Thoth
{
    [Route("api/quoteitem")]
    [ApiController]
    public class QuoteItemController  : ControllerBase
    {
        private readonly IDataRepository<QuoteItem> _repository;
        public QuoteItemController(IDataRepository<QuoteItem> repository)
        {
            _repository = repository;
        }

       //GET: api/quotaitem
       [HttpGet]
       public IActionResult Get()
       {
           IEnumerable<QuoteItem> items = _repository.GetAll();
           return Ok(items);
       }

       //GET: api/quotaitem/1
       [HttpPost]
       public IActionResult Post([FromBody]QuoteItem item)
       {
           if(item == null)
           return BadRequest("Item is null");
           _repository.Add(item);
           return CreatedAtRoute("Get", new { Id = item.Id },item);
       }

       //POST: api/quotaitem

       //PUT: api/quotaitem/1

       //DELETE api/quotaitem/1





    }
    
}