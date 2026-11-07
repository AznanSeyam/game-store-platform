using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_store_platform.Data.Entities;

namespace game_store_platform.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);

}