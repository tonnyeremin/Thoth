using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Thoth.Data
{
    public class QuoteItemManager : IDataRepository<QuoteItem>
    {
        private readonly ThothContext _context;
        public QuoteItemManager(ThothContext context)
        {
            _context = context;
        }
        public Task Add(QuoteItem entity)
        {
            _context.QuoteItems.Add(entity);
             return _context.SaveChangesAsync();
        }

        public Task Delete(QuoteItem entity)
        {
            _context.QuoteItems.Remove(entity);
             return _context.SaveChangesAsync();
        }

        public ValueTask<QuoteItem> Get(long id)
        {
            return _context.QuoteItems.FindAsync(id);
        }

        public Task<List<QuoteItem>> GetAll()
        {
           return _context.QuoteItems.ToListAsync();
        }

        public Task<QuoteItem> GetRandom()
        {
            Random rand = new Random();
            double r = rand.NextDouble();
            int toSkip =  (int)Math.Round((_context.QuoteItems.LongCount() - 1) * r);
            return _context.QuoteItems.Skip(toSkip).Take(1).FirstAsync();
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