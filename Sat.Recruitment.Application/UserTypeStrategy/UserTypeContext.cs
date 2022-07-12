using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Application.UserTypeStrategy
{
    public class UserTypeContext
    {
        private IUserTypeStrategy _strategy;

        public UserTypeContext()
        { }

        // Usually, the Context accepts a strategy through the constructor, but
        // also provides a setter to change it at runtime.
        public UserTypeContext(IUserTypeStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Usually, the Context allows replacing a Strategy object at runtime.
        public void SetStrategy(IUserTypeStrategy strategy)
        {
            this._strategy = strategy;
        }

        // The Context delegates some work to the Strategy object instead of
        // implementing multiple versions of the algorithm on its own.
        public decimal CalulateLogic(string money)
        {
            return this._strategy.Calculate(money);
        }
    }
}
