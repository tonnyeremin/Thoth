using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ThothBase
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
    
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public bool AllItems {get; private set;}
    
        public PagedList(List<T> items, int count, int pageNumber, int pageSize, bool allItems)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AllItems = allItems;
            AddRange(items);
        }
    
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize, bool allItems)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return  new PagedList<T>(items, count, pageNumber, pageSize, allItems);
        }
    }
}