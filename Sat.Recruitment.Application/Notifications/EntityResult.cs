﻿using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Application.Notifications
{
    public class EntityResult<T> : Result where T : class
    {
        public T Entity { get; }

        public EntityResult(IReadOnlyCollection<Notification> notifications, T entity)
            : base(notifications)
        {
            Entity = entity;
        }
    }
}
