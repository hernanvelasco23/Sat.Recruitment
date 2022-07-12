using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.Notifications;
using Sat.Recruitment.Domain.ViewModels;

namespace Sat.Recruitment.Api.Presenter.Interfaces
{
    public interface IUserPresenter
    {
        IActionResult GetResult(EntityResult<UserViewModel> result);
    }
}
