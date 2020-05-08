using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thoth.Data;

namespace Thoth
{
    public class QuoteItemManager : IDataRepository<QuoteItem>
    {
        private readonly ThothContext _context;
        public QuoteItemManager(ThothContext context)
        {
            _context = context;
        }
        public Task Add(QuoteItem  item)
        {
            _context.QuoteItems.Add(item);
             return _context.SaveChangesAsync();
        }

        public Task Delete(QuoteItem  item)
        {
            _context.QuoteItems.Remove(item);
             return _context.SaveChangesAsync();
        }

        public ValueTask<QuoteItem> Get(long id)
        {
            return _context.QuoteItems.FindAsync(id);
        }

        public Task<List<QuoteItem>> GetAll(QuoteItemParameters parameters)
        {
           if(parameters.NewOnly)
           {
                return  _context.QuoteItems.Where(item => !item.IsApproved)
                                        .OrderBy(item=>item.PostTime).ToListAsync();
           }
           else
           {
                return  _context.QuoteItems.OrderBy(item=>item.PostTime).ToListAsync();
           }
          
        }

        public Task<QuoteItem> GetRandom()
        {
            var items = _context.QuoteItems.OrderBy(c=>Guid.NewGuid()).Where(c=>c.IsVisible);
            return items.FirstOrDefaultAsync();
        }

        public Task Update(long id, QuoteItem newEntity)
        {
            var item = _context.QuoteItems.Find(id);
            if ( item  == null)
            {
                throw new KeyNotFoundException();
            }

            item.PrimaryText = newEntity.PrimaryText;
            item.SecondaryText = newEntity.SecondaryText;
            item.Author = newEntity.Author;
            item.PostTime = newEntity.PostTime;
            item.IsVisible = newEntity.IsVisible;
            return  _context.SaveChangesAsync();
        }

    }
}