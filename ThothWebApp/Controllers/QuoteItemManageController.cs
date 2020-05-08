using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThothBase;

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
       public async Task<ActionResult<PagedList<QuoteItemExDTO>>> Get([FromQuery] QuoteItemParameters parameters)
       {
           try
           {
                var items = await _repository.GetAll(parameters);
                var list =  PagedList<QuoteItemExDTO>.ToPagedList(items.Select(s=> GetDTO(s)).AsQueryable(), parameters.PageNumber, parameters.PageSize);
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
        public async Task<ActionResult<QuoteItemExDTO>> GetAction(long id)
        {
            try
            {
                var item = await _repository.Get(id);
                return GetDTO(item);
            }
            catch(Exception)
            {
                 return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
            }
        }

       //POST: api/quotaitem
       [HttpPost]
       public async Task<ActionResult<QuoteItemExDTO>> Post([FromBody]QuoteItemExDTO item)
       {
          try
          {
                var model = GetItem(item);
                model.PostTime = DateTime.UtcNow;
                model.IsApproved = true;
                await _repository.Add(model);
                return  GetDTO(model);
          }
          catch(Exception)
          {
               return BadRequest(new { Message = "Some errors occured. Please, try agian later." });
          }
       }

       //PUT: api/quotaitem/1
        [HttpPut("{id}")]
        public async Task<ActionResult<QuoteItemExDTO>> PutQuoteItem(long id, [FromBody]QuoteItemExDTO item)
        {
            try
            {
                var model = GetItem(item);
                model.PostTime = DateTime.UtcNow;
                model.IsApproved = true;
                await _repository.Update(id, model); 
                return GetDTO(model); 
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


       private QuoteItemExDTO GetDTO(QuoteItem item)
       {
           QuoteItemExDTO dto = new QuoteItemExDTO();
           dto.Id = item.Id.ToString();
           dto.IsApproved = item.IsApproved.ToString();
           dto.IsVisible = item.IsVisible.ToString();
           dto.Author = item.Author;
           dto.PrimaryText = item.PrimaryText;
           dto.SecondaryText = item.SecondaryText;
           dto.PostTime = item.PostTime.ToShortDateString();

           return dto;
       }

       private QuoteItem GetItem(QuoteItemExDTO itemExDTO)
       {
           QuoteItem item = new QuoteItem();
           item.Id = string.IsNullOrEmpty(itemExDTO.Id)? 0: long.Parse(itemExDTO.Id);
           item.Author = itemExDTO.Author;
           item.IsApproved = string.IsNullOrEmpty(itemExDTO.IsApproved)? false : bool.Parse(itemExDTO.IsApproved);
           item.IsVisible = string.IsNullOrEmpty(itemExDTO.IsVisible)? false : bool.Parse(itemExDTO.IsVisible);
           item.PrimaryText = itemExDTO.PrimaryText;
           item.SecondaryText = itemExDTO.SecondaryText;
           DateTime dateTime;
           if(DateTime.TryParse(itemExDTO.PostTime, out dateTime))
                item.PostTime = dateTime;

           return item;
       }





    }
    
}