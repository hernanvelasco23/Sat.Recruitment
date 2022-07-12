using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Sat.Recruitment.Application.Notifications;
using Sat.Recruitment.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;


namespace Sat.Recruitment.Application.Handlers.Users
{
    public class CreateUserRequest : Notifiable, IRequest<EntityResult<UserViewModel>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string Money { get; set; }

        public CreateUserRequest(string name, string email, string address, string phone, string userType, string money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Name, "Name", "Name cannot be null.")
                .IsNotNullOrEmpty(Email, "Email", "Email cannot be null.")
                .IsNotNullOrEmpty(Address, "Address", "Address cannot be null.")
                .IsNotNullOrEmpty(Phone, "Phone", "Phone cannot be null."));
                //.IsNotNullOrEmpty(UserType, "UserType", "UserType cannot be null.")
                //.IsNotNullOrEmpty(Money, "Money", "Money cannot be null.")
                //.IsNotNull(Reader, "Reader", "Reader cannot be null."));
        }
    }
}
