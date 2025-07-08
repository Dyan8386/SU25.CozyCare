using CozyCare.SharedKernel.Store;
using CozyCare.SharedKernel.Utils;

namespace CozyCare.SharedKernel.Base
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public StatusCodeHelper StatusCode { get; set; }
        public string? Code { get; set; }
        private BaseResponse(StatusCodeHelper statusCode, string code, T? data, string? message)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
            Code = code;
        }

        public static BaseResponse<T> FromData(StatusCodeHelper code, T? data)
            => new BaseResponse<T>(code, code.Name(), data, null);

        public static BaseResponse<T> FromMessage(StatusCodeHelper code, string? message)
            => new BaseResponse<T>(code, code.Name(), default, message);


        public static BaseResponse<T> OkResponse(T? data)
        {
            return new BaseResponse<T>(StatusCodeHelper.OK, StatusCodeHelper.OK.Name(), data, null);
        }

        public static BaseResponse<T> OkResponse(string? mess)
        {
            return new BaseResponse<T>(StatusCodeHelper.OK, StatusCodeHelper.OK.Name(), default, mess);
        }

        public static BaseResponse<T> CreatedResponse(T? data)
        {
            return new BaseResponse<T>(StatusCodeHelper.Created, StatusCodeHelper.Created.Name(), data, null);
        }

        public static BaseResponse<T> NoContentResponse()
        {
            return new BaseResponse<T>(StatusCodeHelper.NoContent, StatusCodeHelper.NoContent.Name(), default, null);
        }

        public static BaseResponse<T> ConflictResponse(string? mess)
        {
            return new BaseResponse<T>(StatusCodeHelper.Conflict, StatusCodeHelper.Conflict.Name(), default, mess);
        }

        public static BaseResponse<T> ErrorResponse(string? mess)
        {
            return new BaseResponse<T>(StatusCodeHelper.BadRequest, StatusCodeHelper.BadRequest.Name(), default, mess);
        }

        public static BaseResponse<T> NotFoundResponse(string? mess)
        {
            return new BaseResponse<T>(StatusCodeHelper.NotFound, StatusCodeHelper.NotFound.Name(), default, mess);
        }

        public static BaseResponse<T> UnauthorizeResponse(string? mess)
        {
            return new BaseResponse<T>(StatusCodeHelper.Unauthorized, StatusCodeHelper.Unauthorized.Name(), default, mess);
        }

        public static BaseResponse<T> ForbiddenResponse(string? mess)
        {
            return new BaseResponse<T>(StatusCodeHelper.Forbidden, StatusCodeHelper.Forbidden.Name(), default, mess);
        }

    }
}
