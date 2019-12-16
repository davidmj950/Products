using David.Products.Business.Interfaces;
using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Business.BL
{
    public class BlParameter : IParameter
    {
        public List<Parameter> GetParametersByType(object p, out HttpResponseMessage httpResponse)
        {
            throw new NotImplementedException();
        }
    }
}
