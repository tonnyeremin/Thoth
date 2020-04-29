using System.Collections.Generic;
using System.Linq;

namespace Thoth.Data
{
    public class QuoteManager : IDataRepository<QuoteItem>
    {
        private readonly ThothContext _context;
        public QuoteManager(ThothContext context)
        {
            _context = context;
        }
        public void Add(QuoteItem entity)
        {
            _context.QuoteItems.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(QuoteItem entity)
        {
            _context.QuoteItems.Remove(entity);
            _context.SaveChanges();
        }

        public QuoteItem Get(long id)
        {
            return _context.QuoteItems.FirstOrDefault(q=>q.Id == id);
        }

        public IEnumerable<QuoteItem> GetAll()
        {
           return _context.QuoteItems.ToList();
        }

        public void Update(QuoteItem entity, QuoteItem newEntity)
        {
            entity.PrimaryText = newEntity.PrimaryText;
            entity.SecondaryText = newEntity.SecondaryText;
            entity.Author = newEntity.Author;
            entity.PostTime = newEntity.PostTime;
            entity.IsVisible = newEntity.IsVisible;
        }
    }
}