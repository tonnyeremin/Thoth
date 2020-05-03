using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Thoth.Data;

namespace Thoth
{
    [Route("manage/quoteitem")]
    public class QuoteItemManageController  : ControllerBase
    {
        private readonly IDataRepository<QuoteItem> _repository;
        public QuoteItemManageController(IDataRepository<QuoteItem> repository)
        {
            _repository = repository;
        }

       //GET: api/quotaitem
       [HttpGet]
       public async Task<ActionResult<IEnumerable<QuoteItem>>> Get([FromQuery] QuoteItemParameters parameters)
       {
           try
           {
                var list = await _repository.GetAll(parameters);

                var metadata = new 
                {
                    list.TotalCount,
                    list.PageSize,
                    list.CurrentPage,
                    list.TotalPages,
                    list.HasNext,
                    list.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

                return list;
           }
           catch(Exception)
           {
                return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
           }

       }

       //GET: api/quotaitem/1
        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteItem>> GetAction(long id)
        {
            try
            {
                return await _repository.Get(id);
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        }

       //POST: api/quotaitem
       [HttpPost]
       public async Task<IActionResult> Post([FromBody]NewQuotaItemDto item)
       {
          try
          {
                var model = item.ToModel();
                model.PostTime = DateTime.UtcNow;
                await _repository.Add(model);
                return NoContent();
          }
          catch(Exception)
          {
               return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
          }
       }

       //PUT: api/quotaitem/1
        [HttpPut("{id}")] 
        public async Task<IActionResult> PutQuoteItem(long id, [FromBody]EditQuotaItemDto item)
        {
            try
            {
                var model = item.ToModel();
                model.PostTime = DateTime.UtcNow;
                await _repository.Update(id, model); 
                return NoContent(); 
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        } 

       //DELETE api/quotaitem/1
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteQuoteItem(long id)
       {
            try
            {
                var item = await _repository.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                await _repository.Delete(item);
                return NoContent();
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
       }





    }
    
}