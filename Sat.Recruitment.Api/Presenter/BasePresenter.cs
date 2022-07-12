using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Presenter.Interfaces;
using Sat.Recruitment.Application.Notifications;
using System.Net;

namespace Sat.Recruitment.Api.Presenter
{
    public class BasePresenter : IBasePresenter
    {
        public virtual IActionResult GetActionResult<T>(T result)
           where T : Result
        {
            if (result.Invalid)
            {
                return CreateErrorResult(result);
            }

            return new OkResult();
        }

        public virtual IActionResult GetActionResult<T, Y>(T result)
            where Y : class
            where T : EntityResult<Y>
        {
            if (result.Error != null)
            {
                return CreateErrorResult(result);
            }

            var res = new JsonResult(result.Entity)
            {
                StatusCode = 200
            };
            return res;
        }

        protected virtual IActionResult CreateErrorResult<T>(T result)
            where T : Result
        {
            var errorBody = ApiError.FromResult(result);
            switch (result.Error)
            {
                case ErrorCode.NotFound: return new NotFoundObjectResult(errorBody);
                case ErrorCode.Business: return new UnprocessableEntityObjectResult(errorBody);
                case ErrorCode.Unauthorized: return new UnauthorizedObjectResult(errorBody);
                case ErrorCode.InternalServerError: return new ObjectResult(errorBody) { StatusCode = (int)HttpStatusCode.InternalServerError };
                case ErrorCode.BadRequest:
                default: return new BadRequestObjectResult(errorBody);
            }
        }
    }
}
