using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Presenter.Interfaces;
using Sat.Recruitment.Application.Handlers.Users;
using Sat.Recruitment.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserPresenter _presenter;


        public UsersController(IMediator mediator, IUserPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            return _presenter.GetResult(await _mediator.Send(new CreateUserRequest(name, email, address, phone, userType, money)));
        }
    }
}
