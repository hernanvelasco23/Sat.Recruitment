using System;

namespace Sat.Recruitment.Application.UserTypeStrategy
{
    public class PremiumUserType : IUserTypeStrategy
    {
        public decimal Calculate(string money)
        {
            decimal result = 0;

            var gif = decimal.Parse(money) * 2;
            result = decimal.Parse(money) + gif;

            return result;
        }
    }
}
