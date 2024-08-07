﻿using core.Database.MongoDB.Interfaces;
using entities;

namespace Interfaces.Repositories
{
    public interface IUserRepository : IBaseMongoRepository<User>
    {
    }
}
