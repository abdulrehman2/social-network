using Microsoft.AspNetCore.Mvc;
using Post.Application.Dtos.Common;

namespace Post.Api.Helpers
{
    public static class ObjectResultHelper
    {
        public static IActionResult GetObjectResult(GenericResponse response)
        {
            if (response != null)
            {
                switch (response.Status)
                {

                    case System.Net.HttpStatusCode.OK:
                        return new OkObjectResult(response);

                    case System.Net.HttpStatusCode.Created:
                        return new OkObjectResult(response);

                    case System.Net.HttpStatusCode.Forbidden:
                        return new ForbidResult(response.Message);


                    case System.Net.HttpStatusCode.InternalServerError:
                        return new BadRequestObjectResult(response);

                    case System.Net.HttpStatusCode.BadRequest:
                        return new BadRequestObjectResult(response);

                    case System.Net.HttpStatusCode.UnprocessableEntity:
                        return new UnprocessableEntityObjectResult(response);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new UnauthorizedObjectResult(response);


                    default:
                        return new OkObjectResult(response);
                }
            }
            else
            {
                return new OkObjectResult(response);
            }


        }
    }
}
