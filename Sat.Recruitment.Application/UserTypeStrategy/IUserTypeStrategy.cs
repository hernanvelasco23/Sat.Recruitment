using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Application.UserTypeStrategy
{
    public interface IUserTypeStrategy
    {
        decimal Calculate(string money);
    }
}
