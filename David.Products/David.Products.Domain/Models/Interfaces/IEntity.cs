using System;
using System.Collections.Generic;
using System.Text;

namespace David.Products.Domain.Models.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime LastUpdate { get; set; }
        DateTime CreateDate { get; set; }
        bool Active { get; set; }

    }
}
