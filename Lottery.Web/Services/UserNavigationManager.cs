using Lottery.Web.Models;
using System.Collections.Generic;

namespace Lottery.Web.Services
{
    public class UserNavigationManager : IUserNavigationManager
    {
        public UserMenu GetMenu()
        {
            var menu = new UserMenu();
            menu.Items = new List<UserMenuItem>()
            {
                new UserMenuItem()
                {
                    CustomData = "",
                    DisplayName = "首页",
                    IsEnabled = true,
                    IsVisible = true,
                    Name = "Home",
                    Url = "/"
                },
                new UserMenuItem()
                {
                    DisplayName = "关于",
                    Url = "/Home/About",
                    IsEnabled = true,
                    IsVisible = true,
                    Name = "About"
                }
            };
            return menu;
        }
    }
}