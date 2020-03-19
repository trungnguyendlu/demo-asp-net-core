using System.Collections.Generic;

namespace Demo.Data
{
    public class SaveResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public static SaveResponse<T> SuccessResponse(T data, params string[] messages)
        {
            return new SaveResponse<T>
            {
                Success = true,
                Data = data,
                Messages = new List<string>(messages)
            };
        }

        public static SaveResponse<T> FailResponse(T data, params string[] messages)
        {
            return new SaveResponse<T>
            {
                Success = false,
                Data = data,
                Messages = new List<string>(messages)
            };
        }
    }
}