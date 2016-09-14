using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakeBack.Model
{
    /// <summary>
    /// 菜单model
    /// </summary>
    public class MenuItemModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单指向路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 菜单是否可用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuItemModel> SubMenuItems { get; set; } = new List<MenuItemModel>();
    }
}
