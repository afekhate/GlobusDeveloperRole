using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globus.Utilities.Common
{
    public class ApiResult<T>
    {
        public bool HasError { get; set; } = false;
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public T Result { get; set; }

    }


    public class TransactionResponse<T>
    {
        public string message { get; set; }
        public string code { get; set; }
        public T data { get; set; }
    }

    public class TransactionResponses<T>
    {
        public string message { get; set; }
        public string code { get; set; }
        public List<T> data { get; set; }
    }



    public class MessageOut
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public long RetId { get; set; }
        public long BulkUploadId { get; set; }
        public string BulkUploadHtmlData { get; set; }
        public string RedirectUrl { get; set; }
        public string ReferenceNumber { get; set; }

        public List<string> Errors { get; set; }
    }

    public class MessageSignInOut
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }
    }
}
