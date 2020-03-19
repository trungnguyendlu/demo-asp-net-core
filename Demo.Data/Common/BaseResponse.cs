using System.Collections.Generic;

namespace Demo.Data
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();

        public static BaseResponse SuccessResponse(params string[] messages)
        {
            return new BaseResponse
            {
                Success = true,
                Messages = new List<string>(messages)
            };
        }

        public static BaseResponse FailResponse(params string[] messages)
        {
            return new BaseResponse
            {
                Success = false,
                Messages = new List<string>(messages)
            };
        }

        public void CheckSuccess()
        {
            Success = Messages.Count == 0;
        }
    }
}
