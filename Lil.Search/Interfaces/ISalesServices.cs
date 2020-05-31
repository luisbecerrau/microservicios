using Lil.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lil.Search.Interfaces
{
    public interface ISalesServices
    {
        Task<ICollection<Order>> GetAsync(string customerId);
    }
}
