using System;

namespace AlgorithmsRanking.Models
{
    public class ApiError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }


        public ApiError(string code, string message, string detail)
        {
            Code = code;
            Message = message;
            Detail = detail;
        }

        public ApiError(Exception ex) 
            : this(
                  ex.Data["Code"]?.ToString() ?? "400",
                  ex.Message,
                  ex.InnerException?.Message ?? ""
                  )
        {
        }
    }
}
