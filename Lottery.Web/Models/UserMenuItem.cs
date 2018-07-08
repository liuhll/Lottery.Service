using System.Collections.Generic;

namespace Lottery.Web.Models
{
    public class UserMenuItem
    {
        //
        // 摘要:
        //     Creates a new Abp.Application.Navigation.UserMenuItem object.
        public UserMenuItem()
        {
        }

        //
        // 摘要:
        //     Unique name of the menu item in the application.
        public string Name { get; set; }

        //
        // 摘要:
        //     Icon of the menu item if exists.
        public string Icon { get; set; }

        //
        // 摘要:
        //     Display name of the menu item.
        public string DisplayName { get; set; }

        //
        // 摘要:
        //     The Display order of the menu. Optional.
        public int Order { get; set; }

        //
        // 摘要:
        //     The URL to navigate when this menu item is selected.
        public string Url { get; set; }

        //
        // 摘要:
        //     A custom object related to this menu item.
        public object CustomData { get; set; }

        //
        // 摘要:
        //     Target of the menu item. Can be "_blank", "_self", "_parent", "_top" or a frame
        //     name.
        public string Target { get; set; }

        //
        // 摘要:
        //     Can be used to enable/disable a menu item.
        public bool IsEnabled { get; set; }

        //
        // 摘要:
        //     Can be used to show/hide a menu item.
        public bool IsVisible { get; set; }

        //
        // 摘要:
        //     Sub items of this menu item.
        public IList<UserMenuItem> Items { get; }
    }
}