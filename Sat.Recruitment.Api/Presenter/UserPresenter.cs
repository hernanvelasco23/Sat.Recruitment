using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Presenter.Interfaces;
using Sat.Recruitment.Application.Notifications;
using Sat.Recruitment.Domain.ViewModels;

namespace Sat.Recruitment.Api.Presenter
{
    public class UserPresenter:BasePresenter,IUserPresenter
    {
        public IActionResult GetResult(EntityResult<UserViewModel> result)
        {
            return result.Invalid ? base.GetActionResult(result) :
                new OkObjectResult(result.Entity);
        }
}
}
