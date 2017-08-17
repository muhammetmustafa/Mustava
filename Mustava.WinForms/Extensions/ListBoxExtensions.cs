using System.Windows.Forms;

namespace Mustava.WinForms.Extensions
{
    public static class ListBoxExtensions
    {
        public static void AddChecked(this ListBox list, object item)
        {
            if (!list.Items.Contains(item))
            {
                list.Items.Add(item);
            }
        } 
    }
}