using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1.Data.View
{
    public class MenuData
    {
        public MenuData(string menuName = "", object attachedData = null, 
            string iconUrl = "", MenuData parent = null)
        {
            Name = menuName;
            AttachedData = attachedData;
            IconUrl = iconUrl;
            Parent = parent;
            ChildMenus = new List<MenuData>();
            MenuID = GenMenuID();
        }

        /// <summary>
        /// 生成程序唯一的菜单ID
        /// </summary>
        /// <returns></returns>
        static string GenMenuID()
        {
            var idIdx = Interlocked.Add(ref s_idCounter, 1);
            return string.Format("MU_{0}", idIdx.ToString().PadLeft(6,'0'));
        }

        /// <summary>
        /// 添加子菜单信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isUnique">子菜单是否需要检测已经添加</param>
        public void AddChildMenuData(MenuData data,bool isUnique = true)
        {
            if (isUnique && ChildMenus.Contains(data)) return;
            ChildMenus.Add(data);
            data.Parent = this;
        }

        static int s_idCounter = 0;

        /// <summary>
        /// 菜单的唯一ID
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// 菜单名字(显示)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单图标地址
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 菜单关联信息
        /// </summary>
        public object AttachedData { get; set; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<MenuData> ChildMenus { get; private set; }

        /// <summary>
        /// 父菜单数据引用
        /// </summary>
        public MenuData Parent { get; set; }

        public override string ToString()
        {
            return string.Format("MenuData:Name = {0},ID = {1}", Name, MenuID);
        }
    }


    public static class MenuDataEx
    {
        /// <summary>
        /// 递归查找
        /// </summary>
        /// <param name="data"></param>
        /// <param name="findID"></param>
        /// <returns></returns>
        static MenuData LoopFindMenuData(MenuData data, string findID)
        {
            if (data.MenuID == findID) return data;
            foreach (var eChild in data.ChildMenus)
            {
                var findData = LoopFindMenuData(eChild, findID);
                if (null != findData) return findData;
            }
            return null;
        }

        /// <summary>
        /// 在根菜单中找出menuID的菜单，没有则返回null
        /// </summary>
        /// <param name="rootMenu"></param>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public static MenuData FindMenuDataOnId(this MenuData rootMenu,string menuID)
        {
            MenuData target = null;
            foreach(var eChild in rootMenu.ChildMenus)
            {
                target = LoopFindMenuData(eChild, menuID);
                if (null != target) return target;
            }
            return null;
        }
    }
}
