using System;

namespace Sat.Recruitment.Application.UserTypeStrategy
{
    public class SuperUserType : IUserTypeStrategy
    {
        public decimal Calculate(string money)
        {
            decimal result = 0;

            if (decimal.Parse(money) > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = decimal.Parse(money) * percentage;
                result = decimal.Parse(money) + gif;
            }
            return result;
        }
    }
}
