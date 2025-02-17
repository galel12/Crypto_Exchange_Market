using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto.Queries
{
    public class QueryObject
    {
        public string? Username {get; set;} = null;
        public DateTime DateCreated { get; set;} = DateTime.UtcNow; 
        public string? SortBy {get; set;} = null;
        public bool IsAscending {get; set;} = true;
        public int PageNumber {get; set;} = 1;
        public int PageSize {get; set;} = 10;
    }
}