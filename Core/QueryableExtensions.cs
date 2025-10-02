using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> query, PaginationParameters parameters)
        {
            var totalItems = await query.CountAsync();
            var data = await query
                .Skip(parameters.Skip)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                TotalItems = totalItems,
                CurrentPage = parameters.Page,
                PageSize = parameters.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / parameters.PageSize),
                Data = data
            };
        }
    }

    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

    public class PaginationParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int Skip => (Page - 1) * PageSize;
    }



}
