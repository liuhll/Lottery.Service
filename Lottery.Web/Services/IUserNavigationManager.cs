﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Web.Models;

namespace Lottery.Web.Services
{
    public interface IUserNavigationManager
    {

        UserMenu GetMenu();

    }
}