using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Data
{
    /// <summary>
    /// 使用Dialog展示的编辑框
    /// </summary>
    public interface IDialogPanel
    {
        void SetPanelVisible(bool yes = true);
    }


    public static class DialogPanelEx
    {
        /// <summary>
        /// 对Dialog展示的编辑框进行平滑展示的封装（打开时隐藏内容，打开后显示，关闭前隐藏内容）
        /// </summary>
        /// <param name="dialogPanel"></param>
        /// <param name="dialogHostTag"></param>
        /// <returns></returns>
        public async static Task<bool> SmothShow(this IDialogPanel dialogPanel, string dialogHostTag = "tabContentDialogHost")
        {
            var result = await DialogHost.Show(dialogPanel, dialogHostTag, new DialogOpenedEventHandler((obj, args) =>
            {
                dialogPanel.SetPanelVisible(true);
            }), new DialogClosingEventHandler((obj, args) =>
            {
                dialogPanel.SetPanelVisible(false);
            }));
            return (bool)result;
        }
    }
}
