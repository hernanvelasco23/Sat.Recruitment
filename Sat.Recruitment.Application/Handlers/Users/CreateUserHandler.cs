using Flunt.Notifications;
using MediatR;
using Sat.Recruitment.Application.Notifications;
using Sat.Recruitment.Application.UserTypeStrategy;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, EntityResult<UserViewModel>>
    {
        public async Task<EntityResult<UserViewModel>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entityResult = new EntityResult<UserViewModel>(request.Notifications, null);

                if (request.Valid)
                {
                    var reader = ReadUsersFromFile();

                    var _users = new List<UserEntity>();

                    var newUser = new UserEntity
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Address = request.Address,
                        Phone = request.Phone,
                        UserType = request.UserType,
                        Money = !string.IsNullOrEmpty(request.Money) ? decimal.Parse(request.Money) : 0
                    };

                    var context = new UserTypeContext();

                    switch (newUser.UserType)
                    {
                        case "Normal":
                            context.SetStrategy(new NormalUserType());
                            break;
                        case "SuperUser":
                            context.SetStrategy(new SuperUserType());
                            break;
                        case "Premium":
                            context.SetStrategy(new PremiumUserType());
                            break;
                    }

                    if(!string.IsNullOrEmpty(newUser.UserType))
                        newUser.Money = context.CalulateLogic(request.Money);

                    //Normalize email
                    var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                    var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                    aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                    newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLineAsync().Result;
                        var user = new UserEntity
                        {
                            Name = line.Split(',')[0].ToString(),
                            Email = line.Split(',')[1].ToString(),
                            Phone = line.Split(',')[2].ToString(),
                            Address = line.Split(',')[3].ToString(),
                            UserType = line.Split(',')[4].ToString(),
                            Money = decimal.Parse(line.Split(',')[5].ToString()),
                        };
                        _users.Add(user);
                    }
                    
                    try
                    {
                        var isDuplicated = _users.Any(x => x.Email == newUser.Email || x.Phone == newUser.Phone || x.Name == newUser.Name);

                        if (isDuplicated)
                        {
                            reader.Close();
                            request.AddNotification(new Notification("CreateUserHandler-Handler", $"Exception - [user duplicated.]"));
                            return new EntityResult<UserViewModel>(request.Notifications, null) { Error = ErrorCode.NotFound };
                        }
                        else
                        {
                            reader.Close();
                            entityResult = new EntityResult<UserViewModel>(request.Notifications, new UserViewModel() { Name = newUser.Name, Email = newUser.Address, UserType = newUser.UserType  });
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("The user is duplicated");
                        reader.Close();
                        request.AddNotification(new Notification("CreateUserHandler-Handler", $"Exception - [user duplicated.]"));
                        return new EntityResult<UserViewModel>(request.Notifications, null) { Error = ErrorCode.NotFound };
                    }

                }
                
                return entityResult;
            }
            catch (Exception ex)
            {
                request.AddNotification(new Notification("CreateBookingHandler-Handler", $"Exception - [{ex.Message}]"));
                return new EntityResult<UserViewModel>(request.Notifications, null) { Error = ErrorCode.InternalServerError };
            }
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

    }
}
