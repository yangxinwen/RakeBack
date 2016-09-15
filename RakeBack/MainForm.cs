using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RakeBack.Helper;
using RakeBack.Model;
using System.Diagnostics;
using System.Configuration;

namespace RakeBack
{
    public partial class MainForm : Form
    {
        private Dictionary<string, BaseForm> _formDic = new Dictionary<string, BaseForm>();

        private string _loginType = "0";

        public MainForm()
        {
            InitializeComponent();

            _loginType = ConfigurationManager.AppSettings["LoginType"];
            InitMenuItem();

            ShowForm("Common.MainPage");
        }


        /// <summary>
        /// 通过路径名，显示指定Form
        /// </summary>
        /// <param name="path"></param>
        private void ShowForm(string path)
        {
            if (_formDic.ContainsKey(path))
            {
                _formDic[path].Show(dockPanel1);
            }
            else
            {
                Type type = Type.GetType("RakeBack.Content." + path);
                if (type == null) return;
                object obj = type.Assembly.CreateInstance(type.ToString());
                if (obj != null && obj is BaseForm)
                {
                    var form = obj as BaseForm;
                    form.DockStateChanged += Form_DockStateChanged;
                    form.Show(dockPanel1);
                    _formDic.Add(path, form);
                }
            }
        }

        private void Form_DockStateChanged(object sender, EventArgs e)
        {
            var form = sender as BaseForm;

            //if(form.DockState==XiaoCai.WinformUI.Docking.DockState.Hidden)

        }

        /// <summary>
        /// 初始化菜单
        /// </summary>
        private void InitMenuItem()
        {
            List<MenuItemModel> list = new List<MenuItemModel>();

            var roleId = Business.ApplicationParam.UserInfo.RoleId;

            var menuItem = new MenuItemModel() { Name = "常用操作", Path = null, IsEnable = true };
            menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "首页", Path = "Common.MainPage", IsEnable = true });
            menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "修改密码", Path = "Common.PassordModify", IsEnable = true });
            menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "退出", Path = "Common.Exit", IsEnable = true });
            list.Add(menuItem);

            if (roleId == 0 || roleId == 3)
            {  //系统管理员和会员管理员
                menuItem = new MenuItemModel() { Name = "返佣管理", Path = null, IsEnable = true };
                menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "新建返佣", Path = "RakeBackMgr.NewRakeBack", IsEnable = true });
                menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "返佣管理", Path = "RakeBackMgr.RakeBackMgr", IsEnable = true });
                menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "返佣统计", Path = "RakeBackMgr.RakeBackTotal", IsEnable = true });
                list.Add(menuItem);
            }


            if (roleId == 0 || roleId == 2)
            { //系统管理员和会员
                menuItem = new MenuItemModel() { Name = "提现记录", Path = null, IsEnable = true };
                menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "返佣提取", Path = "TakenRecord.RakeBackTaken", IsEnable = true });
                list.Add(menuItem);
            }

            if (roleId == 0 || roleId == 3)
            {  //系统管理员和会员管理员
                menuItem = new MenuItemModel() { Name = "系统设置", Path = null, IsEnable = true };
                menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "添加用户", Path = "SystemSet.AddMember", IsEnable = true });
                menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "系统用户", Path = "SystemSet.MemberMgr", IsEnable = true });
                list.Add(menuItem);
            }
            var menuList = MenuItemHelper.MakeMenuItem(list);
            mainMenu.Items.Clear();
            mainMenu.Items.AddRange(menuList.ToArray());

            //添加菜单点击事件
            foreach (var mainMenu in menuList)
            {
                AddMenuItemEvent(mainMenu);
            }









        }


        /// <summary>
        /// 添加菜单事件
        /// </summary>
        /// <param name="menuItem"></param>
        private void AddMenuItemEvent(ToolStripMenuItem menuItem)
        {
            if (menuItem == null) return;
            if (menuItem.Tag != null)
                menuItem.Click += MainMenu_Click;
            foreach (ToolStripMenuItem subItem in menuItem.DropDownItems)
            {
                AddMenuItemEvent(subItem);
            }
        }



        private void MainMenu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripItem;
            if (menuItem == null && menuItem.Tag != null) return;

            ShowForm(menuItem.Tag.ToString());

        }
    }
}
