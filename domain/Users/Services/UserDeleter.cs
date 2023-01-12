﻿using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Users.Services
{
    public class UserDeleter : BaseDeleter<User, int>, IUserDeleter
    {
        public UserDeleter(
            INotificationService notificationService,
            IUserRepository repository
        )
            : base(notificationService, repository)
        {
        }

        protected override void Validate(IEntity? entity)
        {
            if (entity is User)
                return;

            _notificationService.AddNotification($"{nameof(entity)} is null");
        }
    }
}
