using System.Collections.Generic;

namespace Lottery.Web.Models
{
    public class UserMenu
    {
        //
        // 摘要:
        //     Creates a new Abp.Application.Navigation.UserMenu object.
        public UserMenu()
        {
        }

        //
        // 摘要:
        //     Unique name of the menu in the application.
        public string Name { get; set; }
        //
        // 摘要:
        //     Display name of the menu.
        public string DisplayName { get; set; }
        //
        // 摘要:
        //     A custom object related to this menu item.
        public object CustomData { get; set; }
        //
        // 摘要:
        //     Menu items (first level).
        public IList<UserMenuItem> Items { get; set; }
    }
}