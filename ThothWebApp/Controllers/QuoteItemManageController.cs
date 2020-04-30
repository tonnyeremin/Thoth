using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thoth.Data;

namespace Thoth
{
    [Route("manage/quoteitem")]
    [ApiController]
    public class QuoteItemManageController  : ControllerBase
    {
        private readonly IDataRepository<QuoteItem> _repository;
        public QuoteItemManageController(IDataRepository<QuoteItem> repository)
        {
            _repository = repository;
        }

       //GET: api/quotaitem
       [HttpGet]
       public async Task<ActionResult<IEnumerable<QuoteItem>>> Get()
       {
           return await _repository.GetAll();
       }

       //GET: api/quotaitem/1
        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteItem>> GetAction(long id)
        {
            return await _repository.Get(id);
        }

       //POST: api/quotaitem
        [HttpPost]
       public async Task<IActionResult> Post([FromBody]QuoteItem item)
       {
          await _repository.Add(item);
          return NoContent();
       }

       //PUT: api/quotaitem/1
        [HttpPut("{id}")] 
        public async Task<IActionResult> PutQuoteItem(long id, [FromBody]QuoteItem item)
        {
           await _repository.Update(id, item); 
           return NoContent(); 
        } 

       //DELETE api/quotaitem/1
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteQuoteItem(long id)
       {
            var item = await _repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            await _repository.Delete(item);
            return NoContent();
       }





    }
    
}