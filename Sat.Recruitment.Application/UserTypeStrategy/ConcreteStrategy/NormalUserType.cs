using System;

namespace Sat.Recruitment.Application.UserTypeStrategy
{
    public class NormalUserType : IUserTypeStrategy
    {
        public decimal Calculate(string money)
        {
            decimal result = 0;

            if (decimal.Parse(money) > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                var gif = decimal.Parse(money) * percentage;
                result =  decimal.Parse(money) + gif;
            }
            if (decimal.Parse(money) < 100)
            {
                if (decimal.Parse(money) > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = decimal.Parse(money) * percentage;
                    result = decimal.Parse(money) + gif;
                }
            }
            return result;
        }
    }
}
