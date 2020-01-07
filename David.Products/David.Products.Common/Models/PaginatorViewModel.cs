using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Common.Models
{
    public class PaginatorViewModel<T>
    {
        public PaginatorViewModel()
        {

        }

        public PaginatorResponse<T> QueryResult { get; set; }
        public int ActualPage { get; set; }
        public int TotalPage { get; set; }
        public T Object { get; set; }
    }

    public class PaginatorResponse<T>
    {
        public PaginatorResponse()
        {

        }

        public IList<T> Results { get; set; }
        public int TotalRows { get; set; }
    }
}
