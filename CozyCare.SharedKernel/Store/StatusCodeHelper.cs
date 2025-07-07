using CozyCare.SharedKernel.Utils;

namespace CozyCare.SharedKernel.Store
{
    public enum StatusCodeHelper
    {
        [CustomName("Success")]
        OK = 200,

        [CustomName("Created")]
        Created = 201,

        [CustomName("No Content")]
        NoContent = 204,

        [CustomName("Bad Request")]
        BadRequest = 400,

        [CustomName("Unauthorized")]
        Unauthorized = 401,

        [CustomName("Forbidden")]
        Forbidden = 403,

        [CustomName("Not Found")]
        NotFound = 404,

        [CustomName("Conflict")]
        Conflict = 409,

        [CustomName("Unprocessable Entity")]
        UnprocessableEntity = 422,

        [CustomName("Internal Server Error")]
        ServerError = 500
    }
}
