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
using RakeBack.Business;

namespace RakeBack
{
    public partial class MainForm : BaseForm
    {
        private Dictionary<string, BaseForm> _formDic = new Dictionary<string, BaseForm>();

        private bool _isLocked = false;
        private Timer _lockTimer = new Timer();
        /// <summary>
        /// 闲置时间
        /// </summary>
        public int _freeTime = 0;

        public MainForm()
        {
            InitializeComponent();

            this.FormClosed += MainForm_FormClosed;
            this.MouseMove += MainForm_MouseMove;

            Business.ApplicationParam.MainForm = this;
            BllHelper.LoadConfig();

            _lockTimer.Interval = 10 * 1000;
            _lockTimer.Tick += _lockTimer_Tick;
            _lockTimer.Start();

            string txt = string.Empty;
            var role = Business.ApplicationParam.UserInfo.RoleId;
            if (role.Equals(0))
                txt = "系统管理员";
            else if (role.Equals(3))
                txt = "会员管理员";
            else if (role.Equals(2))
                txt = "会员";
            this.Text = string.Format("欢迎{0}{1}使用线上返佣系统", txt, Business.ApplicationParam.UserInfo.UserName);

            InitMenuItem();

            if (ApplicationParam.UserInfo.IsUpdatePass == "0")
            {
                ShowForm("Common.PassordModify");
                mainMenu.Enabled = false;

            }
            else
                ShowForm("Common.MainPage");


            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void _lockTimer_Tick(object sender, EventArgs e)
        {
            if (_isLocked) return;
            Debug.WriteLine(_freeTime);
            _freeTime += 10;
            //无操作10分钟锁屏
            if (_freeTime >= 60 * 1000 * 10)
            {
                ShowForm("Common.LockPanel");
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            _freeTime = 0;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BllHelper.OutLogin(Business.ApplicationParam.UserInfo.UserId, Business.ApplicationParam.UserInfo.UserName);
        }


        /// <summary>
        /// 通过路径名，显示指定Form
        /// </summary>
        /// <param name="path"></param>
        public BaseForm ShowForm(string path)
        {
            BaseForm f = null;
            if (_formDic.ContainsKey(path))
            {
                f = _formDic[path];
                f.Show(dockPanel1);
            }
            else
            {
                Type type = Type.GetType("RakeBack.Content." + path);
                if (type == null) return null;
                object obj = type.Assembly.CreateInstance(type.ToString());
                if (obj != null && obj is BaseForm)
                {
                    var form = obj as BaseForm;
                    form.DockStateChanged -= Form_DockStateChanged;
                    form.DockStateChanged += Form_DockStateChanged;
                    form.Show(dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                    _formDic.Add(path, form);
                    f = form;
                }
            }
            return f;
        }
        /// <summary>
        /// 移除指定窗体
        /// </summary>
        /// <param name="path"></param>
        public void RemoveForm(string path)
        {
            if (_formDic.ContainsKey(path))
            {
                dockPanel1.Controls.Remove(_formDic[path]);
                _formDic.Remove(path);
            }
        }

        /// <summary>
        /// 移除指定窗体
        /// </summary>
        /// <param name="path"></param>
        public void RemoveForm(BaseForm form)
        {
            if (_formDic.ContainsValue(form))
            {
                var item = _formDic.FirstOrDefault(a => a.Value.Equals(form));
                item.Value.Dispose();
                dockPanel1.Controls.Remove(item.Value);
                _formDic.Remove(item.Key);
            }
        }

        public void LockPanel()
        {
            _isLocked = true;
            _freeTime = 0;
        }
        public void UnLockPanel(BaseForm form)
        {
            _isLocked = false;

            ThreadHelper.StartNew(() =>
            {
                System.Threading.Thread.Sleep(500);
                this.Invoke(new Action(() =>
                {
                    RemoveForm(form);
                }));



            });

        }

        private void Form_DockStateChanged(object sender, EventArgs e)
        {
            var form = sender as BaseForm;

            if (form.DockState == WeifenLuo.WinFormsUI.Docking.DockState.Hidden)
                RemoveForm(form);

            if (ApplicationParam.MainForm != null)
                ApplicationParam.MainForm._freeTime = 0;
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
            menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "锁屏", Path = "Common.LockPanel", IsEnable = true });
            menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "修改密码", Path = "Common.PassordModify", IsEnable = true });
            menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "注销", Path = "Common.ReLogin", IsEnable = true });
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
                menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "添加会员", Path = "SystemSet.AddMember", IsEnable = true });
                menuItem.SubMenuItems.Add(new MenuItemModel() { Name = "会员管理", Path = "SystemSet.MemberMgr", IsEnable = true });
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
