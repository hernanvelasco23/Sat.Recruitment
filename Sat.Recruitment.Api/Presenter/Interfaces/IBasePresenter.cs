using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Notifications;

namespace Sat.Recruitment.Api.Presenter.Interfaces
{
    public interface IBasePresenter
    {
        IActionResult GetActionResult<T, Y>(T result)
          where T : EntityResult<Y>
          where Y : class;

        IActionResult GetActionResult<T>(T result) where T : Result;
    }
}
