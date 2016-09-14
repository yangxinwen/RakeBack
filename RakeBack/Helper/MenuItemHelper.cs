using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RakeBack.Model;

namespace RakeBack.Helper
{
    public class MenuItemHelper
    {
        public static List<ToolStripMenuItem> MakeMenuItem(List<MenuItemModel> list)
        {
            var menus = new List<ToolStripMenuItem>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    menus.Add(MakeMenuItem(item));
                }
            }
            return menus;
        }
        /// <summary>
        /// 递归生成菜单项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ToolStripMenuItem MakeMenuItem(MenuItemModel model)
        {
            if (model == null)
                return null;

            var menuItem = new ToolStripMenuItem();
            menuItem.Text = model.Name;
            menuItem.Tag = model.Path;
            menuItem.Enabled = model.IsEnable;

            if (model.SubMenuItems != null)
            {
                foreach (var item in model.SubMenuItems)
                {
                    menuItem.DropDownItems.Add(MakeMenuItem(item));
                }
            }
            return menuItem;
        }
    }
}
