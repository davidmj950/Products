using David.Products.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Common.Helpers
{
    public static class ResponseHelper<T>
    {
        public static Response<T> SuccessResponse(string Message, T Result)
        {
            List<MessageResult> messageResults = new List<MessageResult>
            {
                new MessageResult { Message = Message }
            };
            return new Response<T>
            {
                IsSuccess = true,
                Message = messageResults,
                Result = (T)Result
            };
        }

        public static Response<T> ErrorResponse(Exception Ex, T result)
        {
            List<MessageResult> messageResult = new List<MessageResult>()
            {
                new MessageResult { Message = string.Format("Ha ocurrido un error: {0}", Ex.Message) }
            };
            return new Response<T>
            {
                IsSuccess = false,
                Message = messageResult,
                Result = (T)result
            };
        }

        public static Response<T> ExceptionDatabase(Exception ex, string tableAction, string table, T result)
        {
            List<MessageResult> messageResult = new List<MessageResult>()
            {
                new MessageResult { Message = string.Format("Error al {0} en la tabla {1}. Error detallado: {2}", tableAction, table, ex.Message) }
            };
            return new Response<T>
            {
                IsSuccess = false,
                Message = messageResult,
                Result = (T)result
            };
        }
    }
}
