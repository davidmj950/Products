using David.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Business.Interfaces
{
    public interface IParameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        List<Parameter> GetParametersByType(object p, out HttpResponseMessage httpResponse);
    }
}
